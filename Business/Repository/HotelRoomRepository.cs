using AutoMapper;
using Business.Repository.IRepository;
using DataAcesss.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HotelRoomRepository(ApplicationDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreateData = DateTime.Now;
            hotelRoom.CreatedBy = "";

            var addedHotelRoom = _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
           var roomDetails = await _context.HotelRooms.FindAsync(roomId);
            if (roomDetails != null)
            {
                _context.HotelRooms.Remove(roomDetails);
                return await _context.SaveChangesAsync();
            }
            return 0;


        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(int roomId)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                    _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_context.HotelRooms);
                return hotelRoomDTOs;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _context.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId));

                return hotelRoom;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HotelRoomDTO> IsRoomUnique(string name)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _context.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                return hotelRoom;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if(roomId == hotelRoomDTO.Id)
                {
                    HotelRoom roomDetails = await _context.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdateBy = "";
                    room.UpdateDate = DateTime.Now;
                    var updatedRoom = _context.HotelRooms.Update(room);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
