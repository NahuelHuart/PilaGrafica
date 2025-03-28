using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PILA
{
    public class Pila<T> : ICollection<T>
    {
        const int DEFAULT_SIZE = 5;
        private T[] data;
        private int top;

        public Pila()
        {
            data = new T[DEFAULT_SIZE];
            top = -1;
        }
        public Pila(int capacity)
        {
            top = -1;
            data = new T[capacity];
        }

        public Pila(IEnumerable<T> items)
        {
            if(items == null || items.Count() == 0)
            {
                throw new ArgumentException("La colección no puede ser nula o vacía");
            }

            data = new T[DEFAULT_SIZE];
            top = -1;   
            foreach (T item in items)
            {
                if (this.IsFull) this.EnsureCapacity(this.Count*2);
                this.Push(item);
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > top)
                    throw new ArgumentOutOfRangeException();
                return data[index];
            }
        }

        public int Count
        {
            get
            {
                return top + 1;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            //for (int i = 0; i < Count; i++)
            //    data[i] = default;

            data = new T[data.Length];

            top = -1;
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeradorPila<T>(data, top);
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EnumeradorPila<T>(data, top);
        }

        public bool IsFull
        {
            get
            {
                return top == Count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return top == -1;
            }
        }

        public int Capacity
        {
            get
            {
                return this.data.Length;
            }
        }
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("La pila está vacía");
            T item = data[top];
            data[top] = default;
            top--;
            return item;
        }
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("La pila está vacía");
            return data[top];
        }
        public void Push(T item)
        {
            if (IsFull)
                throw new InvalidOperationException("La pila está llena");
            
            data[++top] = item;
        }

        public T[] ToArray()
        {
            T[] result = new T[Count];
            for (int i = 0; i < Count; i++)
                result[i] = data[i];
            return result;
        }
        public int EnsureCapacity(int newCapacity)
        {
            //mirar si la nova capacitat és major que l'actual
            if (this.Capacity < newCapacity)
            {
                //si la nova capacitat és major

                //crear un nou array amb la nova capacitat
                T[] data2 = new T[newCapacity];

                //copiar les dades al array
                for (int i = 0; i < this.Count; i++)
                    data2[i] = this.data[i];

                //assignar el nou array a la variable data de la pila
                this.data = data2;
            }

            //retorna la capacitat actual de la pila
            return this.Capacity;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (T item in this)
            {
                sb.Append(item).Append(",");
                
            }

            if(!this.IsEmpty) sb.Remove(sb.Length - 1, 1);

            sb.Append("]");

            return sb.ToString();
        }

        public bool Equals(object obj)
        {
            bool iguals = true;

            if (obj == null || !(obj is Pila<T>))
            {
                iguals = false; 
            } 

            Pila<T> pila = (Pila<T>)obj;
            
            if (this.Count != pila.Count)
            {
                iguals = false;
            }

            int i = 0;
            while (iguals && i < this.Count)
            {

                iguals = this.data[i].Equals(pila.data[i]);

                i++;
            }   


            return iguals;

        }

    }
}
