using System;
using System.Threading.Tasks;
using DoctorDiary.Models;
using DoctorDiary.Services.PatientCards;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace DoctorDiary.ViewModels.PatientCards
{
    public class NewPatientCardViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _address;
        private DateTime _birthday;
        private string _snils;
        private string _description;
        
        private readonly IPatientCardAppService _patientCardAppService;

        public NewPatientCardViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
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

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }
        

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
                address: Address,
                birthday: Birthday,
                snils: Snils,
                description: Description);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}