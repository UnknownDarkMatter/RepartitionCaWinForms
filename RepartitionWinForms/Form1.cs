using RepartitionWinForms.Business;
using RepartitionWinForms.Business.CaCalcul;
using RepartitionWinForms.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepartitionWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRepartirCa_Click(object sender, EventArgs e)
        {
            var monthByMonthCaCalculator = new MonthByMonthCaCalculator();
            var groupedYearsCaCalculator = new GroupedYearsCaCalculator(monthByMonthCaCalculator);
            var repartitionManager = new RepartitionManager(groupedYearsCaCalculator);
            var affaires = GetAffaires();
            var datatable = repartitionManager.ExportTermesAffaires(affaires);

            var frmRepartition = new FrmRepartition();
            frmRepartition.DataGridView.DataSource = datatable;
            frmRepartition.DataGridView.Refresh();
            frmRepartition.Show();
        }

        private IEnumerable<Affaire> GetAffaires()
        {
            var affaires = new List<Affaire>();

            if(decimal.TryParse(txbMontant1.Text, out decimal montant1))
            {
                affaires.Add(new Affaire() { Montant = montant1, DateDebut = dtpDebut1.Value, DateFin = dtpFin1.Value, NomNavire = txbNomNavire1.Text });
            }
            if (decimal.TryParse(txbMontant2.Text, out decimal montant2))
            {
                affaires.Add(new Affaire() { Montant = montant2, DateDebut = dtpDebut2.Value, DateFin = dtpFin2.Value, NomNavire = txbNomNavire2.Text });
            }
            if (decimal.TryParse(txbMontant3.Text, out decimal montant3))
            {
                affaires.Add(new Affaire() { Montant = montant3, DateDebut = dtpDebut3.Value, DateFin = dtpFin3.Value, NomNavire = txbNomNavire3.Text });
            }
            if (decimal.TryParse(txbMontant4.Text, out decimal montant4))
            {
                affaires.Add(new Affaire() { Montant = montant4, DateDebut = dtpDebut4.Value, DateFin = dtpFin4.Value, NomNavire = txbNomNavire4.Text });
            }
            if (decimal.TryParse(txbMontant5.Text, out decimal montant5))
            {
                affaires.Add(new Affaire() { Montant = montant5, DateDebut = dtpDebut5.Value, DateFin = dtpFin5.Value, NomNavire = txbNomNavire5.Text });
            }

            return affaires;
        }
    }
}
