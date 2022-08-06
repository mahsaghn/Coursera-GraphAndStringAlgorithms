using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q3PatternMatchingSuffixArray : Processor
    {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
        {
            ExcludeTestCaseRangeInclusive(42,50);
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, long n, string[] patterns)
        {
            HashSet<long> result = new HashSet<long>();
            long[] suffix = CompiutSufix(text);
            int numA = 0, numC = 0, numG = 0, numT = 0;
            FillNumberLetters(text
                , ref numA, ref numC, ref numG, ref numT);
            for (int i = 0; i < n; i++)
            {
                List<long> r = PatternCount(patterns[i], text, suffix, numA, numC, numG, numT);
                foreach (var e in r)
                    if (!result.Contains(e - patterns[i].Length + 1))
                        result.Add(e-patterns[i].Length+1);
            }
            if (result.Count == 0)
                return new long[] { -1 };
            return result.ToArray();
        }

        private long[] CompiutSufix(string text)
        {
            long[] c;
            long[] subString = Q2CunstructSuffixArray.SingleSortedChar(text);
            long[] classes = Q2CunstructSuffixArray.DefineFirstClass(subString, text,out c);
            int l = 1;
            while (l < text.Length)
            {
                subString =Q2CunstructSuffixArray. DoubleSortedSubstring(text, subString, classes, l);
                l *= 2;
                classes = Q2CunstructSuffixArray.RecomputeClasses(subString, text, l,out c);
            }
            return subString;
        }

        private List<long> PatternCount(string pattern, string text, long[] sufix
            , int numA, int numC, int numG, int numT)
        {
            Tuple<long, long> range = ReturnRange(text, pattern[0], numA, numC, numG, numT);
            List<long> exist = new List<long>();
            for (long i = range.Item1; i < range.Item2; i++)
                exist.Add(sufix[i]);
            for (int i=1;i<pattern.Length;i++)
            {
                exist=CheckItem(pattern[i], sufix,exist, text, numA, numC, numG, numT);
            }
            return exist;
        }

        private Tuple<long,long> ReturnRange(string text, char v, int numA, int numC, int numG, int numT)
        {
            int f=0, e=0;
            switch (v)
            {
                case 'A':
                    f = 0;
                    e = numA;
                    break;
                case 'C':
                    f = numA;
                    e = numA + numC;
                    break;
                case 'G':
                    f = numA + numC ;
                    e = numA + numC + numG;
                    break;
                case 'T':
                    f = numA + numC + numG ;
                    e = numA + numC + numG + numT;
                    break;
            }
            return new Tuple<long, long>(f, e);
        }

        private List<long> CheckItem(char p, long[] sufix, List<long> exist, string text, int numA, int numC, int numG, int numT)
        {
            List<long> result = new List<long>();
            for (int i = 0; i < exist.Count; i++)
            {
                if (exist[i] == text.Length - 1)
                    continue;
                if (text[(int)exist[i] + 1] == p)
                    result.Add(exist[i] + 1);
            }
            return result;
        }

        public static void FillNumberLetters(string bwt
            , ref int nA, ref int nC, ref int nG, ref int nT)
        {
            for (int i = 0; i < bwt.Length; i++)
            {
                switch (bwt[i])
                {
                    case 'A':
                        nA++;
                        break;
                    case 'C':
                        nC++;
                        break;
                    case 'T':
                        nT++;
                        break;
                    case 'G':
                        nG++;
                        break;
                }
            }
        }


    }
}
