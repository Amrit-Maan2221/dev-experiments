using System;
using System.Collections.Generic;

namespace ListDemo;


class Program
{
    public static void Main(string[] args)
    {
        Dictionary<string, int> dict = new()
        {
            ["name1"] = 1,
            ["name4"] = 4,
        };

        dict.Add("name2", 2);
        dict["name3"] = 3;

        if(dict.TryGetValue("name1", out int val))
        {
            Console.WriteLine(val);
        }
        else
        {
            Console.WriteLine("Value is not present");
        }
        
        // Get all item names
        foreach (string itemName in dict.Keys)
        {
            Console.WriteLine("Item: " + itemName);
        }

        // Get all prices
        foreach (int price in dict.Values)
        {
            Console.WriteLine("Price: " + price);
        }
        Console.WriteLine($"Count: {dict.Count}");
    }
}