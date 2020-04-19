using System;
using System.Collections.Generic;
using System.Text;

namespace WprowadzenieDoApi
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string Job { get; set; }
        public Person() { }

        public Person(string name, string surname, string dateOfBirth, string job)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Job = job;
        }
    }
}
