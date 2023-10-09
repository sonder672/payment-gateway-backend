using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Senior.Services.User.Contracts;
using Senior.Services.User.DTOs.OUT;

namespace Senior.Infrastructure.Database.Repository
{
    public class User : IUserRepository
    {
        private readonly Context _context;

        public User(Context context)
        {
            _context = context;
        }

        public async Task Create(Services.User.DTOs.IN.Register userData)
        {
            var newUser = new Models.User
            {
                Email = userData.Email,
                Password = userData.Password,
                Id = userData.Id,
                Name = userData.Name
            };

            this._context.User.Add(newUser);

            await this._context.SaveChangesAsync();
        }

        public async Task<Login?> GetByEmail(string email)
        {
            return await this._context.User
                .Where(x => x.Email == email)
                .Select(x => new Login(
                    x.Password,
                    x.Id,
                    x.CanSell
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UserCanSell(string uuid)
        {
            var user = await this._context.User
                .Where(u => u.Id == uuid)
                .Select(u => u.CanSell)
                .FirstOrDefaultAsync();

            return user ?? false;
        }
    }
}