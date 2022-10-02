namespace SmartAssistant.Data.Models.SmartDevices;

public class Pet
{
    public string Name { get; set; }
    /// <summary>
    /// Maximum 100 minimum 0
    /// </summary>
    public int Hunger { get; set; }

    /// <summary>
    /// Maximum 100 minimum 0
    /// </summary>
    public int Thirst { get; set; }

    public Pet(string name, int hunger = 0, int thirst = 0)
    {
        Name = name;
        Hunger = hunger;
        Thirst = thirst;
    }
}