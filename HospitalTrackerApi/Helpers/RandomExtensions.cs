namespace HospitalTrackerApi.Helpers
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random)
        {
            return random.Next(2) == 0;
        }
    }
}
