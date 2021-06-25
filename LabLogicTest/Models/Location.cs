namespace LabLogicTest.Models 
{
    // Drew - enum is definitely the right way to go to allow comparison when moving
    public enum Location
    {
        AvailableToOrder,
        Ordered,
        Received,
        Stored,
        Used
    }
}
