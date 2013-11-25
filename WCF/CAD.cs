﻿using System;
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
                
                maConnexion = new SqlConnection("Data Source= '" + serveur + "';Initial Catalog= '" + bdd + "';User Id= '" + login + "';Password= '" + mdp + "'");
                
                SqlCommand maCommande = new SqlCommand();
                maCommande.Connection = maConnexion;

                maCommande.CommandText = "SELECT [Login_Utilisateur], [Mdp_Utilisateur], [Token_Base] FROM T_UTILISATEUR, T_BASE_MILITAIRE WHERE T_UTILISATEUR.[Id_Base] = T_BASE_MILITAIRE.[Id_Base] AND [Login_Utilisateur] = 'valentinc' AND [Mdp_Utilisateur] = 'blabla';";
                
                maConnexion.Open();
                
                Console.WriteLine("test");
                SqlDataReader monReader = maCommande.ExecuteReader();
                
                string token = "rouen76";

                if (!monReader.HasRows)
                {
                    Console.WriteLine("Aucun enregistrement trouvé");
                    return "pas d'enregistrement";
                }
                else
                {
                    while (monReader.Read())
                    {
                        
                        if (token == monReader["Token_Base"].ToString())
                        {

                            //Console.WriteLine(monReader["Nom_Utilisateur"].ToString() + " " + monReader["Prenom_Utilisateur"].ToString() + " " + monReader["Email_Utilisateur"].ToString());
                            return monReader["Token_Base"].ToString();
                        }
                        else
                        {
                            return "error token";
                        }
                    }
                }

                return "Connexion réussie";
            }
            catch
            {
                Console.WriteLine("Erreur lors de la connexion ou l'exécution de la requête");

                return "connexion failed";
            }
            finally
            {
                if (maConnexion != null)
                {
                    maConnexion.Close();
                }


            }

               
            
            
        }


    }
}