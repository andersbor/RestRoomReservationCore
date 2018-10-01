using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace RoomReservationCore.Controllers
{
    public class ConnectionString
    {
        public static String GetConnectionString()
        {
            return "Server=tcp:anbo-databaseserver.database.windows.net,1433;Initial Catalog=anbobase;Persist Security Info=False;User ID=anbo;Password=Hemmelig14;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
