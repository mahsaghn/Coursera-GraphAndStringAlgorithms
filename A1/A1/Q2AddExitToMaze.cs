using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            int count = 0;
            node[] nodes = new node[nodeCount];
            MakeGraph(nodeCount, edges,nodes);
            for(int i=0;i<nodeCount;i++)
            {
                if (nodes[i].visited == false)
                {
                    count++;
                    Find(nodes, i+1);
                }
            }
            return count;

        }

        private void MakeGraph(long nodeCount, long[][] edges,node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = new node(i+1);
            for(int i=0;i<edges.Length;i++)
            {
                nodes[edges[i][0]-1].neighbours.Add(nodes[edges[i][1]-1]);
                nodes[edges[i][1]-1].neighbours.Add(nodes[edges[i][0]-1]);
            }
        }

        private void Find(node[] nodes,long checkingNode)
        {
            node during = nodes[checkingNode-1];
            Stack<node> myStack = new Stack<node>();
            myStack.Push(during);
            while (myStack.Count!=0)
            {
                during = myStack.Pop();
                during.visited = true;
                for (int i=0;i<during.neighbours.Count;i++)
                {
                    if(during.neighbours[i].visited==false)
                    {   
                        myStack.Push(during.neighbours[i]);
                    }
                }
            }
        }
    }

    public class node
    {
        public long value;
        public List<node> neighbours;
        public bool visited;
        public node(long value)
        {
            this.value = value;
            visited = false;
            neighbours = new List<node>();
        }
    }

}
