namespace FileManager
{
    partial class DesignerForm
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            BackgroundColorComboBox = new ComboBox();
            ButtonBackgroundColorComboBox = new ComboBox();
            ApplyButton = new Button();
            FontSizeNumericUpDown = new NumericUpDown();
            CancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)FontSizeNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(48, 28);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 1;
            label2.Text = "Цвет фона";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 64);
            label3.Name = "label3";
            label3.Size = new Size(118, 20);
            label3.TabIndex = 2;
            label3.Text = "Размер шрифта";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 96);
            label4.Name = "label4";
            label4.Size = new Size(96, 20);
            label4.TabIndex = 3;
            label4.Text = "Цвет кнопок";
            // 
            // BackgroundColorComboBox
            // 
            BackgroundColorComboBox.FormattingEnabled = true;
            BackgroundColorComboBox.Location = new Point(136, 25);
            BackgroundColorComboBox.Name = "BackgroundColorComboBox";
            BackgroundColorComboBox.Size = new Size(151, 28);
            BackgroundColorComboBox.TabIndex = 5;
            // 
            // ButtonBackgroundColorComboBox
            // 
            ButtonBackgroundColorComboBox.FormattingEnabled = true;
            ButtonBackgroundColorComboBox.Location = new Point(136, 93);
            ButtonBackgroundColorComboBox.Name = "ButtonBackgroundColorComboBox";
            ButtonBackgroundColorComboBox.Size = new Size(151, 28);
            ButtonBackgroundColorComboBox.TabIndex = 7;
            // 
            // ApplyButton
            // 
            ApplyButton.Location = new Point(182, 145);
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(105, 29);
            ApplyButton.TabIndex = 9;
            ApplyButton.Text = "Применить";
            ApplyButton.UseVisualStyleBackColor = true;
            ApplyButton.Click += ApplyButton_Click;
            // 
            // FontSizeNumericUpDown
            // 
            FontSizeNumericUpDown.Location = new Point(136, 57);
            FontSizeNumericUpDown.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            FontSizeNumericUpDown.Minimum = new decimal(new int[] { 7, 0, 0, 0 });
            FontSizeNumericUpDown.Name = "FontSizeNumericUpDown";
            FontSizeNumericUpDown.Size = new Size(151, 27);
            FontSizeNumericUpDown.TabIndex = 10;
            FontSizeNumericUpDown.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(25, 145);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(105, 29);
            CancelButton.TabIndex = 11;
            CancelButton.Text = "Отмена";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // DesignerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(317, 191);
            Controls.Add(CancelButton);
            Controls.Add(FontSizeNumericUpDown);
            Controls.Add(ApplyButton);
            Controls.Add(ButtonBackgroundColorComboBox);
            Controls.Add(BackgroundColorComboBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "DesignerForm";
            Text = "DesignerForm";
            ((System.ComponentModel.ISupportInitialize)FontSizeNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox BackgroundColorComboBox;
        private ComboBox comboBox2;
        private ComboBox ButtonBackgroundColorComboBox;
        private ComboBox comboBox4;
        private Button ApplyButton;
        private NumericUpDown FontSizeNumericUpDown;
        private Button CancelButton;
    }
}