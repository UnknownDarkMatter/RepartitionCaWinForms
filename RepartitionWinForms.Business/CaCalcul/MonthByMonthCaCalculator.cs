using RepartitionWinForms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepartitionWinForms.Business.CaCalcul
{
    public class MonthByMonthCaCalculator : ICaCalculator
    {
        public IEnumerable<Tuple<Affaire, IEnumerable<TermeCa>>> CalculateTermesCa(IEnumerable<Affaire> affaires)
        {
            var termesByAffaire = new List<Tuple<Affaire, IEnumerable<TermeCa>>>();
            foreach(var affaire in affaires)
            {
                List<TermeCa> termes = new List<TermeCa>();
                termesByAffaire.Add(new Tuple<Affaire, IEnumerable<TermeCa>>(affaire, termes));
                AddTermes(affaire, termes);
            }
            return termesByAffaire;
        }

        private void AddTermes(Affaire affaire, List<TermeCa> termes)
        {
            var dateDebut = new DateTime(affaire.DateDebut.Year, affaire.DateDebut.Month, affaire.DateDebut.Day);
            var dateFin = new DateTime(affaire.DateFin.Year, affaire.DateFin.Month, affaire.DateFin.Day);

            var cursorDate = dateDebut;
            decimal nombreJoursTotal = (decimal) dateFin.Subtract(dateDebut).TotalDays + 1; //+1 pour inclure le dernier jour
            while(cursorDate <= dateFin) //inclure le dernier jour
            {
                DateTime firstDayNextMonth = new DateTime(cursorDate.AddMonths(1).Year, cursorDate.AddMonths(1).Month, 1);
                if(firstDayNextMonth> dateFin)
                {
                    firstDayNextMonth = dateFin.AddDays(1);
                }
                decimal nombreJoursMois = (decimal)firstDayNextMonth.Subtract(cursorDate).TotalDays;
                decimal montantTerme = affaire.Montant * nombreJoursMois / nombreJoursTotal;
                var newTerme = new TermeCa()
                {
                    MontantTerme = montantTerme,
                    DateTerme = cursorDate
                };
                termes.Add(newTerme);
                cursorDate = firstDayNextMonth;
            }
        }
    }
}
