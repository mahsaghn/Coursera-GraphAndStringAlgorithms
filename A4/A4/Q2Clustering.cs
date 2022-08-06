using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q2Clustering : Processor
    {
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);


        public double Solve(long pointCount, long[][] points, long clusterCount)
        {
            Node[] mynodes = new Node[pointCount];
            MakeGraph(points, mynodes);
            Heap<Tuple<long, long>> point = MakeHeap(mynodes);
            DisjointSet set = new DisjointSet(pointCount);
            while (pointCount - set.SetCount >= clusterCount)
            {
                Tuple<Tuple<long, long>, double> minWight = point.ExtractMin();
                set.Union(minWight.Item1.Item1, minWight.Item1.Item2);
            }
            return Math.Round(point.ExtractMin().Item2,6);
        }

        private Heap<Tuple<long, long>> MakeHeap(Node[] mynodes)
        {
            Heap<Tuple<long, long>> myheap = new Heap<Tuple<long, long>>();
            for (int i = 0; i < mynodes.Length; i++)
            {
                foreach (var neighbour in mynodes[i].Children)
                    myheap.Insert(new Tuple<Tuple<long, long>, double>(new Tuple<long, long>
                        (i + 1, neighbour.Item1), neighbour.Item2));
            }
            return myheap;
        }

        private void MakeGraph(long[][] points, Node[] mynodes)
        {
            for (int i = 0; i < mynodes.Length; i++)
            {
                mynodes[i] = new Node();
            }
            for (int i = 1; i <= points.Length; i++)
            {
                for (int j = 1; j <= points.Length; j++)
                {
                    if (j != i)
                    {
                        double p = Math.Sqrt(Math.Pow(
                            (points[i - 1][0] - points[j - 1][0]), 2) + Math.Pow((points[i - 1][1] - points[j - 1][1]), 2));
                        mynodes[i - 1].Children.Add(new Tuple<long, double>(j, p));
                    }
                }
            }
        }
    }
}
