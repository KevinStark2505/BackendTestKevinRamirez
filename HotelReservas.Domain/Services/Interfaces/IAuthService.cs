using HotelReservas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservas.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        LoginRequest AuthenticateUser(string Username, string Password);
        bool IniciarSesion(string usuario, string contrasena);
    }
}
