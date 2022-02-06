using System;
using System.Threading.Tasks;
using DoctorDiary.Shared.Frames;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPatientCardPage : ContentPage
    {
        private readonly EditPatientCardViewModel _editPatientCardViewModel;

        public EditPatientCardPage()
        {
            InitializeComponent();
            BindingContext = _editPatientCardViewModel = new EditPatientCardViewModel();
        }

        private async void CalendarButton_OnClicked(object sender, EventArgs e)
        {
            await Calendar.ScaleTo(1.1, 100);
            await Calendar.ScaleTo(1, 100);
            CalendarDatePicker.Focus();
        }
        
        private void CalendarDatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            var date = CalendarDatePicker.Date.ToString("dd.MM.yyyy");
            _editPatientCardViewModel.Birthday.Value = date;
            _editPatientCardViewModel.Birthday.Validate();
            Birthday.Text = date;
        }

        private async Task ScaleToFocused(MaterialFrame frame)
        {
            frame.Elevation = 14;
            await frame.ScaleTo(1.01, 100);
        }

        private async Task ScaleToUnFocused(MaterialFrame frame)
        {
            frame.Elevation = 2;
            await frame.ScaleTo(1, 100);
        }

        private async void LastNameEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(LastNameFrame);
        }

        private async void LastNameEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(LastNameFrame);
        }

        private async void FirstNameEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(FirstNameFrame);
        }

        private async void FirstNameEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(FirstNameFrame);
        }

        private async void PatronymicEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(PatronymicFrame);
        }
        
        private async void PatronymicEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(PatronymicFrame);
        }

        private async void GenderEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(GenderFrame);
        }
        
        private async void GenderEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(GenderFrame);
        }
        
        private async void BirthdayEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(BirthdayFrame);
        }
        
        private async void BirthdayEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(BirthdayFrame);
        }
        
        private async void InsurancePolicyEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(InsurancePolicyFrame);
        }
        
        private async void InsurancePolicyEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(InsurancePolicyFrame);
        }
        
        private async void SnilsEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(SnilsFrame);
        }
        
        private async void SnilsEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(SnilsFrame);
        }
        
        private async void CityEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(CityFrame);
        }
        
        private async void CityEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(CityFrame);
        }
        
        private async void StreetEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(StreetFrame);
        }
        
        private async void StreetEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(StreetFrame);
        }
        
        private async void HouseEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(HouseFrame);
        }
        
        private async void HouseEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(HouseFrame);
        }
        
        private async void EntranceEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(EntranceFrame);
        }
        
        private async void EntranceEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(EntranceFrame);
        }
        
        private async void FloorEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(FloorFrame);
        }
        
        private async void FloorEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(FloorFrame);
        }
        
        private async void ApartmentEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(ApartmentFrame);
        }
        
        private async void ApartmentEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(ApartmentFrame);
        }
        
        private async void PhoneNumberEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(PhoneNumberFrame);
        }
        
        private async void PhoneNumberEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(PhoneNumberFrame);
        }
        private async void PlaceOfWorkEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(PlaceOfWorkFrame);
        }
        
        private async void PlaceOfWorkEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(PlaceOfWorkFrame);
        }
        
        private async void EmploymentPositionEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(EmploymentPositionFrame);
        }
        
        private async void EmploymentPositionEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(EmploymentPositionFrame);
        }
        
        private async void DescriptionEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(DescriptionFrame);
        }
        
        private async void DescriptionEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(DescriptionFrame);
        }
        
        private async void PrecinctEntry_OnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToFocused(PrecinctFrame);
        }
        
        private async void PrecinctEntry_OnUnFocused(object sender, FocusEventArgs e)
        {
            await ScaleToUnFocused(PrecinctFrame);
        }

        private async void Save_OnClicked(object sender, EventArgs e)
        {
            if (_editPatientCardViewModel.Validate())
            {
                await _editPatientCardViewModel.Save();
            }
            else
            {
                await PatientCardScrollView.ScrollToAsync(0, 0, true);
                await this.DisplayToastAsync(new ToastOptions()
                {
                    BackgroundColor = Color.White,
                    CornerRadius = new Thickness(7, 7, 0, 0),
                    MessageOptions = new MessageOptions()
                    {
                        Foreground = Color.Red,
                        Font = Font.OfSize("HEB", NamedSize.Medium),
                        Message = $"Форма заполнена некорректно"
                    }
                });   
            }
        }

        private void PrecinctEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // if (string.IsNullOrEmpty(Precinct.Text))
            // {
            //     _editPatientCardViewModel.Precinct = null;
            // }
        }
    }
}