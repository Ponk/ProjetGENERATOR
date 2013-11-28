using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFCommunication
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceCom : IServiceCom
    {
        public string[] Data;
        bool isDecrypted = false;

        public void GetData(string DocName, string DocContent, string DocKey, string DocMail, double DocTaux)
        {
            this.Data = new string[] { DocName, DocContent, DocKey, DocMail, DocTaux.ToString() };
            isDecrypted = true;
        }

        public string[] isDecrypt()
        {
            if (!isDecrypted)
                Data = null;
            
            return Data;
        }

        public void Reset()
        {
            Data = null;
            this.isDecrypted = false;
        }
    }
}
