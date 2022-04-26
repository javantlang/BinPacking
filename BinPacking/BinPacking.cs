using Accord.Math;

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

        List<int[]> FirstPopulation(int k)
        {
            List<int[]> population = new List<int[]>();

            for (int i = 0; i < k; i++)
            {
                int[] S = Enumerable.Range(0, count).ToArray();
                S.Shuffle();
                population.Add(S);
            }
            return population;
        }

        int GetF(int[] S)
        {
            int pcount = 0;
            int pcap = 0;
            foreach (var i in S)
            {
                int sub = pcap - weight[i];
                if (sub < 0)
                {
                    pcount++;
                    pcap = capacity - weight[i];
                }
                else
                    pcap = sub;
            }

            return pcount;
        }

        (int, int) Record(List<int[]> P)
        {
            int F = count;
            (int pos, int current) = (0,0);

            foreach (var S in P)
            {
                int pcount = GetF(S);

                if (pcount < F)
                {
                    F = pcount;
                    pos = current;
                }
                current++;
            }

            return (F, pos);
        }

        (int[], int[]) Options(List<int[]> P, int BestPos)
        {
            int[] S1 = P[BestPos];

            Random r = new Random();
            int RandPos;
            while ((RandPos = r.Next(0, P.Count)) == BestPos) {; }
            int[] S2 = P[RandPos];

            return (S1, S2);
        }

        int[] Crossing(int[] S1, int[] S2)
        {
            Random r = new Random();
            int l = r.Next(0, count);

            int[] newS = S1.Get(0, l).Concat(S2).ToArray().Distinct();
            return newS;
        }

        int[] Mutation(int[] S)
        {
            double q = 1 / (double)count;
            int[] newS = S.Copy();

            Random r = new Random();
            for (int i = 0; i < count; ++i)
            {
                if (r.NextDouble() < q)
                {
                    int next = r.Next(0, count);
                    while (next == i)
                    {
                        next = r.Next(0, count);
                    }

                    newS.Swap(i, next);
                }
            }

            return newS;
        }

        int[] LocalImprove(int[] S)
        {
            int[] newS = S.Copy();

            return newS;
        }

        void RemoveAntiRecord(List<int[]> P)
        {
            int F = 0;
            (int pos, int current) = (0, 0);

            foreach (var S in P)
            {
                int pcount = GetF(S);

                if (pcount > F)
                {
                    F = pcount;
                    pos = current;
                }
                current++;
            }

            P.RemoveAt(pos);
        }

        public int GeneticAlgorithm(int n)
        {
            List<int[]> P = FirstPopulation(5);
            (int F, int BestPos) = Record(P);
            Console.WriteLine(bestie);
            Console.WriteLine(F);

            while (n-- > 0)
            {
                (int[] S1, int[] S2) = Options(P, BestPos);
                int[] dS = Crossing(S1, S2);
                int[] ddS = Mutation(dS);
                int[] dddS = LocalImprove(ddS);

                int newF = GetF(dddS);
                if (newF < F)
                    F = newF;

                P.Add(dddS);
                RemoveAntiRecord(P);

                (_, BestPos) = Record(P);
            }

            return F;
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
