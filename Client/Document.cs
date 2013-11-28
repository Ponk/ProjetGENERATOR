using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public enum Status
    {
        Attente = 0,
        Traitement = 1,
        Terminé = 2
    }

    public class Document
    {
        public string Name { get; set; }
        public string  Path { get; set; }
        public string Content { get; set; }
        public Status Status { get; set; }


        public Document()
        {

        }

        public Document(string name, string path, string content, Status etat)
        {
            this.Name = name;
            this.Path = path;
            this.Content = content;
            this.Status = etat;
        }
    }
}
