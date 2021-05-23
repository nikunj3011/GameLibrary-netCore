using Microsoft.Extensions.Logging;

namespace GameLibrary.Services
{
    public interface IDataStructure<T>
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