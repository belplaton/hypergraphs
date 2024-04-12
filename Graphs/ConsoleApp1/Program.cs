using HyperGraphs;

class Program
{
    static void Main(string[] args)
    {
        var degreeVector = new int[] { 1, 2, 2, 1 };
        var reconstructedGraph = Graph.ReconstructGraph(degreeVector);

        Graph.PrintGraph(reconstructedGraph);

        Console.ReadLine();
    }
}