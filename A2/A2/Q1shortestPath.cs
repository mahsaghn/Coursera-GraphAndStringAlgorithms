using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        

        public long Solve(long NodeCount, long[][] edges, long StartNode,  long EndNode)
        {
            Node[] myNodes = new Node[NodeCount];
            MakeGraph(myNodes,edges);
            Queue<Tuple<long,int>> bfs = new Queue<Tuple<long, int>>();
            bfs.Enqueue(new Tuple<long, int>(StartNode,0));
            myNodes[StartNode-1].IsChecked = true;
            while (bfs.Count!=0)
            {
                Tuple<long, int> checking = bfs.Dequeue();
                foreach (var neighbour in myNodes[checking.Item1-1].Neighbours)
                {
                    if (neighbour == EndNode)
                        return checking.Item2 + 1;
                    if (!myNodes[neighbour-1].IsChecked)
                    {
                        bfs.Enqueue(new Tuple<long, int>(neighbour, checking.Item2 + 1));
                        myNodes[neighbour-1].IsChecked = true;
                    }

                }
            }
            return -1;
        }

        public static void MakeGraph(Node[] myNodes,long[][] edges)
        {
            for (int i = 0; i < myNodes.Length; i++)
                myNodes[i] = new Node(i);
            foreach (var edge in edges)
            {
                myNodes[edge[0]-1].Neighbours.Add(edge[1]);
                myNodes[edge[1]-1].Neighbours.Add(edge[0]);
            }
        }
    }

    public class Node
    {
        public long NodeName;
        public bool IsChecked;
        public bool sticker;
        public List<long> Neighbours;
        public Node(long name)
        {
            this.NodeName = name;
            Neighbours = new List<long>();
            this.IsChecked = false;
        }
    }

}
