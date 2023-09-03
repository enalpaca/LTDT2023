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
                    if(matrix[vertex,i]!=0 && mystack.Contains(i)==false && listVisited.Contains(i) == false)
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

            return listVisited.ToArray(); ;
        }
    }
}
