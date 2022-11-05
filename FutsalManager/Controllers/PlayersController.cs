using FutsalManager.Services.PlayerService;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FutsalManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly DataContext _context;
        private readonly IPlayerService _playerService;

        public PlayersController(DataContext context, IPlayerService playerService)
        {
            _context = context;
            _playerService = playerService;
        }

        // GET: Players
        public async Task<IActionResult> Index(int? page)
        {
            var players = await _playerService.GetPlayersAsync();

            var pageNumber = page ?? 1;

            ViewBag.PagedPlayersList = players.ToPagedList(pageNumber, 10);

            return View(players);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var player = await _playerService.GetPlayerAsync(id);

            if (player == null)
                return RedirectToAction(nameof(Index));

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City,Email,PhoneNumber")] Player player)
        {
            if (ModelState.IsValid)
            {
                await _playerService.CreatePlayerAsync(player);
                return RedirectToAction(nameof(Index));
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var player = await _playerService.GetPlayerAsync(id);

            if (player == null)
                return RedirectToAction(nameof(Index));

            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,FirstName,LastName,Birthday,Age,IsActive,AttributeId,TeamId,PositionId")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            bool success = false;

            if (ModelState.IsValid)
            {
                success = await _playerService.UpdatePlayerAsync(player);

                return RedirectToAction(nameof(Index));
            }

            if (success)
                return RedirectToAction(nameof(Index));
            else
                return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var player = await _playerService.GetPlayerAsync(id);

            if (player == null)
                return RedirectToAction(nameof(Index));

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _playerService.DeletePlayerAsync(id);
            if (success)
                return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Delete), id);
        }

        #region PARTIALS

        [HttpGet]
        public async Task<IActionResult> PlayersListPartial(int? page)
        {
            var players = await _playerService.GetPlayersAsync();

            var pageNumber = page ?? 1;

            var pagedPlayers = await players.ToPagedListAsync(pageNumber, 10);
            
            return PartialView("_PlayersList", pagedPlayers);
        }

        #endregion
    }
}