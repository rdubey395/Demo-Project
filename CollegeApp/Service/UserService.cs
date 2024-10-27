using AutoMapper;
using Azure;
using CollegeApp.DTO;
using CollegeApp.Model;
using CollegeApp.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json.Converters;
using System.Security.Cryptography;

namespace CollegeApp.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Usertable> _collegeRepository;
        public UserService(IMapper mapper, ICollegeRepository<Usertable> collegeRepository)
        {
            _mapper = mapper;
            
            _collegeRepository = collegeRepository;
        }

        public PasswordHashDTO CreatePasswordHashWithSalt(string password)
        {
            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create()) { 
                rng.GetBytes(salt);
            }

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password : password,
                salt : salt,
                prf : KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested : 256/8
                
                )
                );

            PasswordHashDTO passwordHashDTO = new PasswordHashDTO();
            passwordHashDTO.PasswordHash = hash;
            passwordHashDTO.Salt = Convert.ToBase64String(salt);
            return passwordHashDTO;
        }


        public async Task<bool> CreateUser(UserDTO userDTO)
        {
            if (userDTO == null) 
                 ArgumentNullException.ThrowIfNull(userDTO,$" {nameof(userDTO)} is null");

            var existingUser = await _collegeRepository.GetbyName(x => x.Username.Equals(userDTO.Username));

            if (existingUser != null)
                throw new Exception("The user is already taken");
            Usertable user = _mapper.Map<Usertable>(userDTO);
            user.IsDeleted = false;
            user.ModifiedDate = DateTime.UtcNow;
            user.CreatedDate = DateTime.UtcNow;

            if (userDTO.Password != null)
            {
                var passHash = CreatePasswordHashWithSalt(userDTO.Password);
                user.Password = passHash.PasswordHash;
                user.PasswordSalt = passHash.Salt;
            }

            await _collegeRepository.CreateData(user);

            return true;

        }
    }
}
