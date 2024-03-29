﻿using SmartAssistant.Data.Models;

namespace SmartAssistant.Services.UserAccount;

public class UserAuthRepository : IUserAuthRepository
{
    private readonly string fileName = "UserCredentials.json";

    public async Task<User> GetLoggedInUser()
    {
        return await Serializer.DeserializeJsonFile<User>(fileName);
    }

    public async Task<bool> Login(string username, string password)
    {
        var user = new User(username, password);
        await Serializer.SaveJsonToFile(fileName, user);
        return true;
    }

    public async Task<bool> Logout()
    {
        await Serializer.SaveJsonToFile(fileName, "");
        return true;
    }
}