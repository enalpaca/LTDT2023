using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace DOAN_LTDT_2023
{
    class Program
    {
        static void PrintGuideLine()
        {
            Console.WriteLine("Đây là đồ án của sinh viên Nguyễn Ba Hoài Nhựt!");
            Console.WriteLine("Mã số sinh viên: 22810211");
            Console.WriteLine("ỨNG DỤNG LÝ THUYẾT ĐỒ THỊ");
            Console.WriteLine("0. Nhập vào danh sách kề của đồ thị");
            Console.WriteLine("1. Phân tích thông tin đồ thị");
            Console.WriteLine("2. Duyệt đồ thị");
            Console.WriteLine("3. Tìm cây khung nhỏ nhất");
            Console.WriteLine("4. Tìm đường đi ngắn nhất");
            Console.WriteLine("5. Tìm chu trình Euler");
            Console.WriteLine("6. Xóa console log");
            Console.WriteLine("7. Thoát chương trình");
        }

        static string ReadFilePath()
        {
            string fileName = "";
            Console.WriteLine("Vui lòng nhập đường dẫn File: ");
            fileName = Console.ReadLine();

            while (FileSystem.CheckExistingFile(fileName) == false)
            {
                Console.WriteLine("File không tồn tại! Vui lòng nhập lại!");
                fileName = Console.ReadLine();
            };
            return fileName;
        }
        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                GraphAnalysis grapInstance = null;
                int request = -1;

                PrintGuideLine();

                grapInstance = new GraphAnalysis(ReadFilePath());

                while (true)
                {
                    Console.WriteLine("_________________________________________________");
                    Console.WriteLine("Nhập yêu cầu: ");
                    request = Convert.ToInt32(Console.ReadLine());

                    switch (request)
                    {
                        case 0:
                            grapInstance = new GraphAnalysis(ReadFilePath());
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
                        case 6:
                            Console.Clear();
                            PrintGuideLine();
                            break;
                        case 7: //thoat ctrinh
                            return;

                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Đã có lỗi xảy ra!");
                Console.WriteLine(e.Message);
            }
        }
    }
}
