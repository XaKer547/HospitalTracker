namespace HospitalTrackerApi.Data.Entities
{
    public class Tracker
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public Room LastVisitedRoom { get; set; }
        public bool IsInsideRoom { get; set; }
        public DateTime LastTrackedTime { get; set; }
    }
}