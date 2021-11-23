using GameLibrary.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.DataStructure
{
    public class DoublyLinkedList<T> : IDataStructureRepo<T>, IEnumerable<T>
    {
        private int sizee = 0;
        private Node<T> head = null;
        private Node<T> tail = null;
        
        private class Node<T>
        {
            public T data;
            public Node<T> prev, next;
            
            public Node(T data, Node<T> prev, Node<T> next)
            {
                this.data = data;
                this.prev = prev;
                this.next = next;
            }
        }

        public void clear()
        {
            Node<T> trav = head;
            while (trav != null)
            {
                Node<T> next = trav.next;
                trav.prev = trav.next = null;
                trav.data = default;
                trav = next;
            }
            head = tail = trav = null;
            sizee = 0;
        }

        public bool isEmpty()
        {
            return size() == 0;
        }

        public int size()
        {
            return sizee;
        }

        public void add(T elem)
        {
            addLast(elem);
        }

        private void addLast(T elem)
        {
            if (isEmpty())
            {
                head = tail = new Node<T>(elem, null, null);
            }
            else
            {
                tail.next = new Node<T>(elem, tail, null);
                tail = tail.next;
            }
            sizee++;
        }

        private void addFirst(T elem)
        {
            if (isEmpty())
            {
                head = tail = new Node<T>(elem, null, null);
            }
            else
            {
                head.prev = new Node<T>(elem, null, head);
                head = head.prev;
            }
            sizee++;
        }

        public T peekFirst()
        {
            if(isEmpty()) throw new NotImplementedException();
            return head.data;
        }

        public T peekLast()
        {
            if (isEmpty()) throw new NotImplementedException();
            return tail.data;
        }

        public T removeFirst()
        {
            if (isEmpty()) throw new NotImplementedException();
            T data = head.data;
            head = head.next;
            --sizee;

            if (isEmpty()) tail = null;
            else head.prev = null;
            return data;
        }

        public T removeLast()
        {
            if (isEmpty()) throw new NotImplementedException();
            T data = tail.data;
            tail = tail.prev;
            --sizee;

            if (isEmpty()) head = null;
            else tail.next = null;
            return data;
        }

        private T remove(Node<T> node)
        {
            if (node.prev == null) return removeFirst();
            if (node.next == null) return removeLast();

            node.next.prev = node.prev;
            node.prev.next = node.next;
            T data = node.data;

            node.data = default;
            node = node.prev = node.next = null;
            --sizee;
            return data;
        }

        public T removeAt(int index)
        {
            if (index < 0 || index >= sizee) throw new Exception();

            int i;
            Node<T> trav;

            if (index < sizee / 2)
            {
                for (i = 0, trav = head; i != index; i++)
                    trav = trav.next;
            }
            else
            {
                for (i = sizee - 1, trav = tail; i != index; i++)
                    trav = trav.prev;
            }
            return remove(trav);
        }

        public bool remove(T obj)
        {
            Node<T> trav = head;

            if(obj == null)
            {
                for (trav = head; trav != null; trav = trav.next)
                {
                    if (trav.data == null)
                    {
                        remove(trav);
                        return true;
                    }
                }
            }
            else
            {
                for (trav = head; trav != null; trav = trav.next)
                {
                    if (obj.Equals(trav.data))
                    {
                        remove(trav);
                        return true;
                    }
                }
            }
            return false;
        }

        public int indexOf(T obj)
        {
            int index = 0;
            Node<T> trav = head;

            // Support searching for null
            if (obj == null)
            {
                for (; trav != null; trav = trav.next, index++)
                {
                    if (trav.data == null)
                    {
                        return index;
                    }
                }
                // Search for non null object
            }
            else
                for (; trav != null; trav = trav.next, index++)
                {
                    if (obj.Equals(trav.data))
                    {
                        return index;
                    }
                }

            return -1;
        }

        public bool contains(T obj)
        {
            return indexOf(obj) != -1;
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
    }
}
