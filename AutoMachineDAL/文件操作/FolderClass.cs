using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AutoMachineDAL
{
    public  class FolderClass
    {
        public bool CreateDirectory(string FolderPath)
        {
            bool flag = Directory.Exists(FolderPath);
            if (!flag)
            {
                Directory.CreateDirectory(FolderPath);
                return false;
            }
            else
            {
                return true;

            }

        }


        public bool DeleteDirectory(string FolderPath)
        {

            if (Directory.Exists(FolderPath))
            {
                Directory.Delete(FolderPath, true);

            }
            else
            {
                return false;

            }
            return true;

        }



    }
}
