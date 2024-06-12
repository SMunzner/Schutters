using SchuttersData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuttersData.Repositories
{
    public interface ILidRepository
    {
        Lid? GetLid(long id);
        IEnumerable<Lid> GetLeden();
        Lid Add(Lid lid);
        Lid Update(Lid gewijzigdLid);
        Lid? Delete(long id);

        //EDIT 
        public void EditLid(Lid gewijzigdLid);

        //SEARCH
        IEnumerable<Lid> SearchVoorLeden(string Voornaam, string Naam,
            string Geslacht, string Niveau, string ClubNaam);
    }
}
