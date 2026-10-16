using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MVVMRefWPF
{
    public static class DataKeeper
    {
        public static List<Person> people = new List<Person>();

        public static string path = "C:\\temp\\people.txt";

        public static string currentUser = "admin";

        public static void SaveToFile()
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\Mikhail\\Documents\\test.txt");
            for (int i = 0; i < people.Count; i++)
            {
                sw.WriteLine(people[i].id + ";" + people[i].name + ";"
                    + people[i].age + ";" + people[i].position);
            }
            sw.Close();
        }

        public static void LoadFromFile()
        {
            try
            {
                StreamReader sr = new StreamReader("C:\\temp\\people.txt");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    Person p = new Person();
                    p.id = int.Parse(parts[0]);
                    p.name = parts[1];
                    p.age = int.Parse(parts[2]);
                    p.position = parts[3];
                    people.Add(p);
                }
                sr.Close();
            }
            catch { }
        }
    }
}
