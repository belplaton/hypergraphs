namespace HyperGraphs
{
    public class Graph
    {

        #region Validation

        public static bool CheckVectorGraphical(in int[] degreeVector)
        {
            if (degreeVector == null)
            {
                Console.WriteLine("degreeVector is null");
                return false;
            }

            var tempDegreeVector = degreeVector.ToArray();
            var degreeVectorSum = tempDegreeVector.Sum();
            if (degreeVectorSum % 2 != 0)
            {
                Console.WriteLine($"Sum {degreeVectorSum} % 2 != 0");
                return false;
            }

            for (var i = 0; i < tempDegreeVector.Length; i++)
            {
                Console.WriteLine($"degreeVector[{i}] >= {tempDegreeVector.Length}");
                if (tempDegreeVector[i] >= tempDegreeVector.Length)
                {
                    return false;
                }
            }

            for (var i = 0; i < tempDegreeVector.Length; i++)
            {
                while (tempDegreeVector[i] > 0)
                {
                    for (var j = i + 1; j < tempDegreeVector.Length; j++)
                    {
                        if (tempDegreeVector[i] > 0 && tempDegreeVector[j] > 0)
                        {
                            tempDegreeVector[i]--;
                            tempDegreeVector[j]--;
                        }
                    }

                    if (tempDegreeVector[i] > 0)
                    {
                        Console.WriteLine($"Not Enough connections: degreeVector[{i}] > 0");
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool CheckAdjacencyGraphical(in int[,] adjacencyMatrix)
        {
            var degreeVector = new int[adjacencyMatrix.Length];

            for (int i = 0; i < adjacencyMatrix.Length; i++)
            {
                var degree = 0;
                for (int j = 0; j < adjacencyMatrix.Length; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        degree++;
                    }
                    else if (adjacencyMatrix[i, j] > 1)
                    {
                        return false;
                    }

                    degreeVector[i] = degree;
                }
            }

            return CheckVectorGraphical(degreeVector);
        }

        public static bool CheckIncidenceGraphical(in int[,] incidenceMatrix)
        {
            return TryIncidenceToVector(incidenceMatrix, out var _);
        }

        #endregion

        #region Safe versions

        public static bool TryVectorToAdjacency(in int[] degreeVector, out int[,] adjacencyMatrix)
        {
            if (degreeVector == null)
            {
                adjacencyMatrix = new int[0, 0];
                return false;
            }

            var tempDegreeVector = degreeVector.ToArray();

            if (tempDegreeVector.Sum() % 2 != 0)
            {
                adjacencyMatrix = new int[0, 0];
                return false;
            }

            var vertices = tempDegreeVector.Length;
            adjacencyMatrix = new int[vertices, vertices];

            for (var i = 0; i < vertices; i++)
            {
                while (tempDegreeVector[i] > 0)
                {
                    if (tempDegreeVector[i] >= vertices)
                    {
                        adjacencyMatrix = new int[0, 0];
                        return false;
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
                        adjacencyMatrix = new int[0, 0];
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool TryAdjacencyToVector(in int[,] adjacencyMatrix, out int[] degreeVector)
        {
            if (adjacencyMatrix == null)
            {
                degreeVector = new int[0];
                return false;
            }

            var vertices = adjacencyMatrix.GetLength(0);
            degreeVector = new int[vertices];

            for (int i = 0; i < vertices; i++)
            {
                var degree = 0;
                for (int j = 0; j < vertices; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        degree++;
                    }
                    else if (adjacencyMatrix[i, j] > 1)
                    {
                        degreeVector = new int[0];
                        return false;
                    }

                    degreeVector[i] = degree;
                }
            }

            if (degreeVector.Sum() % 2 != 0)
            {
                degreeVector = new int[0];
                return false;
            }

            return true;
        }

        public static bool TryAdjacencyToIncidence(in int[,] adjacencyMatrix, out int[,] incidenceMatrix)
        {
            if (adjacencyMatrix == null)
            {
                incidenceMatrix = new int[0, 0];
                return false;
            }

            var vertices = adjacencyMatrix.GetLength(0);
            if (TryAdjacencyToVector(adjacencyMatrix, out var degreeVector))
            {
                var edges = degreeVector.Sum() / 2;

                incidenceMatrix = new int[vertices, edges];
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

                return true;
            }

            incidenceMatrix = new int[0, 0];
            return false;
        }

        public static bool TryVectorIncidence(in int[] degreeVector, out int[,] incidenceMatrix)
        {
            if (degreeVector == null)
            {
                incidenceMatrix = new int[0, 0];
                return false;
            }

            if (TryVectorToAdjacency(degreeVector, out var adjacencyMatrix))
            {
                var vertices = adjacencyMatrix.GetLength(0);
                var edges = degreeVector.Sum() / 2;

                incidenceMatrix = new int[vertices, edges];
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

                return true;
            }

            incidenceMatrix = new int[0, 0];
            return false;
        }

        public static bool TryIncidenceToVector(in int[,] incidenceMatrix, out int[] degreeVector)
        {
            if (incidenceMatrix == null)
            {
                degreeVector = new int[0];
                return false;
            }

            int vertices = incidenceMatrix.GetLength(0);
            int edges = incidenceMatrix.GetLength(1);

            degreeVector = new int[vertices];

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

            if (!CheckVectorGraphical(degreeVector))
            {
                degreeVector = new int[0];
                return false;
            }

            return true;
        }

        public static bool IncidenceToAdjacency(in int[,] incidenceMatrix, out int[,] adjacencyMatrix)
        {
            if (incidenceMatrix == null)
            {
                adjacencyMatrix = new int[0, 0];
                return false;
            }

            if (!TryIncidenceToVector(incidenceMatrix, out var degreeVector))
            {
                adjacencyMatrix = new int[0, 0];
                return false;
            }

            if (!TryVectorToAdjacency(degreeVector, out adjacencyMatrix))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Standart

        public static int[,] VectorToAdjacency(in int[] degreeVector)
        {
            if (degreeVector == null)
            {
                throw new ArgumentNullException(nameof(degreeVector));
            }

            var tempDegreeVector = degreeVector.ToArray();

            if (tempDegreeVector.Sum() % 2 != 0)
            {
                throw new ArgumentException($"[Connections Sum: {tempDegreeVector.Sum()} % 2 != 0] Degree sequence is not graphical. Cannot reconstruct the adjacency graph.");
            }

            var vertices = tempDegreeVector.Length;
            var adjacencyMatrix = new int[vertices, vertices];

            for (var i = 0; i < vertices; i++)
            {
                while (tempDegreeVector[i] > 0)
                {
                    if (tempDegreeVector[i] >= vertices)
                    {
                        throw new ArgumentException($"[Connections in {i} > vector length] Degree sequence is not graphical. Cannot reconstruct the adjacency graph.");
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
                        throw new ArgumentException($"[Not enough other connections after {i}] Degree sequence is not graphical. Cannot reconstruct the adjacency graph.");
                    }
                }
            }

            return adjacencyMatrix;
        }

        public static int[] AdjacencyToVector(in int[,] adjacencyMatrix)
        {
            if (adjacencyMatrix == null)
            {
                throw new ArgumentNullException(nameof(adjacencyMatrix));
            }

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
                    else if (adjacencyMatrix[i, j] > 1)
                    {
                        throw new ArgumentException($"[Error: {i}, {j}] Adjacency matrix contains invalid value. Must be 0 or 1.");
                    }

                    degreeVector[i] = degree;
                }
            }

            if (degreeVector.Sum() % 2 != 0)
            {
                throw new ArgumentException($"[Connections Sum: {degreeVector.Sum()} % 2 != 0] Degree sequence is not graphical. Cannot construct vector graph.");
            }

            return degreeVector;
        }

        public static int[,] AdjacencyToIncidence(in int[,] adjacencyMatrix)
        {
            if (adjacencyMatrix == null)
            {
                throw new ArgumentNullException(nameof(adjacencyMatrix));
            }

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
            if (degreeVector == null)
            {
                throw new ArgumentNullException(nameof(degreeVector));
            }

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
            if (incidenceMatrix == null)
            {
                throw new ArgumentNullException(nameof(incidenceMatrix));
            }

            var degreeVector = IncidenceToVector(incidenceMatrix);
            var adjacencyMatrix = VectorToAdjacency(degreeVector);

            return adjacencyMatrix;
        }

        public static int[] IncidenceToVector(int[,] incidenceMatrix)
        {
            if (incidenceMatrix == null)
            {
                throw new ArgumentNullException(nameof(incidenceMatrix));
            }

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

        #endregion

        #region Utility

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

        #endregion
    }
}