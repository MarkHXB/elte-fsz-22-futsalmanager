using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FutsalManager.Data;
using FutsalManager.Models;
using FutsalManager.Services.PlayerService;
using FutsalManager.Services.TeamService;
using FutsalManager.Services.TransferService;
using X.PagedList;

namespace FutsalManager.Controllers
{
    public class TransfersController : Controller
    {
        private readonly DataContext _context;
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;
        private readonly ITransferService _transferService;

        public TransfersController(DataContext context, IPlayerService playerService, ITeamService teamService, ITransferService transferService)
        {
            _context = context;
            _playerService = playerService;
            _teamService = teamService;
            _transferService = transferService;
        }

        // GET: Transfers
        [HttpGet]
        [Route("/Transfer")]
        [Route("/Transfers")]
        public async Task<IActionResult> PlayersIndex()
        {
            var players = await _playerService.GetPlayersAsync();
            
            players = await players.Where(p => p.IsActive == false).ToListAsync();
            
            return View(players);
        }
        
        public async Task<IActionResult> TeamsIndex(int? playerid)
        {
            if (playerid == null) return RedirectToAction(nameof(PlayersIndex));
            
            if(await _playerService.IsActive(playerid)) return RedirectToAction(nameof(PlayersIndex));
            
            var teams = await _teamService.GetTeamsAsync();

            var freeTeams = await teams.Where(t => t.Players.Count < 5).ToListAsync();

            ViewBag.PlayerId = playerid;
            
            return View(freeTeams);
        }
        
        [HttpPost]
        public async Task<IActionResult> Transfer(int? playerid, int? teamid)
        {
            if (!await _teamService.CheckFreeSpaceInTeam(teamid)) return RedirectToAction(nameof(TeamsIndex), playerid);
            
            if (playerid == null || teamid == null) return RedirectToAction(nameof(TeamsIndex), playerid);

            var player = await _playerService.GetPlayerAsync(playerid);
            var team = await _teamService.GetTeamAsync(teamid);

            bool result = await _transferService.CreateTransferAsync(player, team);

            if (result) // itt majd a transfer detail-éhez
                return RedirectToAction("Index", "Home");
            return RedirectToAction(nameof(TeamsIndex), playerid);
        }
    }
}
