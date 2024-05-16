using HyperGraphs;

class Program
{
    static void Main(string[] args)
    {
        var degreeVector = new int[] { 7, 6, 6, 4, 3, 3, 3, 2 };
        var errmes = string.Empty;
        if (Graph.TryVectorToAdjacency(degreeVector, out var adjacencyMatrix, out errmes))
        {
            Graph.PrintGraph(adjacencyMatrix);
            Console.WriteLine("Error Message: " + (errmes != string.Empty ? errmes : "None"));
        }

        var bases = Graph.GetBases(in adjacencyMatrix);
        foreach (var basa in bases)
        {
            Console.WriteLine($"base: {basa}");
        }

        Console.WriteLine();

        if (Graph.TryVectorToIncidence(degreeVector, out var incidenceMatrix, out errmes))
        {
            Graph.PrintGraph(incidenceMatrix);
            Console.WriteLine("Error Message: " + (errmes != string.Empty ? errmes : "None"));
        }

        Console.WriteLine();

        if (Graph.TryIncidenceToAdjacency(incidenceMatrix, out var aab, out errmes))
        {
            Graph.PrintGraph(aab);
            Console.WriteLine("Error Message: " + (errmes != string.Empty ? errmes : "None"));
        }

        Console.ReadLine();
    }
}