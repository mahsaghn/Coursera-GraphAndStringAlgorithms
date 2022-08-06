using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public virtual long[] Solve(string text)
        {
            long[] c;
            long[] subString = SingleSortedChar(text);
            long[] classes = DefineFirstClass(subString,text,out c);
            int l = 1;
            while (l/2 <text.Length)
            {
                subString = DoubleSortedSubstring(text,subString,classes,l);
                l *= 2;
                classes = RecomputeClasses(subString,text,l,out c);
            }
            return subString;
        }

        public static long[] RecomputeClasses(long[] subString, string text, int l,out long[] c)
        {
            c = new long[subString.Length];
            long[] classes = new long[subString.Length];
            classes[subString[0]] = 0;
            int counter = 0;
            for (int i = 1; i < subString.Length; i++)
            {
                bool check = false;
                for (int j = 0; j < l; j++)
                {
                    int index1= ((int)subString[i]+j)%text.Length
                        , index2 =((int)subString[i - 1] + j)% text.Length;
                    if (text[index1] != text[index2])
                    {
                        check = true; break;
                    }
                }
                if (check == true)
                    counter++;
                classes[subString[i]] = counter;
                c[counter]++;
            }
            return classes;
        }

        public static long[] DoubleSortedSubstring(string text, long[] subString, long[] classes, int l)
        {

            long[] result = new long[subString.Length];
            for (int i = 0; i < result.Length; i++) result[i] = -1;
            long[] c = new long[subString.Length];
            //for(int i=0;i<classes.Length;i++)
              //  c[classes[i]]++;
            long[] count = new long[c.Length];
            for (int i = 1; i < count.Length; i++)
                count[i] = count[i-1] + c[i - 1];
            count[0] = 0;
            for(int i=0;i<subString.Length;i++)
            {
                long start = (subString[i] - l+2*text.Length)%text.Length;
                long clas = classes[start];
                int index = (int)count[clas];
                while (result[index] != -1)
                    index++;
                result[index] = start;
            }
            return result;
        }

        public static long[] DefineFirstClass(long[] subString,string text,out long[] c)
        {
            c = new long[subString.Length];
            long[] classes = new long[subString.Length];
            classes[subString[0]] = 0;
            int counter = 0;
            for(int i=1;i<subString.Length;i++)
            {
                if (text[(int)subString[i]] != text[(int)subString[i - 1]])
                    counter++;
                classes[subString[i]] = counter;
                c[counter]++;
            }
            return classes;
        }

        public static long[] SingleSortedChar(string text)
        {
            Tuple<string, long>[] result = new Tuple<string, long>[text.Length];
            for(int i=0;i<text.Length;i++)
            {
                result[i] = new Tuple<string, long>($"{text[i]}",i);
            }
            return result.OrderBy(a => a.Item1).Select(a=>a.Item2).ToArray();
        }
    }
}
