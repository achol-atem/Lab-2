using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable enable

namespace DataStructures
{
    // DNode class that stores type T
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
                throw new ArgumentNullException("Node is invalid");

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
            // Check if node is sentinel or null
            if (node == null || node == head || node == tail)
                throw new InvalidOperationException("Cannot remove sentinel nodes or null");

            node.Left!.Right = node.Right;
            node.Right!.Left = node.Left;
            size--;
        }


        private DNode<T>? GetNode(int index)
        {
            // Index cannot be negative or greater than size of list
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
        // Confirms whether list contains specified item
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
                // If the node value is equal to given item, remove it
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


        public T PopFront()
        {
            if (size == 0)
                throw new InvalidOperationException("List is empty");
            // Return element after head AKA head.Right
            T first = head.Right!.Value;
            Remove(first);
            return first;
        }
        public T PopBack()
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
            // New node with item
            DNode<T> node = new DNode<T>(item);
            // Insert before the first valid node AKA head.right
            Insert(head.Right!, item);
        }
        public void PushBack(T item)
        {
            // Insert before tail
            Insert(tail, item);
        }

        public void Clear()
        {
            // Create a node that starts at the first valid node
            DNode<T> start = head.Right!;
            // Use remove until the next node points to the tail
            {
                while (start != tail)
                {
                    DNode<T> right = start.Right!;
                    Remove(start);
                    // Update start to go to next node
                    start = right;
                }
            }
        }
        public bool IsEmpty()
        {
            if (size == 0) return true;
            return false;
        }
        // Property that returns the current number of elements in a list
        public int Count
        {
            get { return size; }
        }
        // Property that returns that returns false; list allows modifications
        public bool IsReadOnly
        {
            get { return false; }
        }
        // Appends item to the end of the list, previous PushBack() function
        public void Add(T item)
        {
            PushBack(item);
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

            while (current != tail)
            {
                array[i++] = current!.Value;
                current = current.Right;
            }

        }

        
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

        

    
