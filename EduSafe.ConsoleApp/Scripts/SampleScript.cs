using EduSafe.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;

namespace EduSafe.ConsoleApp.Scripts
{
    public class SampleScript : IScript
    {
        public List<string> GetArgumentsList()
        {
            return new List<string>
            {
                "[1] Enter anything as this argument.",
            };
        }

        public string GetFriendlyName()
        {
            return "Sample";
        }

        public bool GetVisibilityStatus()
        {
            return true;
        }

        public void RunScript(string[] args)
        {
            Console.WriteLine("I am a sample script. Hear me roar!!~");
        }
    }
}
