﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceWCF.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/decrypter", ReplyAction="http://tempuri.org/IService1/decrypterResponse")]
        string decrypter(string cheminFichier);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/decrypter", ReplyAction="http://tempuri.org/IService1/decrypterResponse")]
        System.Threading.Tasks.Task<string> decrypterAsync(string cheminFichier);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/authentification", ReplyAction="http://tempuri.org/IService1/authentificationResponse")]
        string authentification(string login, string mdp, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/authentification", ReplyAction="http://tempuri.org/IService1/authentificationResponse")]
        System.Threading.Tasks.Task<string> authentificationAsync(string login, string mdp, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EncryptDecrypt", ReplyAction="http://tempuri.org/IService1/EncryptDecryptResponse")]
        void EncryptDecrypt(string chaine);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EncryptDecrypt", ReplyAction="http://tempuri.org/IService1/EncryptDecryptResponse")]
        System.Threading.Tasks.Task EncryptDecryptAsync(string chaine);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Client.ServiceWCF.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Client.ServiceWCF.IService1>, Client.ServiceWCF.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string decrypter(string cheminFichier) {
            return base.Channel.decrypter(cheminFichier);
        }
        
        public System.Threading.Tasks.Task<string> decrypterAsync(string cheminFichier) {
            return base.Channel.decrypterAsync(cheminFichier);
        }
        
        public string authentification(string login, string mdp, string token) {
            return base.Channel.authentification(login, mdp, token);
        }
        
        public System.Threading.Tasks.Task<string> authentificationAsync(string login, string mdp, string token) {
            return base.Channel.authentificationAsync(login, mdp, token);
        }
        
        public void EncryptDecrypt(string chaine) {
            base.Channel.EncryptDecrypt(chaine);
        }
        
        public System.Threading.Tasks.Task EncryptDecryptAsync(string chaine) {
            return base.Channel.EncryptDecryptAsync(chaine);
        }
    }
}
