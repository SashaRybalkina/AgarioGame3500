using System;
using System.Text.Json;

namespace TowardAgarioStepTwo
{
	public class TowardAgarioStepTwo
	{
        //private Person person = new Person();

        static void Main(String[] args)
		{
            var person = new Person();
            
            string message = JsonSerializer.Serialize(person);
            Console.WriteLine(message);
        }
    }
}

