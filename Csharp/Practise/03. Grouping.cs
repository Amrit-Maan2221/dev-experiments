using System;
using System.Collections.Generic;

namespace ListDemo;


class Program
{
    public static void Main(string[] args)
    {
        List<int> nums = [1, 2, 3, 4];
        Dictionary<string, List<int>> ans = GroupEvenOdd(nums);
        foreach (KeyValuePair<string, List<int>> kvp in ans)
        {
            // kvp.Key is "even" or "odd"
            // kvp.Value is the List<int>
            Console.WriteLine($"{kvp.Key}: {string.Join(", ", kvp.Value)}");
        }
    }

    static Dictionary<string, List<int>> GroupEvenOdd(List<int> nums)
    {
        Dictionary<string, List<int>> ans = new();
        ans["even"] = new List<int>();
        ans["odd"] = new List<int>();
        foreach(int n in nums)
        {
            if(n % 2 == 0)
            {
                ans["even"].Add(n);
            }else{
                ans["odd"].Add(n);
            }
        }
        return ans;
    }
}