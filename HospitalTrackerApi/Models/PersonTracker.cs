namespace HospitalTrackerApi.Models
{
    public class PersonTracker
    {
        public int PersonCode { get; set; }
        public string PersonRole { get; set; }
        public int LastSecurityPointNumber { get; set; }
        public string LastSecurityPointDirection { get; set; }
        public DateTime LastSecurityPointTime { get; set; }
    }
}