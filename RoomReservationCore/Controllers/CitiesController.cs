using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using RoomReservationCore.model;

namespace RoomReservationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly string _connectionStr = ConnectionString.GetConnectionString();

        // GET: api/Cities
        [HttpGet]
        public IEnumerable<City> Get()
        {
            const string selectAllCities = "select * from roomreservationCity order by name";
            IList<City> result = new List<City>();
            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectAllCities, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                City city = new City
                                {
                                    Id = id,
                                    Name = name
                                };
                                result.Add(city);
                            }
                        }
                    }
                }
                return result;
            }
        }

        // GET: api/Cities/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
