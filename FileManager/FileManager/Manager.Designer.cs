namespace FileManager
{
    partial class Manager
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Path = new TextBox();
            Disks = new ComboBox();
            GoToPath = new Button();
            menuStrip1 = new MenuStrip();
            настройкиToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            accountToolStripMenuItem = new ToolStripMenuItem();
            помощьToolStripMenuItem = new ToolStripMenuItem();
            FilesList = new ListView();
            FileName = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            Delete = new Button();
            Copy = new Button();
            Cut = new Button();
            Paste = new Button();
            CreateFolder = new Button();
            Rename = new Button();
            label2 = new Label();
            newFolderName = new TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Path
            // 
            Path.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Path.Location = new Point(12, 67);
            Path.Name = "Path";
            Path.Size = new Size(844, 27);
            Path.TabIndex = 1;
            // 
            // Disks
            // 
            Disks.BackColor = SystemColors.Window;
            Disks.DropDownStyle = ComboBoxStyle.DropDownList;
            Disks.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Disks.Location = new Point(12, 33);
            Disks.Name = "Disks";
            Disks.Size = new Size(81, 28);
            Disks.TabIndex = 2;
            Disks.SelectedIndexChanged += Disks_SelectedIndexChanged;
            // 
            // GoToPath
            // 
            GoToPath.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            GoToPath.Location = new Point(862, 46);
            GoToPath.Name = "GoToPath";
            GoToPath.Size = new Size(150, 50);
            GoToPath.TabIndex = 4;
            GoToPath.Text = "Перейти";
            GoToPath.UseVisualStyleBackColor = true;
            GoToPath.Click += GoToPath_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { настройкиToolStripMenuItem, помощьToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1025, 28);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            настройкиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewToolStripMenuItem, accountToolStripMenuItem });
            настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            настройкиToolStripMenuItem.Size = new Size(98, 24);
            настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(224, 26);
            viewToolStripMenuItem.Text = "Вид";
            // 
            // accountToolStripMenuItem
            // 
            accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            accountToolStripMenuItem.Size = new Size(224, 26);
            accountToolStripMenuItem.Text = "Аккаунт";
            // 
            // помощьToolStripMenuItem
            // 
            помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            помощьToolStripMenuItem.Size = new Size(83, 24);
            помощьToolStripMenuItem.Text = "Помощь";
            // 
            // FilesList
            // 
            FilesList.Columns.AddRange(new ColumnHeader[] { FileName, columnHeader2, columnHeader3, columnHeader4 });
            FilesList.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FilesList.ForeColor = SystemColors.WindowText;
            FilesList.Location = new Point(12, 102);
            FilesList.Name = "FilesList";
            FilesList.Size = new Size(1000, 424);
            FilesList.TabIndex = 6;
            FilesList.UseCompatibleStateImageBehavior = false;
            FilesList.View = View.Details;
            FilesList.SelectedIndexChanged += FilesList_SelectedIndexChanged;
            // 
            // FileName
            // 
            FileName.Tag = "4";
            FileName.Text = "Name";
            FileName.Width = 500;
            // 
            // columnHeader2
            // 
            columnHeader2.Tag = "3";
            columnHeader2.Text = "Ext";
            columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            columnHeader3.Tag = "2";
            columnHeader3.Text = "Size";
            columnHeader3.Width = 170;
            // 
            // columnHeader4
            // 
            columnHeader4.Tag = "1";
            columnHeader4.Text = "Date";
            columnHeader4.Width = 190;
            // 
            // Delete
            // 
            Delete.Enabled = false;
            Delete.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Delete.Location = new Point(12, 569);
            Delete.Name = "Delete";
            Delete.Size = new Size(150, 50);
            Delete.TabIndex = 7;
            Delete.Text = "Удалить";
            Delete.UseVisualStyleBackColor = true;
            // 
            // Copy
            // 
            Copy.Enabled = false;
            Copy.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Copy.Location = new Point(168, 569);
            Copy.Name = "Copy";
            Copy.Size = new Size(150, 50);
            Copy.TabIndex = 8;
            Copy.Text = "Копировать";
            Copy.UseVisualStyleBackColor = true;
            // 
            // Cut
            // 
            Cut.Enabled = false;
            Cut.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Cut.Location = new Point(324, 569);
            Cut.Name = "Cut";
            Cut.Size = new Size(150, 50);
            Cut.TabIndex = 9;
            Cut.Text = "Вырезать";
            Cut.UseVisualStyleBackColor = true;
            // 
            // Paste
            // 
            Paste.Enabled = false;
            Paste.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Paste.Location = new Point(480, 569);
            Paste.Name = "Paste";
            Paste.Size = new Size(150, 50);
            Paste.TabIndex = 10;
            Paste.Text = "Вставить";
            Paste.UseVisualStyleBackColor = true;
            // 
            // CreateFolder
            // 
            CreateFolder.BackColor = Color.Gainsboro;
            CreateFolder.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            CreateFolder.Location = new Point(812, 569);
            CreateFolder.Name = "CreateFolder";
            CreateFolder.Size = new Size(201, 50);
            CreateFolder.TabIndex = 11;
            CreateFolder.Text = "Создать папку";
            CreateFolder.UseVisualStyleBackColor = false;
            // 
            // Rename
            // 
            Rename.Enabled = false;
            Rename.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Rename.Location = new Point(636, 569);
            Rename.Name = "Rename";
            Rename.Size = new Size(170, 50);
            Rename.TabIndex = 12;
            Rename.Text = "Переименовать";
            Rename.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(509, 539);
            label2.Name = "label2";
            label2.Size = new Size(101, 20);
            label2.TabIndex = 13;
            label2.Text = "Новая папка:";
            // 
            // newFolderName
            // 
            newFolderName.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            newFolderName.Location = new Point(616, 536);
            newFolderName.Name = "newFolderName";
            newFolderName.Size = new Size(397, 27);
            newFolderName.TabIndex = 14;
            // 
            // Manager
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1025, 627);
            Controls.Add(newFolderName);
            Controls.Add(label2);
            Controls.Add(Rename);
            Controls.Add(CreateFolder);
            Controls.Add(Paste);
            Controls.Add(Cut);
            Controls.Add(Copy);
            Controls.Add(Delete);
            Controls.Add(FilesList);
            Controls.Add(GoToPath);
            Controls.Add(Disks);
            Controls.Add(Path);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Name = "Manager";
            Text = "FileManager";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox Path;
        private ComboBox Disks;
        private Button GoToPath;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem настройкиToolStripMenuItem;
        private ToolStripMenuItem помощьToolStripMenuItem;
        private ListView FilesList;
        private ColumnHeader FileName;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem accountToolStripMenuItem;
        private Button Delete;
        private Button Copy;
        private Button Cut;
        private Button Paste;
        private Button CreateFolder;
        private Button Rename;
        private Label label2;
        private TextBox newFolderName;
    }
}