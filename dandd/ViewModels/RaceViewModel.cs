using CommunityToolkit.Mvvm.ComponentModel;
using dandd.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace dandd.ViewModels
{
    internal partial class RaceViewModel : ObservableObject, IDisposable
    {
        HttpClient client;

        JsonSerializerOptions _serializerOptions;
        string baseUrl = "https://www.dnd5eapi.co/api/";

        [ObservableProperty]
        public string _Index;
        [ObservableProperty]
        public string _Name;
        [ObservableProperty]
        public string _Url;

        [ObservableProperty]
        public ObservableCollection<Race> _races;


        public RaceViewModel()
        {
            client = new HttpClient();
            Races = new ObservableCollection<Race>();
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public ICommand GetRacesCommand => new Command(async () => await LoadRacesAsync());

        private async Task LoadRacesAsync()
        {
            var url = $"{baseUrl}/races";
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Races = JsonSerializer.Deserialize<ObservableCollection<Race>>(content, _serializerOptions);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}