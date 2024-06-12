using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchuttersData.Models;

public partial class Lid
{
    [Display(Name = "Lidnr")]
    public long Lidnummer { get; set; }

    public string Naam { get; set; } = null!;

    public string Voornaam { get; set; } = null!;
    
    [StringLength(1)]
    public string? Geslacht { get; set; }
    
    [StringLength(1)]
    public string? Niveau { get; set; }

    public int Club { get; set; }



    //navigation property
    [Display(Name ="Club")]
    public virtual Club ClubNavigation { get; set; } = null!;
}
