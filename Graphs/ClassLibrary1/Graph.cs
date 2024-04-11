namespace HyperGraphs
{
    public class Graph
    {
        public static int[,] ReconstructGraph(int[] degreeVector)
        {
            if (degreeVector == null)
            {
                throw new ArgumentNullException(nameof(degreeVector));
            }
            
            if (degreeVector.Sum() % 2 != 0)
            {
                Console.WriteLine("Degree sequence is not graphical. Cannot reconstruct the graph.");
                return new int[0, 0];
            }

            var vertices = degreeVector.Count();
            var adjacencyMatrix = new int[vertices, vertices];

            for (int i = 0; i < vertices; i++)
            {
                for (int j = i + 1; j < vertices; j++)
                {
                    if (degreeVector[i] > 0 && degreeVector[j] > 0)
                    {
                        adjacencyMatrix[i, j] = 1;
                        adjacencyMatrix[j, i] = 1;
                        degreeVector[i]--;
                        degreeVector[j]--;
                    }
                }
            }

            return adjacencyMatrix;
        }

        public static void PrintGraph(int[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var vertices = matrix.GetLength(0);

            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }
        }
    }
}