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
        string login = "user";
        string mdp = "user123";

        public CAD()
        {
           
        }

        public string connexion()
        {


            return "Connexion réussie";
        }


    }
}