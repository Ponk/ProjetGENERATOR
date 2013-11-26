using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF.CAM
{
    public class CAM_Utilisateur
    {
        private System.Data.DataSet oDS;
        private WCF.CW.CW_Utilisateur oUtilisateur;

        public CAM_Utilisateur()
        {
            this.oDS = new System.Data.DataSet();
            this.oUtilisateur = new WCF.CW.CW_Utilisateur();
        }

        public System.Data.DataSet CAM_selectUser(string rows, string login, string mdp)
        {
            this.oDS = this.oUtilisateur.CW_getUser(rows, login, mdp);
            return this.oDS;

        }
        


    }
}