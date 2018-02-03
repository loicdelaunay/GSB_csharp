using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSB_ServiceWindows
{
    public partial class Service1 : ServiceBase
    {
        private int jourActuel = DateTime.Now.Day;
        private int moisActuel = DateTime.Now.Month;
        private int anneeActuelle = DateTime.Now.Year;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (jourActuel <= 10 && jourActuel >= 1)
            {
                AccesAuxDonnees.ficheClotureAutomatique(moisActuel - 1);
            }
            else if (jourActuel == 20)
            {
                AccesAuxDonnees.ficheRemboursementAutomatique(moisActuel - 1);
            }
        }

        protected override void OnStop()
        {
        }

        private void timerGSB_Tick(object sender, EventArgs e)
        {
            if (jourActuel <= 10 && jourActuel >= 1)
            {
                AccesAuxDonnees.ficheClotureAutomatique(moisActuel - 1);
            }
            else if (jourActuel == 20)
            {
                AccesAuxDonnees.ficheRemboursementAutomatique(moisActuel - 1);
            }
        }
    }
}
