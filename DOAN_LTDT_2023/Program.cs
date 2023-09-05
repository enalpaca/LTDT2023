using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace DOAN_LTDT_2023
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day la do an cua sinh vien: Nguyen Ba Hoai Nhut!");
            Console.WriteLine("Ma so sinh vien: 22810211");
            // string inputPath = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo-canhkhuyen.txt";
            string inputPath1 = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo-yeucau1-vd1.txt";
            string inputPath2 = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo-yeucau1-vd2.txt";
            
            Graph myGraph1 = new Graph(inputPath1);
            myGraph1.AnalyzeGraph();
            myGraph1.PrintGraphInfor();

            Graph myGraph2 = new Graph(inputPath2);
            myGraph2.AnalyzeGraph();
            myGraph2.PrintGraphInfor();


            string inputPath3 = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo-yeucau2-vd2.txt";
            int[,] yeucau2Vd1Matrix = Graph.ReadAdjacencyMatrixFromFile(inputPath3);
            int yeucau2Vd1Source = 0;
            int[] yeucau2Vd1DFSVisitedVertexs = GraphTraversal.DeepFirstSearch(yeucau2Vd1Matrix, yeucau2Vd1Source);
            int[] yeucau2Vd1BFSVisitedVertexs = GraphTraversal.BreadthFirstSearch(yeucau2Vd1Matrix, yeucau2Vd1Source);
            string inputPath4 = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo-yeucau2-vd3.txt";
            int[,] yeucau2Vd3Matrix = Graph.ReadAdjacencyMatrixFromFile(inputPath4);
            ConnectedComponent countConectedComponent = GraphTraversal.ProcessConnectedComponent(yeucau2Vd3Matrix);
            Console.WriteLine($"Source: {yeucau2Vd1Source}");
            Console.WriteLine("Giai thuat DFS");
            Console.WriteLine(string.Join(" ", yeucau2Vd1DFSVisitedVertexs));
            Console.WriteLine("Giai thuat BFS");
            Console.WriteLine(string.Join(" ", yeucau2Vd1BFSVisitedVertexs));
            Console.WriteLine($"So thanh phan lien thong: {countConectedComponent.countLabel}");
            
            for(int k=1; k<=countConectedComponent.countLabel; k++)
            {
                List<int> listConnectedTogether = new List<int>();
                Console.WriteLine($"Thanh phan lien thong thu {k}:");

                for (int i = 0; i < countConectedComponent.vertexLabels.Length; i++)
                {
                    if (countConectedComponent.vertexLabels[i] == k)
                    listConnectedTogether.Add(i);
                }

                Console.WriteLine($"{ string.Join(" ", listConnectedTogether.ToArray())}");

            }




        }
    }
}
