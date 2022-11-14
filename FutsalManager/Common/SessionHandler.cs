using FutsalManager.Models.History;
using Newtonsoft.Json;

namespace FutsalManager.Common;

public static class SessionHandler
{
    public static List<History> GetHistorySession(HttpContext context)
    {
        var playershistory = context.Session.GetString("playershistory");
        var teamshistory = context.Session.GetString("teamshistory");
        var transfershistory = context.Session.GetString("transfershistory");
        
        List<History> history = new();
        List<History> players = new();
        List<History> teams = new();
        List<History> transfers = new();
        
        if(!string.IsNullOrEmpty(playershistory))
            players = JsonConvert.DeserializeObject<List<History>>(playershistory);
        if(!string.IsNullOrEmpty(teamshistory)) 
            teams = JsonConvert.DeserializeObject<List<History>>(teamshistory);
        if(!string.IsNullOrEmpty(transfershistory)) 
            transfers = JsonConvert.DeserializeObject<List<History>>(transfershistory);
        
        history.AddRange(players.Select(p=>p));
        history.AddRange(teams.Select(p=>p));
        history.AddRange(transfers.Select(p=>p));
        
        return history;
    }
    
    public static List<History> GetHistorySession(string key,HttpContext context)
    {
        var str = context.Session.GetString(key);

        List<History> history = new();

        if (string.IsNullOrEmpty(str))
            return history;
        else
            history = JsonConvert.DeserializeObject<List<History>>(str);
        
        return history;
    }
    
    public static void AppendToHistorySession(string key, History model,HttpContext context)
    {
        var clsit = GetHistorySession(key,context);
        clsit.Add(model);
        var str = JsonConvert.SerializeObject(clsit);
        context.Session.Remove(key);
        context.Session.SetString(key,str);
    }
}