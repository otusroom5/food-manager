﻿using UserAuth.DataAccess.Abstractions;
using UserAuth.DataAccess.Entities;

namespace UserAuth.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public Guid Create(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public User? FindUserByName(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}