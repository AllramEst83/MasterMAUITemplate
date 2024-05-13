using CommunityToolkit.Mvvm.ComponentModel;
using MasterTemplate.Interfaces;
using MasterTemplate.Models;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;

namespace MasterTemplate.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly IMainService _mainService;
        private readonly AppSettings _appSettings;

        [ObservableProperty]
        private string serviceMessage = string.Empty;

        [ObservableProperty]
        private string appSettingsMessage = string.Empty;

        [ObservableProperty]
        ObservableCollection<string> serviceMessages = [];

        public MainViewModel(IOptions<AppSettings> appSettings, IMainService mainService)
        {
            _mainService = mainService;
            _appSettings = appSettings.Value;

            GetServiceMessageAtStartup();
        }

        private void GetServiceMessageAtStartup()
        {
            ServiceMessage = _mainService.GetServiceMessage();
            AppSettingsMessage = _appSettings.Test;

            var serviceMessages = _mainService.GetServiceMessages();
            foreach (var message in serviceMessages)
            {
                ServiceMessages.Add(message);
            }
        }
    }
}
