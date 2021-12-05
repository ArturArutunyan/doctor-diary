using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Services.PatientCards;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class EditPatientCardViewModel : BaseViewModel
    {
        private string _patientCardId;
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _address;
        private DateTime _birthday;
        private string _snils;
        private string _description;
        private string _phoneNumber;

        private readonly IPatientCardAppService _patientCardAppService;

        public string PatientCardId
        {
            get => _patientCardId;
            set
            {
                _patientCardId = value;
                LoadPatientCard(value);
            }
        }
        
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        
        public string Patronymic
        {
            get => _patronymic;
            set => SetProperty(ref _patronymic, value);
        }
        
        public string Address 
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        
        public DateTime Birthday 
        {
            get => _birthday;
            set => SetProperty(ref _birthday, value);
        }
        
        public string Snils
        {
            get => _snils;
            set => SetProperty(ref _snils, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public AsyncCommand SaveCommand { get; }

        public AsyncCommand CancelCommand { get; }

        public EditPatientCardViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            Birthday = new DateTime(2000, 1, 1);
            SaveCommand = new AsyncCommand(OnSave, ValidateSave);
            CancelCommand = new AsyncCommand(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.RaiseCanExecuteChanged();
        }

        private bool ValidateSave(object arg)
        {
            return !String.IsNullOrWhiteSpace(_firstName)
                   && !String.IsNullOrWhiteSpace(_lastName);
        }

        private async void LoadPatientCard(string patientCardId)
        {
            try
            {
                var patientCard = await _patientCardAppService.GetAsync(Guid.Parse(patientCardId));
                
                FirstName = patientCard.FirstName;
                LastName = patientCard.LastName;
                Patronymic = patientCard.Patronymic;
                Address = patientCard.Address;
                Birthday = patientCard.Birthday;
                Snils = patientCard.Snils;
                PhoneNumber = patientCard.PhoneNumber;
                Description = patientCard.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load patient Card");
            }
        }
        
        private async Task OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {
            await _patientCardAppService.UpdateAsync(
                id: Guid.Parse(PatientCardId), 
                firstName: FirstName,
                lastName: LastName,
                patronymic: Patronymic,
                address: Address,
                birthday: Birthday,
                snils: Snils,
                description: Description,
                phoneNumber: PhoneNumber);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}