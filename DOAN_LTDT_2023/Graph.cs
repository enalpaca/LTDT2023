using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_LTDT_2023
{
    class Vertex
    {
        public int vertex;
        public int degree = 0;
        public int inDegree = 0;
        public int outDegree = 0;
        public Vertex(int _vertex, int _degree, int _inDegree, int _outDegree)
        {
            vertex = _vertex;
            degree = _degree;
            inDegree = _inDegree;
            outDegree = _outDegree;
        }
    }

    class Graph
    {
        int numberOfVertex = 0;
        int numberOfEdge = 0;
        int numberOfVertexHasParallelEdge = 0;
        int numberOfEdgeLoop = 0;
        int numberOfPendantVertex = 0;
        int numberOfIsolatedVertex = 0;
        int[,] matrix;
        bool isUndirectedGraph=true;
        int[] vertex;
        Vertex[] vertices;
        
        static int[,] ReadMatrixFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            int totalVertex = Convert.ToInt32(lines[0]);
            int[,] matrix = new int[totalVertex, totalVertex];


            for (int i = 0; i < totalVertex; i++)
            {
                for (int j = 0; j < totalVertex; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            for (int i = 0; i < totalVertex; i++)
            {
                string linedata = lines[i + 1];
                string[] tokens = linedata.Split(" ");
                // function convert array string to array int https://stackoverflow.com/questions/1297231/convert-string-to-int-in-one-line-of-code-using-linq
                int[] numberTokens = Array.ConvertAll(tokens, int.Parse);
                Queue<int> tokenQueue = new Queue<int>(numberTokens);
                int numberOfAdjacencyVertex = tokenQueue.Dequeue();

                for (int adjacencyVertexIndex = 0; adjacencyVertexIndex < numberOfAdjacencyVertex; adjacencyVertexIndex++)
                {
                    int currentRow = i;
                    matrix[currentRow, tokenQueue.Dequeue()] = tokenQueue.Dequeue();
                }

            }
            return matrix;
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
        public Graph(string filePath)
        {
            this.matrix = ReadMatrixFromFile(filePath);
            this.vertices = DegreeVertex(this.matrix);
        }
        static bool IsUndirectedGraph(int[,] matrix)
        {
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    if (matrix[rowIndex, colIndex] != matrix[colIndex, rowIndex])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static int CountNumberOfEdge(int[,] matrix)
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

            if (IsUndirectedGraph(matrix) == true)
            {
                numberofEdge = numberofEdge / 2;
            }
            return numberofEdge;
        }
        static int CountNumberOfVertexHasParallelEdge(int[,] matrix)
        {
            int numberofVertexHasParallelEdge = 0;
            return numberofVertexHasParallelEdge;
        }
        static int CountNumberOfEdgeLoops(int[,] matrix)
        {
            int count = 0;
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                if (matrix[rowIndex, rowIndex] != 0)
                {
                    count++;
                }
            }
            return count;
        }
        static int CountNumberOfPendantVertex(int[,] matrix)
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
        static int CountNumberOfIsolatedVertex(Vertex[] vertexs)
        {
            int count = 0;

            for (int i = 0; i < vertexs.Length; i++)
            {
                if (vertexs[i].degree == 0)
                {
                    count++;
                }
            }

            return count;
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
        static void PrintDegreeOfVertices(Vertex[] vertexs, bool checkIsUndirectedGraph)
        {
            string[] vertexString = new string[vertexs.Length];

            for (int i = 0; i < vertexs.Length; i++)
            {
                if (checkIsUndirectedGraph)
                {
                    vertexString[i] = $"{vertexs[i].vertex}({vertexs[i].degree})";
                }
                else
                {
                    vertexString[i] = $"{vertexs[i].vertex}({vertexs[i].inDegree}-{vertexs[i].outDegree})";
                }
            }

            Console.WriteLine(checkIsUndirectedGraph ? "(Bac vao - bac ra) cua tung dinh:" : "Bac cua tung dinh:");
            Console.WriteLine(string.Join(" ", vertexString));
        }

        public void AnalyzeGraph()
        {
            
            numberOfVertex = this.matrix.GetLength(0);
            isUndirectedGraph = IsUndirectedGraph(this.matrix);
            numberOfEdge = CountNumberOfEdge(this.matrix);
            numberOfVertexHasParallelEdge = CountNumberOfVertexHasParallelEdge(this.matrix);
            numberOfEdgeLoop = CountNumberOfEdgeLoops(this.matrix);
            numberOfPendantVertex = CountNumberOfPendantVertex(this.matrix);
            numberOfIsolatedVertex = CountNumberOfIsolatedVertex(this.vertices);
        }
        public void PrintGraphInfor()
        {
            PrintAdjacencyMatrix(matrix);
            string checkIsUndirectedGraphResult = this.isUndirectedGraph ? "Do thi vo huong" : "Do thi co huong";
            Console.WriteLine($"{checkIsUndirectedGraphResult}");
            Console.WriteLine($"So dinh cua do thi:{numberOfVertex}");
            Console.WriteLine($"So canh cua do thi:{numberOfEdge}");
            Console.WriteLine($"So cap dinh xuat hien canh boi cua do thi:{numberOfVertexHasParallelEdge}");
            Console.WriteLine($"So canh khuyen cua do thi:{numberOfEdgeLoop}");
            Console.WriteLine($"So dinh co lap:{numberOfIsolatedVertex}");
            PrintDegreeOfVertices(this.vertices, this.isUndirectedGraph);
        }
    }
}
