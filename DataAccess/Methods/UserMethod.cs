using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Methods
{
    public class UserMethod:BaseMethod
    {
        public async Task<User> SaveUser(User user)
        {
            using(var database = GetDb())
            {
                await database.Users.AddAsync(user);
                await database.SaveChangesAsync();
                return user;
            }
        }

        public async Task<User> СhangeUserWithPassword(User newUser)
        {
            using (var database = GetDb())
            {
                User oldUser = await database.Users.Where(user => user.Id == newUser.Id).FirstOrDefaultAsync();
                oldUser.Name = newUser.Name;
                oldUser.SurName =  newUser.SurName;
                oldUser.MiddleName = newUser.MiddleName;
                oldUser.Telephone = newUser.Telephone;
                oldUser.Mail = newUser.Mail;
                oldUser.Password = newUser.Password;
                await database.SaveChangesAsync();
                return oldUser;
            }
        }

        public async Task<User> СhangeUserWithOutPassword(User newUser)
        {
            using (var database = GetDb())
            {
                User oldUser = await database.Users.Where(user => user.Id == newUser.Id).FirstOrDefaultAsync();
                oldUser.Name = newUser.Name;
                oldUser.SurName = newUser.SurName;
                oldUser.MiddleName = newUser.MiddleName;
                oldUser.Telephone = newUser.Telephone;
                oldUser.Mail = newUser.Mail;
                await database.SaveChangesAsync();
                return oldUser;
            }
        }

        public async Task<bool> DeleteUser(int? id)
        {
            bool delete = false;
            using (var database = GetDb())
            {
                User oldUser = await database.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
                oldUser.Name = "_noname_"+ oldUser.Id;
                oldUser.SurName = "_nosurname_" + oldUser.Id;
                oldUser.MiddleName = "_nomiddlename_" + oldUser.Id;
                oldUser.Telephone = "_nophone_"+ oldUser.Id;
                oldUser.Mail = "_nomail_" + oldUser.Id;
                oldUser.Password = "nopassword"+oldUser.Id;
                oldUser.Login = "nologin" + oldUser.Id;
                await database.SaveChangesAsync();
                delete = true;
                return delete;
            }
        }

        public async Task<User> GetUser(int? id)
        {
            using (var database = GetDb())
            {
                return await database.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            using(var database = GetDb())
            {
                return await database.Users.Where(user => user.Mail == email).FirstOrDefaultAsync();
            }
        }
        public async Task<User> GetUserByTel(string tel)
        {
            using (var database = GetDb())
            {
                return await database.Users.Where(user => user.Telephone == tel).FirstOrDefaultAsync();
            }
        }
        public async Task<User> GetUserByLogin(string login)
        {
            using (var database = GetDb())
            {
                return await database.Users.Where(user => user.Login == login).FirstOrDefaultAsync();
            }
        }
    }
}
