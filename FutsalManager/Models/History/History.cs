namespace FutsalManager.Models.History;

[Serializable]
public class History
{
    public History(int modelId, string modelIdentifier, string modelType, string actionName, DateTime dateWhenHappened)
    {
        ModelId = modelId;
        ActionName = actionName;
        ModelType = modelType;
        ModelIdentifier = modelIdentifier;
        DateWhenHappened = dateWhenHappened;
    }
    
    protected Guid Guid => Guid.NewGuid();

    public DateTime DateWhenHappened { get; set; }

    public int ModelId { get; set; }

    public string ActionUrl
    {
        get
        {
            string re = string.Empty;

            if (string.IsNullOrEmpty(ModelType)) return re;

            if (ModelType.Contains("player"))
                re = $"/players/details/{ModelId}";
            if (ModelType.Contains("team"))
                re = $"/teams/details/{ModelId}";
            if (ModelType.Contains("transfer"))
                re = $"/transfers/details/{ModelId}";

            return re;
        }
    }

    public string ModelType { get; set; }
    public string ModelIdentifier { get; set; }
    public string ActionName { get; set; }
    public string CustomImagePath { get; set; } = string.Empty;
}