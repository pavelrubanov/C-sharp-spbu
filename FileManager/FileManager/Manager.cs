using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace FileManager
{
    public partial class Manager : Form
    {
        private string CurrentPath = "";
        private List<string> clipboard = new();
        public ViewSettings CurrentSettings = new();
        public Account CurrentAccount;
        public Manager()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {

            /*
            DeserializeAccount();

            //CurrentAccount = new("User", "123");

            LoginForm loginForm = new LoginForm(ref CurrentAccount);
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Ошибка авторизации");
                Close();
            }
            */

            viewToolStripMenuItem.Click += OpenViewSettings;
            accountToolStripMenuItem.Click += OpenAccountSettings;
            FilesList.DoubleClick += File_DoubleClick;
            ShowDisks();
            Update();
            //DeserializeViewSettings();
            //UpdateViewSettings();
        }
        private void UpdateViewSettings()
        {
            BackColor = Color.FromName(CurrentSettings.BackgroundColor);
            FilesList.Font = new Font("Segoe UI", CurrentSettings.FontSize);
            foreach (var control in this.Controls)
            {
                Button b = control as Button;
                if (b != null)
                {                                                          //шрифт
                    b.BackColor = Color.FromName(CurrentSettings.ButtonColor);
                    b.Font = new Font("Segoe UI", CurrentSettings.FontSize);
                }
            }
        }
        private void DeserializeViewSettings()
        {
            var fStream = new FileStream("Settings.xml", FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ViewSettings));
                ViewSettings settings = (ViewSettings)serializer.Deserialize(fStream);
                CurrentSettings = settings;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fStream.Close();
            }
        }
        private void SerializeViewSettings()
        {
            var fStream = new FileStream("Settings.xml", FileMode.Open, FileAccess.Write, FileShare.None);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ViewSettings));
                serializer.Serialize(fStream, CurrentSettings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fStream.Close();
            }
        }
        private void SerializeAccount()
        {
            var binFormat = new BinaryFormatter();
            Stream fStream = new FileStream("Account.dat", FileMode.Create, FileAccess.Write, FileShare.None);
#pragma warning disable SYSLIB0011 // Тип или член устарел
            binFormat.Serialize(fStream, CurrentAccount);
#pragma warning restore SYSLIB0011 // Тип или член устарел
            fStream.Close();


        }
        private void DeserializeAccount()
        {
            var binFormat = new BinaryFormatter();
            Stream fStream = new FileStream("Account.dat", FileMode.Open, FileAccess.Read, FileShare.None);
#pragma warning disable SYSLIB0011 // Тип или член устарел
            CurrentAccount = binFormat.Deserialize(fStream) as Account;
#pragma warning restore SYSLIB0011 // Тип или член устарел
            fStream.Close();
        }
        private void ShowDisks()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Disks.Items.Add(drive.Name);
            }
            Disks.SelectedIndex = 0;
            CurrentPath = Disks.Items[0].ToString();
        }
        private void Disks_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPath = Disks.SelectedItem.ToString();
            Update();
        }
        private void Update()
        {
            Path.Text = CurrentPath;
            ShowCatalog(CurrentPath);
        }
        private bool ShowCatalog(string path)
        {
            if (Directory.Exists(path))
            {
                FilesList.Items.Clear();
                FilesList.Items.Add("<-");
                var di = new DirectoryInfo(path);
                foreach (var el in di.EnumerateDirectories())
                {
                    string[] s = { el.Name, el.Extension, "<dir>", el.CreationTime.ToShortDateString() };
                    var item = new ListViewItem(s);
                    item.Tag = el.FullName;
                    FilesList.Items.Add(item);
                }
                foreach (var el in di.EnumerateFiles())
                {
                    var size = (new FileInfo(el.FullName)).Length;
                    string[] s = { el.Name, el.Extension, size.ToString(), el.CreationTime.ToShortDateString() };
                    var item = new ListViewItem(s);
                    item.Tag = el.FullName;
                    FilesList.Items.Add(item);
                }
                Path.Text = path;
                CurrentPath = Path.Text;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void GoToPath_Click(object sender, EventArgs e)
        {
            if (!ShowCatalog(Path.Text))
            {
                MessageBox.Show("Неверный путь");
                Path.Text = CurrentPath;
            }
        }
        private void GoBack()
        {
            var newPath = System.IO.Path.GetDirectoryName(Path.Text);
            if (newPath != null)
            {
                CurrentPath = newPath;
                Update();
            }
        }

        private void FilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilesList.SelectedItems.Count > 0 && FilesList.SelectedItems[0].Index == 0)
            {
                Delete.Enabled = false;
                Copy.Enabled = false;
                Cut.Enabled = false;
                Rename.Enabled = false;
                statistics.Enabled = false;
            }
            else if (FilesList.SelectedItems.Count > 1)
            {
                Delete.Enabled = true;
                Cut.Enabled = true;
                Copy.Enabled = true;
                Rename.Enabled = false;
                statistics.Enabled = false;
            }
            else if (FilesList.SelectedItems.Count == 1)
            {
                Delete.Enabled = true;
                Cut.Enabled = true;
                Copy.Enabled = true;
                Rename.Enabled = true;
                var ext = System.IO.Path.GetExtension(FilesList.SelectedItems[0].Tag.ToString());
                if (ext == ".txt")
                {
                    statistics.Enabled = true;
                }
                else
                    statistics.Enabled = false;
            }
            else if (FilesList.SelectedItems.Count == 0)
            {
                Delete.Enabled = false;
                Copy.Enabled = false;
                Cut.Enabled = false;
                Rename.Enabled = false;
                statistics.Enabled = false;
            }
        }

        private void File_DoubleClick(object sender, EventArgs e)
        {
            if (FilesList.SelectedItems.Count > 1)
                return;

            if (FilesList.SelectedItems[0].Index == 0)
            {
                GoBack();
                return;
            }

            string new_path = FilesList.SelectedItems[0].Tag.ToString();
            if (File.Exists(new_path))
            {
                try
                {
                    Process.Start(new_path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            ShowCatalog(new_path);
        }

        # region [buttons]
        private void CreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(CurrentPath + "\\" + newFolderName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Update();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem el in FilesList.SelectedItems)
            {
                File.Delete(el.Tag.ToString());
            }
            Update();
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            var r = new RenameForm(FilesList.SelectedItems[0].Tag.ToString());
            r.ShowDialog();
            Update();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            clipboard.Clear();
            foreach (ListViewItem el in FilesList.SelectedItems)
            {
                clipboard.Add(el.Tag.ToString());
            }
            Paste.Enabled = true;
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            bool succes = true;
            foreach (var filePath in clipboard)
            {
                FileInfo fileInf = new FileInfo(filePath);
                string newFilePath = CurrentPath + "\\" + fileInf.Name;
                try
                {
                    File.Copy(filePath, newFilePath);
                }
                catch (Exception ex)
                {
                    succes = false;
                    MessageBox.Show(ex.Message);
                }
            }
            if (succes) clipboard.Clear();
            Update();
            Paste.Enabled = false;
        }

        private void Cut_Click(object sender, EventArgs e)
        {

        }
        #endregion
        private void OpenViewSettings(object sender, EventArgs e)
        {
            var designForm = new DesignerForm();
            if (DialogResult.OK == designForm.ShowDialog())
            {
                CurrentSettings = designForm.settings;
                UpdateViewSettings();
                SerializeViewSettings();
            }
        }
        private void OpenAccountSettings(object sender, EventArgs e)
        {
            AccountSettings f = new AccountSettings(ref CurrentAccount);
            if (f.ShowDialog() == DialogResult.OK)
            {
                SerializeAccount();
            }
        }

        private void statistics_Click(object sender, EventArgs e)
        {
            string filePath = FilesList.SelectedItems[0].Tag.ToString();
            string text = File.ReadAllText(filePath);

            int LinesCount = text.Split('\n').Count();

            string[] Words = text.Split(new char[] {' ', ',', '.', '?', '!', ';',')','(' },StringSplitOptions.RemoveEmptyEntries);
            int WordsCount = Words.Length;

            //топ-10 наиболее часто встречающихся слов длиной больше 5 букв.
            var PerfectWords = (from word in Words.AsParallel()
                                where word.Length > 5)
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count())
                .OrderByDescending(pair => pair.Value)
                .Take(10);

            string topWordsString = "";
            foreach (var  word in PerfectWords)
            {
                topWordsString += word + ", ";
            }
            MessageBox.Show("кол-во слов: " + WordsCount +
                            "\nкол-во строк " + LinesCount +
                            "\nтоп слов: " + topWordsString);
        }
    }
}