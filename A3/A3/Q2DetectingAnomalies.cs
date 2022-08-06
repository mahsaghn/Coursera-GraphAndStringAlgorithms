using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long nodeCount, long[][] edges)
        {
            Node[] myNodes = new Node[nodeCount];
            Q1MinCost.MakeGraph(myNodes, nodeCount, edges);
            for (int i = 0; i < nodeCount; i++) myNodes[i].distance = 0;
            for (int i=0;i<nodeCount;i++)
            {
                for (int j = 0; j < edges.Length; j++)
                    if (myNodes[edges[j][1] - 1].distance > edges[j][2] + myNodes[edges[j][0] - 1].distance)
                    {
                        if (i == nodeCount - 1)
                            return 1;
                        myNodes[edges[j][1] - 1].distance = edges[j][2] + myNodes[edges[j][0] - 1].distance;
                    }
            }
            return 0;
        }

        private bool RelaxNode(long index, Node[] myNodes, ref Queue<long> minHeap,ref List<long> nodes)
        {
            bool updateAgain = false;
            if(myNodes[index-1].Children.Count!=0)
                for (int i = 0; i < myNodes[index - 1].Children.Count; i++)//relaxing children
                {
                    long childIndex = myNodes[index - 1].Children[i].Item1;
                    nodes.Remove(childIndex);
                    if (myNodes[childIndex - 1].distance > myNodes[index - 1].distance + myNodes[index - 1].Children[i].Item2)
                    {
                        updateAgain = true;
                        myNodes[childIndex - 1].distance = myNodes[index - 1].distance + myNodes[index - 1].Children[i].Item2;
                    }
                    if (myNodes[childIndex - 1].IsChecked == false)
                    {
                        minHeap.Enqueue(childIndex);
                        myNodes[childIndex - 1].IsChecked = true;
                    }
                }
            return updateAgain;
        }
    }
}
