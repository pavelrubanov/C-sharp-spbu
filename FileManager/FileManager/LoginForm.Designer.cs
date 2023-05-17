namespace FileManager
{
    partial class LoginForm
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
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            LoginTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            LogButton = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(86, 33);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 0;
            label1.Text = "Авторизация";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 70);
            label3.Name = "label3";
            label3.Size = new Size(55, 20);
            label3.TabIndex = 2;
            label3.Text = "Логин:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 104);
            label4.Name = "label4";
            label4.Size = new Size(65, 20);
            label4.TabIndex = 3;
            label4.Text = "Пароль:";
            // 
            // LoginTextBox
            // 
            LoginTextBox.Location = new Point(105, 67);
            LoginTextBox.Name = "LoginTextBox";
            LoginTextBox.Size = new Size(190, 27);
            LoginTextBox.TabIndex = 4;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(105, 103);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.Size = new Size(190, 27);
            PasswordTextBox.TabIndex = 5;
            // 
            // LogButton
            // 
            LogButton.Location = new Point(29, 146);
            LogButton.Name = "LogButton";
            LogButton.Size = new Size(266, 44);
            LogButton.TabIndex = 12;
            LogButton.Text = "Войти";
            LogButton.UseVisualStyleBackColor = true;
            LogButton.Click += LogButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(63, 217);
            label2.Name = "label2";
            label2.Size = new Size(187, 40);
            label2.TabIndex = 13;
            label2.Text = "Стандартный логин: User\r\nСтандартный пароль: 123";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(329, 281);
            Controls.Add(label2);
            Controls.Add(LogButton);
            Controls.Add(PasswordTextBox);
            Controls.Add(LoginTextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "LoginForm";
            Text = "LoginForm";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox LoginTextBox;
        private TextBox PasswordTextBox;
        private Button LogButton;
        private Label label2;
    }
}