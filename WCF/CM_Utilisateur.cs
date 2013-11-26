using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF
{
    public class CM_Utilisateur
    {
        private CAD oCAD;
        private EM_Utilisateur oUtilisateur;
        private System.Data.DataSet oDS;

        public CM_Utilisateur()
        {
            this.oCAD = new CAD("TRISTANJOLY2C5F", "bdd_generator", "valhunter", "valhunter");
            this.oUtilisateur = new EM_Utilisateur();

        }

        public System.Data.DataSet selectUser(string rows, string login, string mdp)
        {
            this.oDS = this.oCAD.getRows(this.oUtilisateur.SelectUser(login, mdp), rows);
            return this.oDS;
        }




    }
}