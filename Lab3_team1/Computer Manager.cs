using System;
using System.Collections.Generic;

namespace Lab3_team1
{
    public class ComputerManager
    {
        public enum ComputerCountModification { Add, Remove };
        public enum ProcessCountModification { Add, Remove };

        public Dictionary<string, Computer> Computers { get; set; }  = new Dictionary<string, Computer>();

        public string AdminName { get; set; }
        private int AdminPassword { get; set; }

        public ComputerManager()
        {
            AdminName = "Бага Яга";
            Random random = new Random();
            AdminPassword = random.Next(-10000, 10000);
        }

        public void ModifyComputerCount(Computer computer, ComputerCountModification computerCountModification)
        {
            switch (computerCountModification)
            {
                case ComputerCountModification.Add: Computers.Add(computer.CompName, computer); break;
                case ComputerCountModification.Remove: Computers.Remove(computer.CompName); break;
            }
        }
        public bool AuthenticateUser(string name, int password) => name == AdminName && password == AdminPassword;
        public void ModifyComputerProcessCount(Computer computer, Process process, ProcessCountModification processCountModification)
        {
            switch (processCountModification)
            {
                case ProcessCountModification.Add: computer.Processes.Add(process.PrName, process); break;
                case ProcessCountModification.Remove: computer.Processes.Remove(process.PrName); break;
            }
        }
        public void ModifyComputerProcess(Process process, string prName = "", string prUser = "", int prProcessor = 0, int prMemory = 0, string prLocation = "", string prDescription = "", int prPriority = 4)
        {
            process.PrName = prName;
            process.PrUser = prUser;
            process.PrProcessor = prProcessor;
            process.PrMemory = prMemory;
            process.PrLocation = prLocation;
            process.PrDescription = prDescription;
            process.PrPriority = prPriority;
        }
        public void ModifyComputer(Computer computer, string compName = "", int compRam = 0, Dictionary<string, Process> processes = null, int compProcessorSpeed = 0, int compProcessorCount = 0)
        {
            computer.CompName = compName;
            computer.CompRam = compRam;
            computer.Processes = new Dictionary<string, Process>(processes);
            computer.CompProcessorSpeed = compProcessorSpeed;
            computer.CompProcessorCount = compProcessorCount;
        }
    }
}
