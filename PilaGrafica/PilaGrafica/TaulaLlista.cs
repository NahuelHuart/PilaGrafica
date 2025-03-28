using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQUIPS
{
    class TaulaLlista<T> : IList<T>
    {
        const int LONGITUD_INICIAL = 10;
        private T[] dades;
        private int nElem;



        public TaulaLlista()
        {
            nElem = 0;
            dades = new T[LONGITUD_INICIAL];
        }

        private void DoubleCapacity()
        {
            T[] dades2;
            dades2 = new T[dades.Length * 2];

            for (int i = 0; i < dades.Length; i++)
            {
                dades2[i] = dades[i];
            }
            dades = dades2;
        }
        public int IndexOf(T item)
        {
            int index = 0;
            bool trobat = false;
            while (!trobat && index < nElem)
            {
                if (dades[index].Equals(item))
                    trobat = true;
                else
                    index++;
            }

            if (!trobat) index = -1;
            return index;
        }

        public void Add(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();

            if (nElem == dades.Length)
            {
                DoubleCapacity();
            }

            dades[nElem] = item;

            nElem++;
        }

        public void Clear()
        {
            /*T[] dades2;
            dades2 = new T[dades.Length];

            for (int i = 0; i < nElem; i++)
            {
                dades[i] = dades2[i];
            }*/

            dades = new T[LONGITUD_INICIAL];
            nElem = 0;
        }

        public bool Contains(T item)
        {
            bool trobat = false;

            if (IndexOf(item) != -1)
                trobat = true;

            return trobat;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < this.Count; i++)
            {
                array[arrayIndex + i] = this[i];
            }
        }

        public bool Remove(T item)
        {
            if (this.IsReadOnly) throw new NotSupportedException();

            bool trobat = false;
            int pos = 0;
            while (!trobat)
            {
                if (item.Equals(dades[pos]))
                {
                    for (int i = pos; i < nElem; i++)
                    {
                        dades[pos] = dades[pos + 1];
                    }

                    trobat = true;
                }
                else
                    pos++;
            }
            if (trobat) nElem--;
            return trobat;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i<nElem; i++)
            {
                yield return dades[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= nElem)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "L'índex està fora dels límits.");
            }

            if (nElem == dades.Length)
            {
                DoubleCapacity();
            }

            for (int i = nElem; i > index; i--)
            {
                dades[i] = dades[i - 1];
            }

            dades[index] = item;
            nElem++;
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            if (index < 0 || index >= nElem)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "L'índex està fora dels límits.");
            }

            for (int i = index; i < nElem - 1; i++)
            {
                dades[i] = dades[i + 1];
            }

            nElem--;
            dades[nElem] = default; // no em deixa fer dades[nElem] = null; llavors faig això perque un objecte T no pot ser null sempre perque es generic.
        }


        public int Count
        {
            get
            {
                return nElem;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= nElem)
                    throw new ArgumentOutOfRangeException();
                return dades[index];
            }

            set => dades[index] = value;
        }

        public void AddRange(IEnumerable<T> items)
        {
            /*foreach (T item in items)
            {
             this.Add(item);
            }*/
            IEnumerator<T> iterador = (IEnumerator<T>)items.GetEnumerator();

            while (iterador.MoveNext())
                this.Add(iterador.Current);
        }
        public void Sort()
        {
            Array.Sort(dades, 0, nElem);
        }
        public void Sort(IComparer<T> comparer)
        {
            Array.Sort(dades, 0, nElem, comparer);
        }
    }
}
