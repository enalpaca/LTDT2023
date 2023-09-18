using System;
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
                GraphPath path = new GraphPath(sourceVertex, m, 0, new List<Edge>()); ;
                int tmp_previous = m;

                while (prevous[tmp_previous] != -1)
                {
                    Edge foundEdge = new Edge(prevous[tmp_previous], tmp_previous, matrix[prevous[tmp_previous], tmp_previous]);
                    path.path.Add(foundEdge);
                    path.weight = path.weight + foundEdge.weight;
                    tmp_previous = prevous[tmp_previous];
                }

                // Đảo thứ tự ds cạnh để được tập cạnh sắp xếp theo chiều thuận
                path.path.Reverse();
                listGraphPath.Add(path);
            }

            return listGraphPath;
        }

        public static List<GraphPath>  FordBellman (int sourceVertex, int[,] matrix)
        {
            List<GraphPath> listGraphPath = new List<GraphPath>();
            int numberOfVertex = matrix.GetLength(0);
            int[] cost = new int[numberOfVertex];
            int[] previous = new int[numberOfVertex];
            
            for (int i = 0; i < numberOfVertex; i++)
            {
                cost[i] = Int32.MaxValue;
                previous[i] = -1;
            }
            cost[sourceVertex] = 0;
            previous[sourceVertex] = 0;

            for(int step = 1;step<=numberOfVertex;step++)
            {
                for (int k = 0; k < numberOfVertex; k++)
                {
                    for (int v = 0; v < numberOfVertex; v++)
                    {
                        if(matrix[v,k]!=0 && previous[v]!=-1)
                        {
                            cost[k] = Math.Min(cost[k], cost[v] + matrix[v, k]);

                            if (cost[k]== cost[v] + matrix[v, k])
                            {
                                previous[k] = v;
                            }
                        }
                    }
                }
            }

            previous[sourceVertex] = -1;
            for (int m = 0; m < numberOfVertex; m++)
            {
                GraphPath path = new GraphPath(sourceVertex, m, 0, new List<Edge>()); ;
                int tmp_previous = m;

                while (previous[tmp_previous] != -1)
                {
                    Edge foundEdge = new Edge(previous[tmp_previous], tmp_previous, matrix[previous[tmp_previous], tmp_previous]);
                    path.path.Add(foundEdge);
                    path.weight = path.weight + foundEdge.weight;
                    tmp_previous = previous[tmp_previous];
                }

                // Đảo thứ tự ds cạnh để được tập cạnh sắp xếp theo chiều thuận
                path.path.Reverse();
                listGraphPath.Add(path);
            }

            return listGraphPath;
        }
    }
}
