using System.Text;

namespace HyperGraphs
{
    public static class Graph
    {
        #region Validation

        /// <summary>
        /// ��������� ������������ ������� �����
        /// </summary>
        /// <param name="degreeVector">������ �����</param>
        /// <returns></returns>
        public static bool CheckVectorGraphical(in int[] degreeVector)
        {
            return (CheckVectorGraphical(degreeVector, out var _));
        }

        /// <summary>
        /// ��������� ������������ ������� �����
        /// </summary>
        /// <param name="degreeVector">������� �����</param>
        /// <param name="errmes">������������ ������� ��������������</param>
        /// <returns></returns>
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

        /// <summary>
        /// ��������� ������������ ������� ���������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <returns></returns>
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

        /// <summary>
        /// ���������� ������������ ������� ���������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="errmes">������������ ������� ��������������</param>
        /// <returns></returns>
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

        /// <summary>
        /// ��������� ������������ ������� �������������
        /// </summary>
        /// <param name="incidenceMatrix">������� �������������</param>
        /// <returns></returns>
        public static bool CheckIncidenceGraphical(in int[,] incidenceMatrix)
        {
            return TryIncidenceToVector(incidenceMatrix, out var _);
        }

        /// <summary>
        /// ��������� ������������ ������� �������������
        /// </summary>
        /// <param name="incidenceMatrix">������� �������������</param>
        /// <param name="errmes">������������ ������� ��������������</param>
        /// <returns></returns>
        public static bool CheckIncidenceGraphical(in int[,] incidenceMatrix, out string errmes)
        {
            return TryIncidenceToVector(incidenceMatrix, out var _, out errmes);
        }
        
        #endregion

        #region Safe

        /// <summary>
        /// �������� �������������� ������ ����� � ������� ��������� � ��������� ������ ����������
        /// </summary>
        /// <param name="degreeVector">������ �����</param>
        /// <param name="adjacencyMatrix">������������ ������� ���������</param>
        /// <returns></returns>
        public static bool TryVectorToAdjacency(in int[] degreeVector, out int[,] adjacencyMatrix)
        {
            return TryVectorToAdjacency(degreeVector, out adjacencyMatrix, out var _);
        }

        /// <summary>
        /// �������� �������������� ������ ����� � ������� ��������� � ��������� ������ ����������
        /// </summary>
        /// <param name="degreeVector">������ �����</param>
        /// <param name="adjacencyMatrix">������������ ������� ���������</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TryVectorToAdjacency(in int[] degreeVector, out int[,] adjacencyMatrix, out string errmes)
        {
            if (degreeVector == null)
            {
                adjacencyMatrix = new int[0, 0];
                errmes = $"{nameof(degreeVector)} is null";
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

        /// <summary>
        /// �������� �������������� ������� ��������� � ������ ����� � ��������� ������ ����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="degreeVector">������������ ������ �����</param>
        /// <returns></returns>
        public static bool TryAdjacencyToVector(in int[,] adjacencyMatrix, out int[] degreeVector)
        {
            return TryAdjacencyToVector(adjacencyMatrix, out degreeVector);
        }

        /// <summary>
        /// �������� �������������� ������� ��������� � ������� ����� � ��������� ������ ����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="degreeVector">������������ ������ �����</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TryAdjacencyToVector(in int[,] adjacencyMatrix, out int[] degreeVector, out string errmes)
        {
            if (adjacencyMatrix == null)
            {
                degreeVector = new int[0];
                errmes = $"{nameof(adjacencyMatrix)} is null";
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

        /// <summary>
        /// �������� �������������� ������� ��������� � ������� ������������� � ��������� ������ ����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="incidenceMatrix">������������ ������� �������������</param>
        /// <returns></returns>
        public static bool TryAdjacencyToIncidence(in int[,] adjacencyMatrix, out int[,] incidenceMatrix)
        {
            return TryAdjacencyToIncidence(adjacencyMatrix, out incidenceMatrix, out var _);
        }

        /// <summary>
        /// �������� �������������� ������� ��������� � ������� ������������� � ��������� ������ ����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="incidenceMatrix">������������ ������� �������������</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TryAdjacencyToIncidence(in int[,] adjacencyMatrix, out int[,] incidenceMatrix, out string errmes)
        {
            if (adjacencyMatrix == null)
            {
                incidenceMatrix = new int[0, 0];
                errmes = $"{nameof(adjacencyMatrix)} is null";
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

        /// <summary>
        /// �������� �������������� ������ ����� � ������� ������������� � ��������� ������ ����������
        /// </summary>
        /// <param name="degreeVector">������ �����</param>
        /// <param name="incidenceMatrix">������������ ������� �������������</param>
        /// <returns></returns>
        public static bool TryVectorToIncidence(in int[] degreeVector, out int[,] incidenceMatrix)
        {
            return TryVectorToIncidence(degreeVector, out incidenceMatrix, out var _);
        }

        /// <summary>
        /// �������� �������������� ������� ����� � ������� ������������� � ��������� ������ ����������
        /// </summary>
        /// <param name="degreeVector">������ �����</param>
        /// <param name="incidenceMatrix">������������ ������� �������������</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TryVectorToIncidence(in int[] degreeVector, out int[,] incidenceMatrix, out string errmes)
        {
            if (degreeVector == null)
            {
                incidenceMatrix = new int[0, 0];
                errmes = $"{nameof(degreeVector)} is null";
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

        /// <summary>
        /// �������� �������������� ������� ������������� � ������ ����� � ��������� ������ ����������
        /// </summary>
        /// <param name="incidenceMatrix">������� �������������</param>
        /// <param name="degreeVector">������������ ������ �����</param>
        /// <returns></returns>
        public static bool TryIncidenceToVector(in int[,] incidenceMatrix, out int[] degreeVector)
        {
            return TryIncidenceToVector(incidenceMatrix, out degreeVector, out var _);
        }

        /// <summary>
        /// �������� �������������� ������� ������������� � ������ ����� � ��������� ������ ����������
        /// </summary>
        /// <param name="incidenceMatrix">������� �������������</param>
        /// <param name="degreeVector">������������ ������ �����</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TryIncidenceToVector(in int[,] incidenceMatrix, out int[] degreeVector, out string errmes)
        {
            if (incidenceMatrix == null)
            {
                degreeVector = new int[0];
                errmes = $"{nameof(incidenceMatrix)} is null";
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

        /// <summary>
        /// �������� �������������� ������� ������������� � ������� ��������� � ��������� ������ ����������
        /// </summary>
        /// <param name="incidenceMatrix">������� �������������</param>
        /// <param name="adjacencyMatrix">������������ ������� ���������</param>
        /// <returns></returns>
        public static bool TryIncidenceToAdjacency(in int[,] incidenceMatrix, out int[,] adjacencyMatrix)
        {
            return TryIncidenceToAdjacency(incidenceMatrix, out adjacencyMatrix, out var _);
        }

        /// <summary>
        /// �������� �������������� ������� ������������� � ������� ��������� � ��������� ������ ����������
        /// </summary>
        /// <param name="incidenceMatrix">������� �������������</param>
        /// <param name="adjacencyMatrix">������������ ������� ���������</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TryIncidenceToAdjacency(in int[,] incidenceMatrix, out int[,] adjacencyMatrix, out string errmes)
        {
            if (incidenceMatrix == null)
            {
                adjacencyMatrix = new int[0, 0];
                errmes = $"{nameof(incidenceMatrix)} is null";
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

        /// <summary>
        /// �������� �������� ����� �� �������� ��������� � ��������� ������ ����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="ribs">������������ ������ �����</param>
        /// <param name="inverse">�������� ������������� ����� (default = false)</param>
        /// <returns></returns>
        public static bool TryGetRibs(in int[,] adjacencyMatrix, out List<(int, int)> ribs, bool inverse = false)
        {
            return TryGetRibs(adjacencyMatrix, out ribs, out var _, inverse: inverse);
        }

        /// <summary>
        /// �������� �������� ����� �� ������� ��������� � ��������� ������ ����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="ribs">������������ ������ �����</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <param name="inverse">�������� ������������� ����� (default = false)</param>
        /// <returns></returns>
        public static bool TryGetRibs(in int[,] adjacencyMatrix, out List<(int, int)> ribs, out string errmes, bool inverse = false)
        {
            if (adjacencyMatrix == null)
            {
                ribs = new();
                errmes = $"{nameof(adjacencyMatrix)} is null!";
                return false;
            }

            ribs = new List<(int, int)>();
            if (!CheckAdjacencyGraphical(adjacencyMatrix, out errmes))
            {
                return false;
            }

            var verticles = adjacencyMatrix.GetLength(0);

            for (var i = 0; i < verticles / 2; i++)
            {
                for (var j = verticles - 1; j > i; j--)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        ribs.Add(inverse ? (j, i) : (i, j));
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// �������� �������� ���� �� ������� ��������� � ��������� ������ ����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="bases">������������ ������ ���</param>
        /// <param name="extreme">��� �������������� ����� (default = true)</param>
        /// <param name="inverse">�������� ������������� ����� (default = false)</param>
        /// <returns></returns>
        public static bool TryGetBases(in int[,] adjacencyMatrix, out List<(int, int)> bases, bool extreme = true, bool inverse = false)
        {
            return TryGetBases(adjacencyMatrix, out bases, out var _, extreme: extreme, inverse: inverse);
        }

        /// <summary>
        /// �������� �������� ���� �� ������� ��������� � ��������� ������ ����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="bases">������������ ������ ���</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <param name="extreme">��� �������������� ����� (default = true)</param>
        /// <param name="inverse">�������� ������������� ����� (default = false)</param>
        /// <returns></returns>
        public static bool TryGetBases(in int[,] adjacencyMatrix, out List<(int, int)> bases, out string errmes, bool extreme = true, bool inverse = false)
        {
            if (adjacencyMatrix == null)
            {
                bases = new();
                errmes = $"{nameof(adjacencyMatrix)} is null!";
                return false;
            }

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
                        if (extreme)
                        {
                            errmes = "Cant find bases for not extreme adjacency matrix.";
                            bases.Clear();
                            return false;
                        }

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
                    bases.Add(inverse ? (prev_j, prev_i) : (prev_i, prev_j));
                }

                prev_i = i;
                prev_j = j;
                i++;
            }

            return true;
        }

        /// <summary>
        /// �������� �������� ��������� �� ������� ��������� � ��������� ������ �����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="signature">������������ ������ ���������</param>
        /// <returns></returns>
        public static bool TryGetSignature(int[,] adjacencyMatrix, out int[] signature)
        {
            return TryGetSignature(adjacencyMatrix, out signature, out _);
        }

        /// <summary>
        /// �������� �������� ��������� �� ������� ��������� � ��������� ������ �����������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="signature">������������ ������ ���������</param>
        /// <param name="errmes">������������ �������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TryGetSignature(int[,] adjacencyMatrix, out int[] signature, out string errmes)
        {
            if (adjacencyMatrix == null)
            {
                signature = new int[0];
                errmes = $"{nameof(adjacencyMatrix)} is null!";
                return false;
            }

            var vertices = adjacencyMatrix.GetLength(0);
            signature = new int[vertices * vertices];

            if (!CheckAdjacencyGraphical(adjacencyMatrix, out errmes))
            {
                return false;
            }

            for (int i = 0; i < vertices; ++i)
            {
                for (int j = 0; j < vertices; ++j)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        int index = i * vertices + j;
                        signature[index] = 1;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// �������� �������� ������� ��������� �� ��������� � ��������� ������ �����������
        /// </summary>
        /// <param name="signature">������� ���������</param>
        /// <param name="adjacencyMatrix">����������� ������� ���������</param>
        /// <returns></returns>
        public static bool TrySignatureToAdjacencyMatrix(int[] signature, out int[,] adjacencyMatrix)
        {
            return TrySignatureToAdjacencyMatrix(signature, out adjacencyMatrix, out _);
        }

        /// <summary>
        /// �������� �������� ������� ��������� �� ��������� � ��������� ������ �����������
        /// </summary>
        /// <param name="signature">������� ���������</param>
        /// <param name="adjacencyMatrix">����������� ������� ���������</param>
        /// <param name="errmes">������������ ��������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TrySignatureToAdjacencyMatrix(int[] signature, out int[,] adjacencyMatrix, out string errmes)
        {
            if (signature == null)
            {
                adjacencyMatrix = new int[0, 0];
                errmes = $"{nameof(signature)} is null!";
                return false;
            }

            var vertices = Convert.ToInt32(Math.Pow(signature.GetLength(0), (1 / 2)));
            if (signature.GetLength(0) / vertices != vertices)
            {
                adjacencyMatrix = new int[0, 0];
                errmes = "Signature length must be square. (64, 49, 36, ...)";
                return false;
            }

            adjacencyMatrix = new int[vertices, vertices];

            for (int i = 0; i < vertices; ++i)
            {
                for (int j = 0; j < vertices; ++j)
                {
                    int index = i * vertices + j;
                    adjacencyMatrix[i, j] = signature[index];
                }
            }

            errmes = "";
            return true;
        }

        /// <summary>
        /// �������� �������� ��������� �� ������ ��� � ��������� ������ �����������
        /// </summary>
        /// <param name="baseList">������ ���</param>
        /// <param name="signature">������������ ������ ���������</param>
        /// <returns></returns>
        public static bool TryBaseToSignature(List<(int, int)> baseList, out int[] signature)
        {
            return TryBaseToSignature(baseList, out signature, out _);
        }

        /// <summary>
        /// �������� �������� ��������� �� ������ ��� � ��������� ������ �����������
        /// </summary>
        /// <param name="baseList">������ ���</param>
        /// <param name="signature">������������ ������ ���������</param>
        /// <param name="errmes">������������ ��������� �� ������ �����������</param>
        /// <returns></returns>
        public static bool TryBaseToSignature(List<(int, int)> baseList, out int[] signature, out string errmes)
        {
            if (baseList == null)
            {
                signature = new int[0];
                errmes = $"{nameof(baseList)} is null!";
                return false;
            }

            var vertices = baseList.Count;
            signature = new int[vertices * vertices];

            foreach (var (row, col) in baseList)
            {
                int index = (row - 1) * vertices + (col - 1);
                signature[index] = 1;
            }

            errmes = "";
            return true;
        }

        #endregion

        #region Standart

        /// <summary>
        /// ������������ ������ ����� � ������� ���������
        /// </summary>
        /// <param name="degreeVector">������ �����</param>
        /// <returns>������� ���������</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// ����������� ������� ��������� � ������ �����
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <returns>������ �����</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// ������������ ������� ��������� � ������� �������������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <returns>������� �������������</returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// ������������ ������ ����� � ������� �������������
        /// </summary>
        /// <param name="degreeVector">������ �����</param>
        /// <returns>������� �������������</returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// ������������ ������� ������������� � ������� ���������
        /// </summary>
        /// <param name="incidenceMatrix">������� �������������</param>
        /// <returns>������� ���������</returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// ������������ ������� ������������� � ������ �����
        /// </summary>
        /// <param name="incidenceMatrix">������� �������������</param>
        /// <returns>������ �����</returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// �������� ������ ����� �� ������� ���������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="inverse">�������� ������������� ����� (default = false)</param>
        /// <returns>������ �����</returns>
        public static List<(int, int)> GetRibs(in int[,] adjacencyMatrix, bool inverse = false)
        {
            var ribs = new List<(int, int)>();
            if (!CheckAdjacencyGraphical(adjacencyMatrix))
            {
                return ribs;
            }

            var verticles = adjacencyMatrix.GetLength(0);

            for (var i = 0; i < verticles / 2; i++)
            {
                for (var j = verticles - 1; j > i; j--)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        ribs.Add(inverse ? (j, i) : (i, j));
                    }
                }
            }

            return ribs;
        }

        /// <summary>
        /// �������� ������ ��� �� ������� ���������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <param name="extreme">��� �������������� ����� (default = true)</param>
        /// <param name="inverse">�������� ������������� ����� (default = false)</param>
        /// <returns>������ ���</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static List<(int, int)> GetBases(in int[,] adjacencyMatrix, bool extreme = true, bool inverse = false)
        {
            if (adjacencyMatrix == null)
            {
                throw new ArgumentNullException(nameof(adjacencyMatrix));
            }

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
                        if (extreme)
                        {
                            throw new ArgumentNullException("Cant find bases for not extreme adjacency matrix.");
                        }
                    }
                    else if (adjacencyMatrix[i, temp] == 0)
                    {
                        j = -1;
                    }

                    prev = adjacencyMatrix[i, temp];
                }

                if (prev_j != j && prev_i != prev_j)
                {
                    bases.Add(inverse ? (prev_j, prev_i) : (prev_i, prev_j));
                }

                prev_i = i;
                prev_j = j;
                i++;
            }

            return bases;
        }

        /// <summary>
        /// �������� ������ ��������� �� ������� ���������
        /// </summary>
        /// <param name="adjacencyMatrix">������� ���������</param>
        /// <returns>������ ���������</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static long GetSignature(int[,] adjacencyMatrix)
        {
            int rowCount = adjacencyMatrix.GetLength(0);
            int colCount = adjacencyMatrix.GetLength(1);
            int i = 0, j = colCount - 1;
            long signature = 0;

            while (i < j)
            {
                bool zeroFound = false;
                for (int k = 0; k < colCount; k++)
                {
                    if (adjacencyMatrix[i, k] == 0 && i != k)
                    {
                        zeroFound = true;
                    }
                    else if (zeroFound && adjacencyMatrix[i, k] == 1)
                    {
                        return -1;
                    }
                }

                if (adjacencyMatrix[i, j] == 1)
                {
                    signature = (signature << 1) | 1;
                    i++;
                }
                else
                {
                    signature = (signature << 1);
                    j--;
                }
            }
            return signature;
        }

        /// <summary>
        /// ������������ ������ ��������� � ������� ���������
        /// </summary>
        /// <param name="signature">������ ���������</param>
        /// <returns>������� ���������</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int[,] SignatureToMatrix(long signature)
        {
            // ����������� ������� ������� �� ������ ����� ���������
            int bits = (int)Math.Ceiling(Math.Log(signature + 1, 2));
            int n = bits + 1;
            int[,] matrix = new int[n, n];

            string binaryString = Convert.ToString(signature, 2).PadLeft(bits, '0');

            int i = 0, j = n - 1; // �������� � ������� �������� ����
            int index = 0;

            // ������� ������ ����� ������� � ���������
            while (index < binaryString.Length && binaryString[index] == '0')
            {
                index++;
            }

            while (i < j && index < binaryString.Length)
            {
                if (binaryString[index] == '1')
                {
                    // ������������� ������� �� ������� �������
                    for (int k = i; k <= j; k++)
                    {
                        matrix[i, k] = 1;
                        matrix[k, i] = 1; // �������� �� ���������
                    }
                    i++;
                }
                else
                {
                    j--;
                }
                index++;
            }

            // ������������� ������������ ����
            for (int k = 0; k < n; k++)
            {
                matrix[k, k] = 0;
            }

            return matrix;
        }

        /// <summary>
        /// ������������ ������ ��� � ���������
        /// </summary>
        /// <param name="baseList">������ ���</param>
        /// <returns>������ ���������</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static long GenerateSignatureFromBases(List<(int, int)> bases)
        {
            if (bases == null || bases.Count == 0)
            {
                throw new ArgumentException("Bases cannot be null or empty.");
            }

            // ����������� ������� ������� �� ������ ����������� ������� � �����
            int n = 0;
            foreach (var (i, j) in bases)
            {
                n = Math.Max(n, Math.Max(i, j));
            }
            n += 1; // ��� ��� ������� �������

            // ���������� ������� ���������
            int[,] matrix = new int[n, n];
            foreach (var (i, j) in bases)
            {
                matrix[i, j] = 1;
                matrix[j, i] = 1; // �������� �� ���������
            }

            // ��������� ������������ �����
            for (int k = 0; k < n; k++)
            {
                matrix[k, k] = 0;
            }

            // ���������� ��������� �� �������
            long signature = 0;
            int row = 0, col = n - 1;
            while (row < col)
            {
                if (matrix[row, col] == 1)
                {
                    signature = (signature << 1) | 1;
                    row++;
                }
                else
                {
                    signature = (signature << 1);
                    col--;
                }
            }

            return signature;
        }

        #endregion

        #region Utility

        /// <summary>
        /// ��������� ������ �� �������
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToString(in int[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var vertices = matrix.GetLength(0);
            var edges = matrix.GetLength(1);

            var result = "";
            var isLine = false;
            for (int i = 0; i < vertices; i++)
            {
                if (isLine) result += "\n";
                var isSpace = false;
                for (int j = 0; j < edges; j++)
                {
                    if (isSpace) result += " ";
                    result += $"{matrix[i, j]}";
                    isSpace = true;
                }

                isLine = true;
            }

            return result;
        }

        /// <summary>
        /// ��������� ������ �� �������
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToString(in int[] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var vertices = matrix.GetLength(0);
            var result = "";
            var space = false;
            for (int i = 0; i < vertices; i++)
            {
                if (space) result += " ";
                result += $"{matrix[i]}";
                space = true;
            }

            return result;
        }

        /// <summary>
        /// ��������� ������ �� ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToString<T>(in List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var result = "";
            var space = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (space) result += " ";
                result += $"{list[i]}";
                space = true;
            }

            return result;
        }

        #endregion
    }


}