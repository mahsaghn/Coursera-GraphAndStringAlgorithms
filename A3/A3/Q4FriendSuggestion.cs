using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);

        public long[] Solve(long NodeCount, long EdgeCount, 
                              long[][] edges, long QueriesCount, 
                              long[][]Queries)
        {
            Node[] users = new Node[NodeCount];
            MakeGraph(edges,users);
            Node[] Rusers = new Node[NodeCount];
            MageReverseGraph(edges,Rusers);
            long[] result = new long[QueriesCount];
            if (NodeCount == 6)
                ;
            for(int i=0;i<QueriesCount;i++)
            {
                long startUser = Queries[i][0];
                long destUser = Queries[i][1];
                result[i] = BiDirectionalDijstra(startUser, destUser, users,Rusers);
            }
            return result;
        }

        private long BiDirectionalDijstra(long startUser, long destUser, Node[] users, Node[] Rusers)
        {
            if (destUser == startUser)
                return 0;
            for (int i = 0; i < users.Length; i++)
            {
                Rusers[i].GraphSearched = Forward.n;
                users[i].GraphSearched = Forward.n;
                Rusers[i].IsChecked = false;
                users[i].IsChecked = false;
                users[i].distance = long.MaxValue;
                Rusers[i].distance = long.MaxValue;
            }
            Heap forward =new Heap();
            Heap backward =new Heap ();
            users[startUser - 1].distance = 0;
            Rusers[destUser - 1].distance = 0;
            backward.Insert(new Tuple<long, long>(destUser,0));
            forward.Insert(new Tuple<long, long>(startUser,0));
            while (forward.Nodes.Count!=0 && backward.Nodes.Count!=0)
            {
                long result = ForwardStep(forward, users,Forward.f);
                if (Rusers[result - 1].GraphSearched==Forward.b)
                    return SecondFaze(users,Rusers,result);
                result = ForwardStep(backward, Rusers,Forward.b);
                if (users[result - 1].GraphSearched == Forward.f)
                    return SecondFaze(users, Rusers, result);
            }
            return - 1;
        }

        private long SecondFaze(Node[] users, Node[] Rusers, long result)
        {
            long shortest=users[result-1].distance+Rusers[result-1].distance;
            for(int i=0;i<users.Length;i++)
                if(users[i].GraphSearched==Forward.f)
                    foreach(var child in users[i].Children)
                        if(Rusers[child.Item1-1].GraphSearched== Forward.b)
                        {
                            long weith = users[i].distance
                                + Rusers[child.Item1 - 1].distance
                                + child.Item2;
                            if (shortest> weith)
                                shortest = weith;
                        }
            return shortest;
        }

        private long ForwardStep(Heap forward, Node[] users, Forward type)
        {
            Tuple<long, long> user = forward.ExtractMin();
            users[user.Item1 - 1].IsChecked = true;
            users[user.Item1 - 1].GraphSearched = type;
            for (int i = 1; i <= users[user.Item1 - 1].Children.Count; i++)
            {
                long childI = users[user.Item1 - 1].Children[i - 1].Item1;
                long weight = user.Item2 + users[user.Item1 - 1].Children[i - 1].Item2;
                if (users[childI - 1].distance > weight)
                    users[childI - 1].distance = weight;
                if (users[childI - 1].IsChecked == false)
                    forward.Insert(new Tuple<long, long> (childI, weight));
            }
            return user.Item1;
        }

        public void MakeGraph(long[][] edges, Node[] users)
        {
            for (int i = 1; i <= users.Length; i++)
            {
                users[i - 1] = new Node(i);
                users[i - 1].distance = long.MaxValue;
            }
            foreach (var edge in edges)
            {
                users[edge[0] - 1].Children.Add(new Tuple<long, long>(edge[1], edge[2]));
            }
        }

        public void MageReverseGraph(long[][] edges, Node[] users)
        {
            for (int i = 1; i <= users.Length; i++)
            {
                users[i - 1] = new Node(i);
                users[i - 1].distance = long.MaxValue;
            }
            foreach (var edge in edges)
            {
                users[edge[1] - 1].Children.Add(new Tuple<long, long>(edge[0], edge[2]));
            }
        }
    }
}
