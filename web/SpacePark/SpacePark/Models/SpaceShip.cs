using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace SpacePark
{
    public class Spaceship
    {
        public int SpaceshipID { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public async static Task<Spaceship> CreateStarshipFromAPI(string url)
        {
            var spaceship = new Spaceship();
            var response = await ParkingEngine.GetSpaceShipData(url);

            spaceship.Name = response.Name;
            spaceship.Length = response.Length;

            return spaceship;
        }
    }
}
