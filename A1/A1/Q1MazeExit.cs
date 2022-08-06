using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            long[] visited = new long[nodeCount];
            FindExit(edges, visited,StartNode,EndNode);
            return visited[EndNode - 1];
        }

        private void FindExit(long[][] edges, long[] visited,long duringNode,long EndNode)
        {
            for(int i=0;i<edges.Length;i++)
            {
                if (duringNode == edges[i][0])
                {
                    if(visited[edges[i][1]-1] == 0)
                    {
                        visited[edges[i][1]-1] = 1;
                        if (edges[i][1] == EndNode)
                            return;
                        FindExit(edges, visited, edges[i][1], EndNode);
                    }
                }
                else if (duringNode == edges[i][1])
                {
                    if (visited[edges[i][0] - 1] == 0)
                    {
                        visited[edges[i][0] - 1] = 1;
                        if (edges[i][0] == EndNode)
                            return;
                        FindExit(edges, visited, edges[i][0], EndNode);
                    }
                }

            }
            return;
        }
    }
}
