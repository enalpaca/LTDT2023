using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace DOAN_LTDT_2023
{
    class Program
    {
        static string GetPathTestCaseFile(string filename)
        {
            string currentDir = System.IO.Directory.GetCurrentDirectory();
            return Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\" + filename));
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Day la do an cua sinh vien: Nguyen Ba Hoai Nhut!");
            Console.WriteLine("Ma so sinh vien: 22810211");
            Console.WriteLine($"args: {string.Join(",", args)}");
            // Yêu cầu 1
            GraphAnalysis grap1 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau1-vd1.txt"));
            grap1.PrintGraphInfor();

            GraphAnalysis grap2 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau1-vd2.txt"));
            grap2.PrintGraphInfor();

            Console.WriteLine("=================");

            // Yêu cầu 2
            GraphAnalysis grap3 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau2-vd1.txt"));
            GraphTraversal graphTraversal1 = new GraphTraversal();
            graphTraversal1.DeepFirstSearch(grap3.listEdges, 7);
            graphTraversal1.BreadthFirstSearch(grap3.listEdges, 7);
            graphTraversal1.PrintVisitedVertex();

            GraphAnalysis grap4 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau2-vd2.txt"));
            GraphTraversal graphTraversal2 = new GraphTraversal();
            graphTraversal2.DeepFirstSearch(grap4.listEdges, 0);
            graphTraversal2.BreadthFirstSearch(grap4.listEdges, 0);
            graphTraversal2.PrintVisitedVertex();

            GraphAnalysis grap5 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau2-vd3.txt"));
            GraphTraversal graphTraversal3 = new GraphTraversal();
            graphTraversal3.ProcessConnectedComponent(grap5);
            graphTraversal3.PrintConectedComponent();

            //Yêu cầu 3
            GraphAnalysis grap6 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau3-vd1.txt"));
            SpanningTree spanningTree1 = new SpanningTree(grap6);
            spanningTree1.MinimumSpanningTreeWithPrim(0);
            spanningTree1.MinimumSpanningTreeWithKruskal();
            spanningTree1.PrintSpanningTree();

            GraphAnalysis grap7 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau3-vd2.txt"));
            SpanningTree spanningTree2 = new SpanningTree(grap7);
            spanningTree2.MinimumSpanningTreeWithPrim(0);
            spanningTree2.MinimumSpanningTreeWithKruskal();
            spanningTree2.PrintSpanningTree();

            //Yêu cầu 4
            //Dijsktra
            GraphAnalysis grap8 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau4-Dijsktra.txt"));
            ShortestPath shortestPathDijsktra = new ShortestPath(grap8);
            shortestPathDijsktra.sourceVertexDijsktra = 0;
            shortestPathDijsktra.Dijkstra();
            shortestPathDijsktra.PrintDijsktraPath();

            //FordBellman 
            GraphAnalysis grap9 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau4-vd1.txt"));
            ShortestPath shortestPathFordBellman1 = new ShortestPath(grap9);
            shortestPathFordBellman1.sourceVertexFordBellman = 0;
            shortestPathFordBellman1.FordBellman();
            shortestPathFordBellman1.PrintFordBellmanPath();

            GraphAnalysis grap10 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau4-vd2.txt"));
            ShortestPath shortestPathFordBellman2 = new ShortestPath(grap10);
            shortestPathFordBellman2.sourceVertexFordBellman = 0;
            shortestPathFordBellman2.FordBellman();
            shortestPathFordBellman2.PrintFordBellmanPath();

            // Yêu cầu 5
            GraphAnalysis grap11 = new GraphAnalysis(GetPathTestCaseFile("data-yeucau3-vd2.txt"));
            EulerCircle eulerCircle = new EulerCircle(grap11);
            eulerCircle.FindEulerCircle();

        }
    }
}
