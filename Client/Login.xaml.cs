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
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            WCF.CAD connexion = new WCF.CAD("TRISTANJOLY2C5F", "bdd_generator", "valhunter", "valhunter");

            //connexion.generatePdf();

            string test = connexion.connexionBdd();

            //MessageBox msg = new MessageBoxResult.(test);
            MessageBoxResult m = MessageBox.Show(test);

            /*MainWindow main = new MainWindow();
            this.Close();
            main.Show();*/
        }
    }
}
