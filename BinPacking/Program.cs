namespace BinPacking
{
    class Program
    {
        static void Main()
        {
            ParserTXT p = new ParserTXT();
            ExcelExport exp = new ExcelExport();

            //TestFit(p);
            exp.Save(TestGenetic(p), "laba2");
        }

        static Dictionary<string, int[]> TestGenetic(ParserTXT p)
        {
            BinPacking[] bp = p.binpackParser($"binpack{2}.txt");
            Dictionary<string, int[]> ex = new Dictionary<string, int[]>();

            int[] best = new int[bp.Length];
            int[] myval = new int[bp.Length];
            for (int j = 0; j < bp.Length; ++j)
            {
                //Console.WriteLine($"START TEST {bp[j].Name}");
                int result = bp[j].GeneticAlgorithm(10000);
                //Console.WriteLine(result);

                best[j] = bp[j].Bestie;
                myval[j] = result;
            }

            ex.Add("bestie", best);
            ex.Add("result", myval);

            return ex;
        }

        static void TestFit(ParserTXT p)
        {
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
