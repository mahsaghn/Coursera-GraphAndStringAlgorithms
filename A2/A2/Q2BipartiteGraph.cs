using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long NodeCount, long[][] edges)
        {
            Node[] myNodes = new Node[NodeCount];
            Q1ShortestPath.MakeGraph(myNodes, edges);
            Queue<long> bfs = new Queue<long>();
            bfs.Enqueue(1);
            myNodes[0].IsChecked = true;
            myNodes[0].sticker = true;
            while (bfs.Count!=0)
            {
                long checking = bfs.Dequeue();
                foreach (var neighbour in myNodes[checking - 1].Neighbours)
                {
                    if (!myNodes[neighbour - 1].IsChecked)
                    {
                        bfs.Enqueue(neighbour);
                        myNodes[neighbour - 1].IsChecked = true;
                        myNodes[neighbour - 1].sticker = !myNodes[checking-1].sticker;
                    }
                    else if (myNodes[checking - 1].sticker == myNodes[neighbour - 1].sticker)
                        return 0;

                }
            }
            return 1;
        }
    }

}
