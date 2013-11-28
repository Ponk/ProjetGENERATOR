using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.IO;


namespace WCF.CAM
{
    public class CAM_Utilisateur
    {
        private System.Data.DataSet oDS;
        private WCF.CW.CW_Utilisateur oUtilisateur;
        private bool result;

        public CAM_Utilisateur()
        {
            this.oDS = new System.Data.DataSet();
            this.oUtilisateur = new WCF.CW.CW_Utilisateur();

            
        }

        public System.Data.DataSet CAM_selectUser(string rows, string login, string mdp)
        {
            this.oDS = this.oUtilisateur.CW_getUser(rows, login, mdp);

            // Compose a string that consists of three lines.
            string text = DateTime.Now + " : Connexion de l'utilisateur.";

            // Write the string to a file.
            File.AppendAllText("c:\\test.txt", text + "\r\n");

            return this.oDS;

        }

        public bool CAM_sendMail(String adressMailReceiver, string subject, string body)
        {
            this.result = this.oUtilisateur.CW_sendMail(adressMailReceiver, subject, body);

            // Compose a string that consists of three lines.
            string text = DateTime.Now + " : Envoi d'un e-mail.";

            // Write the string to a file.
            File.AppendAllText("c:\\test.txt", text + "\r\n");

            return this.result;
        }

        


    }
}