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
            var spaceship = await ParkingEngine.GetSpaceShipData(url);

            return spaceship;
        }
    }
}
