namespace Lab3_team1
{
    public class Process
    {
        public string PrName { get; set; }
        public string PrUser { get; set; }
        
        public int _prProcessor;
        public int PrProcessor
        {
            get { return _prProcessor; }
            set { _prProcessor = value > 0 ? value : 0; }
        }
        
        private int _prMemory;
        public int PrMemory
        {
            get { return _prMemory; }
            set { _prMemory = value > 0 ? value : 0; }
        }
        public string PrLocation { get; set; } //размещение на диске
        public string PrDescription { get; set; }

        private int _prPriority;
        public int PrPriority
        {
            get { return _prPriority; }
            set
            {
                if (value < 0)
                    _prPriority = 0;
                else if (value > 4)
                    _prPriority = 4;
                else
                    _prPriority = value;
            }
        }

        public Process(string prName = "", string prUser = "", int prProcessor = 0, int prMemory = 0, string prLocation = "", string prDescription = "", int prPriority = 4)
        {
            PrName = prName;
            PrUser = prUser;
            PrProcessor = prProcessor;
            PrMemory = prMemory;
            PrLocation = prLocation;
            PrDescription = prDescription;
            PrPriority = prPriority;
        }
        public Process(Process other)
        {
            PrName = other.PrName;
            PrUser = other.PrUser;
            PrProcessor = other.PrProcessor;
            PrMemory = other.PrMemory;
            PrLocation = other.PrLocation;
            PrDescription = other.PrDescription;
            PrPriority = other.PrPriority;
        }
        public static Process operator ++(Process process)
        {
            process.PrPriority = process.PrPriority >= 4 ? 4 : process.PrPriority + 1;
            return process;
        }
        public static Process operator --(Process process)
        {
            process.PrPriority = process.PrPriority <= 0 ? 0 : process.PrPriority - 1;
            return process;
        }
    }
}
