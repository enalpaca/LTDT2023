using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DOAN_LTDT_2023
{
    class ConnectedComponent
    {
        public int countLabel = 0;
        public List<Vertex> vertexs = new List<Vertex>();
        public ConnectedComponent(int _countLabel, List<Vertex> _vertexs)
        {
            this.countLabel = _countLabel;
            this.vertexs = _vertexs;
        }
    }

    class GraphTraversal
    {
        public int[] listDFSVisitedVertex = new int[0];
        public int[] listBFSVisitedVertex = new int[0];
        public int[] DeepFirstSearch(List<Edge> listEdges, int sourceVertext)
        {
            List<int> listVisited = new List<int>();

            void DFS(int vertex)
            {
                listVisited.Add(vertex);
                foreach (Edge edge in listEdges)
                {
                    if (edge.begin== vertex && listVisited.Contains(edge.end) == false)
                    {
                        DFS(edge.end);
                    }
                }
            }

            DFS(sourceVertext);

            listDFSVisitedVertex = listVisited.ToArray();
            return listDFSVisitedVertex;
        }

        public int[] BreadthFirstSearch(List<Edge> listEdges, int sourceVertext)
        {
            List<int> listVisited = new List<int>();
            Queue<int> myqueue = new Queue<int>();

            void BFS(int currentvertex)
            {
                foreach (Edge edge in listEdges)
                {
                    if (edge.begin == currentvertex && myqueue.Contains(edge.end) == false && listVisited.Contains(edge.end) == false)
                    {
                        myqueue.Enqueue(edge.end);
                    }
                }

                listVisited.Add(currentvertex);

                if (myqueue.Count == 0) return;

                BFS(myqueue.Dequeue());

            }
            BFS(sourceVertext);

            listBFSVisitedVertex = listVisited.ToArray();
            return listBFSVisitedVertex;
        }
        
        public void PrintVisitedVertex()
        {
            Console.WriteLine("Giai thuat DFS");
            Console.WriteLine(string.Join(" ", listDFSVisitedVertex));
            Console.WriteLine("Giai thuat BFS");
            Console.WriteLine(string.Join(" ", listBFSVisitedVertex));
        }
        public static ConnectedComponent ProcessConnectedComponent(int[,] matrix)
        {

            int label = 0;

            List<Vertex> myListVertex = new List<Vertex>();

            if (Graph.IsUndirectedGraph(matrix))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    bool flag = myListVertex.Any(item => item.vertex == i);
                    if (!flag)
                    {
                        label++;
                        DFS_AssignLabel(i, label, matrix, ref myListVertex);
                    }
                }

            }
            return new ConnectedComponent(label, myListVertex);
        }

        public static void DFS_AssignLabel(int vertex, int label, int[,] matrix, ref List<Vertex> myListVertex)
        {
            Vertex vertexIns = new Vertex();
            vertexIns.label = label;
            vertexIns.vertex = vertex;

            myListVertex.Add(vertexIns);

            for (int u = 0; u < matrix.GetLength(0); u++)
            {
                bool flag = myListVertex.Any(item => item.vertex == u);

                if (matrix[vertex, u] != 0 && flag != true)
                {
                    DFS_AssignLabel(u, label, matrix, ref myListVertex);
                }
            }
        }

    }
}
