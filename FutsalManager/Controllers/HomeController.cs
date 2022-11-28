using FutsalManager.Models.History;
using FutsalManager.Services.PlayerService;
using FutsalManager.Services.TeamService;
using FutsalManager.Services.TransferService;
using FutsalManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging;

namespace FutsalManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPlayerService _playerService;
    private readonly ITeamService _teamService;
    private readonly ITransferService _transferService;

    public HomeController(IPlayerService playerService, ITeamService teamService, ITransferService transferService,ILogger<HomeController> logger)
    {
        _playerService = playerService;
        _teamService = teamService;
        _transferService = transferService;
        _logger = logger;
    }
    
    // GET: index?searchfilter=barcelona 
    public async Task<IActionResult> Index(string searchstring)
    {
        _logger.LogInformation("Index page was requested");
        
        var foundPlayers = new List<Player>();
        var foundTeams = new List<Team>();
        
        
        if (string.IsNullOrEmpty(searchstring))
        {
            foundPlayers = await _playerService.GetPlayersAsync();
            foundTeams = await _teamService.GetTeamsAsync();
        }
        else
        {
            foundPlayers = await _playerService.SearchPlayersAsync(searchstring);
            foundTeams = await _teamService.SearchTeamsAsync(searchstring);
        }

        ViewBag.Players = foundPlayers;
        ViewBag.Teams = foundTeams;
        
        _logger.LogInformation("Index page was successfully loaded");
        
        return View();
    }
    
    // GET: inactiveplayers
    public async Task<IActionResult> InactivePlayers()
    {
        var players = await _playerService.GetPlayersAsync();
        var teams = await _teamService.GetTeamsAsync();

        var freeTeams = new List<Team>();
        foreach (var team in teams)
        {
            if(await _teamService.CheckFreeSpaceInTeam(team.Id))
                freeTeams.Add(team);
        }

        ViewData["players"] = players;
        ViewData["teams"] = freeTeams;

        return View();
    }
    
    // POST: inactiveplayers
    [HttpPost]
    public async Task<IActionResult> SaveInactivePlayers(InactivePlayersViewModel model)
    {
        int length = model.Players.Count;
        
        for (int i = 0; i < length; i++)
        {
            var playerid = model.Players[i];
            var teamid = model.Teams[i];
            var player = await _playerService.GetPlayerAsync(playerid);
            var team = await _teamService.GetTeamAsync(teamid);
        
            var success = await _transferService.CreateTransferAsync(player, team);
        }
        
        return RedirectToAction(nameof(InactivePlayers));
    }
}