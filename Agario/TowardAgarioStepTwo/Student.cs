using System;
namespace TowardAgarioStepTwo
{
	public class Student : Person
	{
        public float GPA { get; private set; } = 4;
        public int ID { get; private set; } = 1;
        public Student(string name, float GPA, int ID) : base(name)
        {
            this.GPA = GPA;
            this.ID = ID;
        }
    }
}

