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
    public partial class AccountSettings : Form
    {
        private Account account;
        public AccountSettings(ref Account account)
        {
            this.account = account;
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            Login.Text = account.Name;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!account.CompareWithPassword(oldPassword.Text))
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            if (newPasssword.Text == "")
            {
                if (account.Name == Login.Text)
                {
                    MessageBox.Show("Изменений не произошло");
                }
                else
                {
                    account.ChangeName(Login.Text, oldPassword.Text);
                    MessageBox.Show("Логин успешно изменен");
                }
            }
            else
            {
                if (account.Name == Login.Text)
                {
                    account.ChangePassword(newPasssword.Text, oldPassword.Text);
                    MessageBox.Show("Пароль успешно изменен");
                }
                else
                {
                    account.ChangeName(Login.Text, oldPassword.Text);
                    account.ChangePassword(newPasssword.Text, oldPassword.Text);
                    MessageBox.Show("Логин и пароль успешно изменены");
                }
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
