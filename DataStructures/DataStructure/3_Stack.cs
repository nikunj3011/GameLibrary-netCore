using GameLibrary.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.DataStructure
{
    public class Stack<T> : IDataStructureRepo<T>, IEnumerable<T>
    {
        private LinkedList<T> list = new LinkedList<T>();

        public Stack() { }

        public Stack(T firstElm) { push(firstElm); }

        public bool isEmpty()
        {
            return size() == 0;
        }

        public int size()
        {
            return list.Count();
        }

        public void push(T elm)
        {
            list.AddLast(elm);
        }

        public void pop()
        {
            if(isEmpty()) throw new NotImplementedException();

            list.RemoveLast();
        }

        public T peek()
        {
            if (isEmpty()) throw new NotImplementedException();

            return list.Last();
        }

        public int binarySearch(int key)
        {
            throw new NotImplementedException();
        }

        public T get(int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void reverse()
        {
            throw new NotImplementedException();
        }

        public void set(int index, T elem)
        {
            throw new NotImplementedException();
        }

        public void sort()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void add(T elem)
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public T removeAt(int rm_index)
        {
            throw new NotImplementedException();
        }

        public bool remove(T obj)
        {
            throw new NotImplementedException();
        }

        public int indexOf(T obj)
        {
            throw new NotImplementedException();
        }

        public bool contains(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
