using SchuttersData.Models;
using SchuttersServices;
using System.ComponentModel.DataAnnotations;

namespace SchuttersWeb.Models
{
    public class LidZoekenViewModel
    {
        [Display(Name = "Deel van de voornaam")]
        public string? Voornaam { get; set; }

        [Display(Name = "Deel van de naam")]
        public string? Naam { get; set;}

        [Display(Name = "Geslacht")]
        public string? Geslacht { get; set;}

        [Display(Name = "Niveau")]
        public string? Niveau { get; set;}

        [Display(Name = "Deel van de clubnaam")]
        public string? ClubNaam { get; set;}

        public List<Lid> Leden {  get; set;} = new List<Lid>();

        //public IEnumerable<Club> Clubs { get; set;}
        
    }
}
