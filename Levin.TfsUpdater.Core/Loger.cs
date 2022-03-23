using System;
using System.IO;

namespace Levin.TfsUpdater.Core
{
    public static  class Loger
    {
         static string path = "note1.txt";


        public static void  AddMessage(string message)
        {
            CheckFileLog();

            // добавление в файл
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                 writer.WriteLine("message");
            }


        }


        private static void CheckFileLog() {


            var existFile = File.Exists(path);

            if (!existFile)
            {
                File.Create(path);
            }
            

        }

    }
}
