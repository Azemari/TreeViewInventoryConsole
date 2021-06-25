using System.Collections.Generic;

namespace LabLogicTest.Models
{
    public class RootNode
    {
        public Location Category { get; set; }
        public List<BaseItem> Nodes { get; set; }

        public RootNode(Location category)
        {
            Category = category;
            Nodes = new List<BaseItem>();
        }

        public void Delete(BaseItem itemToDelete)
        {
            RecurisivelyFindItemToDelete(Nodes, itemToDelete);
        }

        public BaseItem RecurisivelyFindItemToDelete(List<BaseItem> currentFolder, BaseItem itemToDelete)
        {
            foreach (BaseItem node in currentFolder)
            {
                if (node == itemToDelete)
                {
                    currentFolder.Remove(node);
                    return node;
                }
            }
            foreach (BaseItem node in currentFolder)
            {
                if (node is Folder folder)
                {
                    BaseItem item = RecurisivelyFindItemToDelete(folder.Nodes, itemToDelete);
                    if (item == itemToDelete)
                    {
                        folder.Nodes.Remove(item);
                        return item;
                    }
                }
            }
            return null;
        }
    }
}
