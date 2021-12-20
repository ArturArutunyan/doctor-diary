using System;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPatientCardsPopup : Popup
    {
        private readonly PatientCardsViewModel _parentViewModel;

        public SearchPatientCardsPopup(PatientCardsViewModel parentViewModel)
        {
            InitializeComponent();
            BindingContext = _parentViewModel = parentViewModel;
        }

        private async void Search_OnClicked(object sender, EventArgs e)
        {
            Dismiss(null);
            _parentViewModel.OnAppearing();
        }

        private void Reset_OnClicked(object sender, EventArgs e)
        {
            Dismiss(null);
            _parentViewModel.ResetFilter();
            _parentViewModel.OnAppearing();
        }

        private void ClosePopup_OnClicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}