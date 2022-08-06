using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class DisjointSet
    {
        public long[] set;
        public long SetCount;
        public DisjointSet(long count)
        {
            set = new long[count];
            SetCount = 0;
            for (int i = 0; i < count; i++)
                set[i] = i + 1;
        }

        public bool Union(long i,long j)
        {
            bool result = false;
            long iparent = Find(i);
            long jparent = Find(j);
            if (iparent > jparent)
            {
                result = true;
                SetCount++;
                this.set[iparent - 1] = jparent;
            }
            else if(iparent<jparent)
            {
                result = true;
                SetCount++;
                this.set[jparent - 1] = iparent;
            }
            return result;
        }

        public long Find(long i)
        {
            while (i != set[i - 1])
                i = set[i - 1];
            return i;
        }
    }
}
