namespace LabLogicTest.Models
{
    public class Item : BaseItem 
    {
        public string Type { get; set; }

        public Item(string name, Location location, string type)
           : base(name, location)
        {
            Type = type;
        }
    }
}
