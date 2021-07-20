using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepartitionWinForms.Entities
{
    public class Affaire
    {
        public string NomNavire { get; set; }
        public decimal Montant { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
    }
}
