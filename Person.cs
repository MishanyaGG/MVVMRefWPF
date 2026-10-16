using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMRefWPF
{
    public class Person
    {
        public static int counter = 0; 

        public int id;          
        public string name;     
        public int age;         
        public string position; 
        public bool isDeleted;  

        public Person(string n, int a, string p)
        {
            counter++;
            id = counter;
            name = n;
            age = a;
            position = p;
            isDeleted = false;
        }

        public Person() { }

        public override string ToString()
        {
            return "#" + id + ". " + name + " (" + age + " лет) - " + position;
        }

        public void Delete()
        {
            isDeleted = true;
            counter--;
        }
    }
}
