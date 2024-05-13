using MasterTemplate.ViewModels;

namespace MasterTemplate
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel? viewModel;

        public MainPage()
        {
            InitializeComponent();

            if (Application.Current?.Handler?.MauiContext?.Services is not null)
            {
                viewModel = Application.Current.Handler.MauiContext.Services.GetService<MainViewModel>();
                if (viewModel is null)
                {
                    throw new InvalidOperationException("MainViewModel service not found.");
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to access services.");
            }

            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Called when the page is about to become visible
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Called when the page is no longer visible
        }
    }

}
