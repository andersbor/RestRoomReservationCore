using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using RoomReservationCore.model;

namespace RoomReservationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly string _connectionStr = ConnectionString.GetConnectionString();

        // GET: api/Reservations
        [Route("")]
        public IEnumerable<Reservation> GetAllReservations()
        {
            const string selectReservations = "select * from roomreservationReservation order by roomId, fromTime";
            IList<Reservation> result = new List<Reservation>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectReservations, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Reservation reservation = ReadReservation(reader);
                                result.Add(reservation);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static Reservation ReadReservation(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            int roomId = reader.GetInt32(1);
            //int userId = reader.IsDBNull(2) ? -1 : reader.GetInt32(2);
            string userId = reader.GetString(2);
            DateTime from = reader.GetDateTime(3);
            DateTime to = reader.GetDateTime(4);
            //DateTime to = reader.GetDateTime(4);
            string purpose = reader.IsDBNull(5) ? null : reader.GetString(5);
            // string deviceId = reader.IsDBNull(6) ? null : reader.GetString(6);
            return new Reservation
            {
                Id = id,
                RoomId = roomId,
                UserId = userId,
                FromTimeString = from.ToString("dd-MM-yyyy H:mm"),
                ToTimeString = to.ToString("dd-MM-yyyy H:mm"),
                Purpose = purpose,
                // DeviceId = deviceId
            };
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Route("room/{roomId}")]
        public IEnumerable<Reservation> GetReservationsByRoomId(string roomId)
        {
            const string selectReservations =
                "select * from roomreservationReservation where roomid=@roomId order by fromTime";
            int roomIdInt = int.Parse(roomId);
            IList<Reservation> result = new List<Reservation>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectReservations, connection))
                {
                    command.Parameters.AddWithValue("@roomId", roomIdInt);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Reservation reservation = ReadReservation(reader);
                                result.Add(reservation);
                            }
                        }
                    }
                }
            }
            return result;
        }

        [Route("user/{userId}")]
        public IEnumerable<Reservation> GetReservationsByUserId(string userId)
        {
            const string selectReservations =
                "select * from roomreservationReservation where userId=@userId order by fromTime";
            //int userIdInt = int.Parse(userId);
            IList<Reservation> result = new List<Reservation>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectReservations, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Reservation reservation = ReadReservation(reader);
                                result.Add(reservation);
                            }
                        }
                    }
                }
            }
            return result;
        }

        [Route("{roomId}/{date}")]
        public IEnumerable<Reservation> GetReservationsByRoomAndDate(string idString, string dateString)
        {
            const string selectReservations =
                "select * from roomreservationReservation where roomid=@roomId and convert(date, @dateString) between cast(fromTime as date) and cast(toTime as date) order by fromTime";
            int idInt = int.Parse(idString);
            //IFormatProvider formatProvider = null;
            //DateTime dateTime = DateTime.ParseExact(dateString, "dd-MM-yyyy", formatProvider);
            //throw new NotImplementedException();
            IList<Reservation> result = new List<Reservation>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectReservations, connection))
                {
                    command.Parameters.AddWithValue("@roomId", idInt);
                    command.Parameters.AddWithValue("@dateString", dateString);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Reservation reservation = ReadReservation(reader);
                                result.Add(reservation);
                            }
                        }
                    }
                }
            }
            return result;
        }

        // TODO fix problem with date formats: 01-10-2018 is confused with 10-01-2018
        // POST: api/Reservations
        [HttpPost]
        public void Post([FromBody] Reservation reservation)
        {
            const string insertReservation = "insert into roomreservationReservation (roomId, userId, fromTime, toTime, purpose) values(@roomId, @userId, convert(datetime, @fromTime), convert(datetime, @toTime), @purpose); ";
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(insertReservation, connection))
                {
                    command.Parameters.AddWithValue("@roomId", reservation.RoomId);
                    command.Parameters.AddWithValue("@userId", /*reservation.UserId == -1 ? null :*/ reservation.UserId);
                    command.Parameters.AddWithValue("@fromTime", reservation.FromTimeString);
                    command.Parameters.AddWithValue("@toTime", reservation.ToTimeString);
                    command.Parameters.AddWithValue("@purpose", reservation.Purpose);
                    // command.Parameters.AddWithValue("@deviceID", reservation.DeviceId);
                    int rowsAffected = command.ExecuteNonQuery();
                    // return rowsAffected;
                }
            }
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // int id = int.Parse(reservationId);
            const string deleteReservation = "delete from roomreservationReservation where id=@reservationId";
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(deleteReservation, connection))
                {
                    command.Parameters.AddWithValue("@reservationId", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    // return rowsAffected;
                }
            }
        }
    }
}
