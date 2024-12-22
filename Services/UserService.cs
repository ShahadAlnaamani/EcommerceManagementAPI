﻿using EcommerceManagementAPI.DTOs;
using EcommerceManagementAPI.Models;
using EcommerceManagementAPI.Repositories;

namespace EcommerceManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userrepository;

        public UserService(IUserRepository userrepo)
        {
            _userrepository = userrepo;
        }

        //Adding new user, converts userInDTO --> User
        public int AddUser(UserInDTO Newuser)
        {
            var hashed = PassHasher(Newuser.Password);

            var users = _userrepository.GetAllUsers();

            foreach (var user in users)
            {
                if (user.Email == Newuser.Email)
                {
                    throw new Exception("<!>This email is already in use please try another one<!>");
                }
            }

            var FinalUser = new User
            {
                Name = Newuser.Name,
                Email = Newuser.Email,
                Password = hashed,
                PhoneNumber = Newuser.PhoneNumber,
                Role = Role.NormalUser,
                AccountActive = true,
                Created = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
            };
            return _userrepository.AddUser(FinalUser);
        }


        //Login function uses email and password [returns User]
        public User Login(string email, string password)
        {
            var hashedpass = _userrepository.GetHashedPass(email);

            bool verified = BCrypt.Net.BCrypt.Verify(password, hashedpass);
            if (verified)
            {
                return _userrepository.GetUserByEmail(email, hashedpass);
            }

            else throw new UnauthorizedAccessException("<!>Invalid credentials<!>");
        }


        //Password hashing function used to hash before storage and before comparing login pass to stored pass 
        //!Not done yet - error caused bec of random salting will update!
        public string PassHasher(string password)
        {
            // Generate a salt (we will use the user token)
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the password with the salt using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); //, salt

            return hashedPassword;
        }


        //Function used to get info of current account [returns userOutDTO]
        public UserOutDTO GetMyDetails(int ID)
        {
            var user = _userrepository.GetUserByID(ID);

            var output = new UserOutDTO
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return output;
        }

        //----------------------------------The following are for admin use only-----------------------------//

        //Returns all user DTOs (to prevent oversharing of information)
        public List<UserOutDTO> GetAllUsers()
        {
            var users = _userrepository.GetAllUsers();
            var Outusers = new List<UserOutDTO>();

            //Mapping user -> UserOutDTO
            foreach (var user in users)
            {
                var output = new UserOutDTO
                {
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };

                Outusers.Add(output);
            }

            return Outusers;
        }


        //Add new admin UserInDTO --> User
        public int AddAdmin(UserInDTO Newadmin)
        {
            var hashed = PassHasher(Newadmin.Password);

            var admins = _userrepository.GetAllUsers();

            foreach (var admin in admins)
            {
                if (admin.Email == Newadmin.Email)
                {
                    throw new Exception("<!>This email is already in use please try another one<!>");
                }
            }

            var user = new User
            {
                Name = Newadmin.Name,
                Email = Newadmin.Email,
                Password = hashed,
                PhoneNumber = Newadmin.PhoneNumber,
                Role = Role.Admin,
                AccountActive = true,
                Created = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
            };
            return _userrepository.AddUser(user);
        }


        //Allows Admin to search for users by ID [User DTO]
        public UserOutDTO GetUserByID(int ID)
        {
            var user = _userrepository.GetUserByID(ID);

            //Mapping user -> UserOutDTO
            var output = new UserOutDTO
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return output;
        }


        //Allows Admins to search for users by name [UserDTOs]
        public List<UserOutDTO> GetUserByName(string Name)
        {
            var users = _userrepository.GetUsersByName(Name);
            var OutUsers = new List<UserOutDTO>();

            //Mapping user -> UserOutDTO
            foreach (var user in users)
            {
                var output = new UserOutDTO
                {
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };

                OutUsers.Add(output);
            }

            return OutUsers;
        }

        //Allows Admin to search for users by phone no [User DTO]
        public UserOutDTO GetUserByPhoneNo(string phone)
        {
            var user = _userrepository.GetUserByPhone(phone);

            //Mapping user -> UserOutDTO
            var output = new UserOutDTO
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return output;
        }
    }
}
