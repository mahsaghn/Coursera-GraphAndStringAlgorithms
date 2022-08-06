using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public long[] Solve(string text)
        {
            long[] numberC = new long[text.Length];
            Tuple<string, long>[] strs = new Tuple<string, long>[text.Length];
            strs[0] = new Tuple<string, long>(text,0);
            for (int i = 1; i < text.Length; i++)
            {
                strs[i] =new Tuple<string, long>( text.Substring(text.Length - i)
                    + text.Substring(0, text.Length - i), text.Length - i);
            }
            return strs.OrderBy(a => a.Item1).Select(a=>a.Item2).ToArray();
        }
    }
}
