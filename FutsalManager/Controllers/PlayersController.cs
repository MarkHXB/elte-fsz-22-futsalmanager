using FutsalManager.Services.PlayerService;
using FutsalManager.Services.PositionService;
using FutsalManager.Services.TeamService;
using FutsalManager.Services.TransferService;
using Microsoft.AspNetCore.Mvc;

namespace FutsalManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly DataContext _context;
        private readonly IPlayerService _playerService;
        private readonly IPositionService _positionService;
        private readonly ITeamService _teamService;
        private readonly ITransferService _transferService;

        public PlayersController(DataContext context,
            IPlayerService playerService,
            IPositionService positionService,
            ITeamService teamService,
            ITransferService transferService)
        {
            _context = context;
            _playerService = playerService;
            _positionService = positionService;
            _teamService = teamService;
            _transferService = transferService;
        }

        // GET: Players
        public async Task<IActionResult> Index(bool? operationExecutedSuccessfully)
        {
            var players = await _playerService.GetPlayersAsync();

            // var pageNumber = page ?? 1;
            //
            // ViewBag.PagedPlayersList = players.ToPagedList(pageNumber, 10);

            if (operationExecutedSuccessfully != null)
                ViewBag.ErrorMessage = (operationExecutedSuccessfully == false
                    ? "The operation you requested didn't executed successfully."
                    : string.Empty);
            
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
        public async Task<IActionResult> Create()
        {
            var positions = await _positionService.GetPositionsAsync();
            var teams = await _teamService.GetTeamsAsync();
            
            ViewBag.Positions = positions;
            ViewBag.Teams = teams;
            
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            if (!ModelState.IsValid || !PlayerFunctions.ValidateNationality(player.Nationality))
                return View(player);

            player.IsActive = true;

            if (await _transferService.CreateTransferForNewbies(player) == false)
            {
                return RedirectToAction(nameof(Index));
            }
            
            //await _playerService.CreatePlayerAsync(player);
            return RedirectToAction(nameof(Index));
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var player = await _playerService.GetPlayerAsync(id);

            if (player == null)
                return RedirectToAction(nameof(Index));

            var positions = await _positionService.GetPositionsAsync();

            ViewBag.Positions = positions;

            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            bool success = false;

            if (ModelState.IsValid)
            {
                success = await _playerService.UpdatePlayerAsync(player);
            }

            if (!success) return RedirectToAction(nameof(Edit), player.Id);
            
            return RedirectToAction(nameof(Index));

        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var player = await _playerService.GetPlayerAsync(id);

            if (player == null)
                return RedirectToAction(nameof(Index));

            return View(player);
        }

        // POST: Players/SetActivity?id=5&activity=true
        public async Task<IActionResult> SetActivity(int? id, bool activity)
        {
            if (id == null)
            {
                return NotFound();
            }

            bool? success = await _playerService.SetActivityAsync(id, activity);

            return RedirectToAction(nameof(Index), success);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _playerService.DeletePlayerAsync(id);
            
            if (!success) return RedirectToAction(nameof(Delete), id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}