using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_LTDT_2023
{
    class EulerCircle
    {
        //check don dthi: vo huong, khong canh boi, khong canh khuyen
        public GraphAnalysis graphAnalysis;
        public List<GraphPath> eulerCircle;

        public EulerCircle(GraphAnalysis _graphAnalysis)
        {
            graphAnalysis = _graphAnalysis;
            graphAnalysis.AnalyzeGraph();
        }

        public bool CheckSimpleGraph()
        {
            if (graphAnalysis.isUndirectedGraph == true && graphAnalysis.totalEdgeLoop == 0 && graphAnalysis.totalParallelEdges == 0)
            {
                return true;
            }
            return false;
        }
        //check tinh chat euler 
        public int CheckPropertyEuler()
        {
            int countOddDegree = 0;
            // euler: moi dinh deu bac chan 
            if (CheckSimpleGraph() != true)
            {
                Console.WriteLine("Day khong phai don do thi");
                return -2;
            }
            foreach (Vertex vertex in graphAnalysis.vertices)
            {
                if (vertex.degree % 2 == 0)
                {
                    continue;
                }
                countOddDegree = countOddDegree + 1;
            }

            if (countOddDegree == 0)
            {
                Console.WriteLine("Do thi Euler");
                return 1;
            }
            else if (countOddDegree <= 2)
            {

                Console.WriteLine("Do thi nua Euler");
                return 2;
            }
            else
            {
                Console.WriteLine("Do thi khong Euler");
                return -1;
            }
            // nua euler : khong qua 2 dinh bac le 
        }

        public void FindEulerCircle()
        {
            Stack<int> myStack = new Stack<int>();
            List<int> C = new List<int>();
            int startVertexEuler = 0;
            List<Edge> listEdge = new List<Edge>();
            listEdge.AddRange(graphAnalysis.listEdges);
            int checkStatusEuler = CheckPropertyEuler();

            if (checkStatusEuler > 0)
            {
                if (checkStatusEuler == 1)
                {
                    myStack.Push(startVertexEuler);
                }
                else
                {
                    Vertex vertexOdd = graphAnalysis.vertices.Find(x => x.degree % 2 != 0);
                    myStack.Push(vertexOdd.vertex);
                }

                while (myStack.Count != 0)
                {
                    int v = myStack.Peek();
                    List<Edge> listNeighbor = listEdge.FindAll(x => x.begin == v);
                    int temp = Int32.MaxValue;
                    Edge edgedelete = null;
                    foreach (Edge edge in listEdge)
                    {
                        if (edge.begin == v && temp > edge.end)
                        {
                            temp = edge.end;
                            edgedelete = edge;
                        }
                    }
                    if (edgedelete != null)
                    {
                        listEdge.Remove(edgedelete);
                        myStack.Push(edgedelete.end);
                    }
                    else
                    {
                        C.Add(v);
                        myStack.Pop();
                    }
                }

                Console.WriteLine($"{string.Join("->", C.ToArray())}");
            }
        }
    }
}
