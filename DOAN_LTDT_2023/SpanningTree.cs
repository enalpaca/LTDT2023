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

        public static List<Edge> ListEdge(int[,] matrix)
        {
            List<Edge> listEdge = new List<Edge>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        int begin = i < j ? i : j;
                        int end = j > i ? j : i;
                        listEdge.Add(new Edge(begin, end, matrix[i, j]));
                    }
                }
            }

            listEdge.Sort((x, y) => x.weight.CompareTo(y.weight));

            List<Edge> listWithoutDuplicates = new List<Edge>();
            foreach (Edge item in listEdge)
            {
                if (!listWithoutDuplicates.Any(x => x.begin == item.begin && x.end == item.end && x.weight == item.weight)) //we can also use !listWithoutDuplicates.Any(x => x.Equals(item))
                {
                    listWithoutDuplicates.Add(item);
                }
            }

            return listWithoutDuplicates;
        }

        public static bool CheckCircle(int[] labels, int v, int w)
        {
            if (labels[v] == labels[w])
            {
                return true;
            }
            return false;
        }

        public static List<Edge> MinimumSpanningTreeWithKruskal(int sourceVertex, int[,] matrix)
        {
            List<Edge> listEdge = ListEdge(matrix);
            List<Edge> T = new List<Edge>();
            int[] labels = new int[matrix.GetLength(0)];

            for (int i = 0; i < labels.Length; ++i)
            {
                labels[i] = i;
            }

            while (T.Count < matrix.GetLength(0) - 1)
            {
                Edge edge = listEdge.First();
                listEdge.Remove(edge);

                bool checkCircle = CheckCircle(labels, edge.begin, edge.end);
                if (checkCircle)
                {
                    continue;
                }

                int minLabel = Math.Min(labels[edge.begin], labels[edge.end]);
                int maxLabel = Math.Max(labels[edge.begin], labels[edge.end]);

                for (int i = 0; i < labels.Length; ++i)
                {
                    if (labels[i] == maxLabel)
                    {
                        labels[i] = minLabel;
                    }

                }

                T.Add(edge);
            }

            return T;
        }
    }
}
