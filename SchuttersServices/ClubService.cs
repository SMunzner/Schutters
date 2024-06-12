using SchuttersData.Models;
using SchuttersData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuttersServices
{
    public class ClubService
    {
        private readonly IClubRepository clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            this.clubRepository = clubRepository;
        }

        public IEnumerable<Club> GetAllClubs()
        {
            return clubRepository.GetClubs();
        }

        public Club? GetClub(int id)
        {
            return clubRepository.GetClub(id);
        }

        //Edit Club
        public void EditClub(Club gewijzigdeClub)
        {
            clubRepository.EditClub(gewijzigdeClub);
        }

        //Create Club
        public void CreateClub(Club club)
        {
            clubRepository.Add(club);
        }

        
        //Delete Club
        public void DeleteClub(int id)
        {
            clubRepository.Delete(id);
        }
    }
}
