using System;
using System.Globalization;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards.ValueObjects;
using DoctorDiary.Services.PatientCards;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    public class NewPatientCardViewModel : BaseViewModel
    {
        private string _lastName;
        private string _firstName;
        private string _patronymic;
        private string _city;
        private string _street;
        private string _apartment;
        private string _house;
        private string _entrance;
        private string _floor;
        private string _birthday;
        private string _snils;
        private string _description;
        private string _phoneNumber;
        private string _gender;
        private string _insurancePolicy;
        private string _placeOfWork;
        private string _employmentPosition;
        private int _precinct;

        private readonly IPatientCardAppService _patientCardAppService;

        public NewPatientCardViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            BirthdayDatePicker = null;
            SaveCommand = new AsyncCommand(OnSave, ValidateSave);
            CancelCommand = new AsyncCommand(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.RaiseCanExecuteChanged();
        }

        private bool ValidateSave(object arg)
        {
            return !String.IsNullOrWhiteSpace(_firstName)
                   && !String.IsNullOrWhiteSpace(_lastName);
        }

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }


        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string Patronymic
        {
            get => _patronymic;
            set => SetProperty(ref _patronymic, value);
        }
        
        public string City 
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }
        
        public string Street 
        {
            get => _street;
            set => SetProperty(ref _street, value);
        }
        
        public string Apartment 
        {
            get => _apartment;
            set => SetProperty(ref _apartment, value);
        }
        
        public string House 
        {
            get => _house;
            set => SetProperty(ref _house, value);
        }
        
        public string Entrance 
        {
            get => _entrance;
            set => SetProperty(ref _entrance, value);
        }
        
        public string Floor 
        {
            get => _floor;
            set => SetProperty(ref _floor, value);
        }
        
        public string Birthday 
        {
            get => _birthday;
            set => SetProperty(ref _birthday, value);
        }

        public DateTime? BirthdayDatePicker { get; set; }

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
        
        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }
        
        public string InsurancePolicy
        {
            get => _insurancePolicy;
            set => SetProperty(ref _insurancePolicy, value);
        }

        public string PlaceOfWork
        {
            get => _placeOfWork;
            set => SetProperty(ref _placeOfWork, value);
        }
        
        public string EmploymentPosition
        {
            get => _employmentPosition;
            set => SetProperty(ref _employmentPosition, value);
        }
        
        public int Precinct
        {
            get => _precinct;
            set => SetProperty(ref _precinct, value);
        }
        
        private async Task OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {
            await _patientCardAppService.CreateAsync(
                firstName: FirstName,
                lastName: LastName,
                patronymic: Patronymic,
                address: new Address(city: City, street: Street, apartment: Apartment, house: House, entrance: Entrance, floor: Floor),
                birthday: !string.IsNullOrEmpty(Birthday) 
                    ? DateTime.ParseExact(s: Birthday, format: "dd.MM.yyyy", provider: null)
                    : (DateTime?)null,
                snils: string.IsNullOrEmpty(Snils) 
                    ? Models.PatientCards.ValueObjects.Snils.Empty() 
                    : new Snils(Snils),
                description: Description,
                phoneNumber: new PhoneNumber(PhoneNumber),
                gender: Gender,
                insurancePolicy: string.IsNullOrEmpty(InsurancePolicy) 
                    ? Models.PatientCards.ValueObjects.InsurancePolicy.Empty() 
                    : new InsurancePolicy(InsurancePolicy),
                placeOfWork: PlaceOfWork,
                employmentPosition: EmploymentPosition,
                precinct: Precinct);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}