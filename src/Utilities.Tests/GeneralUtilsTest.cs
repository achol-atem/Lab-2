/**
* Test Utility suite that checks the functionalities of utility methods, specifically edge cases
* exceptions etc.
* 
*
* Bugs: (a list of bugs and / or other problems)
*
* @author <Claude and ChatGPT>
* @date <9/3/2025>
*/
#nullable disable
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1;

public class GeneralUtilsTest
{
    // Instance of the class under test, initialized once per test class instance
    private readonly GeneralUtils _generalUtils;

    /// <summary>
    /// Constructor that initializes the GeneralUtils instance for all test methods.
    /// This follows the AAA (Arrange, Act, Assert) pattern setup.
    /// </summary>
    public GeneralUtilsTest()
    {
        _generalUtils = new GeneralUtils();
    }

    #region IsValidVariable Tests

    /// <summary>
    /// Tests that valid variable names return true.
    /// Valid names should be lowercase, can contain numbers and underscores.
    /// </summary>
    [Theory]
    [InlineData("variable")]
    [InlineData("test123")]
    [InlineData("a")]
    [InlineData("variable_name")]
    [InlineData("123")]
    public void IsValidVariable_ValidNames_ReturnsTrue(string variableName)
    {
        // Act: Test the method with valid input
        bool result = _generalUtils.IsValidVariable(variableName);

        // Assert: Should return true for valid variable names
        Assert.True(result, $"'{variableName}' should be valid");
    }

    /// <summary>
    /// Tests that variable names containing uppercase letters return false.
    /// The system appears to enforce lowercase-only variable naming.
    /// </summary>
    [Theory]
    [InlineData("Variable")]
    [InlineData("TEST")]
    [InlineData("myLongVariableName")]
    [InlineData("A")]
    public void IsValidVariable_ContainsUppercase_ReturnsFalse(string variableName)
    {
        bool result = _generalUtils.IsValidVariable(variableName);
        Assert.False(result, $"'{variableName}' should be invalid (contains uppercase)");
    }

    /// <summary>
    /// Tests that empty strings are considered invalid variable names.
    /// This is a boundary condition test.
    /// </summary>
    [Theory]
    [InlineData("")]
    public void IsValidVariable_Empty_ReturnsFalse(string variableName)
    {
        bool result = _generalUtils.IsValidVariable(variableName);
        Assert.False(result, "Empty string should be invalid");
    }

    #endregion

    #region GetIndentation Tests

    /// <summary>
    /// Tests that positive indentation levels return the correct number of spaces.
    /// Each level appears to add 4 spaces based on the expected values.
    /// </summary>
    [Theory]
    [InlineData(1, "    ")]
    [InlineData(2, "        ")]
    [InlineData(3, "            ")]
    public void GetIndentation_PositiveLevel_ReturnsCorrectSpaces(int level, string expected)
    {
        string result = _generalUtils.GetIndentation(level);
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that zero or negative indentation levels throw InvalidLevelException.
    /// This validates the method's guard clauses for invalid input.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void GetIndentation_ZeroOrNegativeLevel_ThrowsInvalidLevelException(int level)
    {
        Assert.Throws<GeneralUtils.InvalidLevelException>(() => _generalUtils.GetIndentation(level));
    }

    #endregion

    #region IsValidOperator Tests

    /// <summary>
    /// Tests that valid mathematical operators return true.
    /// Covers basic arithmetic and advanced operators like floor division and exponentiation.
    /// </summary>
    [Theory]
    [InlineData("+")]
    [InlineData("-")]
    [InlineData("*")]
    [InlineData("%")]
    [InlineData("/")]
    [InlineData("//")]
    [InlineData("**")]
    public void IsValidOperator_ValidOperators_ReturnsTrue(string op)
    {
        bool result = _generalUtils.IsValidOperator(op);
        Assert.True(result, $"'{op}' should be a valid operator");
    }

    /// <summary>
    /// Tests that invalid operators return false.
    /// Includes assignment operators, invalid syntax, and null/empty values.
    /// </summary>
    [Theory]
    [InlineData("=")]
    [InlineData("++")]
    [InlineData("abc")]
    [InlineData("")]
    [InlineData(null)]
    public void IsValidOperator_InvalidOperators_ReturnsFalse(string op)
    {
        bool result = _generalUtils.IsValidOperator(op);
        Assert.False(result, $"'{op}' should not be a valid operator");
    }

    #endregion

    #region CountOccurrences Tests

    /// <summary>
    /// Tests character counting in various string scenarios.
    /// Covers multiple occurrences, single occurrence, no occurrence, and empty string.
    /// </summary>
    [Theory]
    [InlineData("hello", 'l', 2)]
    [InlineData("programming", 'r', 2)]
    [InlineData("test", 'x', 0)]
    [InlineData("", 'a', 0)]
    public void CountOccurrences_ValidInputs_ReturnsCorrectCount(string s, char c, int expected)
    {
        int result = _generalUtils.CountOccurrences(s, c);
        Assert.Equal(expected, result);
    }

    #endregion

    #region CalculateAverage Tests

    /// <summary>
    /// Tests average calculation for various integer arrays.
    /// Covers positive numbers, mixed positive/negative, and single element arrays.
    /// </summary>
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3.0)]
    [InlineData(new int[] { 10, 20, 30 }, 20.0)]
    [InlineData(new int[] { -5, 5 }, 0.0)]
    [InlineData(new int[] { 5 }, 5.0)]
    public void CalculateAverage_ValidArrays_ReturnsCorrectAverage(int[] numbers, double expected)
    {
        double result = _generalUtils.CalculateAverage(numbers);
        Assert.Equal(expected, result, precision: 10);
    }

    /// <summary>
    /// Tests that calculating average of an empty array throws ArgumentException.
    /// This validates proper error handling for invalid input.
    /// </summary>
    [Fact]
    public void CalculateAverage_EmptyArray_ThrowsArgumentException()
    {
        int[] emptyArray = new int[0];
        Assert.Throws<ArgumentException>(() => _generalUtils.CalculateAverage(emptyArray));
    }

    #endregion

    #region Contains Tests

    /// <summary>
    /// Tests that Contains method correctly identifies when an item exists in an array.
    /// Uses integer array for this test case.
    /// </summary>
    [Fact]
    public void Contains_ItemExists_ReturnsTrue()
    {
        int[] array = { 1, 2, 3, 4, 5 };
        bool result = _generalUtils.Contains(array, 3);
        Assert.True(result);
    }

    /// <summary>
    /// Tests that Contains method correctly identifies when an item does not exist in an array.
    /// Uses string array to test generic functionality.
    /// </summary>
    [Fact]
    public void Contains_ItemDoesNotExist_ReturnsFalse()
    {
        string[] array = { "hello", "world", "test" };
        bool result = _generalUtils.Contains(array, "missing");
        Assert.False(result);
    }

    /// <summary>
    /// Tests Contains method behavior with empty arrays.
    /// This is a boundary condition test.
    /// </summary>
    [Fact]
    public void Contains_EmptyArray_ReturnsFalse()
    {
        int[] emptyArray = new int[0];
        bool result = _generalUtils.Contains(emptyArray, 1);
        Assert.False(result);
    }

    #endregion

    #region IsPasswordStrong Tests

    [Theory]
    [InlineData("Password123!")]
    [InlineData("MyStr0ng@Pass")]
    [InlineData("Abcd1234#")]
    public void IsPasswordStrong_StrongPasswords_ReturnsTrue(string password)
    {
        bool result = _generalUtils.IsPasswordStrong(password);
        Assert.True(result, $"'{password}' should be a strong password");
    }

    [Theory]
    [InlineData("short")]
    [InlineData("password123")]
    [InlineData("PASSWORD123!")]
    [InlineData("Password!")]
    [InlineData("Password123")]
    public void IsPasswordStrong_WeakPasswords_ReturnsFalse(string password)
    {
        bool result = _generalUtils.IsPasswordStrong(password);
        Assert.False(result, $"'{password}' should be a weak password");
    }

    #endregion

    #region Duplicates Tests

    [Fact]
    public void Duplicates_ArrayWithDuplicates_ReturnsDuplicateItems()
    {
        int[] array = { 1, 2, 3, 2, 4, 3, 5 };
        int[] result = _generalUtils.Duplicates(array);
        Assert.Contains(2, result);
        Assert.Contains(3, result);
        Assert.Equal(2, result.Length);
    }

    [Fact]
    public void Duplicates_ArrayWithoutDuplicates_ReturnsEmptyArray()
    {
        string[] array = { "a", "b", "c" };
        string[] result = _generalUtils.Duplicates(array);
        Assert.Empty(result);
    }

    #endregion

    #region GetUniqueItems Tests

    [Fact]
    public void GetUniqueItems_ListWithDuplicates_ReturnsUniqueItems()
    {
        var list = new List<int> { 1, 2, 2, 3, 4, 4, 5 };
        var result = _generalUtils.GetUniqueItems(list);
        Assert.Contains(1, result);
        Assert.Contains(3, result);
        Assert.Contains(5, result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GetUniqueItems_NullList_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _generalUtils.GetUniqueItems<int>(null));
    }

    #endregion

    #region ToCamelCase Tests

    [Theory]
    [InlineData("hello world", "helloWorld")]
    [InlineData("the quick brown fox", "theQuickBrownFox")]
    [InlineData("test case example", "testCaseExample")]
    [InlineData("a b c d", "aBCD")]
    [InlineData("my variable name", "myVariableName")]
    public void ToCamelCase_ValidInputs_ReturnsCorrectCamelCase(string input, string expected)
    {
        string result = _generalUtils.ToCamelCase(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("single", "single")]
    [InlineData("UPPERCASE", "uPPERCASE")]
    [InlineData("MixedCase", "mixedCase")]
    [InlineData("a", "a")]
    [InlineData("Test", "test")]
    public void ToCamelCase_SingleWords_ReturnsFirstCharLowercased(string input, string expected)
    {
        string result = _generalUtils.ToCamelCase(input);
        Assert.Equal(expected, result);
    }

    
    #endregion
}
