using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservas.Domain.Entities
{
    public class Reserva
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int IdInt { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CantidadPersonas { get; set; }
        public bool Confirmada { get; set; }
        public List<Huesped> Huesped { get; set; }
        public int HabitacionIdInt { get; set; }
        public int HotelIdInt { get; set; }
        public ContactoEmergencia ContactoEmergencia { get; set; }
    }
}
