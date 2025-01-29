using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DTO.ViewModel
{
    public class ConnectionStringManager
    {
        private string FilePath { get; }
        public ConnectionStringManager(string filePath)
        {
            FilePath = filePath;
        }
        public string ReadMasterConnectionString()
        {
            string jsonContent = File.ReadAllText(FilePath);
            dynamic json = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);
            if (json.ContainsKey("ConnectionStrings") && json["ConnectionStrings"].ContainsKey("MasterDefaultConnection"))
            {
                string value = json["ConnectionStrings"]["MasterDefaultConnection"];
                return value;
            }
            else
            {
                return "Key 'ConnectionStrings' or property 'MasterDefaultConnection' not found";
            }
        }
        public void WriteMasterConnectionString(string newValue)
        {
            string jsonContent = File.ReadAllText(FilePath);
            dynamic json = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);
            if (json.ContainsKey("ConnectionStrings") && json["ConnectionStrings"].ContainsKey("MasterDefaultConnection"))
            {
                json["ConnectionStrings"]["MasterDefaultConnection"] = newValue;
                string modifiedJson = JsonSerializer.Serialize(json, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, modifiedJson);
            }
            else
            {
                Console.WriteLine("Key 'ConnectionStrings' or property 'MasterDefaultConnection' not found. Unable to write.");
            }
        }
        public string ReadSlaveConnectionString()
        {
            string jsonContent = File.ReadAllText(FilePath);
            dynamic json = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);
            if (json.ContainsKey("ConnectionStrings") && json["ConnectionStrings"].ContainsKey("SlaveDefaultConnection"))
            {
                string value = json["ConnectionStrings"]["SlaveDefaultConnection"];
                return value;
            }
            else
            {
                return "Key 'ConnectionStrings' or property 'SlaveDefaultConnection' not found";
            }
        }
        public void WriteSlaveConnectionString(string newValue)
        {
            string jsonContent = File.ReadAllText(FilePath);
            dynamic json = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);
            if (json.ContainsKey("ConnectionStrings") && json["ConnectionStrings"].ContainsKey("SlaveDefaultConnection"))
            {
                json["ConnectionStrings"]["SlaveDefaultConnection"] = newValue;
                string modifiedJson = JsonSerializer.Serialize(json, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, modifiedJson);
            }
            else
            {
                Console.WriteLine("Key 'ConnectionStrings' or property 'SlaveDefaultConnection' not found. Unable to write.");
            }
        }
        public void WriteConnectionStringFile()
        {
            string jsonContent = @"
            {
          ""ConnectionStrings"": {
            ""MasterDefaultConnection"": ""User Id = postgres; Password=JntK9fDWGdP5jj; Host =144.76.227.210; Database = Sorrow; Port=5432; MinPoolSize = 10; MaxPoolSize = 4096; Pooling = True; ConnectionIdleLifetime = 300;"",
            ""SlaveDefaultConnection"": ""User Id = postgres; Password=ABC@xmenx1690; Host =127.0.0.1; Database = Sorrow; Port=5432; MinPoolSize = 10; MaxPoolSize = 4096; Pooling = True; ConnectionIdleLifetime = 300;""
                                 }
            }";
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, jsonContent);
                Console.WriteLine($"Data written to {FilePath}");
            }
            else
            {
                Console.WriteLine($"File {FilePath} already exists. Not overwriting.");
            }
        }
    }
}
