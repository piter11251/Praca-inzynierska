using Demo.ApiClient;
using DiabetesAppFrontend.Views;

namespace DiabetesAppFrontend
{
    public partial class AppShell : Shell
    {

        public AppShell(DemoApiClientService apiService)
        {
            InitializeComponent();
        }
    }
}
