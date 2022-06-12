using System.Collections.Generic;

namespace siges.Models
{
    public class OsRecurrente
    {
        public int Id { get; set; }
        public int OsOrigenId { get; set; }
        public string Periodo { get; set; }
        public List<OsRecurrente.Oses> OsRecurrentesIds { get; set; }

        public OsRecurrente()
        {
            OsRecurrentesIds = new List<OsRecurrente.Oses>();
        }

        public class Oses
        {
            public int Id { get; set; }
            public int OsId { get; set; }
        }
    }
}