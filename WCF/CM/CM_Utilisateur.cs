using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF.CM
{
    public class CM_Utilisateur
    {
        private WCF.CAD.CAD oCAD;
        private WCF.EM.EM_Utilisateur oUtilisateur;
        private System.Data.DataSet oDS;

        public CM_Utilisateur()
        {
            this.oCAD = new WCF.CAD.CAD();
            this.oUtilisateur = new WCF.EM.EM_Utilisateur();

        }

        public System.Data.DataSet selectUser(string rows, string login, string mdp)
        {
            this.oDS = this.oCAD.getRows(this.oUtilisateur.SelectUser(login, mdp), rows);
            return this.oDS;
        }




    }
}