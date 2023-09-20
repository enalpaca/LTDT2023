using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_LTDT_2023
{
    class ShortestPath
    {
        public static bool CheckGraphHasPositiveWeight(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] < 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<GraphPath> Dijkstra(int sourceVertex, int[,] matrix)
        {
            int numberOfVertex = matrix.GetLength(0);
            int[] L = new int[numberOfVertex]; //Chi phi duong di tu i den k
            int[] prevous = new int[numberOfVertex]; //Dinh lien truoc cua dinh k tren duong di
            List<int> T = new List<int>();
            List<GraphPath> listGraphPath = new List<GraphPath>();

            if (!CheckGraphHasPositiveWeight(matrix))
            {

                return listGraphPath;
            }

            for (int i = 0; i < numberOfVertex; i++)
            {
                T.Add(i);
                L[i] = Int32.MaxValue;
                prevous[i] = -1;
            }
            L[sourceVertex] = 0;
            int j = sourceVertex;

            while (T.Contains(j))
            {
                T.Remove(j);

                for (int k = 0; k < numberOfVertex; k++)
                {
                    if (matrix[j, k] > 0)
                    {
                        if (L[j] + matrix[j, k] < L[k])
                        {
                            L[k] = L[j] + matrix[j, k];
                            prevous[k] = j;
                        }
                    }
                }

                // Tìm đỉnh l mà L[l] là min 
                int indexMin = 0;
                int valueMin = Int32.MaxValue;
                for (int l = 0; l < numberOfVertex; l++)
                {
                    if (L[l] < valueMin && T.Contains(l))
                    {
                        indexMin = l;
                        valueMin = L[l];
                    }
                }

                j = indexMin;
            }

            for (int m = 0; m < numberOfVertex; m++)
            {
                GraphPath graphPath = new GraphPath(sourceVertex, m, 0, new List<Edge>()); ;
                int tmp_previous = m;

                while (prevous[tmp_previous] != -1)
                {
                    Edge foundEdge = new Edge(prevous[tmp_previous], tmp_previous, matrix[prevous[tmp_previous], tmp_previous]);
                    graphPath.paths.Add(foundEdge);
                    graphPath.weight = graphPath.weight + foundEdge.weight;
                    tmp_previous = prevous[tmp_previous];
                }

                // Đảo thứ tự ds cạnh để được tập cạnh sắp xếp theo chiều thuận
                graphPath.paths.Reverse();
                listGraphPath.Add(graphPath);
            }

            return listGraphPath;
        }
        public static List<GraphPath> FordBellman(int sourceVertex, int[,] matrix)
        {
            List<GraphPath> listGraphPath = new List<GraphPath>();
            int numberOfVertex = matrix.GetLength(0);
            int[,] cost = new int[numberOfVertex + 1, numberOfVertex];
            int[,] previous = new int[numberOfVertex + 1, numberOfVertex];

            for (int i = 0; i < numberOfVertex; i++)
            {
                for (int j = 0; j < numberOfVertex; j++)
                {
                    cost[i, j] = Int32.MaxValue;
                    previous[i, j] = -1;
                }
            }

            cost[0, sourceVertex] = 0;
            previous[0, sourceVertex] = 0;

            for (int step = 0; step <= numberOfVertex; step++)
            {
                for (int k = 0; k < numberOfVertex; k++)
                {
                    if (step - 1 >= 0)
                    {
                        cost[step, k] = cost[step - 1, k];
                        previous[step, k] = previous[step - 1, k];
                    }
                    else
                    {
                        continue;
                    }

                    for (int v = 0; v < numberOfVertex; v++)
                    {
                        if (matrix[v, k] != 0 && cost[step - 1, v] != Int32.MaxValue)
                        {
                            cost[step, k] = Math.Min(cost[step - 1, k], cost[step - 1, v] + matrix[v, k]);

                            if (cost[step, k] == cost[step - 1, v] + matrix[v, k])
                            {
                                previous[step, k] = v;
                            }
                        }
                    }
                }
            }

            for (int m = 0; m < numberOfVertex; m++)
            {
                GraphPath graphPath = new GraphPath(sourceVertex, m, 0, new List<Edge>());
                int step = numberOfVertex;
                int end = m;
                int start = previous[step, end];

                if (m == sourceVertex)
                {
                    listGraphPath.Add(graphPath);
                    continue;
                }

                while (true)
                {
                    Edge foundEdge = new Edge(start, end, matrix[start, end]);
                    graphPath.paths.Add(foundEdge);

                    step = step - 1;
                    if (step == 0)
                    {
                        graphPath.negativeCircle = true;
                        break;
                    }

                    if (start == sourceVertex)
                    {
                        break;
                    }

                    end = start;
                    start = previous[step, end];
                }

                graphPath.weight = cost[numberOfVertex, m];

                // Đảo thứ tự ds cạnh để được tập cạnh sắp xếp theo chiều thuận
                graphPath.paths.Reverse();
                listGraphPath.Add(graphPath);
            }

            return listGraphPath;
        }
    }
}
