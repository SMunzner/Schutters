using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SchuttersData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchuttersData.Repositories
{
    public class SQLLidRepository : ILidRepository
    {
        private readonly SchuttersContext context;

        public SQLLidRepository(SchuttersContext context)
        {
            this.context = context;
        }

        public Lid Add(Lid lid)
        {
            context.Leden.Add(lid);
            context.SaveChanges();
            return lid;
        }

        public Lid? Delete(long id)
        {
            Lid? lid = context.Leden.Find(id);
            if(lid != null)
            {
                context.Leden.Remove(lid);
                context.SaveChanges();
            }
            return lid;
        }

        public IEnumerable<Lid> GetLeden()
        {
            return context.Leden.Include(x => x.ClubNavigation);
        }

        public Lid? GetLid(long id)
        {
            return context.Leden.Find(id);
        }

        public Lid Update(Lid gewijzigdLid)
        {
            context.Update(gewijzigdLid);
            context.SaveChanges();
            return gewijzigdLid;
        }


        //EDIT
        public void EditLid(Lid gewijzigdLid)
        {
            Lid? lid = (from leden in context.Leden
                        where leden.Lidnummer == gewijzigdLid.Lidnummer
                        select leden).FirstOrDefault();

            if(lid != null)
            {
                lid.Naam = gewijzigdLid.Naam;
                lid.Voornaam = gewijzigdLid.Voornaam;
                lid.Geslacht = gewijzigdLid.Geslacht;
                lid.Niveau = gewijzigdLid.Niveau;
                lid.Club = gewijzigdLid.Club;

                context.SaveChanges();
            }
            else { throw new Exception("Lid not found"); }
        }

        //SEARCH
        public IEnumerable<Lid> SearchVoorLeden(string Voornaam, string Naam, string Geslacht, string Niveau, string ClubNaam)
        {
            ////var alleLedenMetModifiers = (from leden in context.Leden
            ////                            where leden.Voornaam.Contains(Voornaam) && 
            ////                            leden.Naam.Contains(Naam) && 
            ////                            leden.Geslacht.Contains(Geslacht) && 
            ////                            leden.Niveau.Contains(Niveau)
            ////                            select leden).ToList();

            //var alleLedenMetModifiers = from clubs in context.Clubs
            //                            join leden in context.Leden on clubs.Stamnummer equals leden.Club into lidClubs
            //                            from lidClub in lidClubs
            //                            where lidClub.Naam.Contains(Naam) &&
            //                            lidClub.Voornaam.Contains(Voornaam) &&
            //                            lidClub.Geslacht.Contains(Geslacht) &&
            //                            lidClub.Niveau.Contains(Niveau) &&
            //                            lidClub.Club 

            //return alleLedenMetModifiers;

            IQueryable<Lid> qwery = null;
            if (Voornaam != null)
            {
                    qwery = context.Leden.Where(l => l.Voornaam.Contains(Voornaam));
            }
                
            if(Naam != null)
            {
                if (qwery == null)
                    qwery = context.Leden.Where(l => l.Naam.Contains(Naam));
                else
                    qwery = qwery.Where(l => l.Naam.Contains(Naam));
            }

            if (Geslacht != null)
            {
                if (qwery == null)
                    qwery = context.Leden.Where(l => l.Geslacht.Contains(Geslacht));
                else
                    qwery = qwery.Where(l => l.Geslacht.Contains(Geslacht));
            }

            if (Niveau != null)
            {
                if (qwery == null)
                    qwery = context.Leden.Where(l => l.Niveau.Contains(Niveau));
                else
                    qwery = qwery.Where(l => l.Niveau.Contains(Niveau));
            }

            if (ClubNaam != null)
            {
                if (qwery == null)
                    qwery = context.Leden.Where(lid => lid.ClubNavigation.Naam.Contains(ClubNaam));
                else
                    qwery = qwery.Where(lid => lid.ClubNavigation.Naam.Contains(ClubNaam));
            }


            if (qwery != null)
                return qwery.Include(i => i.ClubNavigation).ToList();
            else
                return context.Leden.Include(i => i.ClubNavigation).ToList();

        }
    }
}
