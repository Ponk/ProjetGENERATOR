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
using WCF.ServiceCom;

namespace WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {

        public string[] EncryptDecrypt(string name, string content)
        {
            GenServiceClient client = new GenServiceClient();
            ServiceComClient clientCom = new ServiceComClient();
            string[] result = new string[5];
            string cleanString = "";

            for (int key = int.Parse("1299"); key > int.Parse("1296"); key--)
			{
			    //Application d'une clé
                cleanString = this.ApplyKey(content, key.ToString());

                if (isTextValid(cleanString))
                {
                    //Envoi d'un texte décrypté au service Java EE
                    cleanString = Regex.Replace(cleanString, @"[^a-zA-ZéèâàêùÇÈÉÀçïî@.\[\]_]", " ");
                    client.SendDocumentOperation(name, cleanString, key.ToString());

                    while (clientCom.isDecrypt() == null)
                    {
                    }

                    //Name
                    result[0] = clientCom.isDecrypt()[0];
                    //Content
                    result[1] = clientCom.isDecrypt()[1];
                    //Key
                    result[2] = clientCom.isDecrypt()[2];
                    //Mail
                    result[3] = clientCom.isDecrypt()[3];
                    //Taux
                    result[4] = clientCom.isDecrypt()[4];

                    clientCom.Reset();
                    return result;
                }
			}
            return null;
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
            //Pourcentage de texte pour l'échantillon
            int sampleCount = 10;
            int charCount = sampleCount * text.Length / 100;
            string sample = text.Substring(0, charCount);

            Char[] badChars = { '#', '|', '>', '~', 'Á', 'Ã', 'Ä', 'Å', 'Æ', 'Ì', 'Í', 'Î', 'Ï', 'Ð', 'Ñ', 'Ò', 'Ó', 'Õ', 'Ö', 'Ø', 'Œ', 'Š', 'þ', 'Ý', 'Ÿ', 'á', 'ã', 'ä', 'å', 'æ', 'ð', 'ñ', 'ò', 'ó', 'õ', 'ö', 'ø', 'š', 'Þ', 'ý', 'ÿ', '¢', 'ß', '¥', '£', '™', '©', '®', 'ª', '¼', '½', '¾', 'º', '§', '¤', '¦', '¬', '‰' };

            if (badChars.Any(l => sample.Contains(l)))
                return false;
            else
                return true;
        }
    }
}
