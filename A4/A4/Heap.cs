using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    class Heap<T>
    {
        public List<Tuple<T,double>> Nodes;
        public Heap()
        {
            Nodes = new List<Tuple<T, double>>();
        }

        public void MakeHeap(List<Tuple<T, double>> nodes)
        {
            this.Nodes = nodes;
            for (int i = Nodes.Count / 2; i > 0; i--)
            {
                long a = SiftDown(i);
            }
        }

        public long SiftUp(long index)
        {
            if (index / 2 >= 1 && Nodes[(int)index - 1].Item2 < Nodes[(int)index / 2 - 1].Item2)
            {

                Tuple<T, double> c = Nodes[(int)index - 1];
                Nodes[(int)index - 1] = Nodes[(int)index / 2 - 1];
                Nodes[(int)index / 2 - 1] = c;
                return SiftUp(index / 2);
            }
            else
                return index;
        }

        public long SiftDown(long index)
        {
            long changingIndex = index;
            if (2 * index <= Nodes.Count && Nodes[(int)index - 1].Item2 > Nodes[2 * (int)index - 1].Item2)//right
            {
                changingIndex = 2 * index;
            }
            if (2 * index + 1 <= Nodes.Count && Nodes[(int)index - 1].Item2 > Nodes[2 * (int)index + 1 - 1].Item2)//left
            {
                if (changingIndex != index && Nodes[2 * (int)index + 1 - 1].Item2 < Nodes[2 * (int)index - 1].Item2)
                    changingIndex = 2 * index + 1;
                else if (changingIndex == index)
                    changingIndex = 2 * index + 1;
            }
            if (index != changingIndex)
            {
                Tuple<T, double> c = Nodes[(int)index - 1];
                Nodes[(int)index - 1] = Nodes[(int)changingIndex - 1];
                Nodes[(int)changingIndex - 1] = c;
                return SiftDown(changingIndex);
            }
            return index;
        }

        public void ChangePriority(long index, Tuple<T, double> value)
        {
            this.Nodes[(int)index - 1] = value;
            index = this.SiftUp(index);
            this.SiftDown(index);
        }

        public Tuple<T, double> ExtractMin()
        {
            Tuple<T, double> last = null;
            if (Nodes.Count != 0)
            {
                last = Nodes[0];
                this.Nodes[0] = this.Nodes[Nodes.Count - 1];
                Nodes.RemoveAt(Nodes.Count - 1);
                this.SiftDown(1);
            }
            return last;
        }

        public void Insert(Tuple<T, double> inserting)
        {
            this.Nodes.Add(inserting);
            this.SiftUp(this.Nodes.Count);
        }
    }
}
