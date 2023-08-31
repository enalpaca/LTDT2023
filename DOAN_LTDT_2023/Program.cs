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
        }
    }
}
