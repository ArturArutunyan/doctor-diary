using System;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientCardsPage : ContentPage
    {
        private bool _searchButtonIsTapped = false;
        private bool _addButtonIsTapped = false;
        
        readonly PatientCardsViewModel _patientCardsViewModel;
        public PatientCardsPage()
        {
            InitializeComponent();
            BindingContext = _patientCardsViewModel = new PatientCardsViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _patientCardsViewModel.OnAppearing();
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            if(_searchButtonIsTapped)
                return;

            _searchButtonIsTapped = true;
            
            var filter = await Navigation.ShowPopupAsync(new SearchPatientCardsPopup(_patientCardsViewModel.Filter));

            if (filter != null)
            {
                _patientCardsViewModel.Filter = filter;
                _patientCardsViewModel.OnAppearing();
            }
            
            _searchButtonIsTapped = false;
        }

        private async void AddButton_OnClicked(object sender, EventArgs e)
        {
            if(_addButtonIsTapped)
                return;

            _addButtonIsTapped = true;
            
            await Shell.Current.GoToAsync(nameof(NewPatientCardPage));

            _addButtonIsTapped = false;
        }

        // Hint: this crap is necessary to get rid of the indicator when refreshing RefreshView
        private async void SearchFilter_OnCompleted(object sender, EventArgs eventArgs)
        {
            await _patientCardsViewModel.LoadPatientCardsWithoutRefresh();
        }

        private async void PhoneButton_OnClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var patientCard = (PatientCard)button.CommandParameter;

            if (patientCard.PhoneNumber != null)
            {
                _patientCardsViewModel.OnOpenPhoneDialer(patientCard);
            }
            else
            {
                await this.DisplayToastAsync(new ToastOptions()
                {
                    BackgroundColor = Color.White,
                    CornerRadius = new Thickness(7, 7, 0, 0),
                    MessageOptions = new MessageOptions()
                    {
                        Foreground = Color.Red,
                        Font = Font.OfSize("HEB", NamedSize.Medium),
                        Message = $"У пациента не указан номер телефона"
                    }
                });   
            }
        }
    }
}