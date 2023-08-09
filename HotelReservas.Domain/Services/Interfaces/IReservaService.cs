using HotelReservas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservas.Domain.Services.Interfaces
{
    public interface IReservaService
    {
        IEnumerable<Hotel> BuscarHoteles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudad);
        Reserva CrearReserva(Reserva nuevaReserva);
        bool EsPosibleReserva(Reserva reserva);
    }
}
