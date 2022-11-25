using FutsalManager.Models.History;
using FutsalManager.Services.PlayerService;
using FutsalManager.Services.TeamService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging;

namespace FutsalManager.Controllers;

public class HomeController : Controller
{
    private readonly IPlayerService _playerService;
    private readonly ITeamService _teamService;

    public HomeController(IPlayerService playerService, ITeamService teamService)
    {
        _playerService = playerService;
        _teamService = teamService;
    }
    
    // GET: index?searchfilter=barcelona 
    public async Task<IActionResult> Index(string Searchstring)
    {
        var foundPlayers = new List<Player>();
        var foundTeams = new List<Team>();
        
        
        if (string.IsNullOrEmpty(Searchstring))
        {
            foundPlayers = await _playerService.GetPlayersAsync();
            foundTeams = await _teamService.GetTeamsAsync();
        }
        else
        {
            foundPlayers = await _playerService.SearchPlayersAsync(Searchstring);
            foundTeams = await _teamService.SearchTeamsAsync(Searchstring);
        }

        ViewBag.Players = foundPlayers;
        ViewBag.Teams = foundTeams;
        
        return View();
    }
    
    // GET: inactiveplayers
    public async Task<IActionResult> InactivePlayers()
    {
        var players = await _playerService.GetPlayersAsync();
        var teams = await _teamService.GetTeamsAsync();

        // ViewBag.Players = players;
        // ViewBag.Teams = teams;

        Test test = new()
        {
            InactivePlayers = players,
            Teams = teams
        };

        return View(test);
    }

    public async Task<IActionResult> Valami()
    {
        var players = await _playerService.GetPlayersAsync();
        var teams = await _teamService.GetTeamsAsync();

        Test test = new()
        {
            InactivePlayers = players,
            Teams = teams
        };
        
        return View(test);
    }

    [HttpPost]
    public IActionResult Valami(Test model)
    {
        return RedirectToAction(nameof(Valami),model);
    }
    

    // POST: inactiveplayers
    [HttpPost]
    public IActionResult SaveInactivePlayers(Test model)
    {
        return RedirectToAction(nameof(InactivePlayers));
    }
}