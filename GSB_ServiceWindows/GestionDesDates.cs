//-----------------------------------------------------------------------
// <copyright file="GestionDesDates.cs" company="GSB">
//     Copyright (c) GSB. All rights reserved.
// </copyright>
// <author>Thomas LAURE</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_ServiceWindows
{
    /// <summary>
    /// Classe de gestion des dates.
    /// </summary>
    public abstract class GestionDesDates
    {
        /// <summary>
        /// Membre privé statique contenant la date actuelle.
        /// </summary>
        private static DateTime _dateActuelle = DateTime.Now;

        /// <summary>
        /// Méthode statique qui permet d'accéder au mois précédent de la date actuelle.
        /// </summary>
        /// <param name="dateActuelle">Date actuelle.</param>
        /// <returns>Le mois précédent sous forme d'une chaîne de caractères.</returns>
        public static string GetMoisPrecedent(DateTime dateActuelle)
        {
            int moisPrecedent = dateActuelle.Month - 1;
            if (moisPrecedent < 10)
            {
                return "0" + moisPrecedent;
            }
            else
            {
                return Convert.ToString(moisPrecedent);
            }
        }

        /// <summary>
        /// Surchage de la méthode statique GetMoisPrecedent().
        /// Permet d'accéder au mois précédent de la date actuelle.
        /// </summary>
        /// <returns>Le mois précédent sous forme d'une chaîne de caractères.</returns>
        public static string GetMoisPrecedent()
        {
            return GetMoisPrecedent(_dateActuelle);
        }

        /// <summary>
        /// Méthode statique qui permet d'accéder au mois suivant de la date actuelle.
        /// </summary>
        /// <param name="dateActuelle">Date Actuelle.</param>
        /// <returns>Le mois suivant sous forme d'une chaîne de caractères.</returns>
        public static string GetMoisSuivant(DateTime dateActuelle)
        {
            int moisSuivant = dateActuelle.Month + 1;
            if (moisSuivant < 10)
            {
                return "0" + moisSuivant;
            }
            else
            {
                return Convert.ToString(moisSuivant);
            }
        }

        /// <summary>
        /// Surcharge de la méthode statique GetMoisSuivant() définie précédemment.
        /// Permet d'accéder au mois suivant de la date actuelle.
        /// </summary>
        /// <returns>Le mois suivant sous forme d'une chaîne de caractères.</returns>
        public static string GetMoisSuivant()
        {
            return GetMoisPrecedent(_dateActuelle);
        }

        /// <summary>
        /// Méthode statique qui permet de savoir si le jour de la date actuelle se trouve entre les deux jours passés en paramètres.
        /// </summary>
        /// <param name="jourUn">Premier jour passé en paramètre.</param>
        /// <param name="jourDeux">Deuxième jour passé en paramètre.</param>
        /// <param name="dateActuelle">Date actuelle.</param>
        /// <returns>Vrai si le jour de la date actuelle se trouve entre les deux jours passés en paramètres.</returns>
        public static bool Entre(DateTime jourUn, DateTime jourDeux, DateTime dateActuelle)
        {
            if (dateActuelle.Month == jourUn.Month && dateActuelle.Month == jourDeux.Month)
            {
                if ((dateActuelle.Day > jourUn.Day && dateActuelle.Day < jourDeux.Day) || (dateActuelle.Day < jourUn.Day && dateActuelle.Day > jourDeux.Day))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("Les jours passés en paramètre ne sont pas du même mois.");
            }
        }

        /// <summary>
        /// Surcharge de la méthode statique Entre définie précédemment.
        /// </summary>
        /// <param name="jourUn">Jour un.</param>
        /// <param name="jourDeux">Jour deux.</param>
        /// <returns>Vrai si le jour de la date actuelle se trouve entre les deux jours passés en paramètres.</returns>
        public static bool Entre(DateTime jourUn, DateTime jourDeux)
        {
            return Entre(jourUn, jourDeux, _dateActuelle);
        }
    }
}
