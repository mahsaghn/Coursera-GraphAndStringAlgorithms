using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A1
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            node[] nodes = new node[nodeCount];
            MakeDag(edges,nodes);
            List<node> myNodes = nodes.ToList();
            bool linear = true;
            while(linear==true && myNodes.Count!=0)
            {
                linear = false;
                for (int i = 0; i < myNodes.Count; i++)
                    if (myNodes[i].neighbours.Count == 0 
                        || myNodes[i].neighbours.TrueForAll(a=>a.visited==true))
                    {
                        myNodes[i].visited = true;
                        myNodes.RemoveAt(i);
                        linear = true;
                    }
            }
            return linear == false ? 1 : 0;
        }

        private void MakeDag(long[][] edges, node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = new node(i+1);
            for(int i=0;i<edges.Length;i++)
                nodes[edges[i][0]-1].neighbours.Add(nodes[edges[i][1]-1]);
        }
    }
}