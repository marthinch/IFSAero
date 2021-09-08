using IFSAero.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IFSAero.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionPage : ContentPage
    {
        private readonly SelectionViewModel selectionViewModel;

        public SelectionPage()
        {
            InitializeComponent();

            BindingContext = selectionViewModel = new SelectionViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            selectionViewModel.LoadTechnologiesCommand.Execute(null);
        }
    }
}