﻿using MySqlWebApp.Entities;
using MySqlWebApp.Requests.User;

namespace MySqlWebApp.Services
{
    public interface IUserService
    {
        User CreateUser(CreateRequest createRequest);
        User Delete(int id);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}