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
            ShortestPath shortestPath = new ShortestPath(grap8);
            shortestPath.sourceVertexDijsktra = 0;
            shortestPath.Dijkstra();
            shortestPath.PrintDijsktraPath();


            /*            
                        string inputPath3 = GetPathTestCaseFile("datademo-yeucau2-vd2.txt");
                        int[,] yeucau2Vd1Matrix = Graph.ReadAdjacencyMatrixFromFile(inputPath3);
                        int yeucau2Vd1Source = 0;
                        int[] yeucau2Vd1DFSVisitedVertexs = GraphTraversal.DeepFirstSearch(yeucau2Vd1Matrix, yeucau2Vd1Source);
                        int[] yeucau2Vd1BFSVisitedVertexs = GraphTraversal.BreadthFirstSearch(yeucau2Vd1Matrix, yeucau2Vd1Source);
                        string inputPath4 = GetPathTestCaseFile("datademo-yeucau2-vd3.txt");
                        int[,] yeucau2Vd3Matrix = Graph.ReadAdjacencyMatrixFromFile(inputPath4);

                        ConnectedComponent countConectedComponent = GraphTraversal.ProcessConnectedComponent(yeucau2Vd3Matrix);
                        Console.WriteLine($"Source: {yeucau2Vd1Source}");
                        Console.WriteLine("Giai thuat DFS");
                        Console.WriteLine(string.Join(" ", yeucau2Vd1DFSVisitedVertexs));
                        Console.WriteLine("Giai thuat BFS");
                        Console.WriteLine(string.Join(" ", yeucau2Vd1BFSVisitedVertexs));
                        Console.WriteLine($"So thanh phan lien thong: {countConectedComponent.countLabel}");


            */
            int[,] yeucau3Vd1Matrix = Graph.ReadAdjacencyMatrixFromFile(GetPathTestCaseFile("datademo-yeucau3-vd1.txt"));
            int[,] yeucau3Vd2Matrix = Graph.ReadAdjacencyMatrixFromFile(GetPathTestCaseFile("datademo-yeucau3-vd2.txt"));
            int[,] yeucau4Vd1Matrix = Graph.ReadAdjacencyMatrixFromFile(GetPathTestCaseFile("datademo-yeucau4-vd3.txt"));


            /*  int fordBellmanSourceVertex = 0;
              List<GraphPath> fordBellmanPaths = ShortestPath.FordBellman(fordBellmanSourceVertex, yeucau4Vd1Matrix);

              Console.WriteLine($"Source:{fordBellmanSourceVertex}");
              if (fordBellmanPaths.Any<GraphPath>(graphPath => graphPath.negativeCircle))
              {
                  Console.WriteLine("Do thi co mach am");
              }
              foreach (GraphPath graphPath in fordBellmanPaths)
              {
                  // https://stackoverflow.com/questions/1178891/convert-or-map-a-list-of-class-to-another-list-of-class-by-using-lambda-or-linq
                  List<int> vertexs = graphPath.paths.ConvertAll<int>(x => x.end);
                  vertexs.Insert(0, fordBellmanSourceVertex);
                  Console.WriteLine($"Duong di ngan nhat den {graphPath.end}:");
                  if (graphPath.weight == Int32.MaxValue)
                  {
                      Console.WriteLine("Khong co duong di");
                  }
                  else
                  {
                      Console.WriteLine($"Cost = {graphPath.weight} Path = {string.Join(" -> ", vertexs.ToArray())}");
                  }
              }*/
        }
    }
}
