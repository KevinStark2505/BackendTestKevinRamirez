using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelReservas.API.Repositories;
using HotelReservas.Domain.Entities;
using HotelReservas.Domain.Repositories;
using HotelReservas.Domain.Repositories.Interfaces;

namespace HotelReservas.Domain.Services
{

    public class AdministracionHotelService : IAdministracionHotelService
    {
        private readonly IAdministracionHotelRepository _administracionHotelRepository;

        public AdministracionHotelService(IAdministracionHotelRepository administracionHotelRepository)
        {
            this._administracionHotelRepository = administracionHotelRepository;
        }

        public Hotel ActualizarHotel(Hotel hotel, Hotel hotelActualizado)
        {
            hotel.Nombre = hotelActualizado.Nombre;
            hotel.Direccion = hotelActualizado.Direccion;
            hotel.Ciudad = hotelActualizado.Ciudad;
            hotel.Disponible = hotelActualizado.Disponible;
            return _administracionHotelRepository.ActualizarHotel(hotel);
        }

        public Habitacion ActualizarHabitacion(Habitacion habitacion, Habitacion habitacionActualizada)
        {
            habitacion.CostoBase = habitacionActualizada.CostoBase;
            habitacion.Impuestos = habitacionActualizada.Impuestos;
            habitacion.TipoHabitacion = habitacionActualizada.TipoHabitacion;
            habitacion.Ubicacion = habitacionActualizada.Ubicacion;
            habitacion.HotelIdInt = habitacionActualizada.HotelIdInt;
            habitacion.Disponible = habitacionActualizada.Disponible;
            return _administracionHotelRepository.ActualizarHabitacion(habitacion);
        }

        public Hotel CrearHotel(Hotel nuevoHotel)
        {
            try
            {
                nuevoHotel._id = null;
                int? IdSiguienteHotel = _administracionHotelRepository.CalcularIdSiguienteHotel();
                if (IdSiguienteHotel == null)
                {
                    return null;
                }
                nuevoHotel.IdInt = IdSiguienteHotel.Value;
                return _administracionHotelRepository.CrearHotel(nuevoHotel);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public Habitacion CrearHabitacion(Habitacion nuevaHabitacion)
        {
            try
            {
                nuevaHabitacion._id = null;
                int? IdSiguienteHabitacion = _administracionHotelRepository.CalcularIdSiguienteHabitacion();
                if (IdSiguienteHabitacion == null)
                {
                    return null;
                }
                nuevaHabitacion.IdInt = IdSiguienteHabitacion.Value;
                return _administracionHotelRepository.CrearHabitacion(nuevaHabitacion);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool EliminarHotel(int IdInt)
        {
            return _administracionHotelRepository.EliminarHotel(IdInt);
        }

        public IEnumerable<Hotel> ObtenerHoteles()
        {
            return _administracionHotelRepository.ObtenerHoteles();
        }

        public Hotel ObtenerHotelPorIdInt(int IdInt)
        {
            return _administracionHotelRepository.ObtenerHotelPorIdInt(IdInt);
        }
        public Habitacion ObtenerHabitacionPorIdInt(int IdInt)
        {
            return _administracionHotelRepository.ObtenerHabitacionPorIdInt(IdInt);
        }
        public bool ActualizarIdIntHotel_Habitacion(int hotelIdInt, int habitacionIdInt)
        {
            return _administracionHotelRepository.ActualizarIdIntHotel_Habitacion(hotelIdInt, habitacionIdInt);
        }

        public Reserva CrearReserva(Reserva nuevaReserva)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Habitacion> ObtenerHabitaciones()
        {
            return _administracionHotelRepository.ObtenerHabitaciones();
        }

        public IEnumerable<Reserva> ObtenerReservas()
        {
            return _administracionHotelRepository.ObtenerReservas();
        }

        public Reserva ObtenerReservaPorIdInt(int IdInt)
        {
            return _administracionHotelRepository.ObtenerReservaPorIdInt(IdInt);
        }

        public Reserva ActualizarReserva(Reserva reserva, Reserva reservaActualizada)
        {
            throw new NotImplementedException();
        }

        public bool EliminarHabitacion(int IdInt)
        {
            throw new NotImplementedException();
        }

        public bool EliminarReserva(int IdInt)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reserva> ObtenerReservasPorHotel(int IdInt)
        {
            return _administracionHotelRepository.ObtenerReservaPorHotelIdInt(IdInt);
        }
    }
}