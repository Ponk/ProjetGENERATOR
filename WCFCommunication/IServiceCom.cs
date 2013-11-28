using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFCommunication
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IServiceCom
    {
        [OperationContract]
        void GetData(string DocName, string DocContent, string DocKey, string DocMail, double DocTaux);

        [OperationContract]
        string[] isDecrypt();

        [OperationContract]
        void Reset();
    }
}
