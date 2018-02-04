//-----------------------------------------------------------------------
// <copyright file="AccesAuxDonnees.cs" company="GSB">
//      Copyright (c) GSB. All rights reserved.
// </copyright>
// <author>Thomas LAURE</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GSB_ServiceWindows
{
    /// <summary>
    /// Classe contenant des outils pour l'accès aux données d'une base de données MySQL.
    /// Cette classe est abstraite car nous n'avons pas à l'instancier.
    /// </summary>
    public abstract class AccesAuxDonnees
    {
        /// <summary>
        /// Déclaration d'un objet de la classe MysqlConnection.
        /// Va être utilisé pour gérer la connexion à la base de données MySQL.
        /// </summary>
        private static MySqlConnection _connexion;

        /// <summary>
        /// Déclaration d'un objet de la classe MySqlCommand.
        /// Permet d'exécuter une requête, récupérer le résultat de la requête, et d'exécuter une procédure stockée.
        /// </summary>
        private static MySqlCommand _cmd;

        /// <summary>
        /// Méthode publique d'ouverture de la connexion à la base de données MySQL.
        /// </summary>
        public static void OuvrirConnexion()
        {
            try
            {
                _connexion = new MySqlConnection("Database=gsb_test; Server=localhost; Uid=root; Pwd=");
                if (_connexion.State == ConnectionState.Closed)
                {
                    _connexion.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion à la base de données locale.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Méthode publique de fermeture de la connexion à la base de données MySQL.
        /// </summary>
        public static void FermerConnexion()
        {
            if (_connexion.State != ConnectionState.Closed)
            {
                _connexion.Dispose();
                _connexion.Close();
            }
        }

        /// <summary>
        /// Requête à exécuter.
        /// Evite la duplication de code pour les requêtes ne qui ne renvoient rien.
        /// </summary>
        /// <param name="requete">Requête à exécuter.</param>
        /// <exception cref="Exception">L'exécution de la requête a échoué.\n" + ex.Message</exception>
        public static void RequeteAExecuter(string requete)
        {
            try
            {
                OuvrirConnexion();
                _cmd = new MySqlCommand(requete, _connexion)
                {
                    CommandType = CommandType.Text
                };
                var insertion = _cmd.ExecuteNonQuery();
                FermerConnexion();
            }
            catch (Exception ex)
            {
                FermerConnexion();
                throw new Exception("L'exécution de la requête a échoué.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Insère un enregistrement dans la base de données.
        /// </summary>
        /// <param name="table">Table sur laquelle sera exécutée la requête.</param>
        /// <param name="valeurs">Valeurs de l'enregistrement à insérer.</param>
        /// <exception cref="Exception">Cet enregistrement est déjà présent en base de données.\n" + ex.Message</exception>
        public static void Inserer(string table, string valeurs)
        {
            try
            {
                if (table != null && valeurs != null)
                {
                    RequeteAExecuter(@"INSERT INTO " + table + " VALUES " + valeurs);
                }
                else if (table == null || table == string.Empty)
                {
                    throw new Exception("La table de l'insertion doit être renseignée.");
                }
                else if (valeurs == null || valeurs == string.Empty)
                {
                    throw new Exception("Les valeurs de l'insertion doivent être renseignées.");
                }
                else
                {
                    throw new Exception("Tous les champs de l'insertion doivent être renseignés.");
                }
            }
            catch (Exception ex)
            {
                FermerConnexion();
                throw new Exception("Cet enregistrement est déjà présent en base de données.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Mise à jour d'un enregistrement en base de données.
        /// </summary>
        /// <param name="table">Table sur laquelle sera exécutée la requête.</param>
        /// <param name="valeur">Valeur à mettre à jour.</param>
        /// <param name="condition">Condition de la mise à jour.</param>
        /// <exception cref="Exception">Cet enregistrement n'est pas présent en base de données.\n" + ex.Message</exception>
        public static void MettreAJour(string table, string valeur, string condition)
        {
            try
            {
                if (table != null && valeur != null && condition != null)
                {
                    RequeteAExecuter(@"UPDATE " + table + " SET " + valeur + " WHERE " + condition);
                }
                else if (table == null || table == string.Empty)
                {
                    throw new Exception("Le nom de la table à mettre à jour doit être renseigné.\n");
                }
                else if (valeur == null || valeur == string.Empty)
                {
                    throw new Exception("Le champ et la valeur à mettre à jour doivent être rensignés.");
                }
                else if (condition == null || condition == string.Empty)
                {
                    throw new Exception("La condition de la mise à jour doit être renseignée.");
                }
                else
                {
                    throw new Exception("Tous les champs doivent être renseignés.");
                }
            }
            catch (Exception ex)
            {
                FermerConnexion();
                throw new Exception("Cet enregistrement n'est pas présent en base de données.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Renvoie un tableau d'enregistrements.
        /// </summary>
        /// <param name="requete">Requête curseur à exécuter.</param>
        /// <returns>un tableau associatif de données.</returns>
        /// <exception cref="Exception">Les éléments à renvoyer ne sont pas présents en base de données.\n" + ex.Message</exception>
        public static DataTable Selectionner(string champs, string ptable, string condition)
        {
            try
            {
                string table = ptable;
                string requete = @"SELECT " + champs + " FROM @table " + condition;
                OuvrirConnexion();
                _cmd = new MySqlCommand(requete, _connexion)
                {
                    CommandType = CommandType.Text
                };
                _cmd.Parameters.AddWithValue("@table", table);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_cmd);
                DataTable newTable = new DataTable();
                adapter.Fill(newTable);
                FermerConnexion();
                return newTable;
            }
            catch (Exception ex)
            {
                FermerConnexion();
                throw new Exception("Les éléments à renvoyer ne sont pas présents en base de données.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Supprime un champ en base de données.
        /// </summary>
        /// <param name="table">Table sur laquelle sera exécutée la requête.</param>
        /// <param name="condition">Condition de la suppression.</param>
        /// <exception cref="Exception">L'élément à supprimer n'est pas présent en base de données.\n" + ex.Message</exception>
        public static void Supprimer(string table, string condition)
        {
            try
            {
                if (table != null && condition != null)
                {
                    RequeteAExecuter(@"DELETE FROM " + table + " WHERE " + condition);
                }
                else if (table == null || table == string.Empty)
                {
                    throw new Exception("Le nom de la table de la suppression doit être renseigné.");
                }
                else if (condition == null || condition == string.Empty)
                {
                    throw new Exception("La condition de la suppression doit être renseignée.");
                }
                else
                {
                    throw new Exception("Tous les champs de la suppression doivent être renseignés.");
                }
            }
            catch (Exception ex)
            {
                FermerConnexion();
                throw new Exception("L'élément à supprimer n'est pas présent en base de données.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Met à jour toutes les fiches d'un mois précis sur l'état cloturé
        /// </summary>
        /// <param name="date">année + mois ex:201609</param>
        public static void FicheClotureAutomatique(string date)
        {
            try
            {
                RequeteAExecuter("UPDATE fichefrais SET idetat = \"CL\" where mois = " + date);
            }
            catch (Exception ex)
            {
                FermerConnexion();
                throw new Exception("Impossible de mettre à jour les fiches frais sur l'état cloturé.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Met à jour toutes les fiches d'un mois précis sur l'état remboursé
        /// </summary>
        /// <param name="date">année + mois ex:201609</param>
        public static void FicheRelboursementAutomatique(string date)
        {
            try
            {
                RequeteAExecuter("UPDATE fichefrais SET idetat = \"RB\" where mois = " + date);
            }
            catch (Exception ex)
            {
                FermerConnexion();
                throw new Exception("Impossible de mettre à jour les fiches frais sur l'état remboursé.\n" + ex.Message);
            }
        }
    }
}
