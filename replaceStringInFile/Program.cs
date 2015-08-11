using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace replaceStringInFile
{
    public class Input {
        public string searchFor {get;set;}
        public string replaceWith { get; set; }

        public Input(string searchstring, string replacestring)
        {
            searchFor = searchstring;
            replaceWith = replacestring;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //
            // replaceStrinInFile.exe
            // Input parameters: search string, replacestring, root folder
            // Description: replaces string in all wanted files. 
            // Filenames to change can be specified settings. 
            //
            string m1 = "\nType a string to be replaced then press Enter. "; 
            string m2 = "\nType a string to replace with then press Enter. ";
            string m3 = "\nType root path for starting file searches for filenames defined in settings. ";
            Console.WriteLine(m1);
            string searchFor = Console.ReadLine();
            Console.WriteLine(m2);
            string replaceWith = Console.ReadLine();
            Console.WriteLine(m3);
            string path = Console.ReadLine();

            Input replaceInput = new Input(searchFor, replaceWith);

            if (Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path, replaceInput);
            }
            else
            {
                Console.WriteLine("{0} is not a valid directory.", path);
            }    
        }
        // Process all files in the directory passed in, recurse on any directories  
        // that are found, and process the files they contain. 
        public static void ProcessDirectory(string targetDirectory, Input replaceInput)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName, replaceInput);

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, replaceInput);
        }
        public static void ProcessFile(string path, Input input)
        {
            String searchfilename = Properties.Settings.Default.searchfilename;

            if (path.Contains(searchfilename))
            {
                String search =  input.searchFor;
                String newvalue = input.replaceWith;

                File.WriteAllText(path, File.ReadAllText(path).Replace(search, newvalue));
                Console.WriteLine("Processed file '{0}'.", path);
            }
        }
    }
}
