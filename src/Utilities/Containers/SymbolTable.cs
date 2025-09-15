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
using Xunit.Abstractions;

#nullable enable

namespace DataStructure
{
    public class SymbolTable<TKey, TValue> : IDictionary<TKey, TValue>
    {
        DLL<TKey> dll_keys = new DLL<TKey>();
        DLL<TValue> dll_values = new DLL<TValue>();

        // Gets or sets the element with the specified key.
        // public TValue this[TKey key]
        // {
        //     get => GetNode;
        //     set => ;
        // }

        private SymbolTable<TKey, TValue>? parent;
        public SymbolTable<TKey, TValue>? Parent
        {
            get { return parent; }

        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        // Add the key and values to their respective lists
        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }
            // ArgumentException
            dll_keys.Add(key);
            dll_values.Add(value);

        }

        // Add the key and values to their respective lists
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            // Access the key by going to the item first
            dll_keys.Add(item.Key);
            dll_values.Add(item.Value);
        }

        // Clear the lists
        public void Clear()
        {
            dll_keys.Clear();
            dll_values.Clear();
        }

        // Check if the key and value exists in their respective lists
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            dll_keys.Contains(item.Key);
            dll_values.Contains(item.Value);
        }

        // Check if the key exists in its list
        public bool ContainsKey(TKey key)
        {
            dll_keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            // What happens when the value doesnt have a corresponding key
            // Find the value corresponding to the key then remove the value
            dll_keys.Remove(key);

        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            dll_keys.Remove(item.Key);
            dll_values.Remove(item.Value); 
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
