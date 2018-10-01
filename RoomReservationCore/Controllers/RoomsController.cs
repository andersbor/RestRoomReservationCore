using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using RoomReservationCore.model;

namespace RoomReservationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly string _connectionStr = ConnectionString.GetConnectionString();

        // GET: api/Rooms
        [HttpGet]
        [Route("")]
        public IEnumerable<Room> Get()
        {
            const string selectAllRooms = "select * from roomreservationRoom order by name";
            List<Room> result = new List<Room>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectAllRooms, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Room user = ReadRoom(reader);
                                result.Add(user);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static Room ReadRoom(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string description = reader.IsDBNull(2) ? null : reader.GetString(2);
            int capacity = reader.GetInt32(3);
            string remarks = reader.IsDBNull(4) ? null : reader.GetString(4);
            int buildingId = reader.GetInt32(5);
            return new Room
            {
                Id = id,
                Name = name,
                Description = description,
                Capacity = capacity,
                Remarks = remarks,
                BuildingId = buildingId
            };
        }

        // GET: api/Rooms/5
        // [HttpGet("{id}", Name = "Get")]
        [Route("{id}")]
        public Room GetRoomById(string id)
        {
            const string selectRoom = "select * from roomreservationRoom where Id = @roomId";
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectRoom, connection))
                {
                    command.Parameters.AddWithValue("@roomId", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) return null;
                        reader.Read();
                        return ReadRoom(reader);
                    }
                }
            }
        }

        [Route("building/{buildingId}")]
        public IEnumerable<Room> GetRoomsByBuilding(string buildingId)
        {
            const string selectRooms = "select * from roomreservationRoom where buildingid = @buildingId order by  name";
            int id = int.Parse(buildingId);

            IList<Room> result = new List<Room>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectRooms, connection))
                {
                    command.Parameters.AddWithValue("@buildingId", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Room room = ReadRoom(reader);
                                result.Add(room);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
