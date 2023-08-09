using HotelReservas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservas.API.Repositories
{
    public interface IReservaRepository
    {
        IEnumerable<Hotel> BuscarHoteles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudad);
        Reserva CrearReserva(Reserva nuevaReserva);
        int? CalcularIdSiguienteReserva();
        bool DisponibleHabitacionFechas(int habitacionIdInt, DateTime fechaentrada, DateTime fechasalida, int hotelIdInt);
        bool CambiarDisponibilidadHabitacion(bool estaDisponible, int habitacionIdInt);
    }
}
