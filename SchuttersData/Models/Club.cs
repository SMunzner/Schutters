using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchuttersData.Models;

public partial class Club
{
    [Display(Name= "Stamnr")]
    public int Stamnummer { get; set; }
    
    [Display(Name = "Clubnaam")]
    public string Naam { get; set; } = null!;
    
    [StringLength(4)]
    public string Postcode { get; set; } = null!;

    public string Gemeente { get; set; } = null!;

    public string? Adres { get; set; }

    
    
    //navigation property
    public virtual ICollection<Lid> Leden { get; set; } = new List<Lid>();
}
