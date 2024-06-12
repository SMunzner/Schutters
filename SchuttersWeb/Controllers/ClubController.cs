using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchuttersData.Models;
using SchuttersServices;

namespace SchuttersWeb.Controllers
{
    [Authorize]
    public class ClubController : Controller
    {
        private readonly ClubService clubService;

        public ClubController(ClubService clubService)
        {
            this.clubService = clubService;
        }

        public IActionResult Index()
        {
            return View(clubService.GetAllClubs());
        }


        public IActionResult Detail(int id)
        {
            Club? club = clubService.GetClub(id);
            if (club == null)
                ViewBag.ErrorMessage = $"Geen info gevonden voor club met id {id}";
            return View(club);
        }

        //EDIT
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int id)
        {
            Club? club = clubService.GetClub(id);
            if (club == null)
            {
                ViewBag.ErrorMessage = $"Geen info gevonden voor club met id {id}";
            }
            return View(club);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Club club)
        {
            if(ModelState.IsValid)
            {
                clubService.EditClub(club);
            }
            return RedirectToAction("Index");
        }


        //CREATE
        [Authorize(Roles = "Admin")]
        public IActionResult Create() 
        { 
            Club club = new Club();
            return View(club); 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateClub(Club club)
        {
            if (ModelState.IsValid)
            {
                var clubMetStamnr = clubService.GetAllClubs().Where(x => x.Stamnummer == club.Stamnummer).FirstOrDefault();
                if (clubMetStamnr != null)
                {
                    ModelState.AddModelError("Stamnummer" , "Een club met dit stamnummer komt al voor in de database. Kies een ander nummer.");
                    return View(nameof(Create), club);
                }

                //create club
                clubService.CreateClub(club);
            }
            return RedirectToAction(nameof(Index));
        }

        //DELETE
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var club = clubService.GetClub(id);

            if (club == null)
                return NotFound();

            clubService.DeleteClub(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
