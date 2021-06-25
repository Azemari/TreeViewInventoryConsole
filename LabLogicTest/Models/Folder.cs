using System.Collections.Generic;

namespace LabLogicTest.Models
{
    public class Folder : BaseItem
    {
        public List<BaseItem> Nodes { get; set; }

        public Folder(string name, Location location, Folder parentFolder, RootNode rootNode, List<BaseItem> itemStacks)
           : base(name, location, parentFolder, rootNode)
        {
            Nodes = itemStacks;
            Root = rootNode;
        }

        public void MoveTo(RootNode newCategory)
        {
            if (this.Location > newCategory.Category)
                return;

            //Setting it to the root node so it no longer has a parent
            ParentFolder = null;
            Location = newCategory.Category;

            RecursivelyUpdateNodeLocation(Nodes, Location);
            Root.Delete(this);
            newCategory.Nodes.Add(this);

        }

        public void MoveTo(Folder parentFolder)
        {
            if (this.Location > parentFolder.Location)
                return;

            ParentFolder = parentFolder;
            Location = parentFolder.Location;

            RecursivelyUpdateNodeLocation(parentFolder.Nodes, parentFolder.Location);
            parentFolder.Nodes.Add(this);
            Root.Nodes.Remove(this);
        }

        public void RecursivelyUpdateNodeLocation(List<BaseItem> currentFolder, Location newLocation)
        {
            foreach (BaseItem node in currentFolder)
            {
                node.Location = newLocation;
            }
            foreach (BaseItem folder in currentFolder)
            {
                if (folder is Folder nestedFolder)
                {
                    RecursivelyUpdateNodeLocation(nestedFolder.Nodes, newLocation);
                }
            }
        }
    }
}
