using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures; // Reference the namespace

namespace DataStructures.Tests
{
    [TestClass]
    public class DLLTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeEmptyList()
        {
            // Theory: Constructor should create empty list with sentinel nodes
            var dll = new DLL<int>();
            
            Assert.AreEqual(0, dll.Size());
            Assert.AreEqual(0, dll.Count);
        }

        [TestMethod]
        public void Add_SingleElement_ShouldIncreaseSize()
        {
            // Theory: Adding element should increase size and make it accessible
            var dll = new DLL<int>();
            
            dll.Add(42);
            
            Assert.AreEqual(1, dll.Size());
            Assert.AreEqual(1, dll.Count);
            Assert.IsTrue(dll.Contains(42));
            Assert.AreEqual(42, dll.Front());
        }

        [TestMethod]
        public void Add_MultipleElements_ShouldMaintainOrder()
        {
            // Theory: Elements should be added in order and accessible via indexer
            var dll = new DLL<int>();
            
            dll.Add(1);
            dll.Add(2);
            dll.Add(3);
            
            Assert.AreEqual(3, dll.Size());
            Assert.AreEqual(1, dll[0]);
            Assert.AreEqual(2, dll[1]);
            Assert.AreEqual(3, dll[2]);
            Assert.AreEqual(1, dll.Front());
        }

        [TestMethod]
        public void Contains_EmptyList_ShouldReturnFalse()
        {
            // Fact: Empty list contains no elements
            var dll = new DLL<int>();
            
            Assert.IsFalse(dll.Contains(5));
            Assert.IsFalse(dll.Contains(0));
            Assert.IsFalse(dll.Contains(-1));
        }

        [TestMethod]
        public void Contains_WithDuplicates_ShouldReturnTrue()
        {
            // Theory: Contains should find any occurrence of duplicate values
            var dll = new DLL<int>();
            
            dll.Add(1);
            dll.Add(2);
            dll.Add(2); // Duplicate
            dll.Add(3);
            dll.Add(2); // Another duplicate
            
            Assert.IsTrue(dll.Contains(2)); // Should find any of the three 2's
            Assert.IsTrue(dll.Contains(1));
            Assert.IsTrue(dll.Contains(3));
            Assert.IsFalse(dll.Contains(4));
        }

        [TestMethod]
        public void Contains_WithReferenceTypes_ShouldUseEquals()
        {
            // Theory: Contains should use .Equals() for reference type comparison
            var dll = new DLL<string>();
            
            string testStr = "hello";
            string duplicateStr = new string("hello".ToCharArray());
            
            dll.Add(testStr);
            dll.Add("world");
            
            // Should find equal strings even if different references
            Assert.IsTrue(dll.Contains(duplicateStr));
            Assert.IsTrue(dll.Contains("world"));
            Assert.IsFalse(dll.Contains("goodbye"));
        }

        [TestMethod]
        public void Remove_FirstOccurrence_ShouldRemoveCorrectElement()
        {
            // Theory: Remove should only remove first occurrence of duplicate elements
            var dll = new DLL<int>();
            
            dll.Add(1);
            dll.Add(2);
            dll.Add(2); // First duplicate
            dll.Add(3);
            dll.Add(2); // Second duplicate
            
            bool result = dll.Remove(2); // Remove first occurrence
            
            Assert.IsTrue(result);
            Assert.AreEqual(4, dll.Size());
            Assert.IsTrue(dll.Contains(2)); // Should still contain other 2's
            
            // Verify order: should be [1, 2, 3, 2]
            Assert.AreEqual(1, dll[0]);
            Assert.AreEqual(2, dll[1]); // Second 2 moved to position 1
            Assert.AreEqual(3, dll[2]);
            Assert.AreEqual(2, dll[3]);
        }

        [TestMethod]
        public void Remove_NonExistentElement_ShouldReturnFalse()
        {
            // Fact: Removing non-existent element should return false
            var dll = new DLL<int>();
            
            // Test on empty list
            Assert.IsFalse(dll.Remove(5));
            
            // Test on populated list
            dll.Add(1);
            dll.Add(2);
            dll.Add(3);
            
            Assert.IsFalse(dll.Remove(4));
            Assert.AreEqual(3, dll.Size()); // Size should remain unchanged
        }

        [TestMethod]
        public void Remove_EmptyList_ShouldReturnFalse()
        {
            // Edge case: Remove from empty list
            var dll = new DLL<int>();
            
            bool result = dll.Remove(1);
            
            Assert.IsFalse(result);
            Assert.AreEqual(0, dll.Size());
        }

        [TestMethod]
        public void RemoveAt_ValidIndex_ShouldRemoveElement()
        {
            // Theory: RemoveAt should remove element at specific index
            var dll = new DLL<int>();
            
            dll.Add(10);
            dll.Add(20);
            dll.Add(30);
            
            dll.RemoveAt(1); // Remove middle element (20)
            
            Assert.AreEqual(2, dll.Size());
            Assert.AreEqual(10, dll[0]);
            Assert.AreEqual(30, dll[1]);
            Assert.IsFalse(dll.Contains(20));
        }

        [TestMethod]
        public void RemoveAt_InvalidIndex_ShouldThrowException()
        {
            // Theory: Invalid index should throw ArgumentOutOfRangeException
            var dll = new DLL<int>();
            dll.Add(1);
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => dll.RemoveAt(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => dll.RemoveAt(1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => dll.RemoveAt(5));
        }

        [TestMethod]
        public void Front_EmptyList_ShouldThrowException()
        {
            // Theory: Accessing front of empty list should throw InvalidOperationException
            var dll = new DLL<int>();
            
            Assert.ThrowsException<InvalidOperationException>(() => dll.Front());
        }

        [TestMethod]
        public void Front_WithElements_ShouldReturnFirstElement()
        {
            // Theory: Front should return value of first valid node after head sentinel
            var dll = new DLL<int>();
            
            dll.Add(10);
            dll.Add(20);
            dll.Add(30);
            
            Assert.AreEqual(10, dll.Front());
            
            // Front should not change after adding more elements
            dll.Add(40);
            Assert.AreEqual(10, dll.Front());
        }

        [TestMethod]
        public void IndexOf_ShouldReturnCorrectIndex()
        {
            // Theory: IndexOf should return first occurrence index
            var dll = new DLL<string>();
            
            dll.Add("apple");
            dll.Add("banana");
            dll.Add("cherry");
            dll.Add("banana"); // Duplicate
            
            Assert.AreEqual(0, dll.IndexOf("apple"));
            Assert.AreEqual(1, dll.IndexOf("banana")); // First occurrence
            Assert.AreEqual(2, dll.IndexOf("cherry"));
            Assert.AreEqual(-1, dll.IndexOf("grape")); // Not found
        }

        [TestMethod]
        public void Insert_AtSpecificIndex_ShouldInsertCorrectly()
        {
            // Theory: Insert at index should place element at correct position
            var dll = new DLL<int>();
            
            dll.Add(1);
            dll.Add(3);
            dll.Add(4);
            
            dll.Insert(1, 2); // Insert 2 at index 1
            
            Assert.AreEqual(4, dll.Size());
            Assert.AreEqual(1, dll[0]);
            Assert.AreEqual(2, dll[1]); // Newly inserted
            Assert.AreEqual(3, dll[2]);
            Assert.AreEqual(4, dll[3]);
        }

        [TestMethod]
        public void Insert_AtEnd_ShouldAppendElement()
        {
            // Theory: Insert at end index should append element
            var dll = new DLL<int>();
            
            dll.Add(1);
            dll.Add(2);
            
            dll.Insert(2, 3); // Insert at end
            
            Assert.AreEqual(3, dll.Size());
            Assert.AreEqual(3, dll[2]);
        }

        [TestMethod]
        public void Clear_ShouldEmptyList()
        {
            // Theory: Clear should remove all elements and reset size
            var dll = new DLL<int>();
            
            dll.Add(1);
            dll.Add(2);
            dll.Add(3);
            
            dll.Clear();
            
            Assert.AreEqual(0, dll.Size());
            Assert.AreEqual(0, dll.Count);
            Assert.IsFalse(dll.Contains(1));
            Assert.IsFalse(dll.Contains(2));
            Assert.IsFalse(dll.Contains(3));
        }

        [TestMethod]
        public void ToString_ShouldReturnStringRepresentation()
        {
            // Theory: ToString should provide meaningful string representation
            var dll = new DLL<int>();
            
            // Test empty list
            string emptyResult = dll.ToString();
            Assert.AreEqual("[]", emptyResult);
            
            // Test with elements
            dll.Add(1);
            dll.Add(2);
            dll.Add(3);
            
            string result = dll.ToString();
            Assert.AreEqual("[1, 2, 3]", result);
        }

        [TestMethod]
        public void Enumeration_ShouldIterateCorrectly()
        {
            // Theory: IEnumerable implementation should allow foreach iteration
            var dll = new DLL<int>();
            
            dll.Add(1);
            dll.Add(2);
            dll.Add(3);
            
            var values = new List<int>();
            foreach (int value in dll)
            {
                values.Add(value);
            }
            
            Assert.AreEqual(3, values.Count);
            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(2, values[1]);
            Assert.AreEqual(3, values[2]);
        }

        [TestMethod]
        public void CopyTo_ShouldCopyElementsCorrectly()
        {
            // Theory: CopyTo should copy all elements to target array
            var dll = new DLL<int>();
            
            dll.Add(10);
            dll.Add(20);
            dll.Add(30);
            
            int[] array = new int[5];
            dll.CopyTo(array, 1); // Copy starting at index 1
            
            Assert.AreEqual(0, array[0]); // Unchanged
            Assert.AreEqual(10, array[1]);
            Assert.AreEqual(20, array[2]);
            Assert.AreEqual(30, array[3]);
            Assert.AreEqual(0, array[4]); // Unchanged
        }

        [TestMethod]
        public void Contains_WithNullValues_ShouldHandleCorrectly()
        {
            // Edge case: Handle null values in reference types
            var dll = new DLL<string>();
            
            // Test searching for null in empty list
            Assert.IsFalse(dll.Contains(null));
            
            // Add null value
            dll.Add(null);
            dll.Add("test");
            
            Assert.IsTrue(dll.Contains(null));
            Assert.IsTrue(dll.Contains("test"));
        }

        [TestMethod]
        public void Remove_AllDuplicates_ShouldRemoveOneByOne()
        {
            // Theory: Multiple Remove calls should remove duplicates sequentially
            var dll = new DLL<int>();
            
            dll.Add(5);
            dll.Add(5);
            dll.Add(5);
            
            // First Remove(5): should have [5, 5] remaining
            Assert.IsTrue(dll.Remove(5));
            Assert.AreEqual(2, dll.Size());
            Assert.IsTrue(dll.Contains(5));
            
            // Second Remove(5): should have [5] remaining
            Assert.IsTrue(dll.Remove(5));
            Assert.AreEqual(1, dll.Size());
            Assert.IsTrue(dll.Contains(5));
            
            // Third Remove(5): should be empty
            Assert.IsTrue(dll.Remove(5));
            Assert.AreEqual(0, dll.Size());
            Assert.IsFalse(dll.Contains(5));
            
            // Fourth Remove(5): should return false
            Assert.IsFalse(dll.Remove(5));
        }

        [TestMethod]
        public void Size_AfterOperations_ShouldMaintainAccuracy()
        {
            // Theory: Size should be consistent after insert/remove operations
            var dll = new DLL<int>();
            
            Assert.AreEqual(0, dll.Size());
            
            // After adds: size increases correctly
            dll.Add(1);
            Assert.AreEqual(1, dll.Size());
            
            dll.Add(2);
            Assert.AreEqual(2, dll.Size());
            
            // After remove: size decreases correctly
            dll.Remove(1);
            Assert.AreEqual(1, dll.Size());
            
            // After remove of non-existent: size unchanged
            dll.Remove(99);
            Assert.AreEqual(1, dll.Size());
        }

        [TestMethod]
        public void Indexer_GetSet_ShouldWorkCorrectly()
        {
            // Theory: Indexer should allow getting and setting values
            var dll = new DLL<int>();
            
            dll.Add(10);
            dll.Add(20);
            dll.Add(30);
            
            // Test getter
            Assert.AreEqual(10, dll[0]);
            Assert.AreEqual(20, dll[1]);
            Assert.AreEqual(30, dll[2]);
            
            // Test setter
            dll[1] = 25;
            Assert.AreEqual(25, dll[1]);
            Assert.AreEqual(25, dll.IndexOf(25));
            Assert.IsFalse(dll.Contains(20));
        }
    }

    // Additional test class for DNode
    [TestClass]
    public class DNodeTests
    {
        [TestMethod]
        public void DNode_Constructor_ShouldInitializeCorrectly()
        {
            // Theory: Node should store value and initialize pointers to null
            var node = new DNode<int>(42);
            
            Assert.AreEqual(42, node.Value);
            Assert.IsNull(node.Left);
            Assert.IsNull(node.Right);
        }

        [TestMethod]
        public void DNode_WithReferenceType_ShouldStoreReference()
        {
            // Theory: Node should store reference types correctly
            string testValue = "test";
            var node = new DNode<string>(testValue);
            
            Assert.AreEqual(testValue, node.Value);
            Assert.AreSame(testValue, node.Value); // Same reference
        }

        [TestMethod]
        public void DNode_WithNull_ShouldAllowNullValues()
        {
            // Edge case: Node should accept null values for reference types
            var node = new DNode<string>(null);
            
            Assert.IsNull(node.Value);
            Assert.IsNull(node.Left);
            Assert.IsNull(node.Right);
        }
    }
}