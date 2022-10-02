using SmartAssistant.Data.Models.SmartDevices;

namespace SmartAssistant.Services.SmartDevices;

public class SmartFeederRepository
{
    private const string PETS_FILE_NAME = "Pets.json";
    private List<Pet> _pets;

    public async Task<List<Pet>> GetAllPets()
    {
        _pets = await Serializer.DeserializeJsonFile<List<Pet>>(PETS_FILE_NAME);

        if (_pets == null)
        {
            return new List<Pet>();
        }
        else
        {
            return _pets;
        }
    }

    public async Task<Pet> GetPetByName(string name)
    {
        if (_pets.Count == 0)
        {
            _pets = await GetAllPets();
        }

        var pet = _pets.FirstOrDefault(p => p.Name == name);

        if (pet == null)
        {
            throw new InvalidOperationException("The given pet name doesn't exist in the list");
        }

        return pet;
    }

    public async Task AddPet(Pet pet)
    {
        if (_pets.Count == 0)
        {
            _pets = await GetAllPets();
        }

        _pets.Add(pet);
        await Serializer.SaveJsonToFile(PETS_FILE_NAME, _pets);
    }

    public async Task DeletePet(Pet pet)
    {
        if (_pets.Count == 0)
        {
            _pets = await GetAllPets();
        }

        _pets.Remove(pet);
        await Serializer.SaveJsonToFile(PETS_FILE_NAME, _pets);
    }
}