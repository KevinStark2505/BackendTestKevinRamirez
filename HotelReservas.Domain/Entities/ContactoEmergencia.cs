using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservas.Domain.Entities
{
    public class ContactoEmergencia
    {
        public string Nombres { get; set; }
        public string TelefonoContacto { get; set; }
    }
}
