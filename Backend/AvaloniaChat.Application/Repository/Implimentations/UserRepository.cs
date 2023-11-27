using AutoMapper;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Data;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Business.Repository.Implimentations
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ChatDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUser(CreateUserDto createUser)
        {
            User user = new User
            {
                Username = createUser.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUser.Password)
            };
            var createdUser  = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(createdUser.Entity);
        }

        public async Task<UserDto> UpdateUser(UpdateUserDto updateUser)
        {
            var user = await GetUserByUsername(updateUser.Username);

            var updatedUser = _mapper.Map<UpdateUserDto, User>(updateUser, user);
            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task<UserDto> GetUser(int userId)
        {
            return await GetUserById(userId);
        }
        public async Task DeleteUser(string username)
        {
            var user = await GetUserByUsername(username);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserIsExistByUsername(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
        }
        private async Task<UserDto> GetUserById(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
            return _mapper.Map<UserDto>(user);
        }
    }
}
