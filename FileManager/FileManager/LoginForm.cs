using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class LoginForm : Form
    {
        private Account account;
        public LoginForm(ref Account account)
        {
            this.account = account;
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void LogButton_Click(object sender, EventArgs e)
        {
            if (LoginTextBox.Text == account.Name && account.CompareWithPassword(PasswordTextBox.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
