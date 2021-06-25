using System;

namespace LabLogicTest.Models
{
    public class ItemStack : BaseItem
    {
        public string Type { get; set; }
        public int Quantity { get; set; }

        public ItemStack(string name, Location location, Folder parentFolder, RootNode rootNode, string type, int quantity)
            : base(name, location, parentFolder, rootNode)
        {
            Type = type;
            Quantity = quantity;
        }

        //Copy Constructor
        public ItemStack(ItemStack item) 
            : base(item.Name, item.Location, item.ParentFolder, item.Root)
        {
            Id = Guid.NewGuid();
            Name = item.Name;
            Location = item.Location;
            ParentFolder = item.ParentFolder;
            Type = item.Type;
            Quantity = item.Quantity;
            Root = item.Root;
        }

        public void MoveTo(RootNode newLocation)
        {
            if (this.Location > newLocation.Category)
                return;
            if (ParentFolder != null)
                ParentFolder.Nodes.Remove(this);
            else
                Root.Nodes.Remove(this);

            newLocation.Nodes.Add(this);

            ParentFolder = null; 
            Location = newLocation.Category;
        }

        public void MoveTo(Folder newFolder)
        {
            if (this.Location > newFolder.Location)
                return;

            if (ParentFolder != null)
                ParentFolder.Nodes.Remove(this);
            else
                Root.Nodes.Remove(this);

            newFolder.Nodes.Add(this);

            ParentFolder = newFolder;
            Location = newFolder.Location;
        }

        public void Split(int newStackSize)
        {
            if (newStackSize > Quantity)
                return;

            ItemStack splitItem = new ItemStack(this)
            {
                Quantity = newStackSize
            };

            Quantity -= newStackSize;

            Root.Nodes.Add(splitItem);
        }

        public void Merge(ItemStack mergeInto)
        {
            if (this.Location != mergeInto.Location
                ||
                this.Type.ToLower() != mergeInto.Type.ToLower())
                return ;

            mergeInto.Quantity += Quantity;

            if (this.ParentFolder != null)
                ParentFolder.Nodes.Remove(this);
            else
                Root.Nodes.Remove(this);
        }

        public void UseOne(RootNode usedNode)
        {
            if(Quantity > 1)
            {
                Quantity--;
            }
            else
            {
                if (this.ParentFolder != null)
                    ParentFolder.Nodes.Remove(this);
                else
                    Root.Nodes.Remove(this);
            }

            usedNode.Nodes.Add(new ItemStack(this) { Quantity = 1 });
        }
    }
}