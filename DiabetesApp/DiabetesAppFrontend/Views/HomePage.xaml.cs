using DiabetesAppFrontend.ViewModels;
using Microsoft.Maui.Controls;

namespace DiabetesAppFrontend.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
