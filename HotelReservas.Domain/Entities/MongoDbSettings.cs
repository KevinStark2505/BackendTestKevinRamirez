using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservas.Domain.Entities
{
    public class MongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string HotelCollectionName { get; set; }
        public string ReservaCollectionName { get; set; }
        public string HabitacionCollectionName { get; set; }
    }
}
