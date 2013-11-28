using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WCF.CAM
{
    public class CAM_Decryptage
    {

        private WCF.CW.CW_Decryptage oDecrypt;
        private bool result;

        public CAM_Decryptage()
        {
            this.oDecrypt = new CW.CW_Decryptage();

                        

        }

        public bool CAM_generatePdf(string titre)
        {
            this.result = this.oDecrypt.CW_generatePdf(titre);

            // Compose a string that consists of three lines.
            string text = DateTime.Now + " : Génération d'un pdf dont le titre est " + titre + ".";

            // Write the string to a file.
            //System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt");
            File.AppendAllText("c:\\test.txt", text + "\r\n");
            
            //file.Close();


            return this.result;
        }


    }
}