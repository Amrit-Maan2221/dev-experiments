using System;
using System.Collections.Generic;

namespace ListDemo;


class Program
{
    public static void Main(string[] args)
    {
        List<int> nums = new List<int>();
        nums.Add(1);// addd

        nums.Insert(0, 5); // insert -> first index
        nums.Remove(1); // remove an item
        nums.RemoveAt(0); // remove an item at index
        Console.WriteLine($"Num count is {nums.Count}"); // get the count

        List<int> arr = [4, 1, 2, 3]; // array
        nums.AddRange(arr);// add array or list
        nums.Sort(); // sorting a list
        if(nums.Contains(4)) // have it ?
        {
            Console.WriteLine($"Have it");
        }

        for(int i = 0; i < nums.Count; i++) // looping accros
        {
            int num = nums[i];
            Console.WriteLine($"Num is {num}");
        }
        nums.Clear();

        foreach(int num in nums) // looping accros
        {
            Console.WriteLine($"Num is {num}");
        }

        return;
    }
}