using Newtonsoft.Json.Linq;
using System.Text;

public interface IAddress
{
    public string StreetName { get; set; }
    public int StreetNumber { get; set; }
    //method to print the address 
    public void PrintAddress();
}

public class Employee
{
    private string _firstName;
    private string _lastName;
    // _internalId is used internally with every record and is assigned by an  
    // external library 
    private int _internalId;
    // Temporay assigned _internald for record that has not been saved to the  
    // database yet. 
    private static int NOPREFIX = -1;
    private IAddress address;

    public Employee(string firstName, string lastName, int internalId = -1)
    {
        _firstName = _firstName;
        _lastName = _lastName;
        if (internalId == -1)
            this._internalId = IDGenerator.newId();
        else
            this._internalId = _internalId;

    }

    // This method prints only the full name 
    public void PrintEmployee()
    {
        Console.WriteLine("Name: {0} {1}", this._firstName, this._lastName);
    }


    public int PrintEmployeeId()
    {
        return this._internalId;
    }

    public override string ToString()
    {
        return $"Name: {this._firstName} {this._lastName} ID: {this._internalId}";
    }


    // Saves the Employee object as the string representation of the  
    // the class in a file provided by the argument fileName 
    public void SaveToFile(string fileName)
    {
        using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.None))
        {
            fs.Write(ASCIIEncoding.ASCII.GetBytes(this.ToString()));
        }
    }


    // static method to instantiate an Employee object from a JSON string 
    // Throws an exception if the string is malformated 
    public static Employee EmployeeFromJson(string jsonEmployeeRecord)

    {
        try
        {
            JObject messageJson = JObject.Parse(jsonEmployeeRecord);
            string firstName = (string)messageJson.GetValue("firstName");
            string lastName = (string)messageJson.GetValue("lastName");
            string internalId = (int)messageJson.GetValue("_internalId");
            return new Employee(firstName, lastName, internalId);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid Employee JSON object");
            throw;

        }
    }
}