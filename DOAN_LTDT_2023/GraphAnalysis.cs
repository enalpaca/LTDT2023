using System;
using System.Collections.Generic;

namespace DOAN_LTDT_2023
{
    public class GraphAnalysis
    {
        public int totalVertex = 0;
        public int totalEdge = 0;
        public int totalParallelEdges = 0;
        public int totalEdgeLoop = 0;
        public int totalPendantVertex = 0;
        public int totalIsolatedVertex = 0;
        public List<Edge> listEdges;
        public List<Vertex> vertices;
        public int[,] adjacencyMatrix;
        public bool isUndirectedGraph = true;
        public GraphAnalysis(string filepath)
        {
            listEdges = FileSystem.ReadEdgesFromFile(filepath);
            adjacencyMatrix = FileSystem.ReadAdjacencyMatrixFromFile(filepath);

            isUndirectedGraph = IsUndirectedGraph(adjacencyMatrix);
            totalVertex = adjacencyMatrix.GetLength(0);
            totalEdge = listEdges.Count;
        }

        public static bool IsUndirectedGraph(int[,] matrix)
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

        public static void ShowAdjacencyMatrix(int[,] matrix)
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

        public int CountParallelEdge()
        {
            int numberOfParallelEdge = 0;

            for (int v = 0; v < totalVertex; v++)
            {
                // Với từng đỉnh v, xét đỉnh k vào hoặc ra v
                for (int k = 0; k < totalVertex; k++)
                {
                    // không tính khuyên
                    if (v == k)
                    {
                        continue;
                    }

                    if (adjacencyMatrix[v, k] == 2)
                    {
                        numberOfParallelEdge++;
                    }

                }

            }

            return numberOfParallelEdge / 2;
        }
        public int CountEdgeLoops()
        {
            int numberOfEdgeLoop = 0;

            foreach (Edge edge in this.listEdges)
            {
                if (edge.begin == edge.end)
                {
                    numberOfEdgeLoop++;
                }
            }
            return numberOfEdgeLoop;
        }

        public int CountPendantVertex()
        {
            int numberOfPendantVertex = 0;

            // count các đỉnh có bậc là 1
            foreach (Vertex vertex in this.vertices)
            {
                if (vertex.degree == 1)
                {
                    numberOfPendantVertex++;
                }
            }
            return numberOfPendantVertex;
        }

        public int CountIsolatedVertex()
        {
            int numberOfIsolatedVertex = 0;

            // count các đỉnh có bậc là 0
            foreach (Vertex vertex in this.vertices)
            {
                if (vertex.degree == 0)
                {
                    numberOfIsolatedVertex++;
                }
            }
            return numberOfIsolatedVertex;
        }

        public List<Vertex> CollectDegreeVertex()
        {
            List<Vertex> listVertices = new List<Vertex>();
            for (int v = 0; v < totalVertex; v++)
            {
                // Với từng đỉnh v, xét số cạnh đi ra/vào
                int inDegree = 0;
                int outDegree = 0;
                int loopEdge = 0;
                foreach (Edge edge in this.listEdges)
                {
                    if (edge.begin == v)
                    {
                        outDegree++;
                    }

                    if (edge.end == v)
                    {
                        inDegree++;
                    }

                    // cạnh khuyên
                    if (edge.begin == edge.end && edge.end == v)
                    {
                        loopEdge++;
                    }

                }

                int degree = ((inDegree + outDegree) / 2 + loopEdge);
                if (isUndirectedGraph == false)
                {
                    degree = inDegree + outDegree;
                }

                Vertex V = new Vertex(v, degree, inDegree, outDegree);
                listVertices.Add(V);
            }

            return listVertices;
        }

        public void PrintDegreeOfVertices(List<Vertex> vertexs, bool checkIsUndirectedGraph)
        {
            string[] vertexString = new string[vertexs.Count];

            for (int i = 0; i < vertexs.Count; i++)
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
        }

        public void AnalyzeGraph()
        {
            this.totalEdgeLoop = this.CountEdgeLoops();

            if (this.isUndirectedGraph)
            {
                totalEdge = (totalEdge + totalEdgeLoop) / 2;
            }

            this.totalParallelEdges = this.CountParallelEdge();
            this.vertices = this.CollectDegreeVertex();
            this.totalPendantVertex = this.CountPendantVertex();
            this.totalIsolatedVertex = this.CountIsolatedVertex();
        }

        public void PrintGraphInfor()
        {
            AnalyzeGraph();

            string checkIsUndirectedGraphResult = this.isUndirectedGraph ? "Do thi vo huong" : "Do thi co huong";
            string[] listStrInfo = new string[7]
            {
                checkIsUndirectedGraphResult,
                $"So dinh cua do thi: {totalVertex}",
                $"So canh cua do thi: {totalEdge}",
                $"So cap dinh xuat hien canh boi cua do thi: {totalParallelEdges}",
                $"So canh khuyen cua do thi: {totalEdgeLoop}",
                $"So dinh treo: {totalPendantVertex}",
                $"So dinh co lap: {totalIsolatedVertex}"
            };

            foreach(string str in listStrInfo)
            {
                Console.WriteLine(str);
            }
        }

        public void PrintEdges()
        {
            foreach (Edge edge in listEdges)
            {
                Console.WriteLine($"{edge.begin}->{edge.end}:{edge.weight}");
            }
        }
    }
}
