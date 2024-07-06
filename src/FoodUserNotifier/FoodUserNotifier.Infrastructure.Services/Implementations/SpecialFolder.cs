using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserNotifier.Infrastructure.Services.Implementations
{
    internal class SpecialFolder
    {
        internal string Folder;

        public SpecialFolder(string folder)
        {
            Folder = folder;
        }

        public string CreateFolder()
        {
            string _folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + Folder;

            if (Directory.Exists(_folder) == false)
            {
                Directory.CreateDirectory(_folder);
            }

            return _folder;
        }
    }
}
