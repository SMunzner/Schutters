using SchuttersData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuttersData.Repositories
{
    public interface IClubRepository
    {
        Club? GetClub(int id);
        IEnumerable<Club> GetClubs();
        Club Add(Club club);
        Club Update(Club club);
        Club? Delete(int id);


        //editClub
        public void EditClub(Club gewijzigdeClub);
    }
}
