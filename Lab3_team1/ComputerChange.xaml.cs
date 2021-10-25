using System.Windows;
using System.Collections.Generic;

namespace Lab3_team1
{
    public partial class ComputerChange : Window
    {
        public Computer Computer { get; private set; } = new Computer();
        public bool AcceptChange { get; set; } = false;

        public ComputerChange() => InitializeComponent();

        private void BPComputerOK_Click(object sender, RoutedEventArgs e)
        {
            AcceptChange = true;
            string ComputerName, ComputerRAM, ComputerSpeed, ComputerProcessorCount;
            int ResComputerRAM, ResComputerSpeed, ResComputerProcessorCount;

            ComputerName = TBComputerName.Text;

            ComputerRAM = TBComputerRAM.Text;
            if (!int.TryParse(ComputerRAM, out ResComputerRAM))
                MessageBox.Show("Неверный формат RAM! Эти данные не будут сохранены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            ComputerSpeed = TBComputerSpeed.Text;
            if (!int.TryParse(ComputerSpeed, out ResComputerSpeed))
                MessageBox.Show("Неверный формат частоты процессора! Эти данные не будут сохранены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            ComputerProcessorCount = TBComputerProcessorCount.Text;
            if (!int.TryParse(ComputerProcessorCount, out ResComputerProcessorCount))
                MessageBox.Show("Неверный формат количества процессоров! Эти данные не будут сохранены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            Computer = new Computer(ComputerName, ResComputerRAM, new Dictionary<string,Process>(), ResComputerSpeed, ResComputerProcessorCount);
            Close();
        }
        private void BComputerCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
