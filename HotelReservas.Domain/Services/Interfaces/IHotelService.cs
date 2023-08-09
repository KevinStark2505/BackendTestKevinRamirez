using HotelReservas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservas.Domain.Services.Interfaces
{
    public interface IHotelService
    {
        IEnumerable<Hotel> ObtenerHoteles();
        Hotel CrearHotel(Hotel nuevoHotel);
        bool AsignarHabitacion(int hotelId, Habitacion nuevaHabitacion);
        bool ModificarHotel(int hotelId, Hotel hotelModificado);
        bool HabilitarHotel(int hotelId, bool habilitar);
        bool ModificarHabitacion(int habitacionId, Habitacion habitacionModificada);
        bool HabilitarHabitacion(int habitacionId, bool habilitar);
    }
}
