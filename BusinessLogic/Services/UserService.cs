using DataAccess.Entity;
using DataAccess.Methods;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService 
    {
        private UserMethod _userMethod = new UserMethod();
        public async Task RegisterUser(RegistrationDto registrationDto)
        {
            if (!(await CheckEmail(registrationDto)))
            {
                registrationDto.ErrorEmail = true;
            }
            if (!(await CheckTel(registrationDto)))
            {
                registrationDto.ErrorTel = true;
            }
            if (!(await CheckLogin(registrationDto)))
            {
                registrationDto.ErrorLogin = true;
            }
            if (!registrationDto.ErrorEmail && !registrationDto.ErrorLogin && !registrationDto.ErrorTel)
            {
                User user = new User
                {
                    Name = registrationDto.Name,
                    SurName = registrationDto.SurName,
                    MiddleName = registrationDto.MiddleName,
                    Mail = registrationDto.Mail,
                    Login = registrationDto.Login,
                    Telephone = registrationDto.Telephone,
                    Type = UserTypeEnum.User,
                    Password = HashPass.HashPassword(registrationDto.Password)
                };
                await _userMethod.SaveUser(user);
            }
        }



        public async Task<int?> LoginUser(LoginDto loginDto)
        {
            User user = await _userMethod.GetUserByLogin(loginDto.Login);
            if (user == null)
            {
                loginDto.Error = true;
                return null;
            }
            if (user.Type == UserTypeEnum.Deleted || !CheckLogin(loginDto, user))
            {
                loginDto.Error = true;
                return null;
            }
            return user.Id;
        }

        public async Task<User> ProfileUser(int? id)
        {
            User user = await _userMethod.GetUser(id);
            return user;
        }

        private async Task<bool> CheckEmail(RegistrationDto registrationDto)
        {
            return (await _userMethod.GetUserByEmail(registrationDto.Mail)) == null;
        }
        private async Task<bool> CheckTel(RegistrationDto registrationDto)
        {
            return (await _userMethod.GetUserByTel(registrationDto.Telephone)) == null;
        }
        private async Task<bool> CheckLogin(RegistrationDto registrationDto)
        {
            return (await _userMethod.GetUserByLogin(registrationDto.Login)) == null;
        }
        private bool CheckLogin(LoginDto loginDto, User user)
        {
            if (user != null && loginDto.Password !=null && HashPass.VerifyHashedPassword(user.Password, loginDto.Password))
            {
                return true;
            }
            else return false;

        }
               
        public async Task ChangeProfileUser(User user, ProfileDto profileDto)
        {
            profileDto.ErrorEmail = await CheckProfileEmail(profileDto, user);
            profileDto.ErrorTel = await CheckProfileTel(profileDto, user);
            if (profileDto.OldPassword != ""  && profileDto.OldPassword != null)
                profileDto.ErrorPassword = !HashPass.VerifyHashedPassword(user.Password, profileDto.OldPassword);
            else
                profileDto.ErrorPassword = false;

            if (!profileDto.ErrorEmail && !profileDto.ErrorTel && !profileDto.ErrorPassword)
            {
                User newUser = new User
                {
                    Login = user.Login,
                    Id = user.Id,
                    Type=user.Type,
                    Name = profileDto.Name,
                    SurName = profileDto.Surname,
                    MiddleName = profileDto.Patronimic,
                    Mail = profileDto.Email,
                    Telephone = profileDto.Phone,                    
                };
                if (profileDto.OldPassword != "" && profileDto.NewPassword!="" && profileDto.OldPassword != null && profileDto.NewPassword != null)
                {
                    newUser.Password = HashPass.HashPassword(profileDto.NewPassword);
                    await _userMethod.СhangeUserWithPassword(newUser);
                }
                else
                    await _userMethod.СhangeUserWithOutPassword(newUser);
            }
        }

        public async Task<UserTypeEnum> GetUserType(int? id)
        {
            if (id == null)
            {
                return UserTypeEnum.Deleted;
            }
            return (await _userMethod.GetUser(id)).Type;
        }

        public async Task<bool> DeleteProfileUser(int? id)
        {
            return (await _userMethod.DeleteUser(id));
        }

        private async Task<bool> CheckProfileEmail(ProfileDto profileDto, User user)
        {
            User db_profile = await _userMethod.GetUserByEmail(profileDto.Email);
            return !((db_profile == null) || (db_profile.Mail==user.Mail));
        }
        private async Task<bool> CheckProfileTel(ProfileDto profileDto, User user)
        {
            User db_profile = await _userMethod.GetUserByTel(profileDto.Phone);
            return !((db_profile == null) || (db_profile.Telephone==user.Telephone));
        }


    }
}
