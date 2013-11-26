using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WCF
{
    public class CAD
    {
        SqlConnection maConnexion = null;
        string serveur;
        string bdd;
        string login;
        string mdp;

        public CAD(string serveur, string bdd, string login, string mdp)
        {
            this.serveur = serveur;
            this.bdd = bdd;
            this.login = login;
            this.mdp = mdp;
        }

        public string connexionBdd()
        {
            try
            {
                
                maConnexion = new SqlConnection("Data Source= '" + serveur + "';Initial Catalog= '" + bdd + "';Integrated Security=True;User Id= '" + login + "';Password= '" + mdp + "'");
                SqlCommand maCommande = new SqlCommand();
                maCommande.Connection = maConnexion;
                maConnexion.Open();
            }
            catch
            {
                return "Connexion failed";
            }
            
            return "Connexion réussie";
        }


    }
}