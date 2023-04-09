using System;
using System.Text.Json;
using System.Diagnostics;

namespace TowardAgarioStepTwo
{
    public class TowardAgarioStepTwo
    {
        static void Main(string[] args)
        {
            //var person = new Person("Aurora");
            //string message = JsonSerializer.Serialize(person);
            //Person ? temp = JsonSerializer.Deserialize<Person>(message);

            Person student = new Student("Jim", 4.0f, 1);
            string message2 = JsonSerializer.Serialize(student);

            Console.WriteLine(message2);
        }
    }
}
