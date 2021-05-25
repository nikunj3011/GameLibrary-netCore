using System.Collections;
using System.Collections.Generic;

namespace GameLibrary.Services
{
    public class DynamicArray<T> : IDataStructureRepo<T>, IEnumerable<T>
    {
        private T[] arr;
        private int len = 0; //Length user thinks array is
        private int capacity = 0; //actual array size 
        public DynamicArray() { arr = new T[16]; }

        public DynamicArray(int capacity)
        {
            if (capacity < 0) throw new System.ArgumentException("Capacity: "+ capacity);
            this.capacity = capacity;
            arr = new T[capacity];
        }

        public int size()
        {
            return len;
        }

        public bool isEmpty()
        {
            return size() == 0;
        }

        public T get(int index)
        {
            return arr[index];
        }

        public void set(int index, T elem)
        {
            arr[index] = elem;
        }

        public void clear()
        {
            for (int i = 0; i < capacity; i++)
                arr[i] = default;
            len = 0;
        }

        public void add(T elem)
        {
            //resize
            if ((len + 1) >= capacity)
            {
                if (capacity == 0) capacity = 1;
                else capacity *= 2; //doubles the size

                T[] new_arr = new T[capacity];
                for (int i = 0; i < len; i++)
                    new_arr[i] = arr[i];
                arr = new_arr;
            }
            arr[len++] = elem;
        }
         
        public void removeAt(int rm_index)
        {
            throw new System.NotImplementedException();
        }

        public int binarySearch(int key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public void reverse()
        {
            throw new System.NotImplementedException();
        }

        

        public void sort()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}