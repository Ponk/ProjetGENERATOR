using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF.CW
{
    public class CW_Utilisateur
    {
        private System.Data.DataSet oDS;
        private WCF.CM.CM_Utilisateur oUtilisateur;

        public CW_Utilisateur()
        {
            this.oDS = new System.Data.DataSet();
            this.oUtilisateur = new WCF.CM.CM_Utilisateur();

        }

        public System.Data.DataSet CW_getUser(string rows, string login, string mdp)
        {
            this.oDS = this.oUtilisateur.selectUser(rows, login, mdp);
            return this.oDS;

        }
    }
}