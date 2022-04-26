namespace BinPacking
{
    class ParserTXT
    {
        string dir;

        public ParserTXT()
        {
            dir = "";
        }

        public BinPacking[] binpackParser(string filename)
        {
            string path = dir + filename;

            StreamReader reader = new StreamReader(path);
            int count = 0;

            try
            {
                count = int.Parse(reader.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            BinPacking[] tests = new BinPacking[count];

            for (int k = 0; k < count; k++)
            {
                try
                {
                    string name = reader.ReadLine().Remove(0, 1);
                    int[] values = Array.ConvertAll(reader.ReadLine().Remove(0, 1).Split(' '), int.Parse);
                    int[] weight = new int[values[1]];

                    for (int i = 0; i < values[1]; ++i)
                        weight[i] = int.Parse(reader.ReadLine());

                    tests[k] = new BinPacking(name, values[0], values[1], values[2], weight);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return tests;
        }

    }
}
