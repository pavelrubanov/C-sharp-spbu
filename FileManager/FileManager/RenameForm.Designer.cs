namespace FileManager
{
    partial class RenameForm
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
            confirm = new Button();
            cancel = new Button();
            FileCatalog = new Label();
            label2 = new Label();
            label1 = new Label();
            newName = new TextBox();
            label3 = new Label();
            Ext = new Label();
            SuspendLayout();
            // 
            // confirm
            // 
            confirm.Location = new Point(240, 152);
            confirm.Name = "confirm";
            confirm.Size = new Size(175, 46);
            confirm.TabIndex = 0;
            confirm.Text = "Подтвердить";
            confirm.UseVisualStyleBackColor = true;
            confirm.Click += confirm_Click;
            // 
            // cancel
            // 
            cancel.Location = new Point(21, 152);
            cancel.Name = "cancel";
            cancel.Size = new Size(173, 46);
            cancel.TabIndex = 1;
            cancel.Text = "Отмена";
            cancel.UseVisualStyleBackColor = true;
            cancel.Click += cancel_Click;
            // 
            // FileCatalog
            // 
            FileCatalog.AutoSize = true;
            FileCatalog.Location = new Point(109, 26);
            FileCatalog.Name = "FileCatalog";
            FileCatalog.Size = new Size(50, 20);
            FileCatalog.TabIndex = 2;
            FileCatalog.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 69);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 3;
            label2.Text = "Имя файла:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 26);
            label1.Name = "label1";
            label1.Size = new Size(66, 20);
            label1.TabIndex = 4;
            label1.Text = "Каталог:";
            // 
            // newName
            // 
            newName.Location = new Point(109, 66);
            newName.Name = "newName";
            newName.Size = new Size(306, 27);
            newName.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(65, 104);
            label3.Name = "label3";
            label3.Size = new Size(38, 20);
            label3.TabIndex = 6;
            label3.Text = "Тип:";
            // 
            // Ext
            // 
            Ext.AutoSize = true;
            Ext.Location = new Point(109, 104);
            Ext.Name = "Ext";
            Ext.Size = new Size(50, 20);
            Ext.TabIndex = 7;
            Ext.Text = "label4";
            // 
            // RenameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(438, 220);
            Controls.Add(Ext);
            Controls.Add(label3);
            Controls.Add(newName);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(FileCatalog);
            Controls.Add(cancel);
            Controls.Add(confirm);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "RenameForm";
            Text = "RenameForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button confirm;
        private Button cancel;
        private Label FileCatalog;
        private Label label2;
        private Label label1;
        private TextBox newName;
        private Label label3;
        private Label Ext;
    }
}