namespace BinPacking
{
    class BinPacking
    {
        readonly string name;
        readonly int capacity;
        readonly int count;
        readonly int bestie;
        readonly int[] weight;

        public BinPacking(string name, int capacity, int count, int bestie, int[] weight)
        {
            this.name = name;
            this.capacity = capacity;
            this.count = count;
            this.bestie = bestie;
            this.weight = weight;
        }

        void FirstPopulation()
        {

        }

        void GeneticAlgorithm()
        {
            FirstPopulation();
        }

        public int FirstFit()
        {
            int[] sw = weight;
            Array.Sort(sw);
            Array.Reverse(sw);

            List<int> packs = new List<int>();
            packs.Add(capacity);

            for (int i = 0; i < count; i++)
            {
                int ff = -1;
                for (int j = 0; j < packs.Count; ++j)
                {
                    if (packs[j] >= sw[i])
                    {
                        ff = j;
                        break;
                    }
                }
                if (ff != -1)
                    packs[ff] -= sw[i];
                else
                    packs.Add(capacity - sw[i]);

            }
            return packs.Count;
        }

        public int BestFit()
        {
            int[] sw = weight;
            Array.Sort(sw);
            Array.Reverse(sw);

            List<int> packs = new List<int>();
            packs.Add(capacity);

            for (int i = 0; i < count; i++)
            {
                int min = capacity;
                int bf = -1;
                for (int j = 0; j < packs.Count; ++j)
                {
                    int sub = packs[j] - sw[i];
                    if (sub < min && sub >= 0)
                    {
                        min = sub;
                        bf = j;
                    }
                }
                if (bf != -1)
                    packs[bf] -= sw[i];
                else
                    packs.Add(capacity - sw[i]);
            }
            return packs.Count;
        }

        public string Name { get { return name; } }
        public int Capacity { get { return capacity; } }
        public int Count { get { return count; } }
        public int Bestie { get { return bestie; } }
        public int[] Weight { get { return weight; } }
    }
}
