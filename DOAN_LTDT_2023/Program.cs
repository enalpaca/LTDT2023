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
            int request = -1;
            string dataInput = "";

            Console.WriteLine("Day la do an cua sinh vien: Nguyen Ba Hoai Nhut!");
            Console.WriteLine("Ma so sinh vien: 22810211");
            Console.WriteLine("UNG DUNG LY THUYET DO THI");
            Console.WriteLine("0.Nhập vào danh sách kề của đồ thị");
            Console.WriteLine("1. Phân tích thông tin đồ thị");
            Console.WriteLine("2. Duyệt đồ thị");
            Console.WriteLine("3. Tìm cây khung nhỏ nhất");
            Console.WriteLine("4. Tìm đường đi ngắn nhất");
            Console.WriteLine("5. Tìm chu trình Euler");
            Console.WriteLine("6. Clear console");

            GraphAnalysis grapInstance = null;
            Console.WriteLine("Vui long nhap duong dan file: ");
            dataInput = Console.ReadLine();

            while (FileSystem.CheckExistingFile(dataInput) == false)
            {
                Console.WriteLine("File không tồn tại! Vui lòng nhập lại!");
                dataInput = Console.ReadLine();
            };

            grapInstance = new GraphAnalysis(dataInput);


            while (true)
            {
                Console.WriteLine("_________________________________________________");
                Console.WriteLine("Nhap yeu cau: ");
                request = Convert.ToInt32(Console.ReadLine());

                switch (request)
                {
                    case 0:
                        Console.WriteLine("Vui long nhap duong dan file: ");
                        dataInput = Console.ReadLine();
                        grapInstance = new GraphAnalysis(dataInput);
                        break;
                    case 1:
                        grapInstance.PrintGraphInfor();
                        break;
                    case 2:
                        int sourceVertex = Utils.ReadVertexSource(grapInstance.totalVertex);

                        GraphTraversal graphTraversalInstance = new GraphTraversal();
                        graphTraversalInstance.DeepFirstSearch(grapInstance.listEdges, sourceVertex);
                        graphTraversalInstance.BreadthFirstSearch(grapInstance.listEdges, sourceVertex);
                        graphTraversalInstance.PrintVisitedVertex();

                        graphTraversalInstance.ProcessConnectedComponent(grapInstance);
                        graphTraversalInstance.PrintConectedComponent();

                        break;
                    case 3:
                        int sourceVertexSpanningTreeWithPrim = Utils.ReadVertexSource(grapInstance.totalVertex);
                        SpanningTree spanningTree = new SpanningTree(grapInstance);
                        spanningTree.MinimumSpanningTreeWithPrim(sourceVertexSpanningTreeWithPrim);
                        spanningTree.MinimumSpanningTreeWithKruskal();
                        spanningTree.PrintSpanningTree();

                        break;
                    case 4:
                        int sourceVertexShortestPath = Utils.ReadVertexSource(grapInstance.totalVertex);
                        ShortestPath shortestPathDijsktra = new ShortestPath(grapInstance);
                        shortestPathDijsktra.Dijkstra(sourceVertexShortestPath);
                        shortestPathDijsktra.PrintDijsktraPath();

                        ShortestPath shortestPathFordBellman = new ShortestPath(grapInstance);
                        shortestPathFordBellman.FordBellman(sourceVertexShortestPath);
                        shortestPathFordBellman.PrintFordBellmanPath();
                        break;
                    case 5:
                        int sourceVertexEuler = Utils.ReadVertexSource(grapInstance.totalVertex);
                        EulerCircle eulerCircle = new EulerCircle(grapInstance);
                        eulerCircle.FindEulerCircle(sourceVertexEuler);
                        break;
                    case 6: //thoat ctrinh
                        return;

                };
            }
        }
    }
}
