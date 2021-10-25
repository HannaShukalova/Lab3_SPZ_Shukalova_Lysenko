using System.Windows;

namespace Lab3_team1
{
    public partial class ProcessChange : Window
    {
        public Process Process { get; private set; } = new Process();
        public bool AcceptChange { get; set; } = false;

        public ProcessChange() => InitializeComponent();

        private void BProcessOK_Click(object sender, RoutedEventArgs e)
        {
            AcceptChange = true;
            string ProcessName, ProcessUser, ProcessProcessor, ProcessMemory, ProcessLocation, ProcessDescription, ProcessPriority;
            int ResProcessProcessor, ResProcessMemory, ResProcessPriority;

            ProcessName = TBProcessName.Text;
            ProcessUser = TBProcessUser.Text;

            ProcessProcessor = TBProcessProcessor.Text;
            if (!int.TryParse(ProcessProcessor, out ResProcessProcessor))
                MessageBox.Show("Неверный формат количества процессоров! Эти данные не будут сохранены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            ProcessMemory = TBProcessMemory.Text;
            if (!int.TryParse(ProcessMemory, out ResProcessMemory))
                MessageBox.Show("Неверный формат количества памяти! Эти данные не будут сохранены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            ProcessLocation = TBProcessLocation.Text;
            ProcessDescription = TBProcessDescription.Text;

            ProcessPriority = TBProcessPriority.Text;
            if (!int.TryParse(ProcessPriority, out ResProcessPriority))
                MessageBox.Show("Неверный формат приоритета процесса! Эти данные не будут сохранены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            Process = new Process(ProcessName, ProcessUser, ResProcessProcessor, ResProcessMemory, ProcessLocation, ProcessDescription, ResProcessPriority);
            Close();
        }
        private void BProcessCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
