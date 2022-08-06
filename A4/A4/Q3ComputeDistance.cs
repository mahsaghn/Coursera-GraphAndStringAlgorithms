using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long, long[][], long[][], long, long[][], long[]>)Solve);


        public long[] Solve(long nodeCount, 
                            long edgeCount,
                            long[][] points,
                            long[][] edges,
                            long queriesCount,
                            long[][] queries)
        {
            Node[] users = new Node[nodeCount];
            Node[] Rusers = new Node[nodeCount];
            MakeGraph(edges, users,Rusers);
            long[] result = new long[queriesCount];
            for (int i = 0; i < queriesCount; i++)
            {
                long startUser = queries[i][0];
                long destUser = queries[i][1];
                if (i == 13 && edgeCount == 27)
                    ;
                result[i] = BiDirectionalDijstra(startUser, destUser, users, Rusers,points);
            }
            return result;
        }

        private long BiDirectionalDijstra(long startUser, long destUser, Node[] users, Node[] Rusers,long[][] points)
        {
            if (destUser == startUser)
                return 0;
            ComputeValue(users, Rusers, startUser, destUser,points);
            Heap<long> forward = new Heap<long>();
            Heap<long> backward = new Heap<long>();
            backward.Insert(new Tuple<long, double>(destUser,0));
            forward.Insert(new Tuple<long, double>(startUser,0));
            while (forward.Nodes.Count != 0 && backward.Nodes.Count != 0)
            {
                long result = ForwardStep(forward, users, Forward.f);
                if (Rusers[result - 1].GraphSearched == Forward.b)
                    return SecondFaze(users, Rusers, result,startUser,destUser);
                result = ForwardStep(backward, Rusers, Forward.b);
                if (users[result - 1].GraphSearched == Forward.f)
                    return SecondFaze(users, Rusers, result, startUser, destUser);
            }
            return -1;
        }

        private void ComputeValue(Node[] users, Node[] Rusers, long startUser, long destUser,long[][] points)
        {
            for (int i = 0; i < users.Length; i++)
            {
                Rusers[i].GraphSearched = Forward.n;
                users[i].GraphSearched = Forward.n;
                Rusers[i].IsChecked = false;
                users[i].IsChecked = false;
                users[i].distance = double.MaxValue;
                Rusers[i].distance = double.MaxValue;
                long dx = points[i][0] - points[destUser - 1][0]
                    ,dy = points[i][1] - points[destUser - 1][1];

                users[i].dekarti = Math.Sqrt(dx * dx + dy * dy);
                dx = points[i][0] - points[startUser - 1][0];
                dy = points[i][1] - points[startUser - 1][1];

                Rusers[i].dekarti = Math.Sqrt(dx * dx + dy * dy);
            }
            users[startUser - 1].distance = 0;
            Rusers[destUser - 1].distance = 0;
        }

        private long SecondFaze(Node[] users, Node[] Rusers, long result,long startUser,long destUser)//should be change!
        {
            if (startUser == 3 && destUser == 5)
                ;
            double shortest = users[result - 1].distance + Rusers[result - 1].distance;
            for (int i = 0; i < users.Length; i++)
                if (users[i].GraphSearched == Forward.f)
                    foreach (var child in users[i].Children)
                        if (Rusers[child.Item1 - 1].GraphSearched == Forward.b)
                        {
                            double weith = users[i].distance
                                + Rusers[child.Item1 - 1].distance
                                + child.Item2;
                            if (shortest > weith)
                                shortest = weith;
                        }
            return (long)Math.Round(shortest);
        }

        private long ForwardStep(Heap<long> forward, Node[] users, Forward type)
        {
            Tuple<long, double> user = forward.ExtractMin();
            users[user.Item1 - 1].IsChecked = true;
            users[user.Item1 - 1].GraphSearched = type;
            for (int i = 1; i <= users[user.Item1 - 1].Children.Count; i++)
            {
                long childI = users[user.Item1 - 1].Children[i - 1].Item1;
                double weight = user.Item2 + users[user.Item1 - 1].Children[i - 1].Item2;
                if (users[childI - 1].distance > weight)
                    users[childI - 1].distance = weight;
                if (users[childI - 1].IsChecked == false)
                    forward.Insert(new Tuple<long, double>(childI, weight));
            }
            return user.Item1;
        }

        private void MakeGraph(long[][] edges , Node[] users,Node[] Rusers)
        {
            for (int i = 1; i <= users.Length; i++)
            {
                users[i - 1] = new Node(0);
                Rusers[i - 1] = new Node(0);
            }
            foreach (var edge in edges)
            {
                users[edge[0] - 1].Children.Add(new Tuple<long, double>
                    (edge[1],edge[2]));
                Rusers[edge[1] - 1].Children.Add(new Tuple<long, double>
                    (edge[0], edge[2]));
            }
        }
    }
}
