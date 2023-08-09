using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HotelReservas.Domain.Entities;
using HotelReservas.Domain.Services.Interfaces;

namespace HotelReservas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet("BuscarHotel")]
        public IActionResult BuscarHoteles([FromQuery] DateTime fechaEntrada, [FromQuery] DateTime fechaSalida, [FromQuery] int cantidadPersonas, [FromQuery] string ciudadDestino)
        {
            var hotelesEncontrados = _reservaService.BuscarHoteles(fechaEntrada, fechaSalida, cantidadPersonas, ciudadDestino);
            if(hotelesEncontrados == null)
            {
                return null;
            }
            return Ok(hotelesEncontrados);
        }

        [HttpPost()]
        public IActionResult CrearReserva([FromBody] Reserva reserva)
        {
            // Validar si el objeto reserva es nulo
            if (reserva == null)
            {
                return BadRequest("Datos de la reserva no proporcionados.");
            }

            // Validar las fechas
            if (reserva.FechaEntrada >= reserva.FechaSalida || reserva.FechaSalida <= DateTime.Now)
            {
                return BadRequest("Las fechas de entrada y salida de la reserva son inválidas.");
            }

            // Validar cantidad de personas y huéspedes
            if (reserva.CantidadPersonas != reserva.Huesped.Count)
            {
                return BadRequest("La cantidad de personas no coincide con la cantidad de huéspedes.");
            }

            // Validar huéspedes
            foreach (var huesped in reserva.Huesped)
            {
                if (string.IsNullOrWhiteSpace(huesped.Nombres) || string.IsNullOrWhiteSpace(huesped.Apellidos))
                {
                    return BadRequest("Los nombres y apellidos de los huéspedes son requeridos.");
                }

                if (huesped.FechaNacimiento >= DateTime.Now)
                {
                    return BadRequest("La fecha de nacimiento del huésped es inválida.");
                }

                if (string.IsNullOrWhiteSpace(huesped.Genero) || string.IsNullOrWhiteSpace(huesped.TipoDocumento) || string.IsNullOrWhiteSpace(huesped.NumeroDocumento))
                {
                    return BadRequest("El género, tipo de documento y número de documento de los huéspedes son requeridos.");
                }
            }

            var reservaresult = _reservaService.CrearReserva(reserva);

            if(reservaresult == null)
            {
                return new StatusCodeResult(500);
            }

            return Ok(new { reservaresult });
        }
    }
}
