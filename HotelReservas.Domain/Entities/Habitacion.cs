using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservas.Domain.Entities
{
    public class Habitacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int IdInt { get; set; }
        public bool Disponible { get; set; }
        public int HotelIdInt { get; set; }
        public decimal CostoBase { get; set; }
        public decimal Impuestos { get; set; }
        public TipoHabitacion TipoHabitacion { get; set; }
        public string Ubicacion { get; set; }
    }
}
