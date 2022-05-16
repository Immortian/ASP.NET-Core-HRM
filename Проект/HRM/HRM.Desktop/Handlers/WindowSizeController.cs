using System.Collections.Generic;
using System.Linq;

namespace HRM.Desktop.Handlers
{
    public class WindowSizeController
    {
        MainWindow window;
        public WindowSizeController(MainWindow mainWindow)
        {
            window = mainWindow;
        }
        List<PageSize> pageslist = new List<PageSize>() { new PageSize("AuthorizationPage", false, 800, 450), new PageSize("AdminIndex", true, 1200, 650) };
        public void CheckWindowSize(string windowTitle)
        {
            PageSize currentPage = pageslist.Where(x => x.WindowTitle == windowTitle).FirstOrDefault();
            if (currentPage == null)
                return;
            if (currentPage.IsChangeble == false)
                window.ResizeMode = System.Windows.ResizeMode.NoResize;
            else
                window.ResizeMode = System.Windows.ResizeMode.CanResize;

            if (currentPage.DefaultHeigth != 0)
            {
                window.Height = currentPage.DefaultHeigth;
                window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            }
            if (currentPage.DefaultWidth != 0)
            {
                window.Width = currentPage.DefaultWidth;
                window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            }
        }
    }
    class PageSize
    {
        public PageSize(string Title, bool CanChange, double Width, double Heigth)
        {
            WindowTitle = Title;
            IsChangeble = CanChange;
            DefaultHeigth = Heigth;
            DefaultWidth = Width;
        }
        public PageSize(string Title, bool CanChange)
        {
            WindowTitle = Title;
            IsChangeble = CanChange;
        }

        public string WindowTitle;
        public bool IsChangeble;
        public double DefaultWidth;
        public double DefaultHeigth;
    }
}

