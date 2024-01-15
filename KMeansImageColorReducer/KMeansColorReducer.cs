namespace KMeansImageColorReducer;

/// <summary>
///     Image color reducer using the k-means algorithm.
/// </summary>
public static class KMeansColorReducer
{
    // Next iteration event
    public static event EventHandler<uint>? NextIteration;

    // Next centroid event
    public static event EventHandler<uint>? NextCentroid;

    /// <summary>
    ///     Reduce image colors using the k-means algorithm.
    /// </summary>
    /// <param name="image">The original image.</param>
    /// <param name="k">The amount of colors after reducing.</param>
    /// <param name="iterations">k-means algorithm iterations.</param>
    /// <returns>The color-reduced image.</returns>
    public static Pixel[,] ReduceImageColors(Pixel[,] image, uint k, uint iterations = 16)
    {
        // Calculate image centroids
        Pixel[] centroids = CalcCentroids(image, k, iterations);

        // Assign each pixel to the closest centroid
        PixelCentroidAssignment?[,] assignments = AssignPixelsToCentroids(image, centroids);

        // Create new image with reduced colors
        Pixel[,] newImage = new Pixel[image.GetLength(0), image.GetLength(1)];

        for (int x = 0; x < image.GetLength(0); x++)
            for (int y = 0; y < image.GetLength(1); y++)
            {
                if (assignments[x, y] is null) continue;

                newImage[x, y] = new Pixel(
                    centroids[assignments[x, y]!.Value.CentroidIndex].R,
                    centroids[assignments[x, y]!.Value.CentroidIndex].G,
                    centroids[assignments[x, y]!.Value.CentroidIndex].B);
            }

        return newImage;
    }

    /// <summary>
    ///     Helper function to convert a 3d-byte-array to a 2d-pixel-array.
    /// </summary>
    /// <param name="image">The 3d-byte-array to convert.</param>
    /// <returns>The same image as a 2d-pixel-array.</returns>
    public static Pixel[,] ConvertImageTo2DPixelArray(byte[,,] image)
    {
        // Convert the image from byte[,,] to Pixel[,]
        Pixel[,] newImage = new Pixel[image.GetLength(0), image.GetLength(1)];

        for (int x = 0; x < image.GetLength(0); x++)
            for (int y = 0; y < image.GetLength(1); y++)
                newImage[x, y] = new Pixel(image[x, y, 0], image[x, y, 1], image[x, y, 2]);

        return newImage;
    }

    /// <summary>
    ///     Helper function to convert a 2d-pixel-array to a 3d-byte-array.
    /// </summary>
    /// <param name="image">The 2d-pixel-array to convert.</param>
    /// <returns>The same image as a 3d-byte-array.</returns>
    public static byte[,,] ConvertImageTo3DByteArray(Pixel[,] image)
    {
        // Convert the image from Pixel[,] to byte[,,]
        byte[,,] newImage = new byte[image.GetLength(0), image.GetLength(1), 3];

        for (int x = 0; x < image.GetLength(0); x++)
            for (int y = 0; y < image.GetLength(1); y++)
            {
                newImage[x, y, 0] = image[x, y].R;
                newImage[x, y, 1] = image[x, y].G;
                newImage[x, y, 2] = image[x, y].B;
            }

        return newImage;
    }

    /// <summary>
    ///     Generate the specified amount of centroids using the k-means algorithm.
    /// </summary>
    /// <param name="image">The original image.</param>
    /// <param name="k">The amount of centroid to generate.</param>
    /// <param name="iterations">k-means algorithm iterations.</param>
    /// <returns>An array with all generated centroids.</returns>
    private static Pixel[] CalcCentroids(Pixel[,] image, uint k, uint iterations)
    {
        // Generate k random centroids
        Pixel[] centroids = GenRandomCentroids(image, k);

        // Assign and recalculate centroids
        for (uint i = 0; i < iterations; i++)
        {
            // Report progress
            NextIteration?.Invoke(null, i);

            // Assign each pixel to the closest centroid
            PixelCentroidAssignment?[,] assignments = AssignPixelsToCentroids(image, centroids);

            // Recalculate centroids
            centroids = RecalculateCentroids(image, centroids, assignments);
        }

        // Report progress
        NextIteration?.Invoke(null, iterations);

        return centroids;
    }

    /// <summary>
    ///     Generate random centroids based on the image.
    /// </summary>
    /// <param name="image">The original image.</param>
    /// <param name="k">The amount of random centroids to generate.</param>
    /// <returns>An array with all generated centroids.</returns>
    private static Pixel[] GenRandomCentroids(Pixel[,] image, uint k)
    {
        // Create array to store the centroids
        Pixel[] centroids = new Pixel[k];

        // Generate k random centroids
        for (uint i = 0; i < k; i++)
        {
            int newX = Random.Shared.Next(image.GetLength(0));
            int newY = Random.Shared.Next(image.GetLength(1));
            centroids[i] = new Pixel(image[newX, newY].R, image[newX, newY].G, image[newX, newY].B);
        }

        return centroids;
    }

    /// <summary>
    ///     Assign every image pixel to the nearest centroid.
    /// </summary>
    /// <param name="image">The reference image.</param>
    /// <param name="centroids">The centroids that the pixels get assigned to.</param>
    /// <returns>A 2d-array with all <see cref="PixelCentroidAssignment" />s.</returns>
    private static PixelCentroidAssignment?[,] AssignPixelsToCentroids(Pixel[,] image, Pixel[] centroids)
    {
        // Get image size
        int width = image.GetLength(0);
        int height = image.GetLength(1);

        // Create an array to store all "pixel to centroids" assignments
        PixelCentroidAssignment?[,] assignments = new PixelCentroidAssignment?[width, height];

        // Create an array to temporarily store the distances from the pixels to the centroids
        float[,] distances = new float[width, height];

        for (uint c = 0; c < centroids.Length; c++)
        {
            // Report progress
            NextCentroid?.Invoke(null, c);

            // Calculate the distance to all other pixels for this centroid
            CalcDistances(image, centroids[c], distances);

            // Create a copy to speed up parallel processing
            uint cParallel = c;

            // Assign pixel this centroid if it's the closest one
            Parallel.For(0, width, x =>
            {
                for (uint y = 0; y < height; y++)
                    // If no centroid was yet assigned to this pixel or this centroid is closer than the other ones, assign it
                    if (assignments[x, y] == null || distances[x, y] < assignments[x, y]!.Value.Distance)
                        assignments[x, y] = new PixelCentroidAssignment(cParallel, distances[x, y]);
            });
        }

        // Report progress
        NextCentroid?.Invoke(null, (uint)centroids.Length);

        return assignments;
    }

    /// <summary>
    ///     Calculate image pixel distances for a specific centroid.
    /// </summary>
    /// <param name="image">The reference image.</param>
    /// <param name="centroid">The centroid.</param>
    /// <param name="distances">A reference to an array in which the distances should be stored.</param>
    private static void CalcDistances(Pixel[,] image, Pixel centroid, float[,] distances)
    {
        // Get image size
        int width = image.GetLength(0);
        int height = image.GetLength(1);

        // Calculate the euclidean distance for every pixel
        Parallel.For(0, width, x =>
        {
            for (uint y = 0; y < height; y++)
            {
                short diffR = (short)(image[x, y].R - centroid.R);
                short diffG = (short)(image[x, y].G - centroid.G);
                short diffB = (short)(image[x, y].B - centroid.B);

                distances[x, y] = (float)Math.Sqrt(diffR * diffR + diffG * diffG + diffB * diffB);
            }
        });
    }

    /// <summary>
    ///     Recalculate centroid position by taking the average color value of the assigned pixels.
    /// </summary>
    /// <param name="image">The reference image.</param>
    /// <param name="centroids">An array with all the centroids.</param>
    /// <param name="assignments">Pixel to centroid assignments.</param>
    /// <returns>An array with all recalculated centroids.</returns>
    private static Pixel[] RecalculateCentroids(Pixel[,] image, Pixel[] centroids,
        PixelCentroidAssignment?[,] assignments)
    {
        // Create array to store the centroids
        Pixel[] newCentroids = new Pixel[centroids.Length];

        Parallel.For(0, newCentroids.Length, c =>
        {
            // Create variable to store the results
            uint countAssignedPixels = 0;
            uint redSum = 0;
            uint greenSum = 0;
            uint blueSum = 0;

            // Loop through each assignment
            for (int x = 0; x < assignments.GetLength(0); x++)
                for (int y = 0; y < assignments.GetLength(1); y++)
                {
                    PixelCentroidAssignment? assign = assignments[x, y];
                    if (assign is null) continue;

                    // Skip every assignment that's not for this centroid
                    if (assign.Value.CentroidIndex != c) continue;

                    // Calculate needed values
                    countAssignedPixels++;
                    redSum += image[x, y].R;
                    greenSum += image[x, y].G;
                    blueSum += image[x, y].B;
                }

            // No assigned pixels were found, keep the current location
            if (countAssignedPixels == 0)
            {
                newCentroids[c] = centroids[c];
            }
            else
            {
                // Calculate new centroid coordinates
                byte red = (byte)(redSum / countAssignedPixels);
                byte green = (byte)(greenSum / countAssignedPixels);
                byte blue = (byte)(blueSum / countAssignedPixels);

                // Store new centroid
                newCentroids[c] = new Pixel(red, green, blue);
            }
        });

        return newCentroids;
    }

    /// <summary>
    ///     Struct to store pixel to centroid assignment values.
    /// </summary>
    /// <param name="centroidIndex">The centroid this pixel is assigned to.</param>
    /// <param name="distance">The distance from this pixel to the centroid.</param>
    private readonly struct PixelCentroidAssignment(uint centroidIndex, float distance)
    {
        /// <summary>
        ///     The centroid this pixel is assigned to.
        /// </summary>
        public uint CentroidIndex { get; } = centroidIndex;

        /// <summary>
        ///     The distance from this pixel to the centroid.
        /// </summary>
        public float Distance { get; } = distance;

        /// <summary>
        ///     Return a string representation if this <see cref="PixelCentroidAssignment" />.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"PixelCentroidAssignment({CentroidIndex}, {Distance})";
    }
}