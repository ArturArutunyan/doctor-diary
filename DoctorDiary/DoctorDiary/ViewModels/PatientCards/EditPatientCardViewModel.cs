using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using DoctorDiary.Models.PatientCards.ValueObjects;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Shared.Validations;
using DoctorDiary.ViewModels.PatientCards.Validations;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class EditPatientCardViewModel : BaseViewModel
    {
        private string _patientCardId;
        private ValidatableObject<string> _lastName;
        private ValidatableObject<string> _firstName;
        private string _patronymic;
        private string _city;
        private string _street;
        private string _apartment;
        private string _house;
        private string _entrance;
        private string _floor;
        private ValidatableObject<string> _birthday;
        private ValidatableObject<string> _snils;
        private string _description;
        private ValidatableObject<string> _phoneNumber;
        private string _gender;
        private ValidatableObject<string> _insurancePolicy;
        private string _placeOfWork;
        private string _employmentPosition;
        private int? _precinct;

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
        
        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public ValidatableObject<string> LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
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
        
        public ValidatableObject<string> Birthday 
        {
            get => _birthday;
            set => SetProperty(ref _birthday, value);
        }
        
        public DateTime? BirthdayDatePicker { get; set; }
        
        public ValidatableObject<string> Snils
        {
            get => _snils;
            set => SetProperty(ref _snils, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public ValidatableObject<string> PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }
        
        public ValidatableObject<string> InsurancePolicy
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
        
        public int? Precinct
        {
            get => _precinct;
            set => SetProperty(ref _precinct, value);
        }

        public ICommand ValidateLastNameCommand => new Command(() => ValidateLastName());

        public ICommand ValidateFirstNameCommand => new Command(() => ValidateFirstName());

        public ICommand ValidateBirthdayCommand => new Command(() => ValidateBirthday());

        public ICommand ValidateSnilsCommand => new Command(() => ValidateSnils());

        public ICommand ValidateInsurancePolicyCommand => new Command(() => ValidateInsurancePolicy());

        public ICommand ValidatePhoneNumberCommand => new Command(() => ValidatePhoneNumber());
        
        public AsyncCommand CancelCommand { get; }

        public EditPatientCardViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            
            _lastName = new ValidatableObject<string>();
            _firstName = new ValidatableObject<string>();
            _birthday = new ValidatableObject<string>();
            _snils = new ValidatableObject<string>();
            _phoneNumber = new ValidatableObject<string>();
            _insurancePolicy = new ValidatableObject<string>();
            
            BirthdayDatePicker = null;
            CancelCommand = new AsyncCommand(OnCancel);
            
            AddValidations();
        }

        private void AddValidations()
        {
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Поле имя обязательное"
            });
            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Поле фамилия обязательное"
            });
            _birthday.Validations.Add(new DateTimeAsStringRule());
            _snils.Validations.AddRange(new List<IValidationRule<string>>()
            {
                new SnilsLengthRule(),
                new SnilsContainsOnlyDigitsRule()
            });
            _insurancePolicy.Validations.Add(new InsurancePolicyRuleLength());
            _phoneNumber.Validations.Add(new PhoneNumberRule());
        }

        internal bool Validate()
        {
            var birthdayIsValid = ValidateBirthday();
            var firstNameIsValid = ValidateFirstName();
            var lastNameIsValid = ValidateLastName();
            var snilsIsValid = ValidateSnils();
            var phoneNumberIsValid = ValidatePhoneNumber();
            var insurancePolicyIsValid = ValidateInsurancePolicy();
            
            return birthdayIsValid &&
                   firstNameIsValid &&
                   lastNameIsValid &&
                   snilsIsValid &&
                   phoneNumberIsValid &&
                   insurancePolicyIsValid;
        }
        
        private bool ValidateLastName()
        {
            return _lastName.Validate();
        }

        private bool ValidateFirstName()
        {
            return _firstName.Validate();
        }

        private bool ValidateBirthday()
        {
            return _birthday.Validate();
        }

        private bool ValidateSnils()
        {
            return _snils.Validate();
        }

        private bool ValidateInsurancePolicy()
        {
            return _insurancePolicy.Validate();
        }

        private bool ValidatePhoneNumber()
        {
            return _phoneNumber.Validate();
        }
        
        private async void LoadPatientCard(string patientCardId)
        {
            try
            {
                var patientCard = await _patientCardAppService.GetAsync(Guid.Parse(patientCardId));

                FirstName.Value = patientCard.FirstName;
                LastName.Value = patientCard.LastName;
                Patronymic = patientCard.Patronymic;
                City = patientCard.Address?.City;
                Street = patientCard.Address?.Street;
                Apartment = patientCard.Address?.Apartment;
                House = patientCard.Address?.House;
                Birthday.Value = patientCard.Birthday?.ToString("dd.MM.yyyy");
                Snils.Value = patientCard.Snils?.ToReadableFormat();
                PhoneNumber.Value = patientCard.PhoneNumber?.Value;
                Description = patientCard.Description;
                Gender = patientCard.Gender;
                InsurancePolicy.Value = patientCard.InsurancePolicy?.ToReadableFormat();
                PlaceOfWork = patientCard.PlaceOfWork;
                Precinct = patientCard.Precinct;
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

        public async Task Save()
        {
            await _patientCardAppService.UpdateAsync(
                id: Guid.Parse(PatientCardId), 
                firstName: FirstName.Value,
                lastName: LastName.Value,
                patronymic: Patronymic,
                address: new Address(city: City, street: Street, apartment: Apartment, house: House, entrance: Entrance, floor: Floor),
                birthday: !string.IsNullOrEmpty(Birthday.Value) 
                    ? DateTime.ParseExact(s: Birthday.Value, format: "dd.MM.yyyy", provider: null)
                    : (DateTime?)null,
                snils: string.IsNullOrEmpty(Snils.Value) 
                    ? Models.PatientCards.ValueObjects.Snils.Empty() 
                    : new Snils(Snils.Value),
                description: Description,
                phoneNumber: new PhoneNumber(PhoneNumber.Value),
                gender: Gender,
                insurancePolicy: string.IsNullOrEmpty(InsurancePolicy.Value) 
                    ? Models.PatientCards.ValueObjects.InsurancePolicy.Empty() 
                    : new InsurancePolicy(InsurancePolicy.Value),
                placeOfWork: PlaceOfWork,
                employmentPosition: EmploymentPosition,
                precinct: Precinct);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}