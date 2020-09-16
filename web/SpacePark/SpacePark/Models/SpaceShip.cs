using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark
{
    public class Spaceship
    {
        public int SpaceShipID { get; set; }

        public string Name { get; set; }

        public string Length { get; set; }

        public Person PersonID { get; set; }

        public static Spaceship CreateStarshipFromAPI(string url)
        {
            var p = new Spaceship();
            var response =ParkingEngine.GetSpaceShipData(url);

            p.Name = response.Name;
            p.Length = response.Length;

            return p;
        }


    }
}
