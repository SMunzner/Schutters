using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchuttersData.Models;
using SchuttersServices;
using SchuttersWeb.Models;

namespace SchuttersWeb.Controllers
{
    [Authorize]
    public class LidController : Controller
    {
        private readonly LidService lidService;
        private readonly ClubService clubService;

        public LidController(LidService lidService, ClubService clubService)
        {
            this.lidService = lidService;
            this.clubService = clubService;
        }

        [Authorize(Roles ="Leden, Admin")]
        public IActionResult Index()
        {
            LidIndexViewModel model = new LidIndexViewModel();
            model.Leden = lidService.GetAllLeden().ToList();

            return View(model);
        }

        [Authorize(Roles = "Leden, Admin")]
        public IActionResult Detail(long id)
        {
            LidDetailsViewModel model = new LidDetailsViewModel();
            
            model.Lid = lidService.GetLid(id);
            model.Club = clubService.GetClub(model.Lid.Club);

            if(model.Lid == null)
            {
                ViewBag.ErrorMessage = $"Geen info gevonden voor lid met lidnummer {id}";
            }
            return View(model);
        }

        //EDIT
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(long id)
        {
            var lid = lidService.GetLid(id);
            if(lid != null)
            {
                var clubsSelectList = new List<SelectListItem>();
                foreach(var club in clubService.GetAllClubs())
                {
                    clubsSelectList.Add(
                        new SelectListItem
                        {
                            Text = club.Naam,
                            Value = club.Stamnummer.ToString()
                        });
                }
                var model = new LidEditViewModel
                {
                    Lid = lid,
                    Clubs = clubsSelectList
                };
                return View(model);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Lid lid)
        {
            if(ModelState.IsValid)
            {
                lidService.EditLid(lid);
            }
            return RedirectToAction(nameof(Index));
        }

        //CREATE
        [Authorize(Roles = "Admin")]
        public IActionResult Create() 
        { 
            var clubsSelectList = new List<SelectListItem>();

            foreach (var club in clubService.GetAllClubs())
            {
                clubsSelectList.Add(
                    new SelectListItem
                    {
                        Text = club.Naam,
                        Value = club.Stamnummer.ToString()
                    });
            }
            var model = new LidEditViewModel
            {
                Lid = new Lid(),
                Clubs = clubsSelectList
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateLid(Lid lid)
        {
            if(ModelState.IsValid)
            {
                //als lid met lidnummer al bestaat, error
                var lidMetLidnummer = lidService.GetAllLeden().Where(x => x.Lidnummer == lid.Lidnummer).FirstOrDefault();
                if (lidMetLidnummer != null)
                {
                    ModelState.AddModelError("Lidnummer", "Een lid met dit nummer komt al voor in de database. Kies een ander nummer.");
                    return View(nameof(Create), lid);
                }

                lidService.CreateLid(lid);
            }
            return RedirectToAction(nameof(Index));
        }

        //DELETE
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            //if (id == null)
            //    return NotFound();

            var lid = lidService.GetLid(id);

            if (lid == null)
                return NotFound();

            lidService.DeleteLid(id);

            return RedirectToAction(nameof(Index));
        }


        //SEARCH
        [Authorize(Roles = "Leden, Admin")]
        public IActionResult Search()
        {
            return View(new LidZoekenViewModel());
        }

        [Authorize(Roles = "Leden, Admin")]
        public IActionResult SearchMetModifiers(LidZoekenViewModel form)
        {
            if (this.ModelState.IsValid)
            {
                //geef alle parameters mee voor query naar leden
                form.Leden = lidService.SearchVoorLeden(form.Voornaam, form.Naam, form.Geslacht, form.Niveau, form.ClubNaam).ToList();

                if (form.Leden.Count == 0)
                    ViewBag.ErrorMessage = $"Er zijn geen leden met deze voorwaarden.";
                else
                    ViewBag.ErrorMessage = string.Empty;
            }
            return View(nameof(Search), form);
        }


    }
}
