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

namespace DataStructure
{
    public class SymbolTable<TKey, TValue> : IDictionary<TKey, TValue>
    {
        DLL<TKey> dll_keys = new DLL<TKey>();
        DLL<TValue> dll_values = new DLL<TValue>();


        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private SymbolTable<TKey, TValue>? parent;
        public SymbolTable<TKey, TValue>? Parent
        {
            get { return parent; }

        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
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
