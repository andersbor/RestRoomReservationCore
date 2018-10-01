using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomReservationCore.model;

namespace RoomReservationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly string _connectionStr = ConnectionString.GetConnectionString();
        private const string SelectAllBuilding = "select * from roomreservationBuilding";


        // GET: api/Buildings
        // [HttpGet]
        [Route("")]
        public IEnumerable<Building> Get()
        {
            List<Building> result = new List<Building>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SelectAllBuilding, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Building building = ReadBuilding(reader);
                                result.Add(building);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static Building ReadBuilding(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string address = reader.GetString(2);
            int cityId = reader.GetInt32(3);
            Building building = new Building
            {
                Id = id,
                Name = name,
                Address = address,
                CityId = cityId
            };
            return building;
        }

        // GET: api/Buildings/5
        //[HttpGet("{id}", Name = "Get")]
        [Route("{id}")]
        public Building Get(int id)
        {
            const string selectBuilding = "select * from roomreservationBuilding where id = @id";
            // int idInt = int.Parse(id);
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectBuilding, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) return null;
                        reader.Read(); // advance cursor
                        return ReadBuilding(reader);
                    }
                }
            }
        }

        [Route("city/{cityId}")]
        public IEnumerable<Building> GetBuildingByCity(string cityId)
        {
            const string selectBuilding = "select * from roomreservationBuilding where cityId = @cityId order by name";
            IList<Building> result = new List<Building>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectBuilding, connection))
                {
                    command.Parameters.AddWithValue("@cityId", cityId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Building building = ReadBuilding(reader);
                                result.Add(building);
                            }
                        }
                    }
                }
            }
            return result;
        }

        // POST: api/Buildings
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Buildings/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
