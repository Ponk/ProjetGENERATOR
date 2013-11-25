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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        public string authentification(string login, string mdp, string token)
        {

            SqlConnection maConnexion = null;
            try
            {



                maConnexion = new SqlConnection("Data Source=VALHUNTER\\SQLEXPRESS;Initial Catalog=Projet_DAD;Integrated Security=True;User Id=user;Password=user123");
                Console.WriteLine(maConnexion);
                SqlCommand maCommande = new SqlCommand();
                maCommande.Connection = maConnexion;

                maCommande.CommandText = "SELECT [Login_Utilisateur], [Mdp_Utilisateur], [Token_Base] FROM T_UTILISATEUR, T_BASE_MILITAIRE WHERE T_UTILISATEUR.[Id_Base] = T_BASE_MILITAIRE.[Id_Base] AND [Login_Utilisateur] = '" + login + "' AND [Mdp_Utilisateur] = '" + mdp + "';";
                Console.WriteLine(maCommande.CommandText);
                maConnexion.Open();

                Console.WriteLine("test");
                SqlDataReader monReader = maCommande.ExecuteReader();
                if (!monReader.HasRows)
                {
                    Console.WriteLine("Aucun enregistrement trouvé");
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

        public string decrypter(string cheminOrigine)
        {
            ArrayList openArray = new ArrayList();
            ArrayList closeArray = new ArrayList();


            /*Console.WriteLine("Enter the path of the output file.");

            string endPath = Console.ReadLine(); //Chemin du fichier de sortie
            */
            //Console.WriteLine("Enter a encryption key. No letters or special characters, only numbers.");

            //int enKey = Convert.ToInt32(Console.ReadLine()); //Variable qui contient la clé de chiffrage




            //=============================Fonction d'ouverture du fichier==================================



            using (FileStream fs = new FileStream(cheminOrigine, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        openArray.Add(sr.ReadLine());

                    }

                    sr.Close();
                }

                fs.Close();
            }

            //=========================Fin de la fonction d'ouverture de fichier==========================


            //======================Décryptage et ajout de la chaîne au deuxième tableau======================

            int codeDecrypt = 0;
            //List<String> newList = openArray.Cast<String>().ToList();

            //Parallel.For(0, 10000, enKey =>
            while (codeDecrypt < 10000)
            {



                foreach (string texte in openArray)
                {
                    closeArray.Add(AlgorithmeXor(texte, codeDecrypt));

                }
                //);

                //======================FIN Décryptage et ajout de la chaîne au second tableau======================


                string array = closeArray[0].ToString();

                Object document = new Object();
                document = closeArray[0];




                closeArray.Clear();

                /*TextWriter tw = new StreamWriter(endPath);
                    
                foreach (string encoded in closeArray)
                {
                    tw.WriteLine(encoded);

                }
                    
                tw.Close();*/




                codeDecrypt++;
                //nbr++;

                Console.WriteLine(codeDecrypt);


                //return array;
            }
            //);



            return codeDecrypt.ToString();


        }

        public static string AlgorithmeXor(string dataCrypte, int cleDecrypt)
        {
            StringBuilder inSb = new StringBuilder(dataCrypte);
            StringBuilder outSb = new StringBuilder(dataCrypte.Length);

            char c;

            for (int i = 0; i < dataCrypte.Length; i++)
            {
                c = inSb[i];

                c = (char)(c ^ cleDecrypt);

                outSb.Append(c);

            }
            return outSb.ToString();
        }

        public static void generatePdf()
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

    }
}
