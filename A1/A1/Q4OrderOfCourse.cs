using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestCommon;

namespace A1
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long[][] edges)
        {
            node[] nodes = new node[nodeCount];
            MakeDag(edges, nodes);
            List<node> myNodes = nodes.ToList();
            List<long> linearList = new List<long>();
            bool linear = true;
            while (linear == true && myNodes.Count != 0)
            {
                linear = false;
                for (int i = 0; i < myNodes.Count; i++)
                    if (myNodes[i].neighbours.Count == 0
                        || myNodes[i].neighbours.TrueForAll(a => a.visited == true))
                    {
                        myNodes[i].visited = true;
                        linearList.Add(myNodes[i].value);
                        myNodes.RemoveAt(i);
                        linear = true;
                    }
            }
            linearList.Reverse();
            return linearList.ToArray();
        }


        private void MakeDag(long[][] edges, node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = new node(i + 1);
            for (int i = 0; i < edges.Length; i++)
                nodes[edges[i][0] - 1].neighbours.Add(nodes[edges[i][1] - 1]);
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        /// <summary>
        /// کد شما با متد زیر راست آزمایی میشود
        /// این کد نباید تغییر کند
        /// داده آزمایشی فقط یک جواب درست است
        /// تنها جواب درست نیست
        /// </summary>
        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
