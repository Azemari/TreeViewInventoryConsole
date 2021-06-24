using System.Collections.Generic;

namespace LabLogicTest.Models
{
    public class Folder : BaseItem
    {
        public List<ItemStack> ItemStacks { get; set; }

        public Folder(string name, Location location, List<ItemStack> itemStacks)
           : base(name, location)
        {
            ItemStacks = itemStacks;
        }

        public static void DeleteFolder(Folder folderToDelete)
        {
            foreach (var itemStack in folderToDelete.ItemStacks)
            {
                itemStack.Items.Clear();
            }

            folderToDelete.ItemStacks.Clear();
        }

        public static void MoveFolder(Folder folderToMove, Location newLocation)
        {
            if (folderToMove.Location > newLocation)
                return;

            folderToMove.Location = newLocation;
            foreach (var itemStack in folderToMove.ItemStacks)
            {
                itemStack.Location = newLocation;
                foreach (var item in itemStack.Items)
                {
                    item.Location = newLocation;
                }
            }
        }
    }
}
