using Newtonsoft.Json.Linq;
using System.Text;


namespace CodingStandardPractice
{
    public interface Address
    {
        string streetName { get; set; }
        int streetNumber { get; set; }
        void Print_Address();
    }

    public class employee
    {
        private string first_name; private string last_name;
        private int InternalId;
        private static int NO_PREFIX = 5;
        private Address address;

        public employee(string first_name, string last_name, int Internal_Id = -1){
            this.first_name = first_name;
            this.last_name = last_name;
            if (Internal_Id == -1)
                this.InternalId = IDGenerator.newId();
            else
                this.InternalId = Internal_Id;
        } 
        
        public employee employee_from_json(string jsonEmployeeRecord)
        {
            JObject messageJson = JObject.Parse(jsonEmployeeRecord);
            first_name = (string)messageJson.GetValue("first_name");
            last_name = (string)messageJson.GetValue("last_name");
            InternalId = (int)messageJson.GetValue("InternalId");
            return new employee(first_name, last_name, InternalId);
        }

        public void Print_Employee()
        {
            Console.WriteLine("Name: " + this.first_name + this.last_name); /* This prints only the full name */
        }

        public int PrintEmployeeId()
        {
            return this.InternalId;
        }

        public string MakeString()
        {
            return "Name: " + this.first_name + " " + this.last_name + " " + " ID: " + this.InternalId;
        }

        public void saveToFile(string fileName)
        {
            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                fs.Write(Encoding.ASCII.GetBytes(this.MakeString()));
            }
        }

    }
}
