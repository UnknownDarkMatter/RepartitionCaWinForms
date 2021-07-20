using RepartitionWinForms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepartitionWinForms.Business.CaCalcul
{
    public interface ICaCalculator
    {
        IEnumerable<Tuple<Affaire, IEnumerable<TermeCa>>> CalculateTermesCa(IEnumerable<Affaire> affaires);
    }
}
