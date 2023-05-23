using System.Net;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

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

            DeserializeAccount();

            LoginForm loginForm = new LoginForm(ref CurrentAccount);
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Ошибка авторизации");
                Close();
            }

            viewToolStripMenuItem.Click += OpenViewSettings;
            accountToolStripMenuItem.Click += OpenAccountSettings;
            FilesList.DoubleClick += File_DoubleClick;
            ShowDisks();
            Update();
            DeserializeViewSettings();
            UpdateViewSettings();
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
            
        }
        private void Disks_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void Update()
        {
            ShowCatalog("");
        }
        private bool ShowCatalog(string path)
        {
            FilesList.Items.Clear();
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/111.0");
            client.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            client.Headers.Add("Accept-Encoding", "br");
            client.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            string url = "https://www.amazon.com/s?k=" + path + "&i=stripbooks-intl-ship&ref=nb_sb_noss";

            string htmlCode = client.DownloadString(url);

            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(htmlCode);

            string xpath = "//div[@data-component-type='s-search-result']";
            var bookNodes = document.DocumentNode.SelectNodes(xpath);

            if (bookNodes != null)
            {
                List<string> bookTitles = new List<string>();
                List<string> bookLinks = new List<string>();

                foreach (var node in bookNodes)
                {
                    // Получаем название книги
                    var titleNode = node.SelectSingleNode(".//span[@class='a-size-medium a-color-base a-text-normal']");
                    string title = titleNode?.InnerText.Trim();

                    // Получаем ссылку на книгу
                    var linkNode = node.SelectSingleNode(".//a[contains(@class, 'a-link-normal') and contains(@class, 's-underline-text') and contains(@class, 's-underline-link-text') and contains(@class, 's-link-style') and contains(@class, 'a-text-normal')]");
                    string link = linkNode?.GetAttributeValue("href", "");

                    if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(link))
                    {
                        bookTitles.Add(title);
                        bookLinks.Add(link);
                    }
                }
                
                
                for (int i = 0; i < bookTitles.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = bookTitles[i];
                    item.Tag = ($"https://www.amazon.com{bookLinks[i]}");
                    FilesList.Items.Add(item);
                }
                 

                Console.WriteLine("Найденные книги:");
                for (int i = 0; i < bookTitles.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {bookTitles[i]}");
                    Console.WriteLine($"Ссылка: https://www.amazon.com{bookLinks[i]}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Книги не найдены.");
            }
            return true;
        }
        private void GoToPath_Click(object sender, EventArgs e)
        {
            ShowCatalog(Path.Text);
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
            }
            else if (FilesList.SelectedItems.Count > 1)
            {
                Delete.Enabled = true;
                Cut.Enabled = true;
                Copy.Enabled = true;
                Rename.Enabled = false;
            }
            else if (FilesList.SelectedItems.Count == 1)
            {
                Delete.Enabled = true;
                Cut.Enabled = true;
                Copy.Enabled = true;
                Rename.Enabled = true;
            }
            else if (FilesList.SelectedItems.Count == 0)
            {
                Delete.Enabled = false;
                Copy.Enabled = false;
                Cut.Enabled = false;
                Rename.Enabled = false;
            }
        }

        private void File_DoubleClick(object sender, EventArgs e)
        {
            if (FilesList.SelectedItems.Count == 1)
            {
                string url = FilesList.SelectedItems[0].Tag.ToString(); // Замените ссылкой, которую хотите открыть
                string browserPath = @"C:\Program Files\Google\Chrome\Application\chrome.exe"; // Укажите путь к исполняемому файлу браузера

                try
                {
                    Process.Start(browserPath, url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при открытии ссылки: " + ex.Message);
                }
            }
        }
        /*
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
        */
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

    }
}