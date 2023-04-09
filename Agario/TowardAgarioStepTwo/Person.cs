using System.Text.Json.Serialization;
using TowardAgarioStepTwo;

[JsonDerivedType(typeof(Person), typeDiscriminator: "Person")]
[JsonDerivedType(typeof(Student), typeDiscriminator: "Student")]
public class Person
{
    public Person(string name)
    {
        this.Name = name;
     
        ID++;
    }
   
    public string Name { get; private set; } = "Jim";
    public int ID { get; private set; } = 1;
}