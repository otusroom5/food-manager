using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserNotifier.Infrastructure.Smtp.Services.Implementations
{
    public class SpecilaFolderReport
    {
       

        public SpecilaFolderReport()
        {
          
        }


        public string CreateFolderReport(string folder)
        {
            string _folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + folder;

            if (Directory.Exists(_folder) == false)
            {
                Directory.CreateDirectory(_folder);
            }

            return _folder;
        }


    }
}
