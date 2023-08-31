using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DOAN_LTDT_2023
{
    class Program
    {
        class Vertex
        {
           public int vertex;
            public int degree = 0;
            public int inDegree = 0;
            public int outDegree = 0;
            public Vertex(int _vertex, int _degree, int _inDegree, int _outDegree)
            {
                 vertex =  _vertex;
                 degree = _degree;
                 inDegree=  _inDegree;
                 outDegree= _outDegree;
            }
        }

        static int[,] ReadMatrixFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            int totalVertex =Convert.ToInt32(lines[0]);
            int[,] matrix = new int[totalVertex, totalVertex] ;


            for (int i = 0; i < totalVertex; i++)
            {
                for (int j = 0; j < totalVertex; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            for ( int i=0; i< totalVertex; i++)
            {
                string linedata = lines[i + 1];
                string[] tokens = linedata.Split(" ");
                // function convert array string to array int https://stackoverflow.com/questions/1297231/convert-string-to-int-in-one-line-of-code-using-linq
                int[] numberTokens = Array.ConvertAll(tokens, int.Parse);
                Queue<int> tokenQueue = new Queue<int>(numberTokens);
                int numberOfAdjacencyVertex = tokenQueue.Dequeue();

                for(int adjacencyVertexIndex=0; adjacencyVertexIndex< numberOfAdjacencyVertex; adjacencyVertexIndex++)
                {
                    int currentRow = i;
                    matrix[currentRow, tokenQueue.Dequeue()] = tokenQueue.Dequeue();
                }    

            }
            return matrix;
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.WriteLine($"matrix[{row}][{col}]:{matrix[row, col]}");
                }
            }
        }
        static void PrintAdjacencyMatrix(int[,] matrix)
        {
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                int[] rows = new int[matrix.GetLength(1)];

                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    rows[colIndex] = matrix[rowIndex, colIndex];
                }

                Console.WriteLine(string.Join(" ", rows));
            }
        }
        static bool IsUndirectedGraph(int[,] matrix)
        {
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    if(matrix[rowIndex, colIndex] != matrix[colIndex, rowIndex])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        static int NumberofEdge(int[,] matrix)
        {
            int numberofEdge = 0;

            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    if (matrix[rowIndex, colIndex] != 0)
                    {
                       
                        numberofEdge++;
                    }
                }
            }

            if(IsUndirectedGraph(matrix)==true)
            {
                numberofEdge = numberofEdge / 2;
            }    
            return numberofEdge;
        }
        static int NumberofVertexHasParallelEdge(int[,] matrix)
        {
            int numberofVertexHasParallelEdge = 0;
            return numberofVertexHasParallelEdge;
        }
        static Vertex[] DegreeVertex(int[,] matrix)
        {
            Vertex[] vertexs = new Vertex[matrix.GetLength(0)];

            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                
                int countDegree = 0;
                int inDegree = 0;
                int outDegree = 0;
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    if (matrix[rowIndex, colIndex] != 0)
                    {
                        countDegree++;
                        outDegree++;
                    }

                }

                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    if (matrix[colIndex, rowIndex] != 0)
                    {
                        countDegree++;
                        inDegree++;
                    }

                }

                vertexs[rowIndex] = new Vertex(rowIndex, countDegree, inDegree, outDegree);
            }
            

            return vertexs;
        }
        static void PrintDegreeofVertexs(Vertex[] vertexs, bool checkIsUndirectedGraph)
        {
            string[] vertexString = new string[vertexs.Length];
            
            for (int i = 0; i < vertexs.Length; i++)
            {
                if(checkIsUndirectedGraph)
                {
                    vertexString[i] = $"{vertexs[i].vertex}({vertexs[i].degree})";
                }
                else
                {
                    vertexString[i] = $"{vertexs[i].vertex}({vertexs[i].inDegree}-{vertexs[i].outDegree})";
                }
            }

            Console.WriteLine(checkIsUndirectedGraph?"(Bac vao - bac ra) cua tung dinh:": "Bac cua tung dinh:");
            Console.WriteLine(string.Join(" ", vertexString));
        }
        static int PendantVertex(int[,] matrix)
        {
            int count = 0;
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    if (matrix[rowIndex, colIndex] != 0)
                    {

                    }
                }
            }
            return count;
        }    
        static int NumberofEdgeLoops(int[,] matrix)
        {
            int count = 0;
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                if(matrix[rowIndex, rowIndex]!=0)
                {
                    count++;
                }    
            }
            return count;
        } 
        static int CountIsolatedVertex(Vertex[] vertexs)
        {
            int count = 0;

            for (int i = 0; i < vertexs.Length; i++)
            {
                if (vertexs[i].degree== 0)
                {
                    count++;
                }
            }

            return count;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // string inputPath = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo-canhkhuyen.txt";
            // string inputPath = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo.txt";
            string inputPath = "D:/Code/C#/LTDT2023/DOAN_LTDT_2023/datademo-dinhcolap.txt";

            int[,] matrix = ReadMatrixFromFile(inputPath);
            PrintMatrix(matrix);
            PrintAdjacencyMatrix(matrix);
            bool checkIsUndirectedGraph= IsUndirectedGraph(matrix);
            string checkIsUndirectedGraphResult = checkIsUndirectedGraph? "Do thi vo huong" : "Do thi co huong";
            Console.WriteLine($"{checkIsUndirectedGraphResult}");
            Console.WriteLine($"So dinh cua do thi:{matrix.GetLength(0)}");
            Console.WriteLine($"So canh cua do thi:{NumberofEdge(matrix)}");
            Console.WriteLine($"So cap dinh xuat hien canh boi cua do thi:{NumberofVertexHasParallelEdge(matrix)}");
            Console.WriteLine($"So canh khuyen cua do thi:{NumberofEdgeLoops(matrix)}");
            Vertex[] degreeVertex = DegreeVertex(matrix);
            
            PrintDegreeofVertexs(degreeVertex, checkIsUndirectedGraph);
            Console.WriteLine($"So dinh co lap:{CountIsolatedVertex(degreeVertex)}");
        }
    }
}
