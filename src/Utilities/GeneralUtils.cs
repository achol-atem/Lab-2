/**
* Utility functions with self contained methods
* Performing specific tasks that can be reused in the future
*
* Bugs: (a list of bugs and / or other problems)
*
* @author <Anita and Achol>
* @date <9/3/2025>
*/
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Project1;

// General Utilites class that contains all the methods
public class GeneralUtils
{
    public bool IsValidVariable(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        for (int i = 0; i < name.Length; i++)
        {
            // Going through loop, if a character is uppercase, return false
            char current_char = name[i];
            if (char.IsUpper(current_char))
            {
                return false;
            }
        }

        return true;
    }

    // Exception class
    public class InvalidLevelException : Exception
    {
        public InvalidLevelException()
            : base("Indentation level must be positive.") { }
    }
    public string GetIndentation(int level)
    {
        // Make sure the input level is > or more than 0
        if (level <= 0) throw new InvalidLevelException();
        string spaces = string.Empty;
        // Multiplies the level by 4 to get correct amount of spaces needed
        for (int i = 0; i < (level * 4); i++)
        {
            spaces += " ";
        }
        return spaces;
    }

    public bool IsValidOperator(string op)
    {
        // Handle null or empty inputs
        if (string.IsNullOrEmpty(op))
            return false;

        // Saving defined operators to a list of string
        string[] operators = { "+", "-", "*", "%", "/", "//", "**" };
        return operators.Contains(op);
    }
    public int CountOccurrences(string s, char c)
    {
        // Initialize count to 0
        int count = 0;
        // Loop through string, find character, and increment count
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == c) count += 1;
        }
        return count;
    }

    // Calculating average from an integer array of numbers
    public double CalculateAverage(int[] numbers)
    {
        // Handling empty array
        if (numbers.Length == 0) throw new ArgumentException();

        int average = 0;
        foreach (int number in numbers)
        {
            average += number;
        }

        return (double)average / numbers.Length;
    }

    // Checking if an item is in an array using EqualityComparer
    public bool Contains<T>(T[] array, T item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(array[i], item))
                return true;
        }
        return false;
    }

    // Confirming if a given password has met the requirements
    // Used xUnit Test PPT as reference
    public bool IsPasswordStrong(string pwd)
    {
        // Checking length
        if (pwd.Length < 8) return false;
        bool hasUppercase = pwd.Any(char.IsUpper);
        bool hasLowercase = pwd.Any(char.IsLower);
        bool hasDigit = pwd.Any(char.IsDigit);
        bool hasSpecial = pwd.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
        return hasUppercase && hasLowercase && hasDigit && hasSpecial;
    }

    // Checking an array for duplicates using a dictionary and storing duplicate values in list
    public T[] Duplicates<T>(T[] array) where T : notnull
    {
        // Creation
        var frequencies = new Dictionary<T, int>();
        var duplicates = new List<T>();

        // Storing key value pairs in dictionary
        foreach (T item in array)
        {
            // If key exists in dictionary, increase value
            if (frequencies.ContainsKey(item))
                frequencies[item]++;
            // First occurence, add to dictionary
            else
                frequencies[item] = 1;

        }
        // Loop through dictionary and finding keys with values greater than 1 and adding them to list
        foreach (var key in frequencies)
        {
            if (key.Value > 1)
                duplicates.Add(key.Key);
        }
        return duplicates.ToArray();
    }

    public List<T> GetUniqueItems<T>(List<T> list) where T : notnull
    {
        // Handling exception
        if (list == null)
            throw new ArgumentException();

        // Creation
        var frequencies = new Dictionary<T, int>();

        // Storing key value pairs in dictionary
        foreach (T item in list)
        {
            // If key exists in dictionary, increase value
            if (frequencies.ContainsKey(item))
                frequencies[item]++;
            // First occurence, add to dictionary
            else
                frequencies[item] = 1;
        }
        var unique_items = new List<T>();

        // Adding keys with value on 1 to list
        foreach (var key in frequencies)
        {
            if (key.Value == 1)
                unique_items.Add(key.Key);
        }
        return unique_items;

    }

    /// <summary>
    /// First character of the string is lowercase, all subsequent spaces removed and next letter is capitalized
    /// </summary>
    /// <param name="s"> A string</param>
    /// <returns>string</returns>
    public string ToCamelCase(string s)
    {
        // Handle null or empty inputs
        if (s == null)
    {
        throw new NullReferenceException("Input cannot be null");
    }

        string result = "";
        result += char.ToLower(s[0]);
        for (int index = 1; index < s.Length; index++)
        {
            if (s[index] == ' ')
            {
                if (index + 1 < s.Length)
                {
                    result += char.ToUpper(s[index + 1]);
                    index++;
                }
            }
            else result += s[index];
        }
        return result;
    }

}
