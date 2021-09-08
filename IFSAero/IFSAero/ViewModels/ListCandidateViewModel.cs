using IFSAero.Helpers;
using IFSAero.Interfaces;
using IFSAero.Models;
using IFSAero.Views;
using Refit;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IFSAero.ViewModels
{
    public class ListCandidateViewModel : BaseViewModel
    {
        public ICommand LoadCandidateCommand => new Command(async () => await LoadCandidatesAsync());
        public ICommand AcceptCommand => new Command<Candidate>((candidate) => Accept(candidate));
        public ICommand RejectCommand => new Command<Candidate>((candidate) => Reject(candidate));
        public ICommand OpenFilteredCandidateCommand => new Command(async () => await OpenFilteredCandidateAsync());

        private ObservableCollection<Candidate> candidates;
        public ObservableCollection<Candidate> Candidates
        {
            get => candidates;
            set => SetProperty(ref candidates, value);
        }

        private readonly string technologyId;
        private readonly int yearOfExperience;

        public ListCandidateViewModel(string technologyId, int yearOfExperience)
        {
            this.technologyId = technologyId;
            this.yearOfExperience = yearOfExperience;
        }

        private async Task LoadCandidatesAsync()
        {
            var apiResponse = RestService.For<IWebApi>(Configurations.Configuration.ApiUrl);
            var items = await apiResponse.GetCandidates();
            if (items != null && items.Any())
            {
                // Filter based year of experience and technology
                items = items.Where(a => a.experience.Where(b => b.yearsOfExperience == yearOfExperience)
                                                     .Select(b => b.technologyId)
                                                     .Contains(technologyId))
                             .ToList();
                if (items == null || !items.Any())
                    return;

                // Compare data to list accepted candidate
                if (CandidateHelper.AcceptedCandidates.Any())
                {
                    items = items.Where(a => !CandidateHelper.AcceptedCandidates.Select(b => b.candidateId).Contains(a.candidateId)).ToList();
                    if (items == null || !items.Any())
                        return;
                }

                // Compare data to list rejected candidate
                if (CandidateHelper.RejectedCandidates.Any())
                {
                    items = items.Where(a => !CandidateHelper.RejectedCandidates.Select(b => b.candidateId).Contains(a.candidateId)).ToList();
                    if (items == null || !items.Any())
                        return;
                }

                Candidates = new ObservableCollection<Candidate>();
                foreach (var item in items)
                    Candidates.Add(item);
            }
        }

        private void Accept(Candidate candidate)
        {
            Candidates.Remove(candidate);

            CandidateHelper.AcceptedCandidates.Add(candidate);
        }

        private void Reject(Candidate candidate)
        {
            Candidates.Remove(candidate);

            CandidateHelper.RejectedCandidates.Add(candidate);
        }

        private async Task OpenFilteredCandidateAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ListFilteredCandidatePage());
        }
    }
}