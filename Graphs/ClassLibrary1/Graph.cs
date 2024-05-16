namespace HyperGraphs
{
    public static class Graph
    {
        #region Validation

        public static bool CheckVectorGraphical(in int[] degreeVector)
        {
            return (CheckVectorGraphical(degreeVector, out var _));
        }

        public static bool CheckVectorGraphical(in int[] degreeVector, out string errmes)
        {
            if (degreeVector == null)
            {
                errmes = "degreeVector is null";
                return false;
            }

            var tempDegreeVector = degreeVector.ToArray();
            var degreeVectorSum = tempDegreeVector.Sum();
            if (degreeVectorSum % 2 != 0)
            {
                errmes = $"Sum {degreeVectorSum} % 2 != 0";
                return false;
            }

            for (var i = 0; i < tempDegreeVector.Length; i++)
            {
                if (tempDegreeVector[i] >= tempDegreeVector.Length)
                {
                    errmes = $"degreeVector[{i}] >= {tempDegreeVector.Length}";
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
                        errmes = $"Not Enough connections: degreeVector[{i}] > 0";
                        return false;
                    }
                }
            }

            errmes = string.Empty;
            return true;
        }

        public static bool CheckAdjacencyGraphical(in int[,] adjacencyMatrix)
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
                    else if (adjacencyMatrix[i, j] > 1)
                    {
                        return false;
                    }

                    degreeVector[i] = degree;
                }
            }

            return CheckVectorGraphical(degreeVector);
        }

        public static bool CheckAdjacencyGraphical(in int[,] adjacencyMatrix, out string errmes)
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
                    else if (adjacencyMatrix[i, j] > 1)
                    {
                        errmes = $"adjacencyMatrix[{i}, {j}] > 1";
                        return false;
                    }

                    degreeVector[i] = degree;
                }
            }

            return CheckVectorGraphical(degreeVector, out errmes);
        }

        public static bool CheckIncidenceGraphical(in int[,] incidenceMatrix)
        {
            return TryIncidenceToVector(incidenceMatrix, out var _);
        }

        public static bool CheckIncidenceGraphical(in int[,] incidenceMatrix, out string errmes)
        {
            return TryIncidenceToVector(incidenceMatrix, out var _, out errmes);
        }

        #endregion

        #region Safe

        public static bool TryVectorToAdjacency(in int[] degreeVector, out int[,] adjacencyMatrix)
        {
            return TryVectorToAdjacency(degreeVector, out adjacencyMatrix, out var _);
        }

        public static bool TryVectorToAdjacency(in int[] degreeVector, out int[,] adjacencyMatrix, out string errmes)
        {
            if (degreeVector == null)
            {
                adjacencyMatrix = new int[0, 0];
                errmes = "degreeVector is null";
                return false;
            }

            var tempDegreeVector = degreeVector.ToArray();

            if (tempDegreeVector.Sum() % 2 != 0)
            {
                adjacencyMatrix = new int[0, 0];
                errmes = $"Sum {tempDegreeVector.Sum()} % 2 != 0";
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
                        errmes = $"degreeVector[{i}] >= {tempDegreeVector.Length}";
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
                        errmes = $"Not Enough connections: degreeVector[{i}] > 0";
                        return false;
                    }
                }
            }

            errmes = string.Empty;
            return true;
        }

        public static bool TryAdjacencyToVector(in int[,] adjacencyMatrix, out int[] degreeVector)
        {
            return TryAdjacencyToVector(adjacencyMatrix, out degreeVector);
        }

        public static bool TryAdjacencyToVector(in int[,] adjacencyMatrix, out int[] degreeVector, out string errmes)
        {
            if (adjacencyMatrix == null)
            {
                degreeVector = new int[0];
                errmes = "adjacencyMatrix is null";
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
                        errmes = $"adjacencyMatrix[{i}, {j}] > 1";
                        return false;
                    }

                    degreeVector[i] = degree;
                }
            }

            if (degreeVector.Sum() % 2 != 0)
            {
                degreeVector = new int[0];
                errmes = $"Sum {degreeVector.Sum()} % 2 != 0";
                return false;
            }

            errmes = string.Empty;
            return true;
        }

        public static bool TryAdjacencyToIncidence(in int[,] adjacencyMatrix, out int[,] incidenceMatrix)
        {
            return TryAdjacencyToIncidence(adjacencyMatrix, out incidenceMatrix, out var _);
        }

        public static bool TryAdjacencyToIncidence(in int[,] adjacencyMatrix, out int[,] incidenceMatrix, out string errmes)
        {
            if (adjacencyMatrix == null)
            {
                incidenceMatrix = new int[0, 0];
                errmes = "adjancencyMatrix is null";
                return false;
            }

            var vertices = adjacencyMatrix.GetLength(0);
            if (TryAdjacencyToVector(adjacencyMatrix, out var degreeVector, out errmes))
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

        public static bool TryVectorToIncidence(in int[] degreeVector, out int[,] incidenceMatrix)
        {
            return TryVectorToIncidence(degreeVector, out incidenceMatrix, out var _);
        }

        public static bool TryVectorToIncidence(in int[] degreeVector, out int[,] incidenceMatrix, out string errmes)
        {
            if (degreeVector == null)
            {
                incidenceMatrix = new int[0, 0];
                errmes = "degreeVector is null";
                return false;
            }

            if (TryVectorToAdjacency(degreeVector, out var adjacencyMatrix, out errmes))
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
            return TryIncidenceToVector(incidenceMatrix, out degreeVector, out var _);
        }

        public static bool TryIncidenceToVector(in int[,] incidenceMatrix, out int[] degreeVector, out string errmes)
        {
            if (incidenceMatrix == null)
            {
                degreeVector = new int[0];
                errmes = "incidenceMatrix is null";
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

            if (!CheckVectorGraphical(degreeVector, out errmes))
            {
                degreeVector = new int[0];
                return false;
            }

            return true;
        }

        public static bool TryIncidenceToAdjacency(in int[,] incidenceMatrix, out int[,] adjacencyMatrix)
        {
            return TryIncidenceToAdjacency(incidenceMatrix, out adjacencyMatrix, out var _);
        }

        public static bool TryIncidenceToAdjacency(in int[,] incidenceMatrix, out int[,] adjacencyMatrix, out string errmes)
        {
            if (incidenceMatrix == null)
            {
                adjacencyMatrix = new int[0, 0];
                errmes = "incidenceMatrix is null";
                return false;
            }

            if (!TryIncidenceToVector(incidenceMatrix, out var degreeVector, out errmes))
            {
                adjacencyMatrix = new int[0, 0];
                return false;
            }

            if (!TryVectorToAdjacency(degreeVector, out adjacencyMatrix, out errmes))
            {
                return false;
            }

            return true;
        }

        public static bool TryGetBases(in int[,] adjacencyMatrix, out List<(int, int)> bases)
        {
            return TryGetBases(adjacencyMatrix, out bases, out var _);
        }

        public static bool TryGetBases(in int[,] adjacencyMatrix, out List<(int, int)> bases, out string errmes)
        {
            bases = new List<(int, int)>();
            if (!CheckAdjacencyGraphical(adjacencyMatrix, out errmes))
            {
                return false;
            }

            var verticles = adjacencyMatrix.GetLength(0);

            var i = 0;
            var j = verticles - 1;

            var prev_i = 0;
            var prev_j = 0;

            while (i < verticles / 2)
            {
                var prev = 0;
                for (var temp = j; temp > i; temp--)
                {
                    if (adjacencyMatrix[i, temp] == 1 && prev == 0)
                    {
                        j = temp;
                    }
                    else if (adjacencyMatrix[i, temp] == 0)
                    {
                        j = -1;
                    }

                    prev = adjacencyMatrix[i, temp];
                }

                if (prev_j != j && prev_i != prev_j)
                {
                    bases.Add((prev_i, prev_j));
                }

                prev_i = i;
                prev_j = j;
                i++;
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

        public static int[,] IncidenceToAdjacency(in int[,] incidenceMatrix)
        {
            if (incidenceMatrix == null)
            {
                throw new ArgumentNullException(nameof(incidenceMatrix));
            }

            var degreeVector = IncidenceToVector(incidenceMatrix);
            var adjacencyMatrix = VectorToAdjacency(degreeVector);

            return adjacencyMatrix;
        }

        public static int[] IncidenceToVector(in int[,] incidenceMatrix)
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

        public static List<(int, int)> GetBases(in int[,] adjacencyMatrix)
        {
            var bases = new List<(int, int)>();
            if (!CheckAdjacencyGraphical(adjacencyMatrix))
            {
                return bases;
            }

            var verticles = adjacencyMatrix.GetLength(0);

            var i = 0;
            var j = verticles - 1;

            var prev_i = 0;
            var prev_j = 0;

            while (i < verticles / 2)
            {
                var prev = 0;
                for (var temp = j; temp > i; temp--)
                {
                    if (adjacencyMatrix[i, temp] == 1 && prev == 0)
                    {
                        j = temp;
                    }
                    else if (adjacencyMatrix[i, temp] == 0)
                    {
                        j = -1;
                    }

                    prev = adjacencyMatrix[i, temp];
                }

                if (prev_j != j && prev_i != prev_j)
                {
                    bases.Add((prev_i, prev_j));
                }

                prev_i = i;
                prev_j = j;
                i++;
            }

            return bases;
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