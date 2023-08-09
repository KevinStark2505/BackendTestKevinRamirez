using HotelReservas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservas.API.Repositories
{
    public interface IAdministracionHotelService
    {
        Hotel CrearHotel(Hotel nuevoHotel);
        Habitacion CrearHabitacion(Habitacion nuevaHabitacion);
        Reserva CrearReserva(Reserva nuevaReserva);
        IEnumerable<Hotel> ObtenerHoteles();
        IEnumerable<Habitacion> ObtenerHabitaciones();
        IEnumerable<Reserva> ObtenerReservas();
        Hotel ObtenerHotelPorIdInt(int IdInt);
        Habitacion ObtenerHabitacionPorIdInt(int idInt);
        Reserva ObtenerReservaPorIdInt(int IdInt);
        IEnumerable<Reserva> ObtenerReservasPorHotel(int IdInt);
        Hotel ActualizarHotel(Hotel hotel, Hotel hotelActualizado);
        Habitacion ActualizarHabitacion(Habitacion habitacion, Habitacion habitacionActualizada);
        Reserva ActualizarReserva(Reserva reserva, Reserva reservaActualizada);
        bool EliminarHotel(int IdInt);
        bool EliminarHabitacion(int IdInt);
        bool EliminarReserva(int IdInt);
        bool ActualizarIdIntHotel_Habitacion(int hotelIdInt, int habitacionIdInt);
    }
}
