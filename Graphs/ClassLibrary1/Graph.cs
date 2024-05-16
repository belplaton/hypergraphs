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
                Console.WriteLine("[A] Degree sequence is not graphical. Cannot reconstruct the adjacency graph.");
                return new int[0, 0];
            }

            var vertices = tempDegreeVector.Length;
            var adjacencyMatrix = new int[vertices, vertices];

            for (var i = 0; i < vertices; i++)
            {
                while (tempDegreeVector[i] > 0)
                {
                    if (tempDegreeVector[i] >= vertices)
                    {
                        Console.WriteLine($"[B: {i}] Degree sequence is not graphical. Cannot reconstruct the adjacency graph.");
                        return new int[0, 0];
                    }

                    for (var j = i + 1; j < vertices; j++)
                    {
                        if (tempDegreeVector[i] > 0 && tempDegreeVector[j] > 0)
                        {
                            adjacencyMatrix[i, j] = 1;
                            adjacencyMatrix[j, i] = 1;
                            tempDegreeVector[i]--;
                            tempDegreeVector[j]--;
                        }
                    }

                    if (tempDegreeVector[i] > 0)
                    {
                        Console.WriteLine($"[C: {i}, {tempDegreeVector[i]}] Degree sequence is not graphical. Cannot reconstruct the adjacency graph.");
                        return new int[0, 0];
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

        public static int[,] VectorToIncidence(in int[] degreeVector)
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

        public static int[,] IncidenceToAdjacency(int[,] incidenceMatrix)
        {
            int vertices = incidenceMatrix.GetLength(0);
            int edges = incidenceMatrix.GetLength(1);

            int[,] adjacencyMatrix = new int[vertices, vertices];

            for (int k = 0; k < edges; k++)
            {
                int vertex1 = -1;
                int vertex2 = -1;

                for (int i = 0; i < vertices; i++)
                {
                    if (incidenceMatrix[i, k] == 1)
                    {
                        if (vertex1 == -1)
                        {
                            vertex1 = i;
                        }
                        else
                        {
                            vertex2 = i;
                            break;
                        }
                    }
                }

                if (vertex1 != -1 && vertex2 != -1)
                {
                    adjacencyMatrix[vertex1, vertex2] = 1;
                    adjacencyMatrix[vertex2, vertex1] = 1;
                }
            }

            return adjacencyMatrix;
        }

        public static int[] IncidenceToVector(int[,] incidenceMatrix)
        {
            int vertices = incidenceMatrix.GetLength(0);
            int edges = incidenceMatrix.GetLength(1);

            int[] degreeVector = new int[vertices];

            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < edges; j++)
                {
                    if (incidenceMatrix[i, j] == 1)
                    {
                        degreeVector[i]++;
                    }
                }
            }

            return degreeVector;
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

        public static void PrintVector(in int[] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var vertices = matrix.GetLength(0);
            for (int i = 0; i < vertices; i++)
            {
                Console.Write($"{matrix[i]} ");
            }
        }
    }
}