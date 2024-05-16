using HyperGraphs;

class Program
{
    static void Main(string[] args)
    {
        var degreeVector = new int[] { 7, 6, 6, 4, 3, 3, 3, 2 };
        var reconstructedGraph = Graph.VectorToAdjacency(degreeVector);
        var incedence = Graph.VectorToIncidence(degreeVector);
        var again = Graph.IncidenceToAdjacency(incedence);
        var agagain = Graph.IncidenceToVector(incedence);
        var agaga = Graph.AdjacencyToVector(again);

        Graph.PrintGraph(reconstructedGraph);

        Console.WriteLine("\n");

        Graph.PrintGraph(again);

        Console.WriteLine("\n");

        Graph.PrintGraph(incedence);

        Console.WriteLine("\n");

        Graph.PrintVector(agagain);

        Console.WriteLine("\n");

        Graph.PrintVector(agaga);

        Console.ReadLine();
    }
}