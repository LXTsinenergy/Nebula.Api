namespace Nebula.Domain.Models
{
    public struct Location
    {
        public Location(float longitude, float latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
