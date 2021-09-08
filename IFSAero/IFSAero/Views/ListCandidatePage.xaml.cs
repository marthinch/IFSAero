using IFSAero.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IFSAero.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListCandidatePage : ContentPage
    {
        private readonly ListCandidateViewModel listCandidateViewModel;
        private bool isFirstTimeLoad;

        public ListCandidatePage(string technology, int yearOfExperience)
        {
            InitializeComponent();

            BindingContext = listCandidateViewModel = new ListCandidateViewModel(technology, yearOfExperience);
            isFirstTimeLoad = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (isFirstTimeLoad)
            {
                isFirstTimeLoad = false;
                listCandidateViewModel.LoadCandidateCommand.Execute(null);
            }
        }
    }
}