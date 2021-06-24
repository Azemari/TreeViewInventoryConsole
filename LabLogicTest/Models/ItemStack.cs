using System.Collections.Generic;
using System.Linq;

namespace LabLogicTest.Models
{
    public class ItemStack
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string Type { get; set; }
        public List<Item> Items { get; set; }
        public int? ParentNode { get; set; }

        public ItemStack(int id, Location location, string type, List<Item> items)
        {
            Id = id;
            Location = location;
            Type = type;
            Items = items;
        }

        //Copy Constructor
        public ItemStack(ItemStack item) 
        {
            Id = item.Id;
            Location = item.Location;
            Type = item.Type;
            Items = item.Items;
        }

        public static ItemStack Create(string itemName, Location location, string type, int numberofItems)
        {
            List<Item> newItems = new List<Item>();
            for (int i = 0; i < numberofItems; i++)
            {
                newItems.Add(new Item(itemName, location, type));
            }
            return new ItemStack(1, location, type, newItems);
        }

        public static void Delete(ItemStack itemToDelete)
        {
            itemToDelete.Items.Clear();
        }

        public static void Move(ItemStack itemStackToMove, Location newLocation)
        {
            //only allow moving forward through catergories 
            if (itemStackToMove.Location > newLocation)
                return;

            itemStackToMove.Location = newLocation;
            foreach (var item in itemStackToMove.Items)
            {
                item.Location = newLocation;
            }
        }

        public static ItemStack Split(ItemStack itemStack, int newStackSize)
        {
            if (newStackSize > itemStack.Items.Count)
                return null;

            ItemStack newItemStack = new ItemStack(itemStack)
            {
                Items = itemStack.Items.Take(newStackSize).ToList()
            };

            itemStack.Items.RemoveRange(0, newStackSize);

            return newItemStack;
        }

        public static void Merge(ItemStack itemStackToMergeInto, ItemStack itemStackToBeMerged)
        {
            if (itemStackToMergeInto.Location != itemStackToBeMerged.Location
                ||
                itemStackToMergeInto.Type != itemStackToBeMerged.Type)
                return ;

            itemStackToMergeInto.Items = itemStackToMergeInto.Items.Concat(itemStackToBeMerged.Items).ToList();

            itemStackToBeMerged.Items.Clear();
        }

        public static void UseOne(ItemStack itemStack)
        {
            if(itemStack.Items.Count > 1)
            {
                Item item = itemStack.Items.Take(1).First();
                item.Location = Location.Used;
                itemStack.Items.Remove(item);
            }
            else
            {
                Move(itemStack, Location.Used);
            }
        }
    }
}