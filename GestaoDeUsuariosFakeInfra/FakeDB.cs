using System.Collections.Generic;
using GestaoDeUsuarios.Domain.Entities;

namespace GestaoDeUsuariosFakeInfra
{
    public static class FakeDB 
    {   
        public static List<User> Users = new List<User>();

        public static void SaveChanges(List<User> users) => Users = users;
    }
}