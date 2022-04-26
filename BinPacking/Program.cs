﻿namespace BinPacking
{
    class Program
    {
        static void Main()
        {
            ParserTXT p = new ParserTXT();

            for (int i = 1; i <= 4; ++i)
            {
                BinPacking[] bp = p.binpackParser($"binpack{i}.txt");

                Console.WriteLine($"START FILE binpack{i}.txt\n");
                for (int j = 0; j < bp.Length; ++j)
                {
                    Console.WriteLine($"START TEST {bp[j].Name}");
                    Console.WriteLine("FF = " + bp[j].FirstFit());
                    Console.WriteLine("BF = " + bp[j].BestFit());
                }
                Console.WriteLine("END OF CURRENT FILE\n");
            }
        }
    }
}