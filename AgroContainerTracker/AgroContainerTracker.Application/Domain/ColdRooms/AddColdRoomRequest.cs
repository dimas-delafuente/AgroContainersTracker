namespace AgroContainerTracker.Domain
{
    public class AddColdRoomRequest
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public double Surface { get; set; }
        public double Capacity { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
