using IFSAero.Helpers;
using IFSAero.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace IFSAero.ViewModels
{
    public class ListFilteredCandidateViewModel : BaseViewModel
    {
        private ObservableCollection<Candidate> candidates = new ObservableCollection<Candidate>();
        public ObservableCollection<Candidate> Candidates
        {
            get => candidates;
            set => SetProperty(ref candidates, value);
        }

        private ObservableCollection<string> filters = new ObservableCollection<string> { "Accepted", "Rejected" };
        public ObservableCollection<string> Filters
        {
            get => filters;
            set => SetProperty(ref filters, value);
        }

        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                SetProperty(ref filter, value);

                if (value == "Accepted")
                {
                    LoadAcceptedCandidates();
                }
                else
                {
                    LoadRejecteedCandidates();
                }
            }
        }

        public ListFilteredCandidateViewModel()
        {
            // Load default data
            LoadAcceptedCandidates();
        }

        private void LoadAcceptedCandidates()
        {
            Candidates.Clear();

            var items = CandidateHelper.AcceptedCandidates;
            if (items.Any())
            {
                foreach (var item in items)
                    Candidates.Add(item);
            }
        }

        private void LoadRejecteedCandidates()
        {
            Candidates.Clear();

            var items = CandidateHelper.RejectedCandidates;
            if (items.Any())
            {
                foreach (var item in items)
                    Candidates.Add(item);
            }
        }
    }
}
