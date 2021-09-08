using IFSAero.Interfaces;
using IFSAero.Models;
using IFSAero.Views;
using Refit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IFSAero.ViewModels
{
    public class SelectionViewModel : BaseViewModel
    {
        public ICommand LoadTechnologiesCommand => new Command(async () => await LoadTechnologiesAsync());
        public ICommand SearchCommand => new Command(async () => await SearchAsync());

        private List<Technology> listTechnology = new List<Technology>();

        private ObservableCollection<string> technologies = new ObservableCollection<string>();
        public ObservableCollection<string> Technologies
        {
            get => technologies;
            set => SetProperty(ref technologies, value);
        }

        private string technology;
        public string Technology
        {
            get => technology;
            set => SetProperty(ref technology, value);
        }

        private int yearOfExperience;
        public int YearOfExperience
        {
            get => yearOfExperience;
            set => SetProperty(ref yearOfExperience, value);
        }

        public SelectionViewModel()
        {
        }

        private async Task LoadTechnologiesAsync()
        {
            var apiResponse = RestService.For<IWebApi>(Configurations.Configuration.ApiUrl);
            listTechnology = await apiResponse.GetTechnologies();
            if (listTechnology != null && listTechnology.Any())
            {
                foreach (var item in listTechnology)
                    Technologies.Add(item.name);
            }
        }

        private async Task SearchAsync()
        {
            if (string.IsNullOrEmpty(Technology))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Please select technology", "OK");
                return;
            }

            if (YearOfExperience == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Please input year of experience", "OK");
                return;
            }

            string technologyId = listTechnology.Where(a => a.name == Technology).FirstOrDefault().guid;
            await Application.Current.MainPage.Navigation.PushAsync(new ListCandidatePage(technologyId, YearOfExperience));
        }
    }
}
