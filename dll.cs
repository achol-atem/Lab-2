using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    // DNode class that can store any value
    public class DNode<T>
    {
        public T Value;
        public DNode<T> Left;
        public DNode<T> Right;
        
        public DNode(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Right = null;
        }
    }

    // DLL class - Fixed implementation
    public class DLL<T> : IEnumerable<T>, IList<T>
    {
        // Fixed: Changed from T to DNode<T> for sentinel nodes
        public DNode<T> head;
        public DNode<T> tail;
        public int size;
        
        public DLL()
        {
            // Fixed: Proper object instantiation for sentinel nodes
            this.head = new DNode<T>(default(T));
            this.tail = new DNode<T>(default(T));
            this.head.Right = tail;
            this.tail.Left = head;
            this.size = 0;
        }
        
        // Fixed: Made public and added proper parameter validation
        public void Insert(DNode<T> node, T item)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
                
            // Make the new item a DNode
            DNode<T> newnode = new DNode<T>(item);
            
            // Insert the new node before the specified node
            newnode.Left = node.Left;
            newnode.Right = node;
            node.Left.Right = newnode;
            node.Left = newnode;
            
            // Increase size
            size++;
        }
        
        // Fixed: Added proper validation and exception handling
        private void Remove(DNode<T> node)
        {
            // Fixed: Proper validation for sentinel nodes
            if (node == null || node == head || node == tail)
                throw new InvalidOperationException("Cannot remove sentinel nodes or null");
            
            node.Left.Right = node.Right;
            node.Right.Left = node.Left;
            size--;
        }
        
        // Fixed: Added proper return statement and exception syntax
        private DNode<T> GetNode(int index)
        {
            // Fixed: Proper exception instantiation
            if (index < 0 || index >= size) 
                throw new ArgumentOutOfRangeException(nameof(index));
            
            // Start at first valid node
            DNode<T> start = head.Right;
            int i = 0;
            
            while (start != tail)
            {
                // If i = index, return the node, else keep going through the list
                if (i == index) return start;
                start = start.Right;
                i++;
            }
            
            // Fixed: Added return for edge case (should not reach here with proper validation)
            throw new InvalidOperationException("Index not found");
        }
        
        public bool Contains(T item)
        {
            DNode<T> start = head.Right;
            for (int i = 0; i < size; i++)
            {
                // Use equality comparer because of type T
                if (EqualityComparer<T>.Default.Equals(start.Value, item)) 
                    return true;
                start = start.Right;
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
            DNode<T> current = head.Right;
            
            while (current != tail)
            {
                result.Append(current.Value?.ToString() ?? "null");
                if (current.Right != tail) result.Append(", ");
                current = current.Right;
            }
            
            result.Append("]");
            return result.ToString();
        }
        
        // Fixed: Corrected logic and parameter handling
        public bool Remove(T item)
        {
            DNode<T> start = head.Right;
            for (int i = 0; i < size; i++)
            {
                if (EqualityComparer<T>.Default.Equals(start.Value, item))
                {
                    // Fixed: Call Remove with the node, not the item
                    Remove(start);
                    return true; // Fixed: Return moved to correct location
                }
                start = start.Right;
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
            return head.Right.Value;
        }
        
        // Fixed: Added missing IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator()
        {
            DNode<T> current = head.Right;
            while (current != tail)
            {
                yield return current.Value;
                current = current.Right;
            }
        }
        
        // Fixed: Non-generic IEnumerable implementation
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        // Fixed: Added missing IList<T> implementations
        public T this[int index]
        {
            get => GetNode(index).Value;
            set => GetNode(index).Value = value;
        }
        
        public int Count => size;
        
        public bool IsReadOnly => false;
        
        public void Add(T item)
        {
            Insert(tail, item); // Insert before tail sentinel
        }
        
        public void Clear()
        {
            head.Right = tail;
            tail.Left = head;
            size = 0;
        }
        
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex + size > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            
            int i = arrayIndex;
            foreach (T item in this)
            {
                array[i++] = item;
            }
        }
        
        public int IndexOf(T item)
        {
            DNode<T> current = head.Right;
            for (int i = 0; i < size; i++)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item)) 
                    return i;
                current = current.Right;
            }
            return -1;
        }
        
        public void Insert(int index, T item)
        {
            if (index < 0 || index > size) 
                throw new ArgumentOutOfRangeException(nameof(index));
            
            if (index == size)
                Insert(tail, item); // Insert at end (before tail)
            else
                Insert(GetNode(index), item); // Insert before specified node
        }
        
        public void RemoveAt(int index)
        {
            Remove(GetNode(index));
        }
    }
}