using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DoAnLTDT2023
{
    class Program
    {
        void Readdata(string text)
        {
            string content = File.ReadAllText(text, Encoding.UTF8);
        }
        static void Main(string[] args)
        {
            var path = "datademo.txt";
            string[] contents = File.ReadAllLines(path, Encoding.UTF8);
            foreach (string content in contents)
            {
                Console.WriteLine(content);
            }
        }
    }
}
