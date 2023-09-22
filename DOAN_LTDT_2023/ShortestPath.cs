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

        public void Dijkstra()
        {
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
                GraphPath graphPath = new GraphPath(sourceVertexDijsktra, m, 0, new List<Edge>()); ;
                int tmp_previous = m;

                while (prevous[tmp_previous] != -1)
                {
                    Edge foundEdge = graphAnalysis.listEdges.Find(x => x.begin == prevous[tmp_previous] && x.end == tmp_previous);
                    graphPath.paths.Add(foundEdge);
                    graphPath.weight = graphPath.weight + foundEdge.weight;
                    tmp_previous = prevous[tmp_previous];
                }

                // Đảo thứ tự ds cạnh để được tập cạnh sắp xếp theo chiều thuận
                graphPath.paths.Reverse();
                listGraphPath.Add(graphPath);
            }

            dijsktraPath = listGraphPath;
        }
        public void FordBellman()
        {
            List<GraphPath> listGraphPath = new List<GraphPath>();
            int numberOfVertex = graphAnalysis.totalVertex;
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

            cost[0, sourceVertexFordBellman] = 0;
            previous[0, sourceVertexFordBellman] = 0;

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

                    foreach(Edge edge in graphAnalysis.listEdges)
                    {
                        if(edge.end==k && cost[step - 1, edge.begin] != Int32.MaxValue)
                        {
                            cost[step, k] = Math.Min(cost[step - 1, k], cost[step - 1, edge.begin] + edge.weight);
                            if(cost[step,k]==cost[step-1, edge.begin] + edge.weight)
                            {
                                previous[step, k] = edge.begin;
                            }
                        }    
                    }    
                }
            }

            for (int m = 0; m < numberOfVertex; m++)
            {
                GraphPath graphPath = new GraphPath(sourceVertexFordBellman, m, 0, new List<Edge>());
                int step = numberOfVertex;
                int end = m;
                int start = previous[step, end];

                if (m == sourceVertexFordBellman)
                {
                    listGraphPath.Add(graphPath);
                    continue;
                }

                while (true)
                {
                    Edge foundEdge = graphAnalysis.listEdges.Find(x=>x.begin==start&&x.end==end);
                    graphPath.paths.Add(foundEdge);

                    step = step - 1;
                    if (step == 0)
                    {
                        graphPath.negativeCircle = true;
                        break;
                    }

                    if (start == sourceVertexFordBellman)
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

            listGraphPath = fordBellmanPaths;
        }

        public void PrintDijsktraPath()
        {
            if (!CheckGraphHasPositiveWeight(graphAnalysis.listEdges))
            {
                Console.WriteLine("Khong co duong di");
            }
            else
            {
                Console.WriteLine($"Source:{sourceVertexDijsktra}");
                foreach (GraphPath graphPath in dijsktraPath)
                {
                    // https://stackoverflow.com/questions/1178891/convert-or-map-a-list-of-class-to-another-list-of-class-by-using-lambda-or-linq
                    List<int> vertexs = graphPath.paths.ConvertAll<int>(x => x.end);
                    vertexs.Insert(0, sourceVertexDijsktra);
                    Console.WriteLine($"Duong di ngan nhat den {graphPath.end}:");
                    Console.WriteLine($"Cost = {graphPath.weight} Path = {string.Join(" -> ", vertexs.ToArray())}");
                }
            }
        }

        public void PrintFordBellmanPath()
        {
            Console.WriteLine($"Source:{sourceVertexFordBellman}");
            if (fordBellmanPaths.Any<GraphPath>(graphPath => graphPath.negativeCircle))
            {
                Console.WriteLine("Do thi co mach am");
            }
            foreach (GraphPath graphPath in fordBellmanPaths)
            {
                // https://stackoverflow.com/questions/1178891/convert-or-map-a-list-of-class-to-another-list-of-class-by-using-lambda-or-linq
                List<int> vertexs = graphPath.paths.ConvertAll<int>(x => x.end);
                vertexs.Insert(0, sourceVertexFordBellman);
                Console.WriteLine($"Duong di ngan nhat den {graphPath.end}:");
                if (graphPath.weight == Int32.MaxValue)
                {
                    Console.WriteLine("Khong co duong di");
                }
                else
                {
                    Console.WriteLine($"Cost = {graphPath.weight} Path = {string.Join(" -> ", vertexs.ToArray())}");
                }
            }
        }
    }
}
