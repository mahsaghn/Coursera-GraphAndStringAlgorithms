using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) : base(testDataName)
        {
            ExcludeTestCaseRangeInclusive(31,40);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        public static string Solve(string bwt)
        {
            long[] numberC = new long[bwt.Length];
            int numA = 0, numC = 0, numG = 0, numT = 0;
            FillNumberLetters(numberC,bwt
                ,ref numA,ref numC,ref numG,ref numT);
            long index = 0;
            string result = "$";
            while(bwt[(int)index]!='$')
            {
                result = bwt[(int)index] + result;
                switch (bwt[(int)index])
                {
                    case 'A':
                        index = 1 + numberC[index] ;
                        break;
                    case 'C':
                        index = 1 + numA + numberC[index];
                        break;
                    case 'G':
                        index = 1 + numA + numC + numberC[index];
                        break;
                    case 'T':
                        index = 1 + numA + numC + numG + numberC[index];
                        break;
                }
            }
            return result;
        }

        public static void FillNumberLetters(long[] numberC, string bwt
            , ref int nA, ref int nC, ref int nG, ref int nT)
        {
            for (int i = 0; i < bwt.Length; i++)
            {
                switch (bwt[i])
                {
                    case 'A':
                        numberC[i] = nA;
                        nA++;
                        break;
                    case 'C':
                        numberC[i] = nC;
                        nC++;
                        break;
                    case 'T':
                        numberC[i] = nT;
                        nT++;
                        break;
                    case 'G':
                        numberC[i] = nG;
                        nG++;
                        break;
                }
            }
        }
    }
}
