using SchuttersData.Models;
using SchuttersData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuttersServices
{
    public class LidService
    {
        private readonly ILidRepository lidRepository;
        private readonly IClubRepository clubRepository;

        public LidService(ILidRepository lidRepository, IClubRepository clubRepository)
        {
            this.lidRepository = lidRepository;
            this.clubRepository = clubRepository;
        }

        public IEnumerable<Lid> GetAllLeden()
        {
            return lidRepository.GetLeden();
        }

        public Lid? GetLid(long id)
        {
            return lidRepository.GetLid(id);
        }

        public void DeleteLid(long id)
        {
            lidRepository.Delete(id);
        }

        //EDIT
        public void EditLid(Lid gewijzigdLid)
        {
            lidRepository.EditLid(gewijzigdLid);
        }

        //CREATE
        public void CreateLid(Lid lid)
        {
            lidRepository.Add(lid);
        }

        //Search
        public IEnumerable<Lid> SearchVoorLeden(string Voornaam, string Naam, string Geslacht, string Niveau, string ClubNaam)
        {          
            return lidRepository.SearchVoorLeden(Voornaam, Naam, Geslacht, Niveau, ClubNaam);
        }
    }
}
