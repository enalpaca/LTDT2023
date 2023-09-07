﻿using System.Collections.Generic;
using System.Linq;

namespace DOAN_LTDT_2023
{
    class Vertex
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

    class MyOderingStack
    {
        public List<int> myList = new List<int>();
        public int Pop()
        {
            int value = myList.First();
            myList.RemoveAt(0);
            return value;
        }

        public void Push(int item)
        {
            myList.Add(item);
            myList.Sort();
        }

        public bool Contains(int value)
        {
            return myList.Contains(value);
        }

        public int GetLength()
        {
            return myList.Count;
        }
    }
}