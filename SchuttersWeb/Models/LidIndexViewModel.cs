using SchuttersData.Models;

namespace SchuttersWeb.Models
{
    public class LidIndexViewModel
    {
        public IEnumerable<Lid> Leden { get; set; }

        public IEnumerable<Club> Clubs { get; set; }
    }
}
