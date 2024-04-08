using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MedClass
{
    // Class storing medicine info from each CSV entry
    // New variables can be added as needed
    class Medication
    {
        public string name;
        public Medication(string[] values)
        {
            name = values[0];
        }
    }

    class CSVReader
    {
        // Function that returns contents from a CSV file as a dictionary 
        public static Dictionary<string, Medication> ReadCSV(string Filename)
        {
            // Ensure target file exists
            if (File.Exists(Filename))
            {
                StreamReader file;
                Dictionary<string, Medication> Med_dict = new Dictionary<string, Medication>();
                using (file = new StreamReader(Filename))
                {
                    // Skip header
                    string line = file.ReadLine();
                    string[] values = line.Split(',');
                    while (!file.EndOfStream)
                    {
                        // Take values from CSV line
                        line = file.ReadLine();
                        values = line.Split(',');
                        // Create instance of medication class
                        Medication medicine = new Medication(values.Skip(1).ToArray());

                        // Add instance to dictionary
                        Med_dict.Add(values[0], medicine);
                    }
                }
                return Med_dict;
            }
            else
            {
                return null;
            }
        }
    }
}
