using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark
{
    public class Spaceship
    {
        public int SpaceshipID { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public static Spaceship CreateStarshipFromAPI(string url)
        {
            var spaceship = new Spaceship();
            var response = ParkingEngine.GetSpaceShipData(url);

            spaceship.Name = response.Name;
            spaceship.Length = response.Length;

            return spaceship;
        }
    }
}
