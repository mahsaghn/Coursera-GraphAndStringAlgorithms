using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, String[] patterns)
        {
            long[] result = new long[n];
            for (int i = 0; i < result.Length; i++)
                result[i] = 0;
            long[] numberC = new long[text.Length];
            int numA = 0, numC = 0, numG = 0, numT = 0;
            Q2ReconstructStringFromBWT.FillNumberLetters(numberC, text
                , ref numA, ref numC, ref numG, ref numT);
            for(int i=0;i<n;i++)
                result[i] = PatternCount(patterns[i],text,numberC,numA,numC,numG,numT);
            return result;
        }

        private int PatternCount(string pattern,string text, long[] numberC
            , int numA, int numC, int numG, int numT)
        {
            Queue<long> indexs = new Queue<long>();
            FillQueue(indexs, pattern[pattern.Length-1],numberC, text,numA,numC,numG,numT);
            int stepnumber = indexs.Count;
            long index = 0;
            for(int i=pattern.Length-2;i>=0;i--)
            {
                for (int j = 0; j < stepnumber; j++)
                {
                    index = indexs.Dequeue();//index last char
                    if (text[(int)index] == pattern[i])
                        indexs.Enqueue(IndexOfSortedArray(pattern[i], numberC[index], numA, numC, numG, numT));
                }
                stepnumber = indexs.Count;
            }
            return indexs.Count ;
        }

        private long IndexOfSortedArray(char ch,long index, int numA, int numC, int numG, int numT)
        {
            switch (ch)
            {
                case 'A':
                    return 1+ index;
                case 'C':
                    return 1 + numA + index;
                case 'G':
                    return 1 + numA + numC + index;
                case 'T':
                    return 1 + numA + numC + numG + index;
            }
            return 0;
        }

        private void FillQueue(Queue<long> indexs, char charact, long[] numberC, string text
            , int numA, int numC, int numG, int numT)
        {
            int val = 0;
            switch (charact)
            {
                case 'A':
                    val = 1 ;
                    break;
                case 'C':
                    val = 1 + numA ;
                    break;
                case 'G':
                    val= 1 + numA + numC ;
                    break;
                case 'T':
                    val= 1 + numA + numC + numG ;
                    break;
            }
            for (int i = 0; i < text.Length; i++)
                if (text[i] == charact)
                    indexs.Enqueue(val+numberC[i]);
        }
    }
}
