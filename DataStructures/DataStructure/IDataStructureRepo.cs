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
        void removeAt(int rm_index);
        void reverse();
        void sort();
        int binarySearch(int key);
    }
}