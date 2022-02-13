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
    public partial class PatientCardDetailPage : ContentPage
    {
        private readonly PatientCardDetailViewModel _patientCardDetailViewModel;
        private static readonly Color TextColor = Color.FromHex(Application.Current.Resources["TextColor"].ToString());
        private bool _editButtonIsTapped;
        private bool _trashButtonIsTapped;
        private bool _visitButtonIsTapped;
        private bool _phoneButtonIsTapped;
        private bool _sickLeaveTrashButtonIsTapped;
        private bool _sickLeaveEditButtonIsTapped;

        public PatientCardDetailPage()
        {
            InitializeComponent();
            BindingContext = _patientCardDetailViewModel = new PatientCardDetailViewModel();
        }

        private async void TrashButton_OnClicked(object sender, EventArgs e)
        {
            var originalColor = TrashButton.TextColor;
            TrashButton.TextColor = TextColor;
            await TrashButton.ScaleTo(1.1, 50);
            await TrashButton.ScaleTo(1, 50);
            TrashButton.TextColor = originalColor;
                
            if(_trashButtonIsTapped)
                return;

            _trashButtonIsTapped = true;
            await _patientCardDetailViewModel.DeletePatientCard();
            _trashButtonIsTapped = false;
        }

        private async void EditButton_OnClicked(object sender, EventArgs e)
        {
            var originalColor = EditButton.TextColor;
            EditButton.TextColor = TextColor;
            await EditButton.ScaleTo(1.1, 50);
            await EditButton.ScaleTo(1, 50);
            EditButton.TextColor = originalColor;
            
            if(_editButtonIsTapped)
                return;

            _editButtonIsTapped = true;
            await _patientCardDetailViewModel.EditPatientCard();
            _editButtonIsTapped = false;
        }

        private async void BackButton_OnClicked(object sender, EventArgs e)
        {
            var originalColor = BackButton.TextColor;
            BackButton.TextColor = TextColor;
            await BackButton.ScaleTo(1.1, 50);
            await BackButton.ScaleTo(1, 50);
            BackButton.TextColor = originalColor;
            
            await Shell.Current.GoToAsync("..");
        }

        private async void VisitButton_OnClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var originalColor = button.TextColor;
            
            button.TextColor = TextColor;
            await button.ScaleTo(1.1, 100);
            await button.ScaleTo(1, 100);
            button.TextColor = originalColor;

            if(_visitButtonIsTapped)
                return;

            _visitButtonIsTapped = true;
            await _patientCardDetailViewModel.CreateDoctorVisit();
            _visitButtonIsTapped = false;
        }

        private async void PhoneButton_OnClicked(object sender, EventArgs eventArgs)
        {
            if (!string.IsNullOrEmpty(_patientCardDetailViewModel.PhoneNumber))
            {
                var button = (Button)sender;
                var originalColor = button.TextColor;
            
                button.TextColor = TextColor;
                await button.ScaleTo(1.1, 100);
                await button.ScaleTo(1, 100);
                button.TextColor = originalColor;

                if(_phoneButtonIsTapped)
                    return;

                _phoneButtonIsTapped = true;
                _patientCardDetailViewModel.OnOpenPhoneDialer();
                _phoneButtonIsTapped = false;
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

        private async void SickLeaveTrashButton_OnClicked(object sender, EventArgs e)
        {
            var originalColor = SickLeaveTrashButton.TextColor;
            SickLeaveTrashButton.TextColor = TextColor;
            await SickLeaveTrashButton.ScaleTo(1.1, 50);
            await SickLeaveTrashButton.ScaleTo(1, 50);
            SickLeaveTrashButton.TextColor = originalColor;
                
            if(_sickLeaveTrashButtonIsTapped)
                return;

            _sickLeaveTrashButtonIsTapped = true;
            await _patientCardDetailViewModel.DeleteSickLeave();
            _sickLeaveTrashButtonIsTapped = false;
        }

        private async void SickLeaveEditButton_OnClicked(object sender, EventArgs e)
        {
            var originalColor = SickLeaveEditButton.TextColor;
            SickLeaveEditButton.TextColor = TextColor;
            await SickLeaveEditButton.ScaleTo(1.1, 50);
            await SickLeaveEditButton.ScaleTo(1, 50);
            SickLeaveEditButton.TextColor = originalColor;
            
            if(_sickLeaveEditButtonIsTapped)
                return;

            _sickLeaveEditButtonIsTapped = true;
            await _patientCardDetailViewModel.EditSickLeave();
            _sickLeaveEditButtonIsTapped = false;
        }
    }
}