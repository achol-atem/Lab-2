using System;
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

            int value = dll.PopFront(0); // parameter is unused in your code

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

            int value = dll.PopBack(0); // parameter is unused in your code

            Assert.Equal(20, value);
            Assert.Equal(1, dll.Size());
            Assert.Equal("[10]", dll.ToString());
        }

        [Fact]
        public void PopFront_OnEmptyList_ShouldThrow()
        {
            var dll = new DLL<int>();
            Assert.Throws<InvalidOperationException>(() => dll.PopFront(0));
        }

        [Fact]
        public void PopBack_OnEmptyList_ShouldThrow()
        {
            var dll = new DLL<int>();
            Assert.Throws<InvalidOperationException>(() => dll.PopBack(0));
        }

        // ---------- ToString Edge Cases ----------
        [Fact]
        public void ToString_WithNullValues_ShouldHandleGracefully()
        {
            var dll = new DLL<string?>();
            dll.PushBack(null);
            dll.PushBack("test");

            Assert.Equal("[null, test]", dll.ToString());
        }
    }
}
