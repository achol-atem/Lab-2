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

    // DLL class - Your original methods with compilation fixes only
    public class DLL<T>
    {
        // Fixed: Changed from T to DNode<T> for sentinel nodes
        public DNode<T> head;
        public DNode<T> tail;
        public int size;

        public DLL()
        {
            // Fixed: Proper object instantiation for sentinel nodes
            this.head = new DNode<T>(default(T)!);
            this.tail = new DNode<T>(default(T)!);
            this.head.Right = tail;
            this.tail.Left = head;
            this.size = 0;
        }

        // Fixed: Added proper parameter validation
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

        // Fixed: Added proper validation and exception handling
        private void Remove(DNode<T>? node)
        {
            // Fixed: Proper validation for sentinel nodes
            if (node == null || node == head || node == tail)
                throw new InvalidOperationException("Cannot remove sentinel nodes or null");

            node.Left!.Right = node.Right;
            node.Right!.Left = node.Left;
            size--;
        }

        // Fixed: Added proper return statement and exception syntax
        private DNode<T>? GetNode(int index)
        {
            // Fixed: Proper exception instantiation
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(nameof(index));

            // Start at first valid node
            DNode<T>? start = head.Right;
            int i = 0;

            while (start != tail)
            {
                // If i = index, return the node, else keep going through the list
                if (i == index) return start;
                start = start?.Right;
                i++;
            }

            // Fixed: Added return for edge case
            throw new InvalidOperationException("Index not found");
        }

        public bool Contains(T item)
        {
            DNode<T>? start = head.Right;
            for (int i = 0; i < size; i++)
            {
                // Use equality comparer because of type T
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

        // Fixed: Proper ToString implementation with override keyword
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

        // Fixed: Corrected logic and parameter handling
        public bool Remove(T item)
        {
            DNode<T>? start = head.Right;
            for (int i = 0; i < size; i++)
            {
                if (start != null && EqualityComparer<T>.Default.Equals(start.Value, item))
                {
                    // Fixed: Call Remove with the node, not the item
                    Remove(start);
                    return true; // Fixed: Return moved to correct location
                }
                start = start?.Right;
            }
            return false;
        }

        // Fixed: Proper exception syntax and property access
        public T Front()
        {
            // Fixed: Proper exception instantiation
            if (size == 0)
                throw new InvalidOperationException("List is empty");

            // Fixed: Use correct property name (Right instead of next)
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
        // Return element after head AKA head.next
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
        // DNode<T> node = new DNode<T>(item);
        Insert(head.Right!, item);
    }
    public void PushBack(T item)
    {
            Insert(tail, item);
    }

    

        

    }
}