using System;
using System.Diagnostics;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.SickLeaves;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class PatientCardDetailViewModel : BaseViewModel
    {
        private string _patientCardId;
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _address;
        private DateTime _birthday;
        private string _snils;
        private string _description;

        private readonly IPatientCardAppService _patientCardAppService;
        private readonly ISickLeaveAppService _sickLeaveAppService;

        public string PatientCardId
        {
            get => _patientCardId;
            set
            {
                _patientCardId = value;
                LoadPatientCard(value);
            }
        }

        public Guid Id { get; set; }

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

        public PatientCardDetailViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
        }

        public async void LoadPatientCard(string id)
        {
            try
            {
                var patientCard = await _patientCardAppService.GetAsync(Guid.Parse(id));
                
                Id = patientCard.Id;
                FirstName = patientCard.FirstName;
                LastName = patientCard.LastName;
                Patronymic = patientCard.Patronymic;
                Address = patientCard.Address;
                Birthday = patientCard.Birthday;
                Snils = patientCard.Snils;
                Description = patientCard.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load patient Card");
            }
        }
    }
}