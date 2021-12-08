using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards.ValueObjects;
using DoctorDiary.Services.PatientCards;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class EditPatientCardViewModel : BaseViewModel
    {
        private string _patientCardId;
        private string _lastName;
        private string _firstName;
        private string _patronymic;
        private string _city;
        private string _street;
        private string _apartment;
        private string _house;
        private DateTime _birthday;
        private string _snils;
        private string _description;
        private string _phoneNumber;
        private string _gender;
        private string _insurancePolicy;
        private string _placeOfWork;
        private int _precinct;

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
        
        public int Precinct
        {
            get => _precinct;
            set => SetProperty(ref _precinct, value);
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
                City = patientCard.Address.City;
                Street = patientCard.Address.Street;
                Apartment = patientCard.Address.Apartment;
                House = patientCard.Address.House;
                Birthday = patientCard.Birthday;
                Snils = patientCard.Snils.Value;
                PhoneNumber = patientCard.PhoneNumber;
                Description = patientCard.Description;
                Gender = patientCard.Gender;
                InsurancePolicy = patientCard.InsurancePolicy.Value;
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
                address: new Address(city: City, street: Street, apartment: Apartment, house: House),
                birthday: Birthday,
                snils: string.IsNullOrEmpty(Snils) 
                    ? Models.PatientCards.ValueObjects.Snils.Empty() 
                    : new Snils(Snils),
                description: Description,
                phoneNumber: PhoneNumber,
                gender: Gender,
                insurancePolicy: string.IsNullOrEmpty(InsurancePolicy) 
                    ? Models.PatientCards.ValueObjects.InsurancePolicy.Empty() 
                    : new InsurancePolicy(Snils),
                placeOfWork: PlaceOfWork,
                precinct: Precinct);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}