using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A1
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            node[] nodes = new node[nodeCount];
            node[] Reversenodes = new node[nodeCount];
            MakeDag(edges, nodes); 
            MakeDagReverse(edges, Reversenodes);
            List<node> myNodes = nodes.ToList();
            List<node> myNodesReversed = Reversenodes.ToList();
            long count=0;
            bool linear = true;
            while (linear == true && myNodes.Count != 0)
            {
                linear = false;
                for (int i = 0; i < myNodes.Count; i++)
                    if (myNodes[i].neighbours.Count == 0
                        || myNodes[i].neighbours.TrueForAll(a => a.visited == true)
                        ||myNodesReversed[i].neighbours.Count == 0
                        || myNodesReversed[i].neighbours.TrueForAll(b => b.visited == true))
                    {
                        myNodes[i].visited = true;
                        myNodesReversed[i].visited = true;
                        count++;
                        myNodes.RemoveAt(i);
                        myNodesReversed.RemoveAt(i);
                        linear = true;
                    }
            }
            while (!myNodes.TrueForAll(a => a.visited == true))
            {
                for (int i = 0; i < myNodes.Count; i++)
                {
                    if (myNodes[i].visited == false)
                    {
                        DFS(myNodes, i);
                        count++;
                    }
                }
            }
            return count;
        }

        private void DFS(List<node> myNodes, int du)
        {
            node during = myNodes[du];
            Stack<node> myStack = new Stack<node>();
            myStack.Push(during);
            while (myStack.Count != 0)
            {
                during = myStack.Pop();
                during.visited = true;
                for (int i = 0; i < during.neighbours.Count; i++)
                {
                    if (during.neighbours[i].visited == false)
                    {
                        myStack.Push(during.neighbours[i]);
                    }
                }
            }
        }

        private void MakeDag(long[][] edges, node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = new node(i + 1);
            for (int i = 0; i < edges.Length; i++)
                nodes[edges[i][0] - 1].neighbours.Add(nodes[edges[i][1] - 1]);
        }

        private void MakeDagReverse(long[][] edges, node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = new node(i + 1);
            for (int i = 0; i < edges.Length; i++)
                nodes[edges[i][1] - 1].neighbours.Add(nodes[edges[i][0] - 1]);
        }
    }
}
