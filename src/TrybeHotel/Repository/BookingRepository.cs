using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var roomToInsert = GetRoomById(booking.RoomId);           

            var addBooking = new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                UserId = user.UserId,
                RoomId = roomToInsert.RoomId
            };

            if (booking.GuestQuant > roomToInsert.Capacity) return null;

            _context.Bookings.Add(addBooking);
            _context.SaveChanges();

            var newBooking = _context.Bookings
                                .Include(b => b.Room)
                                .ThenInclude(r => r.Hotel)
                                .ThenInclude(h => h.City)
                                .FirstOrDefault(b => b.BookingId == addBooking.BookingId);

            return new BookingResponse
            {
                bookingId = addBooking.BookingId,
                checkIn = addBooking.CheckIn,
                checkOut = addBooking.CheckOut,
                guestQuant = addBooking.GuestQuant,
                room = new RoomDto
                {
                    roomId = roomToInsert.RoomId,
                    name = roomToInsert.Name,
                    capacity = roomToInsert.Capacity,
                    image = roomToInsert.Image,
                    hotel = new HotelDto
                    {
                       hotelId = roomToInsert.HotelId,
                       name = roomToInsert.Hotel.Name,
                       address = roomToInsert.Hotel.Address,
                       cityId = roomToInsert.Hotel.City.CityId,
                       cityName = roomToInsert.Hotel.City.Name 
                    }
                }
            };
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            throw new NotImplementedException();
        }

        public Room GetRoomById(int RoomId)
        {
            return _context.Rooms.FirstOrDefault(r => r.RoomId == RoomId);
        }

    }

}