namespace SmartAssistant.Data.Models.SmartDevices;

public class SmartFeeder
{
    public List<Pet> AllPets { get; set; }
    /// <summary>
    /// Maximum 100 minimum 0
    /// </summary>
    public int FoodQuantity { get; set; }
    /// <summary>
    /// Maximum 100 minimum 0
    /// </summary>
    public int WaterQuantity { get; set; }

    public SmartFeeder(int foodQuantity = 100, int waterQuantity = 100, List<Pet> allPets = null)
    {
        FoodQuantity = foodQuantity;
        WaterQuantity = waterQuantity;

        if (allPets == null)
        {
            AllPets = new List<Pet>();
        }
        else
        {
            AllPets = allPets;
        }    
    }
}