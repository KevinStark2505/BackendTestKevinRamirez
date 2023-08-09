using HotelReservas.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservas.API.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly IMongoCollection<Hotel> _hotelCollection;
        private readonly IMongoCollection<Reserva> _reservaCollection;
        private readonly IMongoCollection<Habitacion> _habitacionCollection;

        public ReservaRepository(IOptions<MongoDbSettings> mongoDbSettings, IMongoDatabase mongoDatabase)
        {
            _hotelCollection = mongoDatabase.GetCollection<Hotel>(mongoDbSettings.Value.HotelCollectionName);
            _habitacionCollection = mongoDatabase.GetCollection<Habitacion>(mongoDbSettings.Value.HabitacionCollectionName);
            _reservaCollection = mongoDatabase.GetCollection<Reserva>(mongoDbSettings.Value.ReservaCollectionName);
        }

        public IEnumerable<Hotel> BuscarHoteles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudad)
        {
            try
            {
                var habitacionesDisponibles = _habitacionCollection
                    .Find(h =>
                        h.Disponible &&
                        h.HotelIdInt != 0)
                    .ToList();

                var reservasEnFechas = _reservaCollection
                    .Find(r =>
                        r.HabitacionIdInt != 0 &&
                        r.HotelIdInt != 0 &&
                        r.FechaEntrada < fechaSalida &&
                        r.FechaSalida > fechaEntrada)
                    .ToList();

                var hotelesConHabitacionesDisponibles = habitacionesDisponibles
                    .Select(h => h.HotelIdInt)
                    .Distinct()
                    .ToList();

                var hotelesDisponibles = _hotelCollection
                    .Find(h =>
                        h.Disponible &&
                        h.Ciudad == ciudad &&
                        !reservasEnFechas.Any(r => r.HotelIdInt == h.IdInt))
                    .ToList();

                return hotelesDisponibles;
            }
            catch (Exception ex)
            {
                return null;
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

        public bool CambiarDisponibilidadHabitacion(bool estaDisponible, int habitacionIdInt)
        {
            try
            {
                var filter = Builders<Habitacion>.Filter.Eq(h => h.IdInt, habitacionIdInt);
                var update = Builders<Habitacion>.Update.Set(h => h.Disponible, estaDisponible);

                var result = _habitacionCollection.UpdateOne(filter, update);

                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                return false;
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

        public bool DisponibleHabitacionFechas(int habitacionIdInt, DateTime fechaentrada, DateTime fechasalida, int hotelIdInt)
        {
            var reservasEnFecha = _reservaCollection
                .Find(r =>
                    r.HotelIdInt == hotelIdInt &&
                    r.HabitacionIdInt != habitacionIdInt &&
                    ((r.FechaEntrada <= fechaentrada && r.FechaSalida > fechaentrada) ||
                     (r.FechaEntrada < fechasalida && r.FechaSalida >= fechasalida) ||
                     (r.FechaEntrada >= fechaentrada && r.FechaSalida <= fechasalida)))
                .Any();

            var habitacionDisponible = _habitacionCollection
                .Find(h =>
                    h.HotelIdInt == hotelIdInt &&
                    h.IdInt == habitacionIdInt &&
                    h.Disponible &&
                    !reservasEnFecha)
                .Any();

            return habitacionDisponible;
        }
    }
}
