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
        public int vertex=-1;
        public int degree = 0;
        public int inDegree = 0;
        public int outDegree = 0;
        public int loopEdge = 0;
        public bool isPendantVertex = false;
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
        int totalVertex = 0;
        int totalEdge = 0;
        int totalVertexHasParallelEdge = 0;
        int totalEdgeLoop = 0;
        int totalPendantVertex = 0;
        int totalIsolatedVertex = 0;
        int[,] matrix;
        bool isUndirectedGraph=true;
        int[,] adjacencyMatrix;
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
        
        static int[,] BuildAdjacencyMatrix(string filePath)
        {

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            int totalVertex = Convert.ToInt32(lines[0]);
            int[,] adjacencyMatrix = new int[totalVertex, totalVertex];

            for (int i = 0; i < totalVertex; i++)
            {
                for (int j = 0; j < totalVertex; j++)
                {
                    adjacencyMatrix[i, j] = 0;
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
                    int currentCol = tokenQueue.Dequeue();
                    // skip workload
                    tokenQueue.Dequeue();
                    adjacencyMatrix[currentRow, currentCol] = adjacencyMatrix[currentRow, currentCol] +1;
                }

            }

            return adjacencyMatrix;
        }
        static bool CheckIsPendantVertex(int vertextIndex, int[,] matrix)
        {
            int countPendantVertex = 0;
            for (int colIndex = 0; colIndex < matrix.GetLength(0); colIndex++)
            {
                if (matrix[vertextIndex, colIndex] != 0 && matrix[vertextIndex, vertextIndex] <=0)
                {
                    countPendantVertex++;
                }
              
            }
            return countPendantVertex == 1 ? true: false;
        }
        static Vertex[] DegreeVertex(int[,] matrix, bool isUndirectedGraph)
        {
            Vertex[] vertexs = new Vertex[matrix.GetLength(0)];

            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {

                int countDegree = 0;
                int inDegree = 0;
                int outDegree = 0;
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    // dont count main diagonal
                    if (matrix[rowIndex, colIndex] > 0 && rowIndex!= colIndex)
                    {
                        countDegree+= matrix[rowIndex, colIndex];
                        outDegree+= matrix[rowIndex, colIndex];
                    }

                }

                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    if (matrix[colIndex, rowIndex] > 0 && rowIndex != colIndex)
                    {
                        countDegree+= matrix[colIndex, rowIndex];
                        inDegree+= matrix[colIndex, rowIndex];
                    }

                }

                if (isUndirectedGraph)
                {
                    inDegree = 0;
                    outDegree = 0;
                    countDegree = countDegree / 2;
                }
               
                // check loop edge - main diagonal, 1 loop edge  => 2degree
                if (matrix[rowIndex, rowIndex] >= 1)
                {
                    if (isUndirectedGraph)
                    {
                        countDegree += matrix[rowIndex, rowIndex] * 2;
                    }
                    else
                    {
                        // directed Graph => countDegree = inDegree + outDegree
                        inDegree += matrix[rowIndex, rowIndex];
                        outDegree += matrix[rowIndex, rowIndex];
                        countDegree = inDegree + outDegree;
                    }
                 
                }

                vertexs[rowIndex] = new Vertex(rowIndex, countDegree, inDegree, outDegree);
                vertexs[rowIndex].isPendantVertex = CheckIsPendantVertex(rowIndex, matrix);
                vertexs[rowIndex].loopEdge = matrix[rowIndex, rowIndex];
            }

            return vertexs;
        }
        public Graph(string filePath)
        {
            this.matrix = ReadMatrixFromFile(filePath);
            this.adjacencyMatrix = BuildAdjacencyMatrix(filePath);
            this.isUndirectedGraph = IsUndirectedGraph(this.adjacencyMatrix);
            this.vertices = DegreeVertex(this.adjacencyMatrix, this.isUndirectedGraph);
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
        int CountNumberOfEdge()
        {
            int countEdge = 0;
            foreach (Vertex vertex in this.vertices)
            {
               
               countEdge+= vertex.degree;
            }

            return countEdge/2;
        }
        static int CountVertexHasParallelEdge(int[,] matrix)
        {
            int numberOfParallelEdge = 0;
        
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < matrix.GetLength(0); colIndex++)
                {
                    if (matrix[rowIndex, colIndex] >1)
                    {
                        numberOfParallelEdge++;
                    }
                }
            }

            return numberOfParallelEdge/2;
        }
        int CountEdgeLoops()
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
        int CountPendantVertex()
        {
            int count = 0;
            foreach(Vertex vertex in this.vertices)
            {
                if (vertex.isPendantVertex)
                {
                    count++;
                }
            }
           
            return count;
        }
        int CountIsolatedVertex()
        {
            int count = 0;

            for (int i = 0; i < this.vertices.Length; i++)
            {
                if (this.vertices[i].degree == 0)
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

            Console.WriteLine(checkIsUndirectedGraph ? "Bac cua tung dinh:" : "(Bac vao - bac ra) cua tung dinh:");
            Console.WriteLine(string.Join(" ", vertexString));
        }

        public void AnalyzeGraph()
        {
            
            totalVertex = this.vertices.Length;
            isUndirectedGraph = IsUndirectedGraph(this.adjacencyMatrix);
            totalEdge = this.CountNumberOfEdge();
            totalVertexHasParallelEdge = CountVertexHasParallelEdge(this.adjacencyMatrix);
            totalEdgeLoop = this.CountEdgeLoops();
            totalPendantVertex = this.CountPendantVertex();
            totalIsolatedVertex = this.CountIsolatedVertex();
        }
        public void PrintGraphInfor()
        {
            PrintAdjacencyMatrix(adjacencyMatrix);
            string checkIsUndirectedGraphResult = this.isUndirectedGraph ? "Do thi vo huong" : "Do thi co huong";
            Console.WriteLine($"{checkIsUndirectedGraphResult}");
            Console.WriteLine($"So dinh cua do thi: {totalVertex}");
            Console.WriteLine($"So canh cua do thi: {totalEdge}");
            Console.WriteLine($"So cap dinh xuat hien canh boi cua do thi: {totalVertexHasParallelEdge}");
            Console.WriteLine($"So canh khuyen cua do thi: {totalEdgeLoop}");
            Console.WriteLine($"So dinh treo: {totalPendantVertex}");
            Console.WriteLine($"So dinh co lap: {totalIsolatedVertex}");
            PrintDegreeOfVertices(this.vertices, this.isUndirectedGraph);


        }
    }
}
