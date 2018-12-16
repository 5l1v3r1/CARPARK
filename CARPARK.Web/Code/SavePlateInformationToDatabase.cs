using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace CARPARK.Web.Code
{
    public class SavePlateInformationToDatabase
    {
        public static void Listeleme(string path, List<string> liste)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] rgFiles = di.GetFiles();
            foreach (FileInfo fi in rgFiles)
            {
                liste.Add(@"\AracPlaka\" + fi.Name);
            }
        }


        public static List<string> SavePlateInformation()
        {
            var klasor = @"C:\Users\Ahmet Turan Alp\Documents\Github -Projects\CARPARK\CARPARK.Web\AracPlaka";
            List<string> dataKloser = new List<string>();
            Listeleme(klasor, dataKloser);
            return dataKloser;

        }
    }
}