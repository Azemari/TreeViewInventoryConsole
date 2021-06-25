using System;

namespace LabLogicTest.Models
{
    public class BaseItem 
    {
        //Id could be either int or Guid
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public Folder ParentFolder { get; set; }
        public RootNode Root { get; set; }

        public BaseItem(string name, Location location, Folder parentFolder, RootNode rootNode)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
            ParentFolder = parentFolder;
            Root = rootNode;
        }
    }
}
