using TrybeHotel.Models;
using TrybeHotel.Dto;
using FluentAssertions;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            throw new NotImplementedException();
        }
        public UserDto Add(UserDtoInsert user)
        {
            var newUser = new User()
            {
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                UserType = "client",
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return new UserDto()
            {
                userId = newUser.UserId,
                Name = newUser.Name,
                Email = newUser.Email,
                userType = newUser.UserType
            };
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            var userByEmail = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (userByEmail != null)
            {
                return new UserDto()
                {
                    Name = userByEmail.Name,
                    Email = userByEmail.Email,
                    userType = userByEmail.UserType,
                    userId = userByEmail.UserId
                };
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

    }
}