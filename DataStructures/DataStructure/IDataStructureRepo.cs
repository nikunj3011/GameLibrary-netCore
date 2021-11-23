namespace GameLibrary.Services
{
    public interface IDataStructureRepo<T>
    {
        int size();
        bool isEmpty();
        T get(int index);
        void set(int index, T elem);
        void add(T elem);
        void clear();
        T removeAt(int rm_index);
        bool remove(T obj);
        public int indexOf(T obj);
        public bool contains(T obj);
        void reverse();
        void sort();
        int binarySearch(int key);
    }
}