using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Mail;
using System.Text;

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

            public void generatePdf()
            {
                System.IO.FileStream fs = new FileStream("C:\\Pdftest.pdf", FileMode.Create);

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.AddAuthor("Groupe 1 eXia");
                document.AddTitle("Titre");

                document.Open();
                document.Add(new Paragraph("Hello World!"));
                document.Close();
                writer.Close();
                fs.Close();

            }

        /// <summary>
        /// Send Mail for User by SMTP
        /// </summary>
        /// <param name="Contenu">Mail Boby</param>
        /// <param name="Subject">Mail subject</param>
        /// <param name="MailTo">Mail to adress</param>
        /// <param name="MailFrom">Mail from adress</param>
        /// <param name="SMTPServer">SMTP Server to use</param>
        /// <param name="InHTML">Body Mail in HTML or not</param>
        /// <returns>Send Mail sent OK ou Error</returns>

            public string sendMail(StringBuilder Contenu, string Subject, string MailTo, string MailFrom, string SMTPServer, bool InHTML)
            {
                string retour = string.Empty;
                MailMessage msg = null;
                System.Text.Encoding myEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                try
                {
                    msg = new MailMessage();
                    msg.Body = Contenu.ToString();
                    msg.BodyEncoding = myEncoding;
                    if (InHTML)
                    {
                        msg.BodyFormat = MailFormat.Html;
                    }
                    else
                    {
                        msg.BodyFormat = MailFormat.Text;
                    }

                    msg.Subject = Subject;
                    msg.From = MailFrom;
                    msg.To = MailTo;
                    SmtpMail.SmtpServer = SMTPServer;
                    SmtpMail.Send(msg);
                    retour = "Mail sent to " + MailTo;
                }
                catch(Exception ex)
                {
                    retour = "Error in Sendmail function - Details : "+ ex.ToString();
                }
                finally
	            {
		            msg = null;
		            myEncoding = null;
	            }
	            return retour;
 


                }

               
            
            
       

    }
}