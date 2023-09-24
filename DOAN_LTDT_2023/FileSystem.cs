using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DOAN_LTDT_2023
{
    class FileSystem
    {
        public static int[,] ReadAdjacencyMatrixFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            int totalVertex = Convert.ToInt32(lines[0]);
            int[,] matrix = new int[totalVertex, totalVertex];

            for (int i = 0; i < totalVertex; i++)
            {
                for (int j = 0; j < totalVertex; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            for (int i = 0; i < totalVertex; i++)
            {
                string linedata = lines[i + 1];
                string[] tokens = linedata.Split(" ");
                // function convert array string to array int https://stackoverflow.com/questions/1297231/convert-string-to-int-in-one-line-of-code-using-linq
                int[] numberTokens = Array.ConvertAll(tokens, int.Parse);
                Queue<int> tokenQueue = new Queue<int>(numberTokens);
                int numberOfAdjacencyVertexWithV = tokenQueue.Dequeue();

                for (int adjacencyVertexIndex = 0; adjacencyVertexIndex < numberOfAdjacencyVertexWithV; adjacencyVertexIndex++)
                {
                    int currentRow = i;
                    int currentCol = tokenQueue.Dequeue();
                    tokenQueue.Dequeue();
                    matrix[currentRow, currentCol] = matrix[currentRow, currentCol] + 1;
                }

            }
            return matrix;
        }

        public static List<Edge> ReadEdgesFromFile(string filePath)
        {
            List<Edge> listEdges = new List<Edge>();
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            int totalVertex = Convert.ToInt32(lines[0]);

            for (int v = 0; v < totalVertex; v++)
            {
                string linedata = lines[v + 1];
                string[] tokens = linedata.Split(" ");
                // function convert array string to array int https://stackoverflow.com/questions/1297231/convert-string-to-int-in-one-line-of-code-using-linq
                int[] numberTokens = Array.ConvertAll(tokens, int.Parse);
                Queue<int> tokenQueue = new Queue<int>(numberTokens);
                int numberOfAdjacencyVertexWithV = tokenQueue.Dequeue();

                for (int adjacencyVertexIndex = 0; adjacencyVertexIndex < numberOfAdjacencyVertexWithV; adjacencyVertexIndex++)
                {
                    int end = tokenQueue.Dequeue();
                    Edge edge = new Edge(v, end, tokenQueue.Dequeue());
                    listEdges.Add(edge);
                }
            }

            return listEdges;
        }

        public static bool CheckExistingFile(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
