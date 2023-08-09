using HotelReservas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservas.Domain.Repositories.Interfaces
{
    public interface IAdministracionHotelRepository
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
        IEnumerable<Reserva> ObtenerReservaPorHotelIdInt(int IdInt);
        Hotel ActualizarHotel(Hotel hotel);
        Habitacion ActualizarHabitacion(Habitacion habitacion);
        Reserva ActualizarReserva(Reserva reserva);
        bool EliminarHotel(int IdInt);
        bool EliminarHabitacion(int IdInt);
        bool EliminarReserva(int IdInt);
        int? CalcularIdSiguienteHabitacion();
        int? CalcularIdSiguienteHotel();
        int? CalcularIdSiguienteReserva();
        bool ActualizarIdIntHotel_Habitacion(int hotelIdInt, int habitacionIdInt);
    }
}
