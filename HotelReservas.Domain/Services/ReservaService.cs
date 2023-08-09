using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelReservas.API.Repositories;
using HotelReservas.Domain.Entities;
using HotelReservas.Domain.Repositories;
using HotelReservas.Domain.Services.Interfaces;

namespace HotelReservas.Domain.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            this._reservaRepository = reservaRepository;
        }

        public IEnumerable<Hotel> BuscarHoteles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudad)
        {
            return _reservaRepository.BuscarHoteles(fechaEntrada, fechaSalida, cantidadPersonas, ciudad);
        }

        public Reserva CrearReserva(Reserva nuevaReserva)
        {
            nuevaReserva._id = null;
            int? IdSiguienteReserva = _reservaRepository.CalcularIdSiguienteReserva();
            if (IdSiguienteReserva == null)
            {
                return null;
            }
            nuevaReserva.IdInt = IdSiguienteReserva.Value;
            Reserva reservaCreada = null;
            if (EsPosibleReserva(nuevaReserva))
            {
                reservaCreada = CrearReservaConValidacion(nuevaReserva);
            }
            return reservaCreada;
        }

        public bool EsPosibleReserva(Reserva reserva)
        {
            return _reservaRepository.DisponibleHabitacionFechas(reserva.HabitacionIdInt, reserva.FechaEntrada, reserva.FechaSalida, reserva.HotelIdInt);
        }

        public Reserva CrearReservaConValidacion(Reserva reserva)
        {
            if(_reservaRepository.CambiarDisponibilidadHabitacion(true, reserva.HabitacionIdInt))
            {
                return _reservaRepository.CrearReserva(reserva);
            }
            else
            {
                return null;
            }
        }
    }
}
