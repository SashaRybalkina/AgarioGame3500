﻿using System;
using System.Text.Json;
using System.Diagnostics;

namespace TowardAgarioStepTwo
{
    public class TowardAgarioStepTwo
    {
        static void Main(string[] args)
        {
            var person = new Person();
            string message = JsonSerializer.Serialize(person);
            Console.WriteLine(message);
        }
    }
}
