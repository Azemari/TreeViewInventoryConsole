namespace LabLogicTest.Models
{
    public class BaseItem 
    {
        public string Name { get; set; }
        public Location Location { get; set; }

        public BaseItem(string name, Location location)
        {
            Name = name;
            Location = location;
        }
    }
}
