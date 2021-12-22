using System;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPatientCardsPopup : Popup<PatientCardsFilter>
    {
        private readonly PatientCardsFilter _filter;

        public SearchPatientCardsPopup(PatientCardsFilter filter)
        {
            InitializeComponent();
            BindingContext = _filter = filter;
        }

        private void Search_OnClicked(object sender, EventArgs e)
        {
            Dismiss(_filter);
        }

        private void Reset_OnClicked(object sender, EventArgs e)
        {
            Dismiss(PatientCardsFilter.Default());
        }

        private void ClosePopup_OnClicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}