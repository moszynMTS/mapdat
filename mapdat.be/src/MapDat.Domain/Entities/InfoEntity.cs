using MapDat.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDat.Domain.Entities
{
    public class InfoEntity : BaseEntity
    {
        [BsonElement("WojewodztwoId")]
        public string WojewodztwoId { get; set; } = null!;

        [BsonElement("Wojewodztwo")]
        public string Wojewodztwo { get; set; } = null!;

        [BsonElement("LUDNOŚĆ")]
        public double Ludnosc { get; set; }

        [BsonElement("MEDIANA WIEKU")]
        public double MedianaWieku { get; set; }

        [BsonElement("PRZESTĘPSTWA")]
        public int Przestepstwa { get; set; }

        [BsonElement("BIBLIOTEKI PUBLICZNE")]
        public int BibliotekiPubliczne { get; set; }

        [BsonElement("KINA")]
        public int Kina { get; set; }

        [BsonElement("KLUBY SPORTOWE")]
        public int KlubySportowe { get; set; }

        [BsonElement("GASTRONOMIA")]
        public int Gastronomia { get; set; }

        [BsonElement("SZPITALE")]
        public int Szpitale { get; set; }

        [BsonElement("ŻŁOBKI")]
        public int Zlobki { get; set; }

        [BsonElement("PRACUJĄCY")]
        public int Pracujacy { get; set; }

        [BsonElement("STOPA BEZROBOCIA")]
        public double StopaBezrobocia { get; set; }

        [BsonElement("DOCHODY")]
        public double Dochody { get; set; }

        [BsonElement("WYDATKI")]
        public double Wydatki { get; set; }
    }
}
