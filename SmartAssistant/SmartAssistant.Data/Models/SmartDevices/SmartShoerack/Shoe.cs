namespace SmartAssistant.Data.Models.SmartDevices;

public class Shoe
{
    ShoeCategory Category { get; set; }
    /// <summary>
    /// The shoe style (e.g. Sneakers, Oxford etc)
    /// </summary>
    public string Style { get; set; }
    public string? ImageFileName { get; set; }

    public Shoe(ShoeCategory category, string style)
    {
        Category = category;
        Style = style;
    }
}