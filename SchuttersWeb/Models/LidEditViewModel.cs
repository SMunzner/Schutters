using Microsoft.AspNetCore.Mvc.Rendering;
using SchuttersData.Models;

namespace SchuttersWeb.Models
{
    public class LidEditViewModel
    {
        public Lid Lid { get; set; }

        public IEnumerable<SelectListItem> Clubs { get; set; }

        //public long LidNr { get; set; }

        //public int GekozenClubNr { get; set; }

    }
}
