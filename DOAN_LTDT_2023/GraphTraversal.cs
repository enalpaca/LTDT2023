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

        public static int[] DeepFirstSearch(int[,] matrix, int sourceVertext)
        {
            List<int> listVisited = new List<int>();
            MyOderingStack mystack = new MyOderingStack();

            void DFS(int vertex)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[vertex, i] != 0 && mystack.Contains(i) == false && listVisited.Contains(i) == false)
                    {
                        mystack.Push(i);
                    }
                }

                listVisited.Add(vertex);

                if (mystack.GetLength() == 0) return;

                DFS(mystack.Pop());
            }

            DFS(sourceVertext);

            return listVisited.ToArray();
        }

        public static int[] BreadthFirstSearch(int[,] matrix, int sourceVertext)
        {
            List<int> listVisited = new List<int>();
            Queue<int> myqueue = new Queue<int>();

            void BFS(int currentvertex)
            {
                for (int u = 0; u < matrix.GetLength(0); u++)
                {
                    if (matrix[currentvertex, u] != 0 && listVisited.Contains(u) == false && myqueue.Contains(u) == false)
                    {
                        myqueue.Enqueue(u);
                    }
                }

                listVisited.Add(currentvertex);

                if (myqueue.Count == 0) return;

                BFS(myqueue.Dequeue());

            }
            BFS(sourceVertext);

            return listVisited.ToArray();
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
