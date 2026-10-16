using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVMRefWPF
{
    /// <summary>
    /// Логика взаимодействия для addEdit.xaml
    /// </summary>
    public partial class addEdit : Window
    {
        public addEdit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length > 50)
            {
                MessageBox.Show("Имя слишком длинное!");
                return;
            }

            try
            {
                int a = int.Parse(txtAge.Text);
                if (a < 0)
                {
                    MessageBox.Show("Возраст не может быть отрицательным!");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Возраст должен быть числом!");
                txtAge.Text = "0"; 
                return;
            }

            this.Close();
        }
    }
}
