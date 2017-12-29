//-----------------------------------------------------------------------
// <copyright file="GestionDesDatesTests.cs" company="GSB">
//     Copyright (c) GSB. All rights reserved.
// </copyright>
// <author>Thomas LAURE</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSB_ServiceWindows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSB_ServiceWindows.Tests
{
    [TestClass()]
    public class GestionDesDatesTests
    {
        [TestMethod()]
        public void GetMoisPrecedentTestMoisInferieurADix()
        {
            DateTime dateActuelle = new DateTime(2017, 10, 29);
            Assert.AreEqual("09", GestionDesDates.GetMoisPrecedent(dateActuelle), "Doit renvoyer 9.");
        }

        [TestMethod()]
        public void GetMoisPrecedentTestMoisSuperieurA1Dix()
        {
            DateTime dateActuelle = new DateTime(2012, 11, 12);
            Assert.AreEqual("10", GestionDesDates.GetMoisPrecedent(dateActuelle), "Le mois précédent doit être égal à 10.");
        }

        [TestMethod()]
        public void GetMoisSuivantTestInferieurADix()
        {
            DateTime dateActuelle = new DateTime(2012, 8, 12);
            Assert.AreEqual("09", GestionDesDates.GetMoisSuivant(dateActuelle), "Le mois précédent doit être égal à 09.");
        }

        [TestMethod()]
        public void GetMoisSuivantTestMoisSuperieurADix()
        {
            DateTime dateActuelle = new DateTime(2012, 11, 12);
            Assert.AreEqual("12", GestionDesDates.GetMoisSuivant(dateActuelle), "Le mois précédent doit être égal à 12.");
        }

        [TestMethod()]
        public void EntreTestTrue()
        {
            DateTime jourUn = new DateTime(2017, 10, 9);
            DateTime jourDeux = new DateTime(2017, 10, 30);
            DateTime dateActuelle = new DateTime(2017, 10, 29);
            Assert.AreEqual(true, GestionDesDates.Entre(jourUn, jourDeux, dateActuelle), "Doit renvoyer true.");
        }

        [TestMethod()]
        public void EntreTestFalse()
        {
            DateTime jourUn = new DateTime(2017, 10, 31);
            DateTime jourDeux = new DateTime(2017, 10, 30);
            DateTime dateActuelle = new DateTime(2017, 10, 29);
            Assert.AreEqual(false, GestionDesDates.Entre(jourUn, jourDeux, dateActuelle), "Doit renvoyer false.");
        }
    }
}