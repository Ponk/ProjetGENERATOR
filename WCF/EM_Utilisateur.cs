using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF
{
    public class EM_Utilisateur
    {
        private string rq_sql;

        public string SelectUser(string login, string mdp)
        {
            this.rq_sql = "SELECT [Login_Utilisateur], [Mdp_Utilisateur], [Token_Base] FROM T_UTILISATEUR, T_BASE_MILITAIRE WHERE T_UTILISATEUR.[Id_Base] = T_BASE_MILITAIRE.[Id_Base] AND [Login_Utilisateur] = '"+ login + "' AND [Mdp_Utilisateur] = '"+ mdp +"';";
            return this.rq_sql;
        }
    }
}