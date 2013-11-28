using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private System.Data.DataSet oDS;
        private WcfDecrypt.Service1Client oClient;
        private bool result;


        public Login()
        {
            InitializeComponent();
            this.oDS = new System.Data.DataSet();
            this.oClient = new WcfDecrypt.Service1Client();
        }

     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string login = TextBoxId.Text;
            string mdp = TextBoxMdp.Password;


            //this.result = this.oUtilisateur.envoiMail("valentin.carle@gmail.com");

            //MessageBoxResult m = MessageBox.Show(result.ToString());

            
            string token;
            string token_app = "rouen76";
            string tokenUtilisateur;

            this.oDS = this.oClient.authentification("row", login, mdp);

            if (this.oDS.Tables[0].Rows.Count == 0)
            {
                MessageBoxResult m = MessageBox.Show("Erreur d'identifiant ou de mot de passe");

            }
            else
            {
                tokenUtilisateur = this.oDS.Tables[0].Rows[0][0].ToString();
                login = this.oDS.Tables[0].Rows[0][1].ToString();
                mdp = this.oDS.Tables[0].Rows[0][2].ToString();
                token = this.oDS.Tables[0].Rows[0][3].ToString();

                if (token != token_app)
                {
                    MessageBoxResult m = MessageBox.Show("Vous ne pouvez pas vous connecter sur cette application : erreur token");
                }
                else
                {
                    MessageBoxResult m = MessageBox.Show("Connexion réussie");

                    MessageBoxResult m2 = MessageBox.Show(login);

                    MainWindow main = new MainWindow();
                    this.Close();
                    main.Show();
                }

            }

            /*result = oClient.generatePdf("Titre test");

            MessageBoxResult m5 = MessageBox.Show(result.ToString());*/

            //string mail = connexion.sendMail();

            
            
            
        }

        

    }
}
