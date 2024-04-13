namespace HyperGraphs
{
    public class Graph
    {
        public static int[,] VectorToAdjacency(in int[] degreeVector)
        {
            var tempDegreeVector = degreeVector.ToArray();

            if (tempDegreeVector == null)
            {
                throw new ArgumentNullException(nameof(tempDegreeVector));
            }

            if (tempDegreeVector.Sum() % 2 != 0)
            {
                Console.WriteLine("Degree sequence is not graphical. Cannot reconstruct the graph.");
                return new int[0, 0];
            }

            var vertices = tempDegreeVector.Count();
            var adjacencyMatrix = new int[vertices, vertices];

            for (int i = 0; i < vertices; i++)
            {
                for (int j = i + 1; j < vertices; j++)
                {
                    if (tempDegreeVector[i] > 0 && tempDegreeVector[j] > 0)
                    {
                        adjacencyMatrix[i, j] = 1;
                        adjacencyMatrix[j, i] = 1;
                        tempDegreeVector[i]--;
                        tempDegreeVector[j]--;
                    }
                }
            }

            return adjacencyMatrix;
        }

        public static int[] AdjacencyToVector(in int[,] adjacencyMatrix)
        {
            var vertices = adjacencyMatrix.GetLength(0);
            var degreeVector = new int[vertices];
            
            for (int i = 0; i < vertices; i++)
            {
                var degree = 0;
                for (int j = 0; j < vertices; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        degree++;
                    }

                    degreeVector[i] = degree;
                }
            }

            return degreeVector;
        }

        public static int[,] AdjacencyToIncidence(in int[,] adjacencyMatrix)
        {
            var vertices = adjacencyMatrix.GetLength(0);
            var edges = AdjacencyToVector(adjacencyMatrix).Sum() / 2;

            var incidenceMatrix = new int[vertices, edges];
            var k = 0;

            for (int i = 0; i < vertices; i++)
            {
                for (int j = i; j < vertices; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        incidenceMatrix[i, k] = 1;
                        incidenceMatrix[j, k] = 1;
                        k++;
                    }
                }
            }

            return incidenceMatrix;
        }

        public static int[,] VectorToIncedence(in int[] degreeVector)
        {
            var tempDegreeVector = degreeVector.ToArray();
            var adjacencyMatrix = VectorToAdjacency(tempDegreeVector);

            var vertices = adjacencyMatrix.GetLength(0);
            var edges = tempDegreeVector.Sum() / 2;

            var incidenceMatrix = new int[vertices, edges];
            var k = 0;

            for (int i = 0; i < vertices; i++)
            {
                for (int j = i; j < vertices; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        incidenceMatrix[i, k] = 1;
                        incidenceMatrix[j, k] = 1;
                        k++;
                    }
                }
            }

            return incidenceMatrix;
        }

        public static void PrintGraph(in int[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var vertices = matrix.GetLength(0);
            var edges = matrix.GetLength(1);

            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < edges; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }
        }
    }
}