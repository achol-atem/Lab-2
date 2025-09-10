using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable enable

namespace DataStructures
{
    // DNode class that can store any value
    public class DNode<T>
    {
        public T Value;
        public DNode<T>? Left;
        public DNode<T>? Right;

        public DNode(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Right = null;
        }
    }


    public class DLL<T> : IEnumerable<T>
    {

        public DNode<T> head;
        public DNode<T> tail;
        public int size;

        public DLL()
        {
            // Instantiation for sentinel nodes
            this.head = new DNode<T>(default(T)!);
            this.tail = new DNode<T>(default(T)!);
            this.head.Right = tail;
            this.tail.Left = head;
            this.size = 0;
        }


        private void Insert(DNode<T>? node, T item)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            // Make the new item a DNode
            DNode<T> newnode = new DNode<T>(item);

            // Insert the new node before the specified node
            newnode.Left = node.Left;
            newnode.Right = node;
            node.Left!.Right = newnode;
            node.Left = newnode;

            // Increase size
            size++;
        }


        private void Remove(DNode<T>? node)
        {
            // Confirm if check for null is necessary
            if (node == null || node == head || node == tail)
                throw new InvalidOperationException("Cannot remove sentinel nodes or null");

            node.Left!.Right = node.Right;
            node.Right!.Left = node.Left;
            size--;
        }


        private DNode<T>? GetNode(int index)
        {

            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("Index out of range");

            // Start at first valid node
            DNode<T> start = head.Right!;
            int i = 0;

            while (start != tail)
            {
                // If i = index, return the node, else keep going through the list
                if (i == index) return start;
                start = start.Right!;
                i++;
            }

            return start;


        }

        public bool Contains(T item)
        {
            DNode<T>? start = head.Right;
            for (int i = 0; i < size; i++)
            {
                // Equality comparer because of type T
                if (start != null && EqualityComparer<T>.Default.Equals(start.Value, item))
                    return true;
                start = start?.Right;
            }
            return false;
        }

        public int Size()
        {
            return size;
        }

        
        public override string ToString()
        {
            var result = new StringBuilder("[");
            DNode<T>? current = head.Right;

            while (current != tail)
            {
                result.Append(current?.Value?.ToString() ?? "null");
                if (current?.Right != tail) result.Append(", ");
                current = current?.Right;
            }

            result.Append("]");
            return result.ToString();
        }

        
        public bool Remove(T item)
        {
            DNode<T>? start = head.Right;
            for (int i = 0; i < size; i++)
            {
                if (start != null && EqualityComparer<T>.Default.Equals(start.Value, item))
                {
                    
                    Remove(start);
                    return true; 
                }
                start = start?.Right;
            }
            return false;
        }

        
        public T Front()
        {
            
            if (size == 0)
                throw new InvalidOperationException("List is empty");

            
            return head.Right!.Value;
        }

        public T Back()
        {
            if (size == 0)
                throw new InvalidOperationException("List is empty");

            return tail.Left!.Value;
        }


        public T PopFront(T item)
        {
            if (size == 0)
                throw new InvalidOperationException("List is empty");
            // Return element after head AKA head.Right
            T first = head.Right!.Value;
            Remove(first);
            return first;
        }
        public T PopBack(T item)
        {
            if (size == 0)
                throw new InvalidOperationException("List is empty");
            // Return element before tail
            T last = tail.Left!.Value;
            Remove(last);
            return last;
        }

        public void PushFront(T item)
        {
            DNode<T> node = new DNode<T>(item);
            Insert(head.Right!, item);
        }
        public void PushBack(T item)
        {
            Insert(tail, item);
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException("Index is out of range");

            if (index == size)
                Insert(tail, item);

            else
                Insert(GetNode(index), item);


        }

        public int IndexOf(T item)
        {
            int index = 0;
            DNode<T> current = head.Right!;

            while (current != tail)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item))
                {
                    return index;
                }

                current = current.Right!;
                index++;

            }

            return -1;

        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                    throw new ArgumentOutOfRangeException("Index is out of range");

                DNode<T>? node = GetNode(index);
                return node!.Value;
            }

            set
            {
                if (index < 0 || index >= size)
                    throw new ArgumentOutOfRangeException("Index is out of range");

                DNode<T>? node = GetNode(index);
                node!.Value = value;
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("Index is out of range");

            // Retrieve desired node then delete
            DNode<T>? node = GetNode(index);
            Remove(node);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("Array is null");

            if (arrayIndex < 0 || arrayIndex >= size)
                throw new ArgumentOutOfRangeException("Index is out of range");

            // Check if there is enough space from index to end of array
            if (arrayIndex + size > array.Length)
                throw new ArgumentException("Not enough space in array");

            // Begin after sentinel head node
            DNode<T>? current = head.Right!;
            int i = arrayIndex;

            while (current != null)
            {
                array[i++] = current.Value;
                current = current.Right;
            }

        }

        //
        public IEnumerator<T> GetEnumerator()
        {
            DNode<T>? current = head.Right!;

            while (current != tail)
            {
                yield return current!.Value; // yield each item in list
                current = current.Right;
            }
        }

        // Generic version of GetEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        }
        

    }

        

    
