using System.Collections.Generic;

namespace DOAN_LTDT_2023
{
    class GraphTraversal
    {

        public static int[] DeepFirstSearch(int[,] matrix, int sourceVertext)
        {
            List<int> listVisited = new List<int>();
            Stack<int> mystack = new Stack<int>();

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

                if (mystack.Count == 0) return;

                // sort vertex in stack
                List<int> myList = new List<int>(mystack);
                myList.Sort();
                myList.Reverse();
                mystack = new Stack<int>(myList);

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
        public static int CountConnectedComponent(int[,] matrix)
        {

            int label = 0;
            if (true||Graph.IsUndirectedGraph(matrix))
            {
                int[] vertexLabels = new int[matrix.GetLength(0)];
                for(int k=0; k< vertexLabels.Length;k++)
                {
                    vertexLabels[k] = 0;
                }    

                for (int i=0; i<matrix.GetLength(0); i++)
                {
                    if(vertexLabels[i]==0)
                    {
                        label++;
                        DFS_AssignLabel(i, label, ref vertexLabels, matrix);
                    }    
                } 
                
            }
            return label;
        }

        public static void DFS_AssignLabel(int vertex,int label,ref int[] vertexlabels, int[,] matrix)
        {
            vertexlabels[vertex] = label;
            for(int u=0; u<matrix.GetLength(0);u++)
            {
                if(matrix[vertex,u]!=0 &&vertexlabels[u]==0)
                {
                    DFS_AssignLabel(u, label, ref vertexlabels, matrix);
                }    
            }    

        }
    }
}
