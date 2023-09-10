using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_LTDT_2023
{
    class SpanningTree
    {
        public static List<Edge> MinimumSpanningTreeWithPrim(int sourceVertex, int[,] matrix)
        {
            List<int> Y = new List<int>();
            List<Edge> T = new List<Edge>();
            ConnectedComponent countConectedComponent = GraphTraversal.ProcessConnectedComponent(matrix);

            if (countConectedComponent.countLabel != 1)
            {
                Console.WriteLine("Day la do thi vo huong hoac co nhieu hon 1 thanh phan lien thong");
                return T;
            }

            Y.Add(sourceVertex);

            List<int> V = new List<int>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                V.Add(i);
            }

            while (T.Count < matrix.GetLength(0) - 1)
            {
                Edge minEdge = new Edge(-1, -1, int.MaxValue);

                foreach (int v in Y)
                {

                    for (int w = 0; w < matrix.GetLength(0); w++)
                    {
                        if (matrix[v, w] != 0 && Y.Contains(w) == false)
                        {

                            if (minEdge.weight > matrix[v, w])
                            {
                                minEdge.begin = v;
                                minEdge.end = w;
                                minEdge.weight = matrix[v, w];
                            }
                        }
                    }
                }

                Y.Add(minEdge.end);
                T.Add(minEdge);
            }

            return T;
        }
    }
}
