using System;
using System.Collections.Generic;

namespace DOAN_LTDT_2023
{
    class EulerCircle
    {
        //check don dthi: vo huong, khong canh boi, khong canh khuyen
        public GraphAnalysis graphAnalysis;

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

            // Euler: Mọi đỉnh đều có bậc chẳn
            if (countOddDegree == 0)
            {
                Console.WriteLine("Do thi Euler");
                return 1;
            }
            // Nữa Euler: Có đúng 2 đỉnh bậc lẻ
            else if (countOddDegree == 2)
            {
                Console.WriteLine("Do thi nua Euler");
                return 2;
            }
            else
            {
                Console.WriteLine("Do thi khong Euler");
                return -1;
            }
        }

        public void FindEulerCircle(int sourceEuler)
        {
            Stack<int> myStack = new Stack<int>();
            List<int> C = new List<int>();
            List<Edge> listEdge = new List<Edge>();
            listEdge.AddRange(graphAnalysis.listEdges);
            int checkStatusEuler = CheckPropertyEuler();

            myStack.Push(sourceEuler);

            // Nếu nữa Euler thì source phải là 1 trong 2 đỉnh bậc lẻ
            if (checkStatusEuler == 2)
            {
                Vertex vertexOdd = graphAnalysis.vertices.Find(x => x.vertex == sourceEuler);
                if (vertexOdd == null || vertexOdd.degree % 2 == 0)
                {
                    // gán checkStatusEuler <0 để không gọi hàm tìm đường đi Euler bên dưới
                    checkStatusEuler = -3;
                }
            }

            if (checkStatusEuler > 0)
            {
                while (myStack.Count != 0)
                {
                    int v = myStack.Peek();
                    List<Edge> listNeighbor = listEdge.FindAll(x => x.begin == v);
                    int temp = Int32.MaxValue;
                    Edge deletedEdge = null;

                    foreach (Edge edge in listEdge)
                    {
                        if (edge.begin == v && temp > edge.end)
                        {
                            temp = edge.end;
                            deletedEdge = edge;
                        }
                    }

                    if (deletedEdge != null)
                    {
                        // Đây là đồ thị vô hướng liên thông nên xóa cạnh v-k thì củng xóa cạnh k-v
                        listEdge.Remove(deletedEdge);
                        deletedEdge = listEdge.Find(x => x.begin == deletedEdge.end && x.end == deletedEdge.begin);
                        if (deletedEdge != null)
                        {
                            listEdge.Remove(deletedEdge);
                        }
                        myStack.Push(temp);
                    }
                    else
                    {
                        C.Add(v);
                        myStack.Pop();
                    }
                }
            }

            Console.WriteLine($"Source: {sourceEuler}");
            if (C.Count == 0)
            {
                Console.WriteLine($"Khong co loi giai");
            }
            else
            {
                Console.WriteLine($"Duong di Euler:");
                C.Reverse();
                Console.WriteLine($"{string.Join("->", C.ToArray())}");
            }
        }
    }
}
