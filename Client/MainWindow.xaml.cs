using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Client.WCFGenService;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Document> docList { get; set; }
        public string TextResultName { get; set; }
        public string TextResultContent { get; set; }
        public string TextResultKey { get; set; }
        public string TextResultMail { get; set; }
        public string TextResultTaux { get; set; }

        public Visibility BtnEnvoyerVisibility { get; set; }
        public Visibility ProgressBarVisibility { get; set; }

        Service1Client clientWCF;

        public MainWindow()
        {
            InitializeComponent();
            this.docList = new List<Document>();
            this.clientWCF = new Service1Client();

            this.ProgressBarVisibility = Visibility.Collapsed;
            this.BtnEnvoyerVisibility = Visibility.Visible;
            MajBtnEnvoyerVisibility();
            MajProgressBarVisibility();
        }
        private void btn_parcourir_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.Multiselect = true;
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                foreach (var file in dlg.FileNames)
                {
                    FileInfo fi = new FileInfo(file);

                    Document doc = new Document(fi.Name, fi.FullName, File.ReadAllText(fi.FullName), Status.Attente);
                    docList.Add(doc);
                }

                MajList();
            }
        }

        private void btn_envoyer_click(object sender, RoutedEventArgs e)
        {
            TextResultContent = "";
            MajResult();
            ThreadPool.QueueUserWorkItem((o) =>
                {
                    CallWCFDecrypt(docList);
                });
        }

        private void CallWCFDecrypt(List<Document> list)
        {
            this.BtnEnvoyerVisibility = Visibility.Collapsed;
            this.ProgressBarVisibility = Visibility.Visible;
            MajBtnEnvoyerVisibility();
            MajProgressBarVisibility();

            foreach (Document doc in list)
            {
                string[] result = clientWCF.EncryptDecrypt(doc.Name, doc.Content);

                TextResultName = result[0];

                TextResultContent += result[1];

                TextResultKey = result[2];
                TextResultMail = result[3];
                TextResultTaux = result[4];

                doc.Status = Status.Terminé;

                MajResult();
                MajList();
            }

            this.BtnEnvoyerVisibility = Visibility.Visible;
            this.ProgressBarVisibility = Visibility.Collapsed;
            MajBtnEnvoyerVisibility();
            MajProgressBarVisibility();
        }

        private void MajList()
        {
            DispatcherOperation op = Dispatcher.BeginInvoke((Action)delegate
            {
                ((ListView)lst_files).SetBinding(ListView.ItemsSourceProperty, "docList");
                ((ListView)lst_files).GetBindingExpression(ListView.ItemsSourceProperty).UpdateTarget();

                lst_files.ItemsSource = docList;
            });

            op.Wait();
        }

        private void MajBtnEnvoyerVisibility()
        {
            DispatcherOperation op = Dispatcher.BeginInvoke((Action)delegate
            {
                ((Button)btn_envoyer).SetBinding(Button.VisibilityProperty, "BtnEnvoyerVisibility");
                ((Button)btn_envoyer).GetBindingExpression(Button.VisibilityProperty).UpdateTarget();

                btn_envoyer.Visibility = BtnEnvoyerVisibility;
            });

            op.Wait();
        }

        private void MajProgressBarVisibility()
        {
            DispatcherOperation op = Dispatcher.BeginInvoke((Action)delegate
            {
                ((ProgressBar)progressBar).SetBinding(ProgressBar.VisibilityProperty, "ProgressBarVisibility");
                ((ProgressBar)progressBar).GetBindingExpression(ProgressBar.VisibilityProperty).UpdateTarget();

                progressBar.Visibility = ProgressBarVisibility;
            });

            op.Wait();
        }

        private void MajResult()
        {
            DispatcherOperation op = Dispatcher.BeginInvoke((Action)delegate
            {
                ((TextBlock)tb_resultName).SetBinding(TextBlock.TextProperty, "TextResultName");
                ((TextBlock)tb_resultName).GetBindingExpression(TextBlock.TextProperty).UpdateTarget();

                ((TextBlock)tb_resultContent).SetBinding(TextBlock.TextProperty, "TextResultContent");
                ((TextBlock)tb_resultContent).GetBindingExpression(TextBlock.TextProperty).UpdateTarget();

                ((TextBlock)tb_resultKey).SetBinding(TextBlock.TextProperty, "TextResultKey");
                ((TextBlock)tb_resultKey).GetBindingExpression(TextBlock.TextProperty).UpdateTarget();

                ((TextBlock)tb_resultMail).SetBinding(TextBlock.TextProperty, "TextResultMail");
                ((TextBlock)tb_resultMail).GetBindingExpression(TextBlock.TextProperty).UpdateTarget();

                ((TextBlock)tb_resultTaux).SetBinding(TextBlock.TextProperty, "TextResultTaux");
                ((TextBlock)tb_resultTaux).GetBindingExpression(TextBlock.TextProperty).UpdateTarget();

                tb_resultName.Text = TextResultName;
                tb_resultContent.Text = TextResultContent;
                tb_resultKey.Text = TextResultKey;
                tb_resultMail.Text = TextResultMail;
                tb_resultTaux.Text = TextResultTaux;
            });

            op.Wait();
        }
    }
}
