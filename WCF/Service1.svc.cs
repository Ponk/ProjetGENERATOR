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
        
                
        private string GetFileContent(string path)
        {
            StreamReader myFile = new StreamReader(path, Encoding.UTF8);
            
            return myFile.ReadToEnd();
        }

        public void EncryptDecrypt(string path)
        {
            FileInfo file = new FileInfo(path);
            string chaineCodee = this.GetFileContent(path);
            string cleanString = "";

            string key;
            var result = new StringBuilder();

            GenServiceClient client = new GenServiceClient();

            for (int i = 1298; i <= 1298; i++)
            {
                key = i.ToString();
                for (int c = 0; c < chaineCodee.Length; c++)
                {
                    char character = chaineCodee[c];
                    uint charCode = (uint)character;

                    int keyPosition = c % key.Length;
                    char keyChar = key[keyPosition];

                    uint keyCode = (uint)keyChar;
                    uint combinedCode = charCode ^ keyCode;
                    char combinedChar = (char)combinedCode;

                    result.Append(combinedChar);
                    cleanString = Regex.Replace(result.ToString(), @"[^a-zA-ZéèàêùÇÈÉÀçïî@.\[\]_]", " ");
                }

                client.SendDocumentOperation(file.Name, cleanString, key);
            }            
        }
    }
}
