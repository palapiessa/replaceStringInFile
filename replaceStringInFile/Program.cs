using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace replaceStringInFile
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //
            // replaceStrinInFile.exe
            // Input parameter: root folder
            // Description: replaces string in all wanted files. 
            // Filenames to change can be specified. Root folder for
            // filesearches must be given.
            //
            //String filepath2 = "XMLFile2.xml";
            //Console.WriteLine("Path: " + filepath);
            //Console.WriteLine("Search: " + search);
            //Console.WriteLine("replace: " +replacement);
            foreach (string path in args)
            {
                if (File.Exists(path))
                {
                    // This path is a file
                    ProcessFile(path);
                }
                else if (Directory.Exists(path))
                {
                    // This path is a directory
                    ProcessDirectory(path);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                }
            }        
            
        }
        // Process all files in the directory passed in, recurse on any directories  
        // that are found, and process the files they contain. 
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }
        public static void ProcessFile(string filepath)
        {
            String searchfilepath = "reference values.xml";

            if (filepath.Contains(searchfilepath))
            {
                String search = "0\\Mode\\UserDefined$0\\Err\\0$0\\Value\\100.00$";
                String replacement = "0\\Err\\0$0\\Mode\\UserDefined$0\\Value\\100.00$";

                File.WriteAllText(filepath, File.ReadAllText(filepath).Replace(search, replacement));
                //Console.WriteLine("Processed file '{0}'.", path);
            }
        }
    }
}
