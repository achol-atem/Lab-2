using System;
using System.Collections.Generic; // Added for List<T> usage
using Xunit;
using DataStructures;

namespace DataStructures.Tests
{
    public class DLLTests
    {
        // ---------- Construction & Size ----------
        [Fact]
        public void NewList_ShouldBeEmpty()
        {
            var dll = new DLL<int>();

            Assert.Equal(0, dll.Size());
            Assert.Equal("[]", dll.ToString());
        }

        // ---------- Push & ToString ----------
        [Fact]
        public void PushFront_ShouldAddItemToFront()
        {
            var dll = new DLL<int>();
            dll.PushFront(10);
            dll.PushFront(20);

            Assert.Equal(2, dll.Size());
            Assert.Equal("[20, 10]", dll.ToString());
        }

        [Fact]
        public void PushBack_ShouldAddItemToBack()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            Assert.Equal(2, dll.Size());
            Assert.Equal("[10, 20]", dll.ToString());
        }

        [Fact]
        public void PushFront_ThenPushBack_ShouldPreserveOrder()
        {
            var dll = new DLL<int>();
            dll.PushFront(10);
            dll.PushBack(20);
            dll.PushFront(5);

            Assert.Equal("[5, 10, 20]", dll.ToString());
        }

        // ---------- Front & Back ----------
        [Fact]
        public void Front_And_Back_ShouldReturnCorrectElements()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            Assert.Equal(10, dll.Front());
            Assert.Equal(20, dll.Back());
        }

        [Fact]
        public void Front_OnEmptyList_ShouldThrow()
        {
            var dll = new DLL<int>();
            Assert.Throws<InvalidOperationException>(() => dll.Front());
        }

        [Fact]
        public void Back_OnEmptyList_ShouldThrow()
        {
            var dll = new DLL<int>();
            Assert.Throws<InvalidOperationException>(() => dll.Back());
        }

        // ---------- Contains ----------
        [Theory]
        [InlineData(10, true)]
        [InlineData(20, false)]
        public void Contains_ShouldReturnExpected(int value, bool expected)
        {
            var dll = new DLL<int>();
            dll.PushBack(10);

            Assert.Equal(expected, dll.Contains(value));
        }

        // ---------- Remove by Value ----------
        [Fact]
        public void Remove_ExistingItem_ShouldReturnTrueAndShrinkList()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            bool removed = dll.Remove(10);

            Assert.True(removed);
            Assert.False(dll.Contains(10));
            Assert.Equal(1, dll.Size());
            Assert.Equal("[20]", dll.ToString());
        }

        [Fact]
        public void Remove_NonExistingItem_ShouldReturnFalse()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);

            bool removed = dll.Remove(20);

            Assert.False(removed);
            Assert.Equal(1, dll.Size());
        }

        // ---------- PopFront & PopBack ----------
        [Fact]
        public void PopFront_ShouldRemoveAndReturnFirstElement()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            int value = dll.PopFront(); // Fixed: removed parameter if PopFront() takes no parameters

            Assert.Equal(10, value);
            Assert.Equal(1, dll.Size());
            Assert.Equal("[20]", dll.ToString());
        }

        [Fact]
        public void PopBack_ShouldRemoveAndReturnLastElement()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            int value = dll.PopBack(); // Fixed: removed parameter if PopBack() takes no parameters

            Assert.Equal(20, value);
            Assert.Equal(1, dll.Size());
            Assert.Equal("[10]", dll.ToString());
        }

        [Fact]
        public void PopFront_OnEmptyList_ShouldThrow()
        {
            var dll = new DLL<int>();
            Assert.Throws<InvalidOperationException>(() => dll.PopFront());
        }

        [Fact]
        public void PopBack_OnEmptyList_ShouldThrow()
        {
            var dll = new DLL<int>();
            Assert.Throws<InvalidOperationException>(() => dll.PopBack());
        }

        // ---------- ToString Edge Cases ----------
        [Fact]
        public void ToString_WithNullValues_ShouldHandleGracefully()
        {
            var dll = new DLL<string?>();
            dll.PushBack(null);
            dll.PushBack("test");

            Assert.Equal("[null, test]", dll.ToString()); // Adjust expected format based on implementation
        }

        // ---------- Insert at Index ----------
        [Fact]
        public void Insert_AtValidIndex_ShouldInsertCorrectly()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(30);
            dll.Insert(1, 20);

            Assert.Equal(3, dll.Size());
            Assert.Equal("[10, 20, 30]", dll.ToString());
        }

        [Fact]
        public void Insert_AtBeginning_ShouldInsertCorrectly()
        {
            var dll = new DLL<int>();
            dll.PushBack(20);
            dll.Insert(0, 10);

            Assert.Equal("[10, 20]", dll.ToString());
        }

        [Fact]
        public void Insert_AtEnd_ShouldInsertCorrectly()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.Insert(1, 20);

            Assert.Equal("[10, 20]", dll.ToString());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void Insert_AtInvalidIndex_ShouldThrow(int index)
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            Assert.Throws<ArgumentOutOfRangeException>(() => dll.Insert(index, 30));
        }

        // ---------- IndexOf ----------
        [Fact]
        public void IndexOf_ExistingItem_ShouldReturnCorrectIndex()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);
            dll.PushBack(30);

            Assert.Equal(1, dll.IndexOf(20));
            Assert.Equal(0, dll.IndexOf(10));
            Assert.Equal(2, dll.IndexOf(30));
        }

        [Fact]
        public void IndexOf_NonExistingItem_ShouldReturnMinusOne()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            Assert.Equal(-1, dll.IndexOf(30));
        }

        [Fact]
        public void IndexOf_OnEmptyList_ShouldReturnMinusOne()
        {
            var dll = new DLL<int>();

            Assert.Equal(-1, dll.IndexOf(10));
        }

        // ---------- Indexer (this[index]) ----------
        [Fact]
        public void Indexer_Get_ShouldReturnCorrectValue()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);
            dll.PushBack(30);

            Assert.Equal(10, dll[0]);
            Assert.Equal(20, dll[1]);
            Assert.Equal(30, dll[2]);
        }

        [Fact]
        public void Indexer_Set_ShouldUpdateValue()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            dll[1] = 25;

            Assert.Equal(25, dll[1]);
            Assert.Equal("[10, 25]", dll.ToString());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        public void Indexer_Get_InvalidIndex_ShouldThrow(int index)
        {
            var dll = new DLL<int>();
            dll.PushBack(10);

            Assert.Throws<ArgumentOutOfRangeException>(() => dll[index]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        public void Indexer_Set_InvalidIndex_ShouldThrow(int index)
        {
            var dll = new DLL<int>();
            dll.PushBack(10);

            Assert.Throws<ArgumentOutOfRangeException>(() => dll[index] = 20);
        }

        // ---------- RemoveAt ----------
        [Fact]
        public void RemoveAt_ValidIndex_ShouldRemoveCorrectElement()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);
            dll.PushBack(30);

            dll.RemoveAt(1);

            Assert.Equal(2, dll.Size());
            Assert.Equal("[10, 30]", dll.ToString());
        }

        [Fact]
        public void RemoveAt_FirstElement_ShouldWork()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            dll.RemoveAt(0);

            Assert.Equal("[20]", dll.ToString());
        }

        [Fact]
        public void RemoveAt_LastElement_ShouldWork()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            dll.RemoveAt(1);

            Assert.Equal("[10]", dll.ToString());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        public void RemoveAt_InvalidIndex_ShouldThrow(int index)
        {
            var dll = new DLL<int>();
            dll.PushBack(10);

            Assert.Throws<ArgumentOutOfRangeException>(() => dll.RemoveAt(index));
        }

        // ---------- CopyTo ----------
        [Fact]
        public void CopyTo_ValidArrayAndIndex_ShouldCopyElements()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);
            dll.PushBack(30);

            int[] array = new int[5];
            dll.CopyTo(array, 1);

            Assert.Equal(new int[] { 0, 10, 20, 30, 0 }, array);
        }

        [Fact]
        public void CopyTo_AtBeginning_ShouldWork()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            int[] array = new int[3];
            dll.CopyTo(array, 0);

            Assert.Equal(new int[] { 10, 20, 0 }, array);
        }

        [Fact]
        public void CopyTo_NullArray_ShouldThrow()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);

            Assert.Throws<ArgumentNullException>(() => dll.CopyTo(null!, 0));
        }

        [Fact]
        public void CopyTo_InsufficientSpace_ShouldThrow()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);
            dll.PushBack(30);

            int[] array = new int[3];

            Assert.Throws<ArgumentException>(() => dll.CopyTo(array, 1));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void CopyTo_InvalidArrayIndex_ShouldThrow(int arrayIndex)
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);

            int[] array = new int[5];

            Assert.Throws<ArgumentOutOfRangeException>(() => dll.CopyTo(array, arrayIndex));
        }

        // ---------- IEnumerable ----------
        [Fact]
        public void GetEnumerator_ShouldIterateAllElements()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);
            dll.PushBack(30);

            var result = new List<int>();
            foreach (int value in dll)
            {
                result.Add(value);
            }

            Assert.Equal(new int[] { 10, 20, 30 }, result);
        }

        [Fact]
        public void GetEnumerator_EmptyList_ShouldNotIterate()
        {
            var dll = new DLL<int>();

            var result = new List<int>();
            foreach (int value in dll)
            {
                result.Add(value);
            }

            Assert.Empty(result);
        }

        // ---------- PopFront & PopBack Duplicate Test Section ----------
        [Fact]
        public void PopFront_ShouldUseNodeRemovalNotValueRemoval()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(10); // Duplicate value
            dll.PushBack(20);

            int popped = dll.PopFront();

            Assert.Equal(10, popped);
            Assert.Equal(2, dll.Size());
            Assert.Equal("[10, 20]", dll.ToString()); // Should remove first node, not first occurrence
        }

        [Fact]
        public void PopBack_ShouldUseNodeRemovalNotValueRemoval()
        {
            var dll = new DLL<int>();
            dll.PushBack(10);
            dll.PushBack(20);
            dll.PushBack(20); // Duplicate value

            int popped = dll.PopBack();

            Assert.Equal(20, popped);
            Assert.Equal(2, dll.Size());
            Assert.Equal("[10, 20]", dll.ToString()); // Should remove last node, not first occurrence
        }

        // ---------- Edge Cases ----------
        [Fact]
        public void SingleElement_AllOperations_ShouldWork()
        {
            var dll = new DLL<int>();
            dll.PushBack(42);

            Assert.Equal(42, dll.Front());
            Assert.Equal(42, dll.Back());
            Assert.Equal(42, dll[0]);
            Assert.Equal(0, dll.IndexOf(42));
            Assert.True(dll.Contains(42));

            dll[0] = 99;
            Assert.Equal("[99]", dll.ToString());
        }

        [Fact]
        public void MultipleOperations_ShouldMaintainConsistency()
        {
            var dll = new DLL<string>();
            dll.PushBack("first");
            dll.PushFront("start");
            dll.Insert(1, "middle");
            dll.PushBack("end");

            Assert.Equal("[start, middle, first, end]", dll.ToString());
            Assert.Equal(4, dll.Size());
            Assert.Equal("start", dll.Front());
            Assert.Equal("end", dll.Back());
        }

        // ---------- Clear Tests ----------
        [Fact]
        public void Clear_EmptyList_ShouldRemainEmpty()
        {
            var dll = new DLL<int>();
            dll.Clear();
            Assert.Equal(0, dll.Size());
            Assert.Equal("[]", dll.ToString());
        }

        [Fact]
        public void Clear_SingleElement_ShouldBecomeEmpty()
        {
            var dll = new DLL<int>();
            dll.Add(42);
            dll.Clear();
            Assert.Equal(0, dll.Size());
            Assert.Equal("[]", dll.ToString());
        }

        [Fact]
        public void Clear_MultipleElements_ShouldBecomeEmpty()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            dll.Add(20);
            dll.Add(30);
            dll.Clear();
            Assert.Equal(0, dll.Size());
            Assert.Equal("[]", dll.ToString());
        }

        [Fact]
        public void Clear_ThenAdd_ShouldWork()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            dll.Add(20);
            dll.Clear();
            dll.Add(30);
            Assert.Equal(1, dll.Size());
            Assert.Equal("[30]", dll.ToString());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void Clear_VariousListSizes_ShouldBecomeEmpty(int size)
        {
            var dll = new DLL<int>();
            for (int i = 0; i < size; i++)
            {
                dll.Add(i);
            }
            dll.Clear();
            Assert.Equal(0, dll.Size());
            Assert.Equal("[]", dll.ToString());
        }

        // ---------- IsEmpty Tests ----------
        [Fact]
        public void IsEmpty_NewList_ShouldReturnTrue()
        {
            var dll = new DLL<int>();
            Assert.True(dll.IsEmpty());
        }

        [Fact]
        public void IsEmpty_AfterAdd_ShouldReturnFalse()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            Assert.False(dll.IsEmpty());
        }

        [Fact]
        public void IsEmpty_AfterClear_ShouldReturnTrue()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            dll.Add(20);
            dll.Clear();
            Assert.True(dll.IsEmpty());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(50)]
        public void IsEmpty_WithElements_ShouldReturnFalse(int elementCount)
        {
            var dll = new DLL<int>();
            for (int i = 0; i < elementCount; i++)
            {
                dll.Add(i);
            }
            Assert.False(dll.IsEmpty());
        }

        // ---------- Count Property Tests ----------
        [Fact]
        public void Count_EmptyList_ShouldReturnZero()
        {
            var dll = new DLL<int>();
            Assert.Equal(0, dll.Count);
        }

        [Fact]
        public void Count_AfterAdd_ShouldIncrement()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            Assert.Equal(1, dll.Count);
            dll.Add(20);
            Assert.Equal(2, dll.Count);
        }

        [Fact]
        public void Count_AfterClear_ShouldReturnZero()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            dll.Add(20);
            dll.Clear();
            Assert.Equal(0, dll.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(15)]
        public void Count_MatchesSize_ShouldBeConsistent(int elementCount)
        {
            var dll = new DLL<int>();
            for (int i = 0; i < elementCount; i++)
            {
                dll.Add(i);
            }
            // Ensure Count property matches Size() method
            Assert.Equal(dll.Size(), dll.Count);
        }

        // ---------- IsReadOnly Property Tests ----------
        [Fact]
        public void IsReadOnly_ShouldAlwaysReturnFalse()
        {
            var dll = new DLL<int>();
            Assert.False(dll.IsReadOnly);
            dll.Add(10);
            Assert.False(dll.IsReadOnly);
            dll.Clear();
            Assert.False(dll.IsReadOnly);
        }

        // ---------- Add Method Tests ----------
        [Fact]
        public void Add_SingleElement_ShouldAddToBack()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            Assert.Equal(1, dll.Size());
            Assert.Equal("[10]", dll.ToString());
        }

        [Fact]
        public void Add_MultipleElements_ShouldAddToBack()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            dll.Add(20);
            dll.Add(30);
            Assert.Equal(3, dll.Size());
            Assert.Equal("[10, 20, 30]", dll.ToString());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Add_VariousIntegerValues_ShouldAddSuccessfully(int value)
        {
            var dll = new DLL<int>();
            // Edge case: Testing boundary values and special integers
            dll.Add(value);
            Assert.Equal(1, dll.Size());
            Assert.Contains(value.ToString(), dll.ToString());
        }

        [Fact]
        public void Add_StringElements_ShouldWork()
        {
            var dll = new DLL<string>();
            dll.Add("hello");
            dll.Add("world");
            Assert.Equal(2, dll.Size());
            Assert.Equal("[hello, world]", dll.ToString());
        }

        [Fact]
        public void Add_NullString_ShouldWork()
        {
            var dll = new DLL<string?>();
            dll.Add(null);
            Assert.Equal(1, dll.Size());
            Assert.Equal("[null]", dll.ToString()); // Adjust based on actual null representation
        }

        [Fact]
        public void Add_AfterClear_ShouldWork()
        {
            var dll = new DLL<int>();
            dll.Add(10);
            dll.Clear();
            dll.Add(20);
            Assert.Equal(1, dll.Size());
            Assert.Equal("[20]", dll.ToString());
        }

        // ---------- Integration Tests ----------
        [Fact]
        public void Integration_AddClearAddSequence_ShouldWork()
        {
            var dll = new DLL<int>();
            // Initial state
            Assert.True(dll.IsEmpty());
            Assert.Equal(0, dll.Count);
            Assert.Equal("[]", dll.ToString());
            
            // Add elements
            dll.Add(1);
            dll.Add(2);
            dll.Add(3);
            Assert.False(dll.IsEmpty());
            Assert.Equal(3, dll.Count);
            Assert.Equal("[1, 2, 3]", dll.ToString());
            
            // Clear
            dll.Clear();
            Assert.True(dll.IsEmpty());
            Assert.Equal(0, dll.Count);
            Assert.Equal("[]", dll.ToString());
            
            // Add after clear
            dll.Add(42);
            Assert.False(dll.IsEmpty());
            Assert.Equal(1, dll.Count);
            Assert.Equal("[42]", dll.ToString());
        }

        [Fact]
        public void Integration_PropertiesConsistency_ShouldBeMaintained()
        {
            var dll = new DLL<int>();
            
            // Empty list consistency
            Assert.Equal(dll.Count == 0, dll.IsEmpty());
            Assert.Equal(dll.Count, dll.Size());
            
            // After adding elements
            dll.Add(10);
            dll.Add(20);
            Assert.Equal(dll.Count == 0, dll.IsEmpty());
            Assert.Equal(dll.Count, dll.Size());
            
            // After clearing
            dll.Clear();
            Assert.Equal(dll.Count == 0, dll.IsEmpty());
            Assert.Equal(dll.Count, dll.Size());
        }
    }
}