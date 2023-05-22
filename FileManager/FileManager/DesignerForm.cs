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
    public partial class DesignerForm : Form
    {
        public ViewSettings settings = new ViewSettings();
        public DesignerForm()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            BackgroundColorComboBox.Items.Add("White");
            BackgroundColorComboBox.Items.Add("LightBlue");
            BackgroundColorComboBox.Items.Add("PapayaWhip");
            BackgroundColorComboBox.Items.Add("LIghtYellow");
            BackgroundColorComboBox.SelectedIndex = 0;

            ButtonBackgroundColorComboBox.Items.Add("Gainsboro");
            ButtonBackgroundColorComboBox.Items.Add("LightBlue");
            ButtonBackgroundColorComboBox.Items.Add("LightPink");
            ButtonBackgroundColorComboBox.Items.Add("WhiteSmoke");
            ButtonBackgroundColorComboBox.SelectedIndex = 0;


        }


        private void ApplyButton_Click(object sender, EventArgs e)
        {
            settings.BackgroundColor = BackgroundColorComboBox.SelectedItem.ToString();
            settings.ButtonColor = ButtonBackgroundColorComboBox.SelectedItem.ToString();
            settings.FontSize = Convert.ToInt32(FontSizeNumericUpDown.Value);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
