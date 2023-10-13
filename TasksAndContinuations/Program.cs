class Program
{
    static void Main()
    {
        bool IsCorrect = true;

        Task ShowSplash = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Show Splash");
        });

        Task RequestLicense = ShowSplash.ContinueWith(t =>
        {
            if (Error())
            {
                Console.WriteLine("Error Request License");
                throw new Exception("Oops!");
            }
            Console.WriteLine("Request License");
        }, TaskContinuationOptions.NotOnFaulted);

        Task CheckForUpdate = ShowSplash.ContinueWith(t =>
        {
            Thread.Sleep(2000);
            if (Error())
            {
                Console.WriteLine("Error Check For Update");
                throw new Exception("Error");
            }
            Console.WriteLine("Check For Update");
        }, TaskContinuationOptions.NotOnFaulted);

        Task DownloadUpdate = CheckForUpdate.ContinueWith(t =>
        {
            Thread.Sleep(2000);
            if (Error())
            {
                Console.WriteLine("Error Download Update"); 
                throw new Exception("Error!");
            }
            Console.WriteLine("Download Update");
        }, TaskContinuationOptions.NotOnFaulted);

        Task SetupMenus = RequestLicense.ContinueWith(t =>
        {
            Thread.Sleep(1000);
            if (Error())
            {
                Console.WriteLine("Error. Setup Menus");
                throw new Exception("Error!");
            }
            Console.WriteLine("Setup Menus");
        }, TaskContinuationOptions.NotOnFaulted);

        Task[] predecessors = new Task[] { SetupMenus, DownloadUpdate, RequestLicense, CheckForUpdate }; 
        Task DisplayWelcomeScreen = Task.WhenAll(predecessors).ContinueWith(t =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("Display Welcome Screen");
        }, TaskContinuationOptions.NotOnFaulted);

        Task HideSplash = DisplayWelcomeScreen.ContinueWith(t =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hide Splash");
        }, TaskContinuationOptions.NotOnCanceled);

        try
        {
            HideSplash.Wait();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }


    private static bool Error()
    {
        Random rand = new Random(); if (rand.Next(100) % 2 == 0) return true;
        else return false;
    }
}