using System;
using System.Collections.Generic;

public class GraphOther
{
    private int[,] adjacencyMatrix;
    private int[,] incidenceMatrix;
    private List<int>[] adjacencyList;

    public GraphOther(int[,] adjMatrix)
    {
        adjacencyMatrix = adjMatrix;
        InitializeAdjacencyListFromAdjacencyMatrix();
    }

    public GraphOther(int[,] incMatrix, bool isIncidence)
    {
        incidenceMatrix = incMatrix;
        InitializeAdjacencyListFromIncidenceMatrix();
    }

    public GraphOther(int[] vector)
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