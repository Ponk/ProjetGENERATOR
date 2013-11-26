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
using System.ServiceModel.Description;

namespace WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {
                
        [OperationContract]
        void EncryptDecrypt(string chaine);
    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
            {
                Uri baseAddress = new Uri("http://localhost:8080/service");

                // Create the ServiceHost.
                using (ServiceHost host = new ServiceHost(typeof(IService1), baseAddress))
                {
                    // Enable metadata publishing.
                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                    host.Description.Behaviors.Add(smb);

                    // Open the ServiceHost to start listening for messages. Since
                    // no endpoints are explicitly configured, the runtime will create
                    // one endpoint per base address for each service contract implemented
                    // by the service.
                    host.Open();

                    Console.WriteLine("The service is ready at {0}", baseAddress);
                    Console.WriteLine("Press <Enter> to stop the service.");
                    Console.ReadLine();

                    // Close the ServiceHost.
                    host.Close();
                }
            }
        }
}
