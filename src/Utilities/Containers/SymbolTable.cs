/**
* Implementation of DLL class with functionalities
*
* Bugs: (a list of bugs and / or other problems)
*
* @author <Anita and Achol>
* @date <9/12/2025>
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using DataStructures;

#nullable enable

namespace DataStructures
{
    public class SymbolTable<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private DLL<TKey> dll_keys = new DLL<TKey>();
        private DLL<TValue> dll_values = new DLL<TValue>();

        public SymbolTable()
        {
            parent = null;
        }


        private SymbolTable<TKey, TValue>? parent;
        public SymbolTable<TKey, TValue>? Parent
        {
            get { return parent; }

        }

        public SymbolTable(SymbolTable<TKey, TValue> parent)
        {
            this.parent = parent;
        }

        private int FindIndex(TKey key)
        {
            for (int i = 0; i < dll_keys.Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(dll_keys[i], key))
                {
                    return i;
                }

            }
            return -1;
        }

        // WORK ON THIS
        public TValue this[TKey key]
        { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        // WORK ON THIS
        public ICollection<TKey>? Keys { get; }


        // WORK ON THIS
        public ICollection<TValue>? Values { get; }

        public int Count => dll_keys.Count;

        public bool IsReadOnly => false;
        public void Add(TKey key, TValue value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentNullException("Input passed in is null");
            }
            if (FindIndex(key) >= 0)
            {
                throw new ArgumentException("A similar key exists");
            }
            dll_keys.Add(key);
            dll_values.Add(value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }
         
        
        public void Clear()
        {
            dll_keys.Clear();
            dll_values.Clear();
        }

    
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
                throw new ArgumentNullException("Key passed in is null");

            int index = FindIndex(item.Key);
            if (index >= 0)
            {
                return EqualityComparer<TValue>.Default.Equals(dll_values[index], item.Value);
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("Key passed in is null");

            return FindIndex(key)>= 0;

        }


        // WORK ON THIS
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            dll_keys.GetEnumerator();
            dll_values.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            int index = FindIndex(key);
            if (index == -1)
                {
                return false;
                }
    
            dll_keys.RemoveAt(index);
            dll_values.RemoveAt(index);
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = FindIndex(item.Key);
            if (index == -1)
            {
                return false;
            }

            if (!EqualityComparer<TValue>.Default.Equals(dll_values[index], item.Value))
            {
                return false;
            }
            dll_keys.RemoveAt(index);
            dll_values.RemoveAt(index);
            return true;
        }

        // WORK ON THIS
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("Key passed in is null");

            int index = FindIndex(key);
            if (index >= 0)
            {
                value = dll_values[index];
                return true;
            }
            // General syntax for methods with out
            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // WORK ON THIS
        public bool ContainsKeyLocal(TKey key)
        {
            throw new NotImplementedException();
        }

        // WORK ON THIS
        public bool TryGetValueLocal(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        
    }
}
