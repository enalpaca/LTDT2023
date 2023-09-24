using System.Collections.Generic;
using System.Linq;

namespace DOAN_LTDT_2023
{
    public class Vertex
    {
        public int vertex = -1;
        public int degree = 0;
        public int inDegree = 0;
        public int outDegree = 0;
        public int loopEdge = 0;
        public int label = 0;
        public bool isPendantVertex = false;

        public Vertex() { }
        public Vertex(int _vertex, int _degree, int _inDegree, int _outDegree)
        {
            vertex = _vertex;
            degree = _degree;
            inDegree = _inDegree;
            outDegree = _outDegree;
        }
    }

    public class Edge
    {
        public int begin;// begin Edge 
        public int end;// end Edge 
        public int weight; // weighted graph of begin - end edge
        public Edge(int _begin, int _end, int _weight)
        {
            begin = _begin;
            end = _end;
            weight = _weight;
        }
    }

    class GraphPath
    {
        public int start;
        public int end;
        public int weight;
        public bool negativeCircle = false;
        public List<Edge> paths;
        public List<int> visitedVertices = new List<int>();
        public GraphPath(int _start, int _end, int _weight, List<Edge> _paths)
        {
            start = _start;
            end = _end;
            weight = _weight;
            paths = _paths;
        }
    }
}
