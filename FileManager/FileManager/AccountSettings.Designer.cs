namespace FileManager
{
    partial class AccountSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Login = new TextBox();
            newPasssword = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            oldPassword = new TextBox();
            SaveButton = new Button();
            CancelButton = new Button();
            SuspendLayout();
            // 
            // Login
            // 
            Login.Location = new Point(148, 24);
            Login.Name = "Login";
            Login.Size = new Size(326, 27);
            Login.TabIndex = 0;
            // 
            // newPasssword
            // 
            newPasssword.Location = new Point(148, 57);
            newPasssword.Name = "newPasssword";
            newPasssword.Size = new Size(326, 27);
            newPasssword.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(87, 27);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 6;
            label3.Text = "Логин:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 60);
            label4.Name = "label4";
            label4.Size = new Size(115, 20);
            label4.TabIndex = 7;
            label4.Text = "Новый пароль:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 95);
            label5.Name = "label5";
            label5.Size = new Size(209, 40);
            label5.TabIndex = 8;
            label5.Text = "Для сохранения изменений \r\nвведите пароль:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // oldPassword
            // 
            oldPassword.Location = new Point(231, 102);
            oldPassword.Name = "oldPassword";
            oldPassword.Size = new Size(243, 27);
            oldPassword.TabIndex = 9;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(304, 156);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(170, 56);
            SaveButton.TabIndex = 10;
            SaveButton.Text = "Сохранить";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(16, 156);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(170, 56);
            CancelButton.TabIndex = 11;
            CancelButton.Text = "Отмена";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // AccountSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 236);
            Controls.Add(CancelButton);
            Controls.Add(SaveButton);
            Controls.Add(oldPassword);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(newPasssword);
            Controls.Add(Login);
            Name = "AccountSettings";
            Text = "AccountSettings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Login;
        private TextBox newPasssword;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox oldPassword;
        private Button SaveButton;
        private Button CancelButton;
    }
}