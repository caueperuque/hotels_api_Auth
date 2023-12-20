using TrybeHotel.Models;
using TrybeHotel.Dto;

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
                Email = user.Email,
                Password = user.Email,
                UserType = "client"
            };
            _context.Users.Add(newUser); 
            _context.SaveChanges();

            return new UserDto() 
            {
                userId = newUser.UserId, 
                Email = newUser.Email,
                Name = newUser.Name,
                userType = newUser.UserType
            };
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            return new UserDto()
            {
                Email = existUser.Email,
                Name = existUser.Name,
                userId = existUser.UserId,
                userType = existUser.UserType
            };
        }

        public IEnumerable<UserDto> GetUsers()
        {
           throw new NotImplementedException();
        }

    }
}