using IFSAero.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IFSAero.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListFilteredCandidatePage : ContentPage
    {
        public ListFilteredCandidatePage()
        {
            InitializeComponent();

            BindingContext = new ListFilteredCandidateViewModel();
        }
    }
}