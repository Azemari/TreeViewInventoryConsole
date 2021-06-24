using System;
using System.Collections.Generic;
using LabLogicTest.Models;

namespace LabLogicTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> itemList = new List<Item>
            {
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type"),
                new Item("name", Location.Ordered, "type")
            };
            Models.ItemStack itemStack = new Models.ItemStack(1, Location.Ordered, "type", itemList);
            Models.ItemStack.UseOne(itemStack);

            int initialCount = itemStack.Items.Count;

            Console.ReadLine();

        }
    }
}
