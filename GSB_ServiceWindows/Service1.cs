using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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

        }

        protected override void OnStop()
        {
        }
    }
}
