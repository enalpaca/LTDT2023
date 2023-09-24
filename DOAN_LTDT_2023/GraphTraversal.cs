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
        public ConnectedComponent conectedComponent;
        public int[] DeepFirstSearch(List<Edge> listEdges, int sourceVertext)
        {
            List<int> listVisited = new List<int>();

            void DFS(int vertex)
            {
                listVisited.Add(vertex);
                foreach (Edge edge in listEdges)
                {
                    if (edge.begin == vertex && listVisited.Contains(edge.end) == false)
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
        public void ProcessConnectedComponent(GraphAnalysis graph)
        {

            int label = 0;

            List<Vertex> myListVertex = new List<Vertex>();

            if (graph.isUndirectedGraph)
            {
                for (int i = 0; i < graph.totalVertex; i++)
                {
                    bool flag = myListVertex.Any(item => item.vertex == i);
                    if (!flag)
                    {
                        label++;
                        DFS_AssignLabel(i, label, graph.listEdges, ref myListVertex);
                    }
                }

            }
            conectedComponent = new ConnectedComponent(label, myListVertex);

        }
        public void PrintConectedComponent()
        {
            for (int k = 1; k <= conectedComponent.countLabel; k++)
            {
                List<int> listConnectedTogether = new List<int>();

                conectedComponent.vertexs.FindAll(item => item.label == k).ForEach(item =>
                {
                    listConnectedTogether.Add(item.vertex);
                });

                Console.WriteLine($"Thanh phan lien thong thu {k}:");
                Console.WriteLine($"{ string.Join(" ", listConnectedTogether.ToArray())}");

            }
        }

        public static void DFS_AssignLabel(int vertex, int label, List<Edge> edges, ref List<Vertex> myListVertex)
        {
            Vertex vertexIns = new Vertex();
            vertexIns.label = label;
            vertexIns.vertex = vertex;

            myListVertex.Add(vertexIns);

            foreach (Edge edge in edges)
            {
                //bool flag = 
                if (edge.begin == vertex)
                {
                    bool flag = myListVertex.Any(item => item.vertex == edge.end);
                    if (flag != true)
                    {
                        DFS_AssignLabel(edge.end, label, edges, ref myListVertex);
                    }
                }

            }

            /*            for (int u = 0; u < matrix.GetLength(0); u++)
                        {
                            bool flag = myListVertex.Any(item => item.vertex == u);

                            if (matrix[vertex, u] != 0 && flag != true)
                            {
                                DFS_AssignLabel(u, label, matrix, ref myListVertex);
                            }
                        }*/
        }

    }
}
