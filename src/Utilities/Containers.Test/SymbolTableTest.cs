using System;
using System.Collections.Generic;
using Xunit;

namespace DataStructures.Tests
{
    public class SymbolTableTests
    {
        // Constructor Tests
        [Fact]
        public void Constructor_Default_SetsParentToNull()
        {
            // Arrange & Act
            var symbolTable = new SymbolTable<string, int>();

            // Assert
            Assert.Null(symbolTable.Parent);
        }

        [Fact]
        public void Constructor_WithParent_SetsParentCorrectly()
        {
            // Arrange
            var parentTable = new SymbolTable<string, int>();

            // Act
            var childTable = new SymbolTable<string, int>(parentTable);

            // Assert
            Assert.Same(parentTable, childTable.Parent);
        }

        // Parent Property Tests
        [Fact]
        public void Parent_Get_ReturnsCorrectParent()
        {
            // Arrange
            var parentTable = new SymbolTable<string, int>();
            var childTable = new SymbolTable<string, int>(parentTable);

            // Act
            var result = childTable.Parent;

            // Assert
            Assert.Same(parentTable, result);
        }

        // Count Property Tests
        [Fact]
        public void Count_EmptyTable_ReturnsZero()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act & Assert
            Assert.Empty(symbolTable);
        }

        [Fact]
        public void Count_AfterAddingItems_ReturnsCorrectCount()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act
            symbolTable.Add("key1", 1);
            symbolTable.Add("key2", 2);

            // Assert
            Assert.Equal(2, symbolTable.Count);
        }

        // IsReadOnly Property Tests
        [Fact]
        public void IsReadOnly_Always_ReturnsFalse()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act & Assert
            Assert.False(symbolTable.IsReadOnly);
        }

        // Add(TKey, TValue) Tests
        [Fact]
        public void Add_ValidKeyValue_AddsSuccessfully()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act
            symbolTable.Add("test", 42);

            // Assert
            Assert.Single(symbolTable);
            Assert.True(symbolTable.ContainsKey("test"));
        }

        [Fact]
        public void Add_NullKey_ThrowsArgumentNullException()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => symbolTable.Add(null!, 42));
        }

        [Theory]
        [InlineData("key1", 1)]
        [InlineData("key2", 2)]
        [InlineData("key3", 3)]
        public void Add_MultipleValidKeyValues_AddsAllSuccessfully(string key, int value)
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act
            symbolTable.Add(key, value);

            // Assert
            Assert.True(symbolTable.ContainsKey(key));
            Assert.Single(symbolTable);
        }

        // Add(KeyValuePair) Tests
        [Fact]
        public void Add_ValidKeyValuePair_AddsSuccessfully()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            var kvp = new KeyValuePair<string, int>("test", 42);

            // Act
            symbolTable.Add(kvp);

            // Assert
            Assert.Single(symbolTable);
            Assert.True(symbolTable.ContainsKey("test"));
        }

        [Fact]
        public void Add_KeyValuePairWithNullKey_ThrowsArgumentNullException()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            var kvp = new KeyValuePair<string, int>(null!, 42);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => symbolTable.Add(kvp));
        }

        [Theory]
        [InlineData("key1", 1)]
        [InlineData("key2", 2)]
        [InlineData("key3", 3)]
        public void Add_KeyValuePairTheory_AddsSuccessfully(string key, int value)
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            var kvp = new KeyValuePair<string, int>(key, value);

            // Act
            symbolTable.Add(kvp);

            // Assert
            Assert.True(symbolTable.ContainsKey(key));
            Assert.Single(symbolTable);
        }

        // Clear Tests
        [Fact]
        public void Clear_EmptyTable_RemainEmpty()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act
            symbolTable.Clear();

            // Assert
            Assert.Empty(symbolTable);
        }

        [Fact]
        public void Clear_TableWithItems_RemovesAllItems()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("key1", 1);
            symbolTable.Add("key2", 2);

            // Act
            symbolTable.Clear();

            // Assert
            Assert.Empty(symbolTable);
            Assert.False(symbolTable.ContainsKey("key1"));
            Assert.False(symbolTable.ContainsKey("key2"));
        }

        // ContainsKey Tests
        [Fact]
        public void ContainsKey_ExistingKey_ReturnsTrue()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);

            // Act
            var result = symbolTable.ContainsKey("test");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsKey_NonExistingKey_ReturnsFalse()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);

            // Act
            var result = symbolTable.ContainsKey("nonexistent");

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("key1", true)]
        [InlineData("key2", false)]
        [InlineData("key3", false)]
        public void ContainsKey_Theory_ReturnsExpectedResult(string searchKey, bool expected)
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("key1", 1);

            // Act
            var result = symbolTable.ContainsKey(searchKey);

            // Assert
            Assert.Equal(expected, result);
        }

        // Contains Tests
        [Fact]
        public void Contains_ExistingKeyValuePair_ReturnsTrue()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);
            var kvp = new KeyValuePair<string, int>("test", 42);

            // Act
            var result = symbolTable.Contains(kvp);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Contains_KeyExistsValueDoesnt_ReturnsFalse()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);
            var kvp = new KeyValuePair<string, int>("test", 99);

            // Act
            var result = symbolTable.Contains(kvp);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Contains_NonExistingKeyValuePair_ReturnsFalse()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);
            var kvp = new KeyValuePair<string, int>("nonexistent", 42);

            // Act
            var result = symbolTable.Contains(kvp);

            // Assert
            Assert.False(result);
        }

        // Remove(TKey) Tests
        [Fact]
        public void Remove_ExistingKey_RemovesAndReturnsTrue()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);

            // Act
            var result = symbolTable.Remove("test");

            // Assert
            Assert.True(result);
            Assert.False(symbolTable.ContainsKey("test"));
            Assert.Empty(symbolTable);
        }

        [Fact]
        public void Remove_NonExistingKey_ReturnsFalse()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);

            // Act
            var result = symbolTable.Remove("nonexistent");

            // Assert
            Assert.False(result);
            Assert.Single(symbolTable);
        }

        [Theory]
        [InlineData("key1", true)]
        [InlineData("key2", false)]
        [InlineData("nonexistent", false)]
        public void Remove_Theory_ReturnsExpectedResult(string keyToRemove, bool expected)
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("key1", 1);

            // Act
            var result = symbolTable.Remove(keyToRemove);

            // Assert
            Assert.Equal(expected, result);
        }

        // Remove(KeyValuePair) Tests
        [Fact]
        public void Remove_ExistingKeyValuePair_RemovesAndReturnsTrue()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);
            var kvp = new KeyValuePair<string, int>("test", 42);

            // Act
            var result = symbolTable.Remove(kvp);

            // Assert
            Assert.True(result);
            Assert.False(symbolTable.ContainsKey("test"));
        }

        [Fact]
        public void Remove_NonMatchingKeyValuePair_ReturnsFalse()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();
            symbolTable.Add("test", 42);
            var kvp = new KeyValuePair<string, int>("test", 99);

            // Act
            var result = symbolTable.Remove(kvp);

            // Assert
            Assert.False(result);
            Assert.True(symbolTable.ContainsKey("test"));
        }



        // Edge Cases and Integration Tests
        [Fact]
        public void Add_MultipleItemsThenClear_WorksCorrectly()
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act
            symbolTable.Add("key1", 1);
            symbolTable.Add("key2", 2);
            symbolTable.Add("key3", 3);
            
            Assert.Equal(3, symbolTable.Count);
            
            symbolTable.Clear();

            // Assert
            Assert.Empty(symbolTable);
            Assert.False(symbolTable.ContainsKey("key1"));
            Assert.False(symbolTable.ContainsKey("key2"));
            Assert.False(symbolTable.ContainsKey("key3"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void Add_VariableNumberOfItems_CountIsCorrect(int numberOfItems)
        {
            // Arrange
            var symbolTable = new SymbolTable<string, int>();

            // Act
            for (int i = 0; i < numberOfItems; i++)
            {
                symbolTable.Add($"key{i}", i);
            }

            // Assert
            Assert.Equal(numberOfItems, symbolTable.Count);
        }

        // Test with different generic types
        [Fact]
        public void SymbolTable_IntStringTypes_WorksCorrectly()
        {
            // Arrange
            var symbolTable = new SymbolTable<int, string>();

            // Act
            symbolTable.Add(1, "one");
            symbolTable.Add(2, "two");

            // Assert
            Assert.Equal(2, symbolTable.Count);
            Assert.True(symbolTable.ContainsKey(1));
            Assert.True(symbolTable.ContainsKey(2));
        }
    }
}