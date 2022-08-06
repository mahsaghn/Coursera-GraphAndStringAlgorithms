using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            Node[] myNodes = new Node[nodeCount];
            MakeGraph(myNodes, nodeCount, edges);
            myNodes[startNode - 1].distance = 0;
            myNodes[startNode - 1].IsChecked = true;
            long checkedNode = 1;
            Heap minHeap = new Heap();
            RelaxNode(startNode, myNodes, ref minHeap);
            while (checkedNode <= nodeCount && minHeap.Nodes.Count != 0)
            {
                Tuple<long, long> executing = minHeap.ExtractMin();
                if (myNodes[executing.Item1 - 1].IsChecked == false)
                {
                    checkedNode++;
                    myNodes[executing.Item1 - 1].IsChecked = true;
                    if (executing.Item1 == endNode)
                        return myNodes[endNode - 1].distance;
                    RelaxNode(executing.Item1, myNodes, ref minHeap);
                }
            }
            return -1;
        }

        public static void RelaxNode(long index, Node[] myNodes, ref Heap minHeap)
        {
            for (int i = 0; i < myNodes[index - 1].Children.Count; i++)//relaxing children
            {
                long childIndex = myNodes[index - 1].Children[i].Item1;
                if (myNodes[childIndex - 1].IsChecked == false)
                {
                    if (myNodes[childIndex - 1].distance > myNodes[index - 1].distance + myNodes[index - 1].Children[i].Item2)
                        myNodes[childIndex - 1].distance = myNodes[index - 1].distance + myNodes[index - 1].Children[i].Item2;
                    minHeap.Insert(new Tuple<long, long>(childIndex, myNodes[childIndex - 1].distance));
                }
            }
        }

        public static void MakeGraph(Node[] Nodes, long nodeCount, long[][] edges)
        {
            for (int i = 1; i <= nodeCount; i++)
            {
                Nodes[i - 1] = new Node(i);
                Nodes[i - 1].distance = long.MaxValue;
            }
            foreach (var edge in edges)
            {
                Nodes[edge[0] - 1].Children.Add(new Tuple<long, long>(edge[1], edge[2]));
            }
        }
    }
    public enum Forward{f,b,n }

    public class Node
    {
        public static long CheckedNumber=0;
        public bool IsChecked;
        public long distance;
        public List<Tuple<long,long>> Children;
        public List<Tuple<long,long>> Parent;
        public Forward GraphSearched;

        public Node(long value)
        {
            this.IsChecked = false;
            this.distance = long.MaxValue;
            this.Children = new List<Tuple<long,long>>();
            this.Parent = new List<Tuple<long, long>>();
            this.GraphSearched = Forward.n;
        }

    }
}
