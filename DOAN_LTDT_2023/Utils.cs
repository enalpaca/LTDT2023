using System;

namespace DOAN_LTDT_2023
{
    class Utils
    {
        public static bool CheckIsValidSource(int source, int totalVertex)
        {
            if (source < 0 || source >= totalVertex)
            {
                return false;
            }
            return true;
        }
        public static int ReadVertexSource(int totalVertex)
        {
            int sourceVertex = -1;

            do
            {
                Console.WriteLine($"Vui long nhap dinh bat dau hop le tu {0} - {totalVertex-1}:");
                sourceVertex = Convert.ToInt32(Console.ReadLine());
            }
            while (Utils.CheckIsValidSource(sourceVertex, totalVertex) == false);
            return sourceVertex;
        }
    }
}
