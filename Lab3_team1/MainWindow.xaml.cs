using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab3_team1
{
    public partial class MainWindow : Window
    {
        public ComputerManager computerManager { get; private set; } = new ComputerManager();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            //for quicker testing
            Computer[] ComputerInput = new Computer[3];
            ComputerInput[0] = new Computer("Granny", 64, new Dictionary<string, Process>(), 4500, 16);
            ComputerInput[1] = new Computer("Pit Lord", 16, new Dictionary<string, Process>(), 3200, 8);
            ComputerInput[2] = new Computer("Phoenix", 4, new Dictionary<string, Process>(), 1600, 2);

            Process[] ProcessInput = new Process[7];
            ProcessInput[0] = new Process("Calculator", "Andrew", 4, 2, "C:\\Program Files(x86)", "just studying", 4);
            ProcessInput[1] = new Process("Notepad", "Anna", 0, 1, "C:\\Program Files(x86)", "for notes", 3);
            ProcessInput[2] = new Process("Player", "System", 1, 4, "C:\\Program Files", "music", 2);
            ProcessInput[3] = new Process("Adobe reader", "John", 1, 5, "C:\\Program Files\\User", "pdf file", 1);
            ProcessInput[4] = new Process("Skype", "Will", 1, 4, "C:\\Windows\\Local", "talking online", 0);
            ProcessInput[5] = new Process("Dead By Daylight", "Andrew", 0, 10, "C:\\Steam\\Games", "playing and chilling", 1);
            ProcessInput[6] = new Process("Telegram", "Parents", 8, 7, "C:\\Messengers\\Telegram", "online texting", 1);

            for (int i = 0; i < ComputerInput.Length; i++)
                computerManager.Computers.Add(ComputerInput[i].CompName, ComputerInput[i]);

            foreach (var item in computerManager.Computers)
                for (int i = 0; i < 7; i++)
                    item.Value.Processes.Add(ProcessInput[i].PrName, ProcessInput[i]);

            foreach (var item in computerManager.Computers)
                CBCurrentComputer.Items.Add(item.Value.ToString());

            CBCurrentComputer.SelectedIndex = 0;
        }
        private void BChangeComInfo_Click(object sender, RoutedEventArgs e)
        {
            if (CBCurrentComputer.SelectedIndex < 0)
            {
                MessageBox.Show("Элемент для изменения не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ComputerChange computerChange = new ComputerChange();
            computerChange.Title = "Изменить текущий компьютер";
            computerChange.ShowDialog();

            if (!computerChange.AcceptChange) return;

            foreach (var item in computerManager.Computers)
            {
                if (item.Value.ToString() == CBCurrentComputer.Items[CBCurrentComputer.SelectedIndex].ToString())
                {
                    computerManager.Computers.Remove(item.Key);
                    CBCurrentComputer.Items.RemoveAt(CBCurrentComputer.SelectedIndex);
                    break;
                }
            }
            if (computerManager.Computers.ContainsKey(computerChange.Computer.CompName))
            {
                MessageBox.Show("Элемент с таким именем уже существует. Оно будет изменено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                computerChange.Computer.CompName += $"_copy_{DateTime.Now}";
            }

            computerManager.Computers.Add(computerChange.Computer.CompName, computerChange.Computer);
            CBCurrentComputer.Items.Add(computerChange.Computer.ToString());
        }
        private void BAddCom_Click(object sender, RoutedEventArgs e)
        {
            ComputerChange computerChange = new ComputerChange();
            computerChange.Title = "Добавить компьютер";
            computerChange.ShowDialog();

            if (!computerChange.AcceptChange) return;

            if (computerManager.Computers.ContainsKey(computerChange.Computer.CompName))
                MessageBox.Show("Компьютер с таким именем уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                computerManager.Computers.Add(computerChange.Computer.CompName, computerChange.Computer);
                CBCurrentComputer.Items.Add(computerChange.Computer.ToString());
            }
        }
        private void BDeleteCom_Click(object sender, RoutedEventArgs e)
        {
            if (CBCurrentComputer.SelectedIndex < 0)
            {
                MessageBox.Show("Элемент для удаления не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var item in computerManager.Computers)
            {
                if (item.Value.ToString() == CBCurrentComputer.Items[CBCurrentComputer.SelectedIndex].ToString())
                {
                    computerManager.Computers.Remove(item.Key);
                    CBCurrentComputer.Items.RemoveAt(CBCurrentComputer.SelectedIndex);
                    break;
                }
            }
        }
        private void CBCurrentComputer_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => BChangeComInfo_Click(sender, e);

        private void BAddProcess_Click(object sender, RoutedEventArgs e)
        {
            if (CBCurrentComputer.SelectedIndex < 0)
            {
                MessageBox.Show("Компьютер не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProcessChange processChange = new ProcessChange();
            processChange.Title = "Добавить процесс";
            processChange.ShowDialog();

            if (!processChange.AcceptChange) return;

            foreach (var item in computerManager.Computers)
            {
                if (item.Value.ToString() == CBCurrentComputer.Items[CBCurrentComputer.SelectedIndex].ToString())
                {
                    if (item.Value.Processes.ContainsKey(processChange.Process.PrName))
                    {
                        MessageBox.Show("Процесс с таким именем уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        DGCurrentProcesses.Items.Add(processChange.Process);
                        computerManager.Computers[item.Key].Processes.Add(processChange.Process.PrName, processChange.Process);
                        break;
                    }
                }
            }
            UpdateBottomLabels();
        }
        private void BDeleteProcess_Click(object sender, RoutedEventArgs e)
        {
            if (CBCurrentComputer.SelectedIndex < 0)
            {
                MessageBox.Show("Компьютер не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DGCurrentProcesses.SelectedIndex < 0)
            {
                MessageBox.Show("Процесс для удаления не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var ComputerItem in computerManager.Computers)
            {
                if (ComputerItem.Value.ToString() == CBCurrentComputer.Items[CBCurrentComputer.SelectedIndex].ToString())
                {
                    foreach (var ProcessItem in ComputerItem.Value.Processes)
                    {
                        if (ProcessItem.Value == DGCurrentProcesses.SelectedItem as Process)
                        {
                            DGCurrentProcesses.Items.RemoveAt(DGCurrentProcesses.SelectedIndex);
                            computerManager.Computers[ComputerItem.Key].Processes.Remove(ProcessItem.Key);
                            break;
                        }
                    }
                }
            }
            UpdateBottomLabels();
        }
        private void BChangeProcessInfo_Click(object sender, RoutedEventArgs e)
        {
            if (CBCurrentComputer.SelectedIndex < 0)
            {
                MessageBox.Show("Компьютер для не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DGCurrentProcesses.SelectedIndex < 0)
            {
                MessageBox.Show("Процесс для изменения не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ProcessChange processChange = new ProcessChange();
            processChange.Title = "Изменить процесс";
            processChange.ShowDialog();

            if (!processChange.AcceptChange) return;


            foreach (var ComputerItem in computerManager.Computers)
            {
                if (ComputerItem.Value.ToString() == CBCurrentComputer.Items[CBCurrentComputer.SelectedIndex].ToString())
                {
                    foreach (var ProcessItem in ComputerItem.Value.Processes)
                    {
                        if (ProcessItem.Value == DGCurrentProcesses.SelectedItem as Process)
                        {
                            DGCurrentProcesses.Items.RemoveAt(DGCurrentProcesses.SelectedIndex);
                            computerManager.Computers[ComputerItem.Key].Processes.Remove(ProcessItem.Key);
                            break;
                        }
                    }
                }
            }

            foreach (var item in computerManager.Computers)
            {
                if (item.Value.ToString() == CBCurrentComputer.Items[CBCurrentComputer.SelectedIndex].ToString())
                {
                    if (item.Value.Processes.ContainsKey(processChange.Process.PrName))
                    {
                        MessageBox.Show("Элемент с таким именем уже существует. Оно будет изменено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        processChange.Process.PrName += $"_copy_{DateTime.Now}";
                    }

                    DGCurrentProcesses.Items.Add(processChange.Process);
                    computerManager.Computers[item.Key].Processes.Add(processChange.Process.PrName, processChange.Process);
                    break;
                }

            }
            UpdateBottomLabels();
        }
        private void DGCurrentProcesses_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => BChangeProcessInfo_Click(sender, e);

        private void CBCurrentComputer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DGCurrentProcesses.Items.Clear();
            if (CBCurrentComputer.SelectedIndex < 0) return;

            foreach (var ComputerItem in computerManager.Computers)
            {
                if (ComputerItem.Value.ToString() == CBCurrentComputer.Items[CBCurrentComputer.SelectedIndex].ToString())
                {
                    foreach (var ProcessItem in ComputerItem.Value.Processes)
                    {
                        DGCurrentProcesses.Items.Add(ProcessItem.Value);
                    }
                    UpdateBottomLabels();
                }
            }
        }
        private void UpdateBottomLabels()
        {
            Random random = new Random();
            LbProcessCount.Content = "Количество процессов: " + DGCurrentProcesses.Items.Count.ToString();
            LbProcessUtilization.Content = "Загрузка ЦП: " + random.Next(0, 100) + " %";
            LbCPUUsage.Content = "Использование ОЗУ: " + random.Next(0, 100) + " %";
        }
    }
}


