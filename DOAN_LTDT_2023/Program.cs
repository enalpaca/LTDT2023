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
            // string inputPath = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo.txt";
            string inputPath = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo-dinhcolap.txt";
            
            Graph myGraph = new Graph(inputPath);
            myGraph.AnalyzeGraph();
            myGraph.PrintGraphInfor();
        }
    }
}
