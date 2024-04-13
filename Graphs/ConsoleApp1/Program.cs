using HyperGraphs;

class Program
{
    static void Main(string[] args)
    {
        var degreeVector = new int[] { 1, 2, 2, 2, 2, 1 };
        var reconstructedGraph = Graph.VectorToAdjacency(degreeVector);
        var incedence = Graph.VectorToIncedence(degreeVector);

        Graph.PrintGraph(reconstructedGraph);

        Console.WriteLine("\n");

        Graph.PrintGraph(incedence);

        Console.ReadLine();
    }
}