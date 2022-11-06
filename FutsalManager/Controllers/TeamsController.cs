using Microsoft.AspNetCore.Mvc;
using FutsalManager.Services.TeamService;

namespace FutsalManager.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        #region CONTROLLERS

         // GET: Teams
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return Ok();
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var team = await _teamService.GetTeamAsync(id);

            if (team == null)
                return RedirectToAction(nameof(Index));

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City,Email,PhoneNumber,FileModel")] Team team)
        {
            if (!ModelState.IsValid) return View(team);

            FileUpload.UploadTeamLogo(team);

            await _teamService.CreateTeamAsync(team);

            return RedirectToAction(nameof(Index));
        }


        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var team = await _teamService.GetTeamAsync(id);

            if (team == null)
                return RedirectToAction(nameof(Index));
            
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(team);

            FileUpload.UploadTeamLogo(team);

            var success = await _teamService.UpdateTeamAsync(team);

            if (success)
                return RedirectToAction(nameof(Index));

            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var team = await _teamService.GetTeamAsync(id);

            if (team == null)
                return RedirectToAction(nameof(Index));

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _teamService.DeleteTeamAsync(id);
            if (success)
                return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Delete), id);
        }

        #endregion

        #region PARTIALS
        public async Task<IActionResult> TeamsListPartial()
        {
            var teams = await _teamService.GetTeamsAsync();

            return PartialView("_TeamsList", teams);
        }

        public IActionResult FilterTablePartial()
        {
            var model = new FilterViewModel()
            {
                FilterOptions = new List<string>
                {
                    "Name",
                    "City"
                },
                FilterTitles = new List<string>()
                {
                    "Featured",
                    "Most popular",
                    "Quality"
                },
                FilterModelType = typeof(Team),
                ShowDetailedFilter = true
            };
            return PartialView("_FilterTable", model);
        }
        
       
        #endregion
    }
}