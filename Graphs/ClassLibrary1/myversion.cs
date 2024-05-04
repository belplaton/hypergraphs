using System;
using System.Collections.Generic;

public class Graph
{
    private int[,] adjacencyMatrix;
    private int[,] incidenceMatrix;
    private List<int>[] adjacencyList;

    public Graph(int[,] adjMatrix)
    {
        adjacencyMatrix = adjMatrix;
        InitializeAdjacencyListFromAdjacencyMatrix();
    }

    public Graph(int[,] incMatrix, bool isIncidence)
    {
        incidenceMatrix = incMatrix;
        InitializeAdjacencyListFromIncidenceMatrix();
    }

    public Graph(int[] vector)
    {
        InitializeAdjacencyListFromVector(vector);
    }

    private void InitializeAdjacencyListFromAdjacencyMatrix()
    {
        int vertexCount = adjacencyMatrix.GetLength(0);
        adjacencyList = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            adjacencyList[i] = new List<int>();
            for (int j = 0; j < vertexCount; j++)
            {
                if (adjacencyMatrix[i, j] == 1)
                {
                    adjacencyList[i].Add(j);
                }
            }
        }
    }

    private void InitializeAdjacencyListFromIncidenceMatrix()
    {
        int vertexCount = incidenceMatrix.GetLength(0);
        int edgeCount = incidenceMatrix.GetLength(1);
        adjacencyList = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            adjacencyList[i] = new List<int>();
            for (int j = 0; j < edgeCount; j++)
            {
                if (incidenceMatrix[i, j] == 1)
                {
                    for (int k = 0; k < vertexCount; k++)
                    {
                        if (incidenceMatrix[k, j] == -1 && k != i)
                        {
                            adjacencyList[i].Add(k);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void InitializeAdjacencyListFromVector(int[] vector)
    {
        int vertexCount = vector.Length;
        adjacencyList = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            adjacencyList[i] = new List<int>();
            for (int j = 0; j < vertexCount; j++)
            {
                if (vector[j] == 1 && i != j)
                {
                    adjacencyList[i].Add(j);
                }
            }
        }
    }

    public void PrintAdjacencyMatrix()
    {
        Console.WriteLine("Adjacency Matrix:");
        for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
            {
                Console.Write(adjacencyMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void PrintIncidenceMatrix()
    {
        Console.WriteLine("Incidence Matrix:");
        for (int i = 0; i < incidenceMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < incidenceMatrix.GetLength(1); j++)
            {
                Console.Write(incidenceMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void PrintAdjacencyList()
    {
        Console.WriteLine("Adjacency List:");
        for (int i = 0; i < adjacencyList.Length; i++)
        {
            Console.Write(i + ": ");
            foreach (var vertex in adjacencyList[i])
            {
                Console.Write(vertex + " ");
            }
            Console.WriteLine();
        }
    }
    
    public int[,] ConvertToAdjacencyMatrix()
    {
        int vertexCount = adjacencyList.Length;
        int[,] resultMatrix = new int[vertexCount, vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            foreach (var vertex in adjacencyList[i])
            {
                resultMatrix[i, vertex] = 1;
            }
        }
        return resultMatrix;
    }

    public int[,] ConvertToIncidenceMatrix()
    {
        int vertexCount = adjacencyList.Length;
        int edgeCount = 0;
        foreach (var list in adjacencyList)
        {
            edgeCount += list.Count;
        }
        edgeCount /= 2; // Assuming undirected graph

        int[,] resultMatrix = new int[vertexCount, edgeCount];
        int edgeIndex = 0;
        for (int i = 0; i < vertexCount; i++)
        {
            for (int j = i + 1; j < vertexCount; j++)
            {
                if (adjacencyList[i].Contains(j))
                {
                    resultMatrix[i, edgeIndex] = 1;
                    resultMatrix[j, edgeIndex] = -1;
                    edgeIndex++;
                }
            }
        }
        return resultMatrix;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Пример инициализации через матрицу смежности
        int[,] adjacencyMatrix = {
            {0, 1, 1},
            {1, 0, 0},
            {1, 0, 0}
        };
        Graph graph1 = new Graph(adjacencyMatrix);
        graph1.PrintAdjacencyMatrix();
        graph1.PrintAdjacencyList();

        // Пример инициализации через матрицу инцидентности
        int[,] incidenceMatrix = {
            {1, 1, 0, 0},
            {-1, 0, 1, 0},
            {0, -1, -1, 1}
        };
        Graph graph2 = new Graph(incidenceMatrix, true);
        graph2.PrintIncidenceMatrix();
        graph2.PrintAdjacencyList();

        // Пример инициализации через вектор
        int[] vector = {0, 1, 1, 0, 0};
        Graph graph3 = new Graph(vector);
        graph3.PrintAdjacencyList();
    }
}