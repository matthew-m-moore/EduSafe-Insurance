using System;
using System.Collections.Generic;
using System.IO;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.IO.Files;

namespace EduSafe.ConsoleApp.Scripts
{
    public class CreateNewCustomerScript : IScript
    {
        private const string _institutionalCustomer = "institutional";
        private const string _individualCustomer = "individual";

        private static readonly string _institutionalDirectoryPath = FileServerSettings.InstitutionalCustomersDirectoryPath;
        private static readonly string _individualDirectoryPath = FileServerSettings.IndividualCustomersDirectoryPath;

        public List<string> GetArgumentsList()
        {
            return new List<string>
            {
                "[1] Enter 'Individual' or 'Institutional'",
            };
        }

        public string GetFriendlyName()
        {
            return "Create New Customer Tool";
        }

        public bool GetVisibilityStatus()
        {
            return true;
        }

        public void RunScript(string[] args)
        {
            var newUniqueId = Guid.NewGuid();

            switch(args[1].ToLower())
            {
                case _individualCustomer:
                    CreateCustomer(newUniqueId, _individualDirectoryPath);
                    break;

                case _institutionalCustomer:
                    CreateCustomer(newUniqueId, _institutionalDirectoryPath);
                    break;

                default:
                    throw new Exception(string.Format("No such customer type exists for '{0}'. Please use either '{1}' or '{2}'.",
                        args[1], _individualCustomer, _institutionalCustomer));
            }
        }

        private void CreateCustomer(Guid uniqueId, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var uniqueIdAsString = uniqueId.ToString();
            var customerDirectory = Path.Combine(directoryPath, uniqueIdAsString);

            if (!Directory.Exists(customerDirectory))
                Directory.CreateDirectory(customerDirectory);

            // Other things to do to create a customer:
            // 1. Insert a new record into customer table, get back PK as ID #.
            // 2. Insert ID # and unique ID into appropriate table.
            
            // Need to think about about if/how individuals would be nested under institutional clients.
            // I would think not, since this will all be handled via table mappings.
        }
    }
}
