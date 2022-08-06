using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
            ExcludeTestCases(0, 6);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");
            
        protected virtual long[] Solve(string text, string pattern)
        {
            List<long> result = new List<long>();
            if (pattern.Length > 20)
                ;
            long[] borders = MakePrefixArray(pattern + "$" + text);
            for(int i=pattern.Length+1;i<borders.Length;i++)
                if (borders[i] == pattern.Length)
                    result.Add(i - 2 * pattern.Length);
            if (result.Count == 0)
                result.Add(-1);
            return result.ToArray();
        }

        public long[] MakePrefixArray(string pattern)
        {
            long[] prefixArr = new long[pattern.Length];
            prefixArr[0] = 0;
            int border = 0;
            for(int i=1;i<pattern.Length;i++)
            {
                while(border>0 && pattern[i] != pattern[border])
                    border = (int)prefixArr[border-1];
                if (pattern[i] == pattern[border])
                    border++;
                else
                    border = 0;
                prefixArr[i] = border;

            }
            return prefixArr;
        }
    }
}
