using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string text)
        {
            string[] strs = new string[text.Length];
            strs[0] = text;
            for (int i = 1;i<text.Length;i++)
            {
                strs[i] =text.Substring(text.Length-i)
                    +text.Substring(0,text.Length-i);
            }
            strs = strs.OrderBy(a => a).ToArray();
            string result = "";
            foreach (var c in strs)
                result += c.Last();
            return result;
        }
    }
}
