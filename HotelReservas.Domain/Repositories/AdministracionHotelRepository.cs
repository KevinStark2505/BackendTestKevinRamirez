using HotelReservas.Domain.Entities;
using HotelReservas.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelReservas.Domain.Repositories
{
    public class AdministracionHotelRepository : IAdministracionHotelRepository
    {
        private readonly IMongoCollection<Hotel> _hotelCollection;
        private readonly IMongoCollection<Habitacion> _habitacionCollection;
        private readonly IMongoCollection<Reserva> _reservaCollection;

        public AdministracionHotelRepository(IMongoDatabase mongoDatabase, IOptions<MongoDbSettings> mongoDbSettings)
        {
            _hotelCollection = mongoDatabase.GetCollection<Hotel>(mongoDbSettings.Value.HotelCollectionName);
            _habitacionCollection = mongoDatabase.GetCollection<Habitacion>(mongoDbSettings.Value.HabitacionCollectionName);
            _reservaCollection = mongoDatabase.GetCollection<Reserva>(mongoDbSettings.Value.ReservaCollectionName);
        }

        public Hotel CrearHotel(Hotel nuevoHotel)
        {
            try
            {
                _hotelCollection.InsertOne(nuevoHotel);
                return nuevoHotel;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Hotel> ObtenerHoteles()
        {
            var hoteles = _hotelCollection.Find(h => true).ToList();
            return hoteles;
        }

        public Hotel ObtenerHotelPorIdInt(int IdInt)
        {
            var hotel = _hotelCollection.Find(h => h.IdInt == IdInt).FirstOrDefault();
            return hotel;
        }
        public Habitacion ObtenerHabitacionPorIdInt(int idInt)
        {
            try
            {
                return _habitacionCollection.Find(h => h.IdInt == idInt).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Hotel ActualizarHotel(Hotel hotel)
        {
            var result = _hotelCollection.ReplaceOne(h => h.IdInt == hotel.IdInt, hotel);
            if ((result.IsAcknowledged && result.ModifiedCount > 0) == true)
            {
                return hotel;
            }
            else
            {
                return null;
            }
        }

        public Habitacion ActualizarHabitacion(Habitacion habitacion)
        {
            var result = _habitacionCollection.ReplaceOne(h => h.IdInt == habitacion.IdInt, habitacion);
            if((result.IsAcknowledged && result.ModifiedCount > 0) == true)
             {
                return habitacion;
             }
            else
            {
                return null;
            }
        }

        public bool EliminarHotel(int IdInt)
        {
            var result = _hotelCollection.DeleteOne(h => h.IdInt == IdInt);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        public Habitacion CrearHabitacion(Habitacion nuevaHabitacion)
        {
            try
            {
                _habitacionCollection.InsertOne(nuevaHabitacion);
                return nuevaHabitacion;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int? CalcularIdSiguienteHotel()
        {
            try
            {
                int maxIdInt = _hotelCollection.AsQueryable().Any() ? _hotelCollection.AsQueryable().Max(h => h.IdInt) : 0;

                int siguienteIdInt = maxIdInt + 1;

                return siguienteIdInt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int? CalcularIdSiguienteHabitacion()
        {
            try
            {
                int maxIdInt = _habitacionCollection.AsQueryable().Any() ? _habitacionCollection.AsQueryable().Max(h => h.IdInt) : 0;

                int siguienteIdInt = maxIdInt + 1;

                return siguienteIdInt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int? CalcularIdSiguienteReserva()
        {
            try
            {
                int maxIdInt = _reservaCollection.AsQueryable().Any() ? _reservaCollection.AsQueryable().Max(h => h.IdInt) : 0;

                int siguienteIdInt = maxIdInt + 1;

                return siguienteIdInt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ActualizarIdIntHotel_Habitacion(int hotelIdInt, int habitacionIdInt)
        {
            try
            {
                var habitacion = _habitacionCollection.Find(h => h.IdInt == habitacionIdInt).FirstOrDefault();

                if (habitacion == null)
                {
                    return false;
                }

                habitacion.HotelIdInt = hotelIdInt;

                var result = _habitacionCollection.ReplaceOne(h => h._id == habitacion._id, habitacion);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Reserva CrearReserva(Reserva nuevaReserva)
        {
            try
            {
                _reservaCollection.InsertOne(nuevaReserva);
                return nuevaReserva;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Habitacion> ObtenerHabitaciones()
        {
            var habitaciones = _habitacionCollection.Find(h => true).ToList();
            return habitaciones;
        }

        public IEnumerable<Reserva> ObtenerReservas()
        {
            var reservas = _reservaCollection.Find(h => true).ToList();
            return reservas;
        }

        public Reserva ObtenerReservaPorIdInt(int IdInt)
        {
            var reserva = _reservaCollection.Find(h => h.IdInt == IdInt).FirstOrDefault();
            return reserva;
        }

        public Reserva ActualizarReserva(Reserva reserva)
        {
            var result = _reservaCollection.ReplaceOne(h => h.IdInt == reserva.IdInt, reserva);
            if ((result.IsAcknowledged && result.ModifiedCount > 0) == true)
            {
                return reserva;
            }
            else
            {
                return null;
            }
        }

        public bool EliminarHabitacion(int IdInt)
        {
            var result = _habitacionCollection.DeleteOne(h => h.IdInt == IdInt);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public bool EliminarReserva(int IdInt)
        {
            var result = _reservaCollection.DeleteOne(h => h.IdInt == IdInt);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public IEnumerable<Reserva> ObtenerReservaPorHotelIdInt(int IdInt)
        {
            var reservas = _reservaCollection.Find(h => h.HotelIdInt == IdInt).ToList();
            return reservas;
        }
    }
}
