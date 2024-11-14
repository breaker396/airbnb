using Airbnb.Api.Common;
using Airbnb.Api.Models;
using Airbnb.Api.Repository;
using Airbnb.Api.Utils;
using Airbnb.Data.Models;
using AutoMapper;

namespace Airbnb.Api.Services
{
    public interface IUserService
    {
        Task<UserDto?> Register(UserRegisterDto userRegisterDto);
        Task<UserDto?> Login(UserRegisterDto userRegisterDto);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper _mapper)
        {
            this._userRepository = userRepository;
            this._mapper = _mapper;
        }
        public async Task<UserDto?> Register(UserRegisterDto userRegisterDto)
        {
            bool isExistUsername = await _userRepository.IsExistUsername(userRegisterDto.UserName);
            if (isExistUsername)
            {
                throw new BadRequestException("User is existing");
            }
            else 
            {
                string pwHash = PasswordUtils.ComputeSha256Hash(userRegisterDto.Password);
                User user = new()
                {
                    Name = userRegisterDto.UserName,
                    Password = pwHash
                };
                _userRepository.Add(user);
                await _userRepository.SaveChanges();
                return _mapper.Map<UserDto>(user);
            }
        }
        public async Task<UserDto?> Login(UserRegisterDto userRegisterDto)
        {
            string pwHash = PasswordUtils.ComputeSha256Hash(userRegisterDto.Password);
            var user = await _userRepository.GetByUsernameAndPassword(userRegisterDto.UserName, pwHash);
            if(user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            else return _mapper.Map<UserDto>(user);
        }
    }
}
