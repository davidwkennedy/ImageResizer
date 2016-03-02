using System;
using System.IO;
using System.Linq;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("About to rezise all jpeg (or jpg) images in the current directory. These NEW files will all be saved to a new sub-directory named 'Resized Image Output'. ");
                Console.Write("EXISTING FILES WILL BE LEFT UNTOUCHED. After running, please review the auto-generated log.txt file. ");
                Console.Write("Smash the 'Any' key to continue. ");
                Console.ReadKey();

                var files = Directory.GetFiles("./", "*.jpeg").Union(Directory.GetFiles("./", "*.jpg")).ToArray();
                const string subPath = "Resized Image Output";
                var exists = Directory.Exists(subPath);
                if (!exists) Directory.CreateDirectory(subPath);

                foreach (var file in files)
                {
                    var message = DateTime.Now + "\tResizing : " + file;
                    Console.WriteLine(message);
                    File.AppendAllText("./log.txt", message + Environment.NewLine);
                    var resizedImage = Jpegger.ResizeOriginal(File.ReadAllBytes(file));
                    Console.Write(resizedImage.Length);
                    File.WriteAllBytes("./" + subPath + "/" + file, resizedImage);
                }
                File.AppendAllText("./log.txt", "Success!" + Environment.NewLine);
                Console.WriteLine("Success! Mash the 'Any' key to exit.");

            }
            catch (Exception e)
            {
                File.AppendAllText("./log.txt", DateTime.Now + " ERROR: " + e.Message + Environment.NewLine);
            }

        }


    }
}
