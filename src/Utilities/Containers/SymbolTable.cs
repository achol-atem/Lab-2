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

        public TValue this[TKey key]
        { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => dll_keys.Count;

        public bool IsReadOnly => false;
        public void Add(TKey key, TValue value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentNullException();
            }
            dll_keys.Add(key)
            dll_values.Add(value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null || item.Value == null)
            {
                throw new ArgumentNullException();
            }
            dll_keys.Add(item.Key);
            dll_values.Add(item.Value);
            // Add(item.Key, item.Value)
        }
         
        
        public void Clear()
        {
            dll_keys.Clear();
            dll_values.Clear();
        }

    
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return dll_keys.Contains(item.Key) && dll_values.Contains(item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            return dll_keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            // What happens when value doesn't have corresponding key
            // Find the value corresponding to key then remove the value
            return dll_keys.Remove(key)
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return dll_keys.Remove(item.Key) && dll_values.Remove(item.Value);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainsKeyLocal(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValueLocal(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        
    }
}
