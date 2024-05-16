using HyperGraphs;

class Program
{
    static void Main(string[] args)
    {
        var degreeVector = new int[] { 7, 6, 6, 4, 3, 3, 3, 2 };
        if (Graph.TryVectorToAdjacency(degreeVector, out var adjacencyMatrix))
        {
            Graph.PrintGraph(adjacencyMatrix);
        }

        Console.WriteLine();

        if (Graph.TryVectorToIncidence(degreeVector, out var incidenceMatrix))
        {
            Graph.PrintGraph(incidenceMatrix);
        }

        Console.WriteLine();

        if (Graph.TryIncidenceToAdjacency(incidenceMatrix, out var aab))
        {
            Graph.PrintGraph(aab);
        }

        Console.ReadLine();
    }
}