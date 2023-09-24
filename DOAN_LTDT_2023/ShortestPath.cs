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
        public GraphAnalysis graphAnalysis;
        public List<GraphPath> dijsktraPath;
        public List<GraphPath> fordBellmanPaths;
        public int sourceVertexDijsktra;
        public int sourceVertexFordBellman;
        public ShortestPath(GraphAnalysis _graphAnalysis)
        {
            graphAnalysis = _graphAnalysis;
        }

        public static bool CheckGraphHasPositiveWeight(List<Edge> edges)
        {
            foreach (Edge edge in edges)
            {
                if (edge.weight < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void Dijkstra(int sourceVertexDijsktra)
        {
            this.sourceVertexDijsktra = sourceVertexDijsktra;
            int numberOfVertex = graphAnalysis.totalVertex;
            int[] L = new int[numberOfVertex]; //Chi phi duong di tu i den k
            int[] prevous = new int[numberOfVertex]; //Dinh lien truoc cua dinh k tren duong di
            List<int> T = new List<int>();
            List<GraphPath> listGraphPath = new List<GraphPath>();

            if (!CheckGraphHasPositiveWeight(graphAnalysis.listEdges))
            {
                return;
            }

            for (int i = 0; i < numberOfVertex; i++)
            {
                T.Add(i);
                L[i] = Int32.MaxValue;
                prevous[i] = -1;
            }

            L[sourceVertexDijsktra] = 0;
            int j = sourceVertexDijsktra;

            while (T.Contains(j))
            {
                T.Remove(j);

                foreach (Edge edge in graphAnalysis.listEdges)
                {
                    if (edge.begin == j)
                    {
                        if (L[j] + edge.weight < L[edge.end])
                        {
                            L[edge.end] = L[j] + edge.weight;
                            prevous[edge.end] = j;
                        }

                    }
                }

                // Tìm đỉnh l thuộc T mà L[l] là min, gán j=l cho lần duyệt tiếp theo
                int valueMin = Int32.MaxValue;
                foreach (int l in T)
                {
                    if (L[l] < valueMin && T.Contains(l))
                    {
                        j = l;
                        valueMin = L[l];
                    }
                }
            }

            for (int m = 0; m < numberOfVertex; m++)
            {
                GraphPath graphPath = new GraphPath(sourceVertexDijsktra, m, 0, new List<Edge>()); ;
                int tmp_previous = m;

                while (prevous[tmp_previous] != -1)
                {
                    Edge foundEdge = graphAnalysis.listEdges.Find(x => x.begin == prevous[tmp_previous] && x.end == tmp_previous);
                    graphPath.weight = graphPath.weight + foundEdge.weight;
                    graphPath.visitedVertices.Add(tmp_previous);
                    tmp_previous = prevous[tmp_previous];
                }

                // Thêm đỉnh nguồn vào visitedVertices nếu tìm được đường đi tới m hoặc m là đỉnh nguồn
                if (prevous[m] != -1 || sourceVertexDijsktra == m)
                {
                    graphPath.visitedVertices.Add(sourceVertexDijsktra);
                }

                // Đảo thứ tự ds đỉnh để được tập đỉnh theo thứ tự viếng thăm
                graphPath.visitedVertices.Reverse();
                listGraphPath.Add(graphPath);
            }

            dijsktraPath = listGraphPath;
        }
        public void FordBellman(int sourceVertexFordBellman)
        {
            this.sourceVertexFordBellman = sourceVertexFordBellman;
            List<GraphPath> listGraphPath = new List<GraphPath>();
            int numberOfVertex = graphAnalysis.totalVertex;
            int[,] cost = new int[numberOfVertex + 1, numberOfVertex];
            int[,] previous = new int[numberOfVertex + 1, numberOfVertex];

            for (int i = 0; i <= numberOfVertex; i++)
            {
                for (int j = 0; j < numberOfVertex; j++)
                {
                    cost[i, j] = Int32.MaxValue;
                    previous[i, j] = -1;
                }
            }

            cost[0, sourceVertexFordBellman] = 0;
            previous[0, sourceVertexFordBellman] = 0;

            for (int step = 1; step <= numberOfVertex; step++)
            {
                // với mỗi step, tìm những đỉnh v đi vào k và cập nhật trọng số nếu cost[step, k] > cost[step - 1, v] + d(v,k)
                for (int k = 0; k < numberOfVertex; k++)
                {
                    // gán cost và pre của step trước vào step hiện tại
                    cost[step, k] = cost[step - 1, k];
                    previous[step, k] = previous[step - 1, k];

                    for (int v = 0; v < numberOfVertex; v++)
                    {
                        Edge edge = graphAnalysis.listEdges.Find(x => x.begin == v && x.end == k);
                        if (edge != null && cost[step - 1, v] != Int32.MaxValue)
                        {
                            // cập nhật cost và pre của đỉnh k  số nếu cost[step, k] > cost[step - 1, v] + d(v, k)
                            if (cost[step, k] > cost[step - 1, v] + edge.weight)
                            {
                                cost[step, k] = cost[step - 1, v] + edge.weight;
                                previous[step, k] = v;
                            }
                        }
                    }
                }
            }

            // Với mỗi đỉnh m thuộc đồ thị, ta truy vết để tìm đường đi tới m
            for (int m = 0; m < numberOfVertex; m++)
            {
                GraphPath graphPath = new GraphPath(sourceVertexFordBellman, m, 0, new List<Edge>());
                int step = numberOfVertex;
                int end = m;
                int start = previous[step, end];

                // nếu previous[numberOfVertex, end] =-1 nghĩa là không có đường đi tới m
                if (previous[numberOfVertex, end] == -1)
                {
                    listGraphPath.Add(graphPath);
                    continue;
                }

                graphPath.visitedVertices.Add(end);

                if (m == sourceVertexFordBellman)
                {
                    listGraphPath.Add(graphPath);
                    continue;
                }

                while (true)
                {
                    graphPath.visitedVertices.Add(start);

                    Edge foundEdge = graphAnalysis.listEdges.Find(x => x.begin == start && x.end == end);

                    if (foundEdge == null)
                    {
                        break;
                    }

                    graphPath.weight = graphPath.weight + foundEdge.weight;

                    if (step - 1 == 0)
                    {
                        graphPath.negativeCircle = true;
                        break;
                    }

                    if (start == sourceVertexFordBellman)
                    {
                        break;
                    }

                    step = step - 1;
                    end = start;
                    start = previous[step, end];
                }

                // Đảo thứ tự ds đỉnh để được tập đỉnh theo thứ tự viếng thăm
                graphPath.visitedVertices.Reverse();
                listGraphPath.Add(graphPath);
            }

            fordBellmanPaths = listGraphPath;
        }

        public void PrintDijsktraPath()
        {
            Console.WriteLine($"Tim duong di bang thuat toan Dijsktra");
            if (!CheckGraphHasPositiveWeight(graphAnalysis.listEdges))
            {
                Console.WriteLine("Do thi co trong so am, Dijsktra khong ho tro do thi co trong so am");
            }
            else
            {
                Console.WriteLine($"Source:{sourceVertexDijsktra}");
                foreach (GraphPath graphPath in dijsktraPath)
                {
                    // https://stackoverflow.com/questions/1178891/convert-or-map-a-list-of-class-to-another-list-of-class-by-using-lambda-or-linq
                    Console.WriteLine($"Duong di ngan nhat den {graphPath.end}:");
                    if (graphPath.visitedVertices.Count == 0)
                    {
                        Console.WriteLine("Khong co duong di");
                    }
                    else
                    {
                        Console.WriteLine($"Cost = {graphPath.weight} Path = {string.Join(" -> ", graphPath.visitedVertices.ToArray())}");
                    }
                }
            }
        }

        public void PrintFordBellmanPath()
        {
            Console.WriteLine($"Tim duong di bang thuat toan FordBellman");
            Console.WriteLine($"Source:{sourceVertexFordBellman}");
            if (fordBellmanPaths.Any(graphPath => graphPath.negativeCircle))
            {
                Console.WriteLine("Do thi co mach am");
            }
            foreach (GraphPath graphPath in fordBellmanPaths)
            {
                Console.WriteLine($"Duong di ngan nhat den {graphPath.end}:");
                if (graphPath.visitedVertices.Count == 0)
                {
                    Console.WriteLine("Khong co duong di");
                }
                else
                {
                    Console.WriteLine($"Cost = {graphPath.weight} Path = {string.Join(" -> ", graphPath.visitedVertices.ToArray())}");
                }
            }
        }
    }
}
