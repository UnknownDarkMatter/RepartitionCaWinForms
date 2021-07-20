using RepartitionWinForms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepartitionWinForms.Business.CaCalcul
{
    public class GroupedYearsCaCalculator : ICaCalculator
    {
        private readonly MonthByMonthCaCalculator _monthByMonthCaCalculator;
        public GroupedYearsCaCalculator(MonthByMonthCaCalculator monthByMonthCaCalculator)
        {
            _monthByMonthCaCalculator = monthByMonthCaCalculator ?? throw new ArgumentNullException(nameof(monthByMonthCaCalculator));
        }

        public IEnumerable<Tuple<Affaire, IEnumerable<TermeCa>>> CalculateTermesCa(IEnumerable<Affaire> affaires)
        {
            var termesByAffaireGrouped = new List<Tuple<Affaire, IEnumerable<TermeCa>>>();
            var termesByAffaireNotGrouped = _monthByMonthCaCalculator.CalculateTermesCa(affaires);
            foreach(var tuple in termesByAffaireNotGrouped)
            {
                List<TermeCa> termes = new List<TermeCa>();
                termes = GroupTermes(tuple.Item2);
                termesByAffaireGrouped.Add(new Tuple<Affaire, IEnumerable<TermeCa>>(tuple.Item1, termes));
            }
            return termesByAffaireGrouped;
        }

        private List<TermeCa> GroupTermes(IEnumerable<TermeCa> termes)
        {
            if(termes.Count() == 0) { return new List<TermeCa>(); }

            List<TermeCa> result = new List<TermeCa>();
            termes = termes.OrderBy(m => m.DateTerme);
            TermeCa resultTerme = null;
            TermeCa previousTerme = null;
            foreach (var terme in termes)
            {
                if(terme.DateTerme.Year == Constants.Now.Year)
                {
                    result.Add(terme);
                } 
                else
                {
                    resultTerme = AddGroupedTerm(result, resultTerme, previousTerme, terme);
                }
                previousTerme = terme;
            }
            return result;
        }

        private static TermeCa AddGroupedTerm(List<TermeCa> result, TermeCa resultTerme, TermeCa previousTerme, TermeCa terme)
        {
            if (previousTerme != null && previousTerme.DateTerme.Year == terme.DateTerme.Year)
            {
                resultTerme.MontantTerme += terme.MontantTerme;
            }
            else
            {
                resultTerme = new TermeCa()
                {
                    MontantTerme = terme.MontantTerme,
                    DateTerme = new DateTime(terme.DateTerme.Year, 1, 1)
                };
                result.Add(resultTerme);
            }

            return resultTerme;
        }
    }
}
