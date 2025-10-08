using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Figure f = new Figure();
            f.Area = 36.6;
            Console.WriteLine(f.GetFigure());
        }
    }
}