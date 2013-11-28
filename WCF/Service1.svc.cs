using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WCF.GenService;
using System.Text.RegularExpressions;

namespace WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        
        private System.Data.DataSet oDS;
        private WCF.CAM.CAM_Utilisateur oUtilisateur;
        private WCF.CAM.CAM_Decryptage oDecrypt;
        private bool resultMail;
        private bool resultPdf;

        public Service1()
        {
            this.oUtilisateur = new CAM.CAM_Utilisateur();
            this.oDecrypt = new CAM.CAM_Decryptage();



        }

        

        public System.Data.DataSet authentification(string rows, string login, string mdp)
        {
            this.oDS = this.oUtilisateur.CAM_selectUser(rows, login, mdp);
            return this.oDS;

        }

        public bool envoiMail(String adressMailReceiver, string subject, string body)
        {
            this.resultMail = this.oUtilisateur.CAM_sendMail(adressMailReceiver, subject, body);
            return this.resultMail;

        }

        public bool generatePdf(string titre)
        {
            this.resultPdf = this.oDecrypt.CAM_generatePdf(titre);
            return this.resultPdf;

        }



        private string GetFileContent(string path)
        {
            StreamReader myFile = new StreamReader(path, Encoding.UTF8);
            return myFile.ReadToEnd();
        }

        public void EncryptDecrypt(string path)
        {
            GenServiceClient client = new GenServiceClient();
            FileInfo file = new FileInfo(path);
            string chaineCodee = this.GetFileContent(path);
            string cleanString = "";

            //for (int key = 0; key < 10000; key++)
            for (int key = int.Parse("1300"); key > int.Parse("1250"); key--)
			{
			    //Application d'une clé
                cleanString = this.ApplyKey(chaineCodee, key.ToString());

                if (isTextValid(cleanString))
                {
                    //Envoi d'un texte décrypté au service Java EE
                    cleanString = Regex.Replace(cleanString, @"[^a-zA-ZéèàêùÇÈÉÀçïî@.\[\]_]", " ");
                    client.SendDocumentOperation(file.Name, cleanString, key.ToString());
                }
			}
        }            

        private string ApplyKey(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
            {
                char character = text[c];
                uint charCode = (uint)character;

                int keyPosition = c % key.Length;
                char keyChar = key[keyPosition];

                uint keyCode = (uint)keyChar;
                uint combinedCode = charCode ^ keyCode;
                char combinedChar = (char)combinedCode;

                result.Append(combinedChar); 
            }

            //return Regex.Replace(result.ToString(), @"[^a-zA-ZéèàêùÇÈÉÀçïî@.\[\]_]", " ");
            return result.ToString();
        }

        private bool isTextValid(string text)
        {
            Char[] badChars = { '#', '|', '>', '~', 'Á', 'Ã', 'Ä', 'Å', 'Æ', 'Ì', 'Í', 'Î', 'Ï', 'Ð', 'Ñ', 'Ò', 'Ó', 'Õ', 'Ö', 'Ø', 'Œ', 'Š', 'þ', 'Ý', 'Ÿ', 'á', 'ã', 'ä', 'å', 'æ', 'ð', 'ñ', 'ò', 'ó', 'õ', 'ö', 'ø', 'š', 'Þ', 'ý', 'ÿ', '¢', 'ß', '¥', '£', '™', '©', '®', 'ª', '¼', '½', '¾', 'º', '§', '¤', '¦', '¬', '‰' };
            //Char[] badChars = { '#', '|', '<', '>', '~', 'Á', 'Ã', 'Ä', 'Å', 'Æ' };
            //Char[] badChars = { 'Ì', 'Í', 'Î', 'Ï', 'Ð', 'Ñ', 'Ò', 'Ó', 'Õ', 'Ö' };
            //Char[] badChars = { 'Ø', 'Œ', 'Š', 'þ', 'Ý', 'Ÿ', 'á', 'ã', 'ä', 'å' };
            //Char[] badChars = { 'æ', 'ð', 'ñ', 'ò', 'ó', 'õ', 'ö', 'ø', 'š', 'Þ' };
            //Char[] badChars = { 'ý', 'ÿ', '¢', 'ß', '¥', '£', '™', '©', '®', 'ª' };
            //Char[] badChars = { '¼', '½', '¾', 'º', '§', '¤', '¦', '¬', '‰' };
            //return badChars.Any(c => text.Contains(c));

            if (badChars.Any(l => text.Contains(l)))
                return false;
            else
                return true;
                

            //bool match = text.IndexOfAny(badChars) != -1;
            //return match;
        }
    }
}
