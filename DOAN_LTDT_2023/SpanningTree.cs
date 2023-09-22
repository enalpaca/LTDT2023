using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_LTDT_2023
{

    class SpanningTree
    {

        public List<Edge> minSpanningTreeWithPrim = new List<Edge>();
        public List<Edge> minSpanningTreeWithKruskal = new List<Edge>();
        public GraphAnalysis graphAnalysis;
        public bool isUndirectedAndConnectedGraph = false;

        public SpanningTree(GraphAnalysis _graphAnalysis)
        {
            graphAnalysis = _graphAnalysis;
            GraphTraversal graphTraversal = new GraphTraversal();

            graphTraversal.ProcessConnectedComponent(graphAnalysis);
            if (graphTraversal.conectedComponent.countLabel == 1 && graphAnalysis.isUndirectedGraph)
            {
                isUndirectedAndConnectedGraph = true;
            }
        }

        public void MinimumSpanningTreeWithPrim(int sourceVertex)
        {
            List<int> Y = new List<int>();
            List<Edge> T = new List<Edge>();

            if (!isUndirectedAndConnectedGraph)
            {
                return;
            }

            Y.Add(sourceVertex);

            List<int> V = new List<int>();

            for (int i = 0; i < graphAnalysis.totalVertex; i++)
            {
                V.Add(i);
            }

            while (T.Count < graphAnalysis.totalVertex - 1)
            {
                Edge minEdge = new Edge(-1, -1, int.MaxValue);

                foreach (int v in Y)
                {
                    foreach (Edge edge in graphAnalysis.listEdges)
                    {
                        if (edge.begin == v && Y.Contains(edge.end) == false)
                        {
                            if (minEdge.weight > edge.weight)
                            {
                                minEdge = edge;
                            }
                        }
                    }
                }

                Y.Add(minEdge.end);
                T.Add(minEdge);
            }
            minSpanningTreeWithPrim = T;
        }

        public static List<Edge> FormatKruskalListEdge(List<Edge> edges)
        {
            List<Edge> listEdge = new List<Edge>();

            foreach (Edge edge in edges)
            {
                if (edge.begin < edge.end)
                {
                    listEdge.Add(edge);
                }
                else
                {
                    listEdge.Add(new Edge(edge.end, edge.begin, edge.weight));
                }
            }


            // https://stackoverflow.com/questions/4875737/how-can-i-sort-a-listt-by-multiple-t-attributes
            listEdge.Sort((x, y) =>
            {
                var ret = x.weight.CompareTo(y.weight);
                if (ret == 0) ret = x.begin.CompareTo(y.begin);
                return ret;
            });

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

        public void MinimumSpanningTreeWithKruskal()
        {
            if (!isUndirectedAndConnectedGraph)
            {
                return;
            }

            List<Edge> listEdge = FormatKruskalListEdge(graphAnalysis.listEdges);
            List<Edge> T = new List<Edge>();
            int[] labels = new int[graphAnalysis.totalVertex];

            for (int i = 0; i < labels.Length; ++i)
            {
                labels[i] = i;
            }

            while (T.Count < graphAnalysis.totalVertex - 1)
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

            minSpanningTreeWithKruskal = T;
        }

        public void PrintSpanningTree()
        {
            if (!isUndirectedAndConnectedGraph)
            {
                Console.WriteLine("Do thi da cho KHONG phai la do thi vo huong lien thong");
                return;
            }

            //Prim
            Console.WriteLine("Giai thuat Prim");
            Console.WriteLine("Tap canh cua cay khung: ");

            int sumMinSpanningTreeWithPrimrRs = 0;
            foreach (Edge edge in minSpanningTreeWithPrim)
            {
                Console.WriteLine($"{edge.begin}-{edge.end}:{edge.weight}");
                sumMinSpanningTreeWithPrimrRs = sumMinSpanningTreeWithPrimrRs + edge.weight;
            }
            Console.WriteLine($"Trong so cua cay khung: {sumMinSpanningTreeWithPrimrRs}");



            //Kruskal
            Console.WriteLine("Giai thuat Kruskal");
            Console.WriteLine("Tap canh cua cay khung: ");

            int sumMinSpanningTreeWithKruskalRs = 0;

            foreach (Edge edge in minSpanningTreeWithKruskal)
            {
                Console.WriteLine($"{edge.begin}-{edge.end}:{edge.weight}");
                sumMinSpanningTreeWithKruskalRs = sumMinSpanningTreeWithKruskalRs + edge.weight;
            }
            Console.WriteLine($"Trong so cua cay khung: {sumMinSpanningTreeWithKruskalRs}");
        }
    }
}
