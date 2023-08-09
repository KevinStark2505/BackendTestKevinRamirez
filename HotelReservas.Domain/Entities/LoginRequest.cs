using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservas.Domain.Entities
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
