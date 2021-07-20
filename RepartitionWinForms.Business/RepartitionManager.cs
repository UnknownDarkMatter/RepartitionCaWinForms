using RepartitionWinForms.Business.CaCalcul;
using RepartitionWinForms.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepartitionWinForms.Business
{
    public class RepartitionManager
    {
        private readonly GroupedYearsCaCalculator _caCalculator;
        public RepartitionManager(GroupedYearsCaCalculator caCalculator)
        {
            _caCalculator = caCalculator ?? throw new ArgumentNullException(nameof(caCalculator));
        }

        public DataTable ExportTermesAffaires(IEnumerable<Affaire> affaires)
        {
            DataTable dataTable = new DataTable();
            var termesByAffaire = _caCalculator.CalculateTermesCa(affaires);
            CreateColumns(dataTable, termesByAffaire);
            foreach(var tuple in termesByAffaire)
            {
                DataRow dr = dataTable.NewRow();
                dataTable.Rows.Add(dr);
                dr[Constants.ColumnNomNavire] = tuple.Item1.NomNavire;
                foreach (var terme in tuple.Item2)
                {
                    var columnName = GetColumnName(terme);
                    dr[columnName] = terme.MontantTerme;
                }
            }
            return dataTable;
        }

        private void CreateColumns(DataTable dataTable, IEnumerable<Tuple<Affaire, IEnumerable<TermeCa>>> termesByAffaire)
        {
            dataTable.Columns.Add(Constants.ColumnNomNavire);
            var columnNames = GetOrderedColumnsNames(termesByAffaire);
            foreach(var columnName in columnNames)
            {
                dataTable.Columns.Add(columnName);
            }
        }

        private IEnumerable<string> GetOrderedColumnsNames(IEnumerable<Tuple<Affaire, IEnumerable<TermeCa>>> termesByAffaire)
        {
            List<Tuple<DateTime, string>> columnsNames = new List<Tuple<DateTime, string>>();
            foreach (var tuple in termesByAffaire)
            {
                foreach (var terme in tuple.Item2)
                {
                    string columnName = GetColumnName(terme);
                    var dateTerme = new DateTime(terme.DateTerme.Year, terme.DateTerme.Month, 1);
                    if (!columnsNames.Any(m => m.Item2 == columnName))
                    {
                        columnsNames.Add(new Tuple<DateTime, string>(dateTerme, columnName));
                    }
                }
            }
            columnsNames = columnsNames.OrderBy(m => m.Item1).ToList();
            return columnsNames.Select(m => m.Item2);
        }

        private string GetColumnName(TermeCa termeCa)
        {
            if(termeCa.DateTerme.Year != Constants.Now.Year)
            {
                return termeCa.DateTerme.Year.ToString();
            }

            var dateTerme = new DateTime(termeCa.DateTerme.Year, termeCa.DateTerme.Month, 1);
            return dateTerme.ToString("dd/MM/yyyy");
        }
    }
}
