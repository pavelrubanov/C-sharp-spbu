namespace FileManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            try
            {
                Application.Run(new Manager());
            }
            catch (Exception ex) { }
        }
    }
}