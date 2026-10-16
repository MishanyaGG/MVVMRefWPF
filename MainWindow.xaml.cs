using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMRefWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int selectedIdx = -1;

        private bool flag = false;

        public MainWindow()
        {
            InitializeComponent();

            DataKeeper.LoadFromFile();

            for (int i = 0; i < DataKeeper.people.Count; i++)
            {
                listPerson.Items.Add(DataKeeper.people[i]);
            }

            lblStatus.Text = "Загружено " + DataKeeper.people.Count + " чел.";

            if (DataKeeper.currentUser == "admin")
            {
                btnClear.IsEnabled = true;
            }
            else
            {
                btnClear.IsEnabled = false;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addEdit win = new addEdit();
            win.ShowDialog();

            if (win.txtName.Text != "" && win.txtAge.Text != "")
            {
                if (int.Parse(win.txtAge.Text) >= 18 && int.Parse(win.txtAge.Text) <= 100)
                {
                    Person p = new Person(win.txtName.Text,
                        int.Parse(win.txtAge.Text),
                        win.txtPos.Text == "" ? "сотрудник" : win.txtPos.Text);

                    DataKeeper.people.Add(p);
                    listPerson.Items.Add(p);

                    DataKeeper.SaveToFile();

                    lblStatus.Text = "Добавлен: " + p.name;
                    lblStatus.Foreground = new SolidColorBrush(Colors.Green);

                    Thread.Sleep(500);
                    lblStatus.Foreground = new SolidColorBrush(Colors.Gray);
                }
                else
                {
                    MessageBox.Show("Возраст должен быть от 18 до 100 лет!!!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!!!");
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if ((listPerson.SelectedIndex != -1) == true)
            {
                Person p = listPerson.SelectedItem as Person;

                if (p.id == 999)
                {
                    MessageBox.Show("Этого сотрудника нельзя удалить!");
                    return;
                }

                listPerson.Items.Remove(p);
                DataKeeper.people.Remove(p);
                p.Delete();

                DataKeeper.SaveToFile();

                lblStatus.Text = "Удалён";
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления!");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listPerson.SelectedIndex != -1)
            {
                Person p = listPerson.SelectedItem as Person;

                addEdit win = new addEdit();
                win.txtName.Text = p.name;
                win.txtAge.Text = p.age.ToString();
                win.txtPos.Text = p.position;
                win.ShowDialog();

                if (win.txtName.Text != "" && win.txtAge.Text != "")
                {
                    if (int.Parse(win.txtAge.Text) >= 18 && int.Parse(win.txtAge.Text) <= 100)
                    {
                        p.name = win.txtName.Text;
                        p.age = int.Parse(win.txtAge.Text);
                        p.position = win.txtPos.Text == "" ? "сотрудник" : win.txtPos.Text;

                        int idx = listPerson.SelectedIndex;
                        listPerson.Items.RemoveAt(idx);
                        listPerson.Items.Insert(idx, p);

                        DataKeeper.SaveToFile();
                        lblStatus.Text = "Отредактирован: " + p.name;
                    }
                    else
                    {
                        MessageBox.Show("Возраст должен быть от 18 до 100 лет!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!!!");
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!");
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string filename = "C:\\temp\\export_" + DateTime.Now.Ticks + ".txt";

            try
            {
                System.IO.File.WriteAllText(filename, "=== ОТЧЁТ ===\n");
                for (int i = 0; i < DataKeeper.people.Count; i++)
                {
                    System.IO.File.AppendAllText(filename,
                        (i + 1) + ") " + DataKeeper.people[i].ToString() + "\n");
                }
                System.IO.File.AppendAllText(filename, "=== ВСЕГО: " +
                    DataKeeper.people.Count + " ===\n");

                MessageBox.Show("Экспорт в " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            string msg = flag ? "Точно очистить?" : "Вы уверены?";

            var result = MessageBox.Show(msg, "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                listPerson.Items.Clear();
                DataKeeper.people.Clear();
                DataKeeper.SaveToFile();

                flag = !flag;

                lblStatus.Text = "Очищено";
            }
        }
    }
}