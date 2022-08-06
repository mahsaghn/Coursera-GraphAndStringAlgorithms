    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q3ExchangingMoney : Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);


        public string[] Solve(long nodeCount, long[][] edges, long startNode)
        {
            string[] result = new string[nodeCount];
            Node[] myNodes = new Node[nodeCount];
            MakeGraph(myNodes, nodeCount, edges);
            HashSet<long> circle = new HashSet<long>();
            myNodes[startNode - 1].distance = 0;
            for (int i = 0; i < nodeCount; i++)
            {
                Queue<long> checking = new Queue<long>();
                checking.Enqueue(startNode);
                myNodes[startNode - 1].IsChecked = true;
                while (checking.Count != 0)
                {
                    long j = checking.Dequeue();
                    for (int k = 1; k <= myNodes[j - 1].Children.Count; k++)
                    {
                        long childI = myNodes[j - 1].Children[k - 1].Item1;
                        if (!myNodes[childI-1].IsChecked)
                        {
                            myNodes[childI - 1].IsChecked=true;
                            checking.Enqueue(childI);
                        }
                        if (myNodes[childI - 1].distance > myNodes[j - 1].distance + myNodes[j - 1].Children[k - 1].Item2)
                        {
                            if (i == nodeCount - 1 && !circle.Contains(childI))
                            {
                                circle.Add(childI);
                                CompleteCircleForward(myNodes, circle, childI);
                            }
                            myNodes[childI - 1].distance = myNodes[j - 1].distance + myNodes[j - 1].Children[k - 1].Item2;
                        }
                    }
                }
                for (int h = 0; h < nodeCount; h++) myNodes[h].IsChecked = false;
            }
            FindCircle(ref result,myNodes, circle, startNode);
            return result;
        }

        private void FindCircle(ref string[] result,Node[] myNodes, HashSet<long> inCircle,long startNode)
        {
            Bfs(startNode, myNodes, inCircle, ref result);
            for (int i = 0; i < myNodes.Length; i++)
            {
                if (myNodes[i].IsChecked==false)
                    result[i] = "*";
            }
            if (result[startNode - 1] != "-")
                result[startNode - 1] = "0";
        }

        private void Bfs(long startNode, Node[] myNodes, HashSet<long> inCircle,ref string[] result)
        {
            myNodes[startNode - 1].IsChecked = true;
            if (myNodes[startNode - 1].Children.Count != 0)
                foreach (var child in myNodes[startNode-1].Children)
                {
                    if (!myNodes[child.Item1 - 1].IsChecked)
                    {
                        result[child.Item1 - 1] =myNodes[child.Item1 - 1].distance.ToString();
                        Bfs(child.Item1, myNodes, inCircle, ref result);
                    }
                    else if (myNodes[child.Item1 - 1].IsChecked && result[child.Item1 - 1] != "-" && inCircle.Contains(child.Item1))
                    {
                        result[child.Item1 - 1] = "-";
                        Bfs(child.Item1, myNodes, inCircle, ref result);
                    }
                }
        }

        private void CompleteCircleForward(Node[] myNodes, HashSet<long> inCircle,long node)
        {
            if(myNodes[node - 1].Children.Count!=0)
                foreach (var child in myNodes[node - 1].Children)
                if (!inCircle.Contains(child.Item1))
                {
                    inCircle.Add(child.Item1);
                    CompleteCircleForward(myNodes,inCircle,child.Item1);
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
}
