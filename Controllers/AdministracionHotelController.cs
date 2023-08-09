using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HotelReservas.Domain.Entities;
using HotelReservas.Domain.Services.Interfaces;
using System.Linq;
using System;
using HotelReservas.API.Repositories;

namespace HotelReservas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministracionHotelController : ControllerBase
    {
        private readonly IAdministracionHotelService _administracionHotelService;
        private readonly IReservaService _reservaService;

        public AdministracionHotelController(IAdministracionHotelService administracionHotelService, IReservaService reservaService)
        {
            _administracionHotelService = administracionHotelService;
            _reservaService = reservaService;
        }

        // Métodos relacionados con hoteles

        [HttpPost("hoteles")]
        public IActionResult CrearHotel([FromBody] Hotel nuevoHotel)
        {
            if (nuevoHotel == null)
            {
                return BadRequest("Datos del hotel no proporcionados.");
            }

            if (string.IsNullOrWhiteSpace(nuevoHotel.Nombre))
            {
                return BadRequest("El nombre del hotel es requerido.");
            }

            if (string.IsNullOrWhiteSpace(nuevoHotel.Direccion))
            {
                return BadRequest("La dirección del hotel es requerida.");
            }

            if (string.IsNullOrWhiteSpace(nuevoHotel.Ciudad))
            {
                return BadRequest("La ciudad del hotel es requerida.");
            }

            if (nuevoHotel.Disponible != true && nuevoHotel.Disponible != false)
            {
                return BadRequest("La disponibilidad del hotel es requerida.");
            }

            var hotelCreado = _administracionHotelService.CrearHotel(nuevoHotel);

            if (hotelCreado == null)
            {
                return new StatusCodeResult(500);
            }

            // Devolver el hotel creado en la respuesta
            return Ok(new { hotelCreado });
        }

        [HttpGet("hoteles")]
        public IActionResult ObtenerHoteles()
        {
            var hoteles = _administracionHotelService.ObtenerHoteles();
            if (hoteles == null)
            {
                return NotFound("No hay hoteles registrados en base de datos");
            }
            return Ok(hoteles);
        }

        [HttpGet("hoteles/{hotelIdInt}")]
        public IActionResult ObtenerHotelPorIdInt(int hotelIdInt)
        {
            var hotel = _administracionHotelService.ObtenerHotelPorIdInt(hotelIdInt);
            if (hotel == null)
            {
                return NotFound("No hay hotel registrado en base de datos");
            }
            return Ok(hotel);
        }

        [HttpDelete("hoteles/{hotelIdInt}")]
        public IActionResult EliminarHotelPorIdInt(int hotelIdInt)
        {
            var exitoEliminacion = _administracionHotelService.EliminarHotel(hotelIdInt);
            return Ok(exitoEliminacion);
        }

        [HttpPut("hoteles")]
        public ActionResult<Hotel> ActualizarHotel([FromBody] Hotel hotelActualizado)
        {
            if (hotelActualizado == null)
            {
                return BadRequest("Datos del hotel no proporcionados.");
            }

            if (string.IsNullOrWhiteSpace(hotelActualizado.Nombre))
            {
                return BadRequest("El nombre del hotel es requerido.");
            }

            if (string.IsNullOrWhiteSpace(hotelActualizado.Direccion))
            {
                return BadRequest("La dirección del hotel es requerida.");
            }

            if (string.IsNullOrWhiteSpace(hotelActualizado.Ciudad))
            {
                return BadRequest("La ciudad del hotel es requerida.");
            }

            if (hotelActualizado.Disponible != true && hotelActualizado.Disponible != false)
            {
                return BadRequest("La disponibilidad del hotel es requerida.");
            }

            var hotel = _administracionHotelService.ObtenerHotelPorIdInt(hotelActualizado.IdInt);

            if (hotel == null)
            {
                return NotFound("El hotel no existe en base de datos");
            }

            return _administracionHotelService.ActualizarHotel(hotel, hotelActualizado);
        }

        // Métodos relacionados con habitaciones

        [HttpPost("habitaciones")]
        public IActionResult CrearHabitacion([FromBody] Habitacion nuevaHabitacion)
        {
            // Validar si el objeto nuevaHabitacion es nulo
            if (nuevaHabitacion == null)
            {
                return BadRequest("Datos de la habitación no proporcionados.");
            }

            // Validar campos requeridos
            if (!decimal.TryParse(nuevaHabitacion.CostoBase.ToString(), out decimal costobase))
            {
                return BadRequest("El costo base de la habitación debe ser un valor numérico válido.");
            }

            if (!decimal.TryParse(nuevaHabitacion.Impuestos.ToString(), out decimal impuestos))
            {
                return BadRequest("Los impuestos de la habitación deben ser un valor numérico válido.");
            }

            if (nuevaHabitacion.HotelIdInt <= 0)
            {
                return BadRequest("El ID del hotel debe ser mayor que cero.");
            }

            if (!Enum.IsDefined(typeof(TipoHabitacion), nuevaHabitacion.TipoHabitacion))
            {
                return BadRequest("El tipo de habitación es inválido.");
            }

            if (nuevaHabitacion.Disponible != true && nuevaHabitacion.Disponible != false)
            {
                return BadRequest("La disponibilidad de la habitación es requerida.");
            }

            if (string.IsNullOrEmpty(nuevaHabitacion.Ubicacion))
            {
                return BadRequest("La Ubicacion no está correctamente radicada.");
            }

            var habitacionCreada = _administracionHotelService.CrearHabitacion(nuevaHabitacion);
            if (habitacionCreada == null)
            {
                return new StatusCodeResult(500);
            }
            else
            {
                return Ok(new { habitacionCreada });
            }
        }

        [HttpGet("habitaciones")]
        public IActionResult ObtenerHabitaciones()
        {
            var habitaciones = _administracionHotelService.ObtenerHabitaciones();
            if (habitaciones == null)
            {
                return NotFound("No hay habitaciones registradas en base de datos");
            }
            return Ok(habitaciones);
        }

        [HttpGet("habitaciones/{habitacionIdInt}")]
        public IActionResult ObtenerHabitacionPorIdInt(int habitacionIdInt)
        {
            var habitacion = _administracionHotelService.ObtenerHabitacionPorIdInt(habitacionIdInt);
            if (habitacion == null)
            {
                return NotFound("No hay habitación registrada en base de datos");
            }
            return Ok(habitacion);
        }

        [HttpDelete("habitaciones/{habitacionIdInt}")]
        public IActionResult EliminarHabitacionPorIdInt(int habitacionIdInt)
        {
            var exitoEliminacion = _administracionHotelService.EliminarHabitacion(habitacionIdInt);
            return Ok(exitoEliminacion);
        }

        [HttpPut("habitaciones")]
        public ActionResult<Habitacion> ActualizarHabitacion([FromBody] Habitacion habitacionActualizada)
        {
            // Validar si el objeto habitacionActualizada es nulo
            if (habitacionActualizada == null)
            {
                return BadRequest("Datos de la habitación no proporcionados.");
            }

            // Validar campos requeridos
            if (!decimal.TryParse(habitacionActualizada.CostoBase.ToString(), out decimal costobase))
            {
                return BadRequest("El costo base de la habitación debe ser un valor numérico válido.");
            }

            if (!decimal.TryParse(habitacionActualizada.Impuestos.ToString(), out decimal impuestos))
            {
                return BadRequest("Los impuestos de la habitación deben ser un valor numérico válido.");
            }

            if (habitacionActualizada.HotelIdInt <= 0)
            {
                return BadRequest("El ID del hotel debe ser mayor que cero.");
            }

            if (!Enum.IsDefined(typeof(TipoHabitacion), habitacionActualizada.TipoHabitacion))
            {
                return BadRequest("El tipo de habitación es inválido.");
            }

            if (habitacionActualizada.Disponible != true && habitacionActualizada.Disponible != false)
            {
                return BadRequest("La disponibilidad de la habitación es requerida.");
            }

            if (habitacionActualizada.IdInt < 0)
            {
                return BadRequest("El IdInt de la habitación no está correctamente proporcionado.");
            }

            if (string.IsNullOrEmpty(habitacionActualizada.Ubicacion))
            {
                return BadRequest("La Ubicación no está correctamente radicada.");
            }

            var habitacion = _administracionHotelService.ObtenerHabitacionPorIdInt(habitacionActualizada.IdInt);

            if (habitacion == null)
            {
                return NotFound("La habitación no existe en base de datos");
            }

            return _administracionHotelService.ActualizarHabitacion(habitacion, habitacionActualizada);
        }

        // Métodos relacionados con reservas

        [HttpGet("reservas")]
        public IActionResult ObtenerReservas()
        {
            var reservas = _administracionHotelService.ObtenerReservas();
            return Ok(reservas);
        }

        [HttpGet("hoteles/{hotelIdInt}/reservas")]
        public IActionResult ObtenerReservasPorHotel(int hotelIdInt)
        {
            if (hotelIdInt < 0)
            {
                return BadRequest("El idint del hotel no está correctamente diligenciado.");
            }
            var reservas = _administracionHotelService.ObtenerReservasPorHotel(hotelIdInt);
            if (reservas == null)
                return NotFound();

            return Ok(reservas);
        }

        [HttpGet("reservas/{reservaIdInt}")]
        public IActionResult ObtenerDetallesReserva(int reservaIdInt)
        {
            if (reservaIdInt < 0)
            {
                return BadRequest("El idint de la habitación no está correctamente diligenciado.");
            }
            var reserva = _administracionHotelService.ObtenerReservaPorIdInt(reservaIdInt);
            if (reserva == null)
                return NotFound();

            return Ok(reserva);
        }

        [HttpDelete("reservas/{reservaIdInt}")]
        public IActionResult EliminarReservasPorIdInt(int reservaIdInt)
        {
            var exitoEliminacion = _administracionHotelService.EliminarReserva(reservaIdInt);
            return Ok(exitoEliminacion);
        }
    }
}
