using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Net;

namespace WCF.CAD
{
    public class CAD
    {

        
        private string serveur ;
        private string bdd;
        private string login;
        private string mdp;

        private string rq_sql;
        private SqlConnection maConnexion;
        private SqlCommand maCommande;
        private System.Data.DataSet oDS;
        private SqlDataAdapter oDA;
        
        public CAD()
        {
            this.rq_sql = null;
            this.maCommande = null;
            this.serveur = "TRISTANJOLY2C5F";
            this.bdd = "bdd_generator";
            this.login = "valhunter";
            this.mdp = "valhunter";
            

            maConnexion = new SqlConnection("Data Source= '" + serveur + "';Initial Catalog= '" + bdd + "';User Id= '" + login + "';Password= '" + mdp + "'");

        }

        public System.Data.DataSet getRows(string rq_sql, string rows)
        {
            this.oDS = new System.Data.DataSet();
            this.rq_sql = rq_sql;
            this.maCommande = new SqlCommand(this.rq_sql, this.maConnexion);
            this.oDA = new SqlDataAdapter(this.maCommande);
            this.oDA.Fill(this.oDS, "rows");
            return this.oDS;

        }


    }
}