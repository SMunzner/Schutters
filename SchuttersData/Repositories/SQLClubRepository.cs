using Microsoft.EntityFrameworkCore;
using SchuttersData.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuttersData.Repositories
{
    public class SQLClubRepository : IClubRepository
    {
        private readonly SchuttersContext context;

        public SQLClubRepository(SchuttersContext context)
        {
            this.context = context;
        }


        public Club Add(Club club)
        {
            context.Clubs.Add(club);
            context.SaveChanges();
            return club;
        }

        public Club? Delete(int id)
        {
            Club? club = context.Clubs.Find(id);
            if (club != null) 
            { 
                ////delete all members
                //foreach(var lid in club.Leden)
                //{
                //    context.Leden.Remove(lid);
                //}

                var alleLedenUitClub = (from lid in club.Leden
                                       where lid.Club == club.Stamnummer
                                       select lid).ToList();
                if(alleLedenUitClub != null)
                {
                    foreach(var lid in alleLedenUitClub)
                        context.Leden.Remove(lid);
                }

                context.Clubs.Remove(club);
                context.SaveChanges();
            }
            return club;
        }

        public Club? GetClub(int id)
        {
            return context.Clubs.Find(id);
        }

        public IEnumerable<Club> GetClubs()
        {
            return context.Clubs;
        }

        public Club Update(Club gewijzigdeClub)
        {
            context.Update(gewijzigdeClub);
            context.SaveChanges();
            return gewijzigdeClub;
        }


        //EDIT CLUB
        public void EditClub(Club gewijzigdeClub)
        {
            Club? club = (from clubs in context.Clubs
                          where clubs.Stamnummer == gewijzigdeClub.Stamnummer
                          select clubs).FirstOrDefault();

            //Club? club = context.Clubs.Find(gewijzigdeClub.Stamnummer);

            if (club != null)
            {
                club.Naam = gewijzigdeClub.Naam;
                club.Postcode = gewijzigdeClub.Postcode;
                club.Gemeente = gewijzigdeClub.Gemeente;
                club.Adres = gewijzigdeClub.Adres;

                context.SaveChanges();
            }
            else { throw new Exception("Club not found"); }

        }
    }
}
