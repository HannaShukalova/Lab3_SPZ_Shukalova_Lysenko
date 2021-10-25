using System.Collections.Generic;

namespace Lab3_team1
{
    public class Computer
    {
        public enum ProcessorSpeedChange { SpeedUp, SlowDown };
        public enum ProcessorModification { Add, Remove };

        public string CompName { get; set; }

        private int _compRam; //изменение RAM делается через свойство
        public int CompRam
        {
            get { return _compRam; }
            set { _compRam = value > 0 ? value : 0; }
        }

        //В задании опечатка. По его логике процессы должны находиться в компьютере,
        //а не в менеджере компьютеров, поскольку иначе задание не имеет смысла
        public Dictionary<string, Process> Processes { get; set; } = new Dictionary<string, Process>();

        private int _compProcessorSpeed;
        public int CompProcessorSpeed
        {
            get { return _compProcessorSpeed; }
            set { _compProcessorSpeed = value > 0 ? value : 0; }
        }

        private int _compProcessorCount;
        public int CompProcessorCount
        {
            get { return _compProcessorCount; }
            set { _compProcessorCount = value > 0 ? value : 0; }
        }

        public Computer(string compName = " ", int compRam = 0, Dictionary<string, Process> processes = null, int compProcessorSpeed = 0, int compProcessorCount = 0)
        {
            CompName = compName;
            CompRam = compRam;
            Processes = processes;
            CompProcessorSpeed = compProcessorSpeed;
            CompProcessorCount = compProcessorCount;
        }
        public void ChangeProcessorsSpeed(int changeValue, ProcessorSpeedChange processorSpeedChange)
        {
            switch (processorSpeedChange)
            {
                case ProcessorSpeedChange.SpeedUp: CompProcessorSpeed += changeValue; break;
                case ProcessorSpeedChange.SlowDown: CompProcessorSpeed -= changeValue; break;
            }
        }
        public void ModifyProcessors(int changeCount, ProcessorModification processorModification)
        {
            switch (processorModification)
            {
                case ProcessorModification.Add: CompProcessorCount += changeCount; break;
                case ProcessorModification.Remove: CompProcessorCount -= changeCount; break;
            }
        }
        public override string ToString() => $"Компьютер \"{CompName}\": кол-во ЦП {CompProcessorCount}, их скорость {CompProcessorSpeed} МГц ОЗУ {CompRam} Гб";
    }
}
