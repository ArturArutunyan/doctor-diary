using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Shared.SickLeaves;
using DoctorDiary.ViewModels.SickLeaves;
using DoctorDiary.Views.SickLeaves;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class PatientCardDetailViewModel : BaseViewModel
    {
        
        private readonly IPatientCardAppService _patientCardAppService;
        private readonly ISickLeaveAppService _sickLeaveAppService;

        #region PatientCard
        private string _patientCardId;
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _address;
        private DateTime _birthday;
        private string _snils;
        private string _phoneNumber;
        private string _description;
        
        public string PatientCardId
        {
            get => _patientCardId;
            set
            {
                _patientCardId = value;
                LoadPatientCard(value);
                LoadSickLeave(value);
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

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }
        
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        #endregion
        
        #region SickLeave
        private Guid? _sickLeaveId;
        private long? _number;
        private List<Term> _terms;

        public Guid? SickLeaveId
        {
            get => _sickLeaveId;
            set => SetProperty(ref _sickLeaveId, value);
        }

        public long? Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        #endregion

        #region Buttons
        private bool _sickSickLeaveButtonIsEnabled;

        public bool OpenSickLeaveButtonIsEnabled
        {
            get => _sickSickLeaveButtonIsEnabled;
            set => SetProperty(ref _sickSickLeaveButtonIsEnabled, value);
        }
        #endregion

        #region Others
        // TODO: Move to another page
        private SickLeaveCode? _sickLeaveCode;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private bool _sickLeaveVisible;

        public SickLeaveCode? SickLeaveCode
        {
            get => _sickLeaveCode;
            set => SetProperty(ref _sickLeaveCode, value);
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        
        public bool SickLeaveVisible
        {
            get => _sickLeaveVisible;
            set => SetProperty(ref _sickLeaveVisible, value);
        }

        #endregion

        public AsyncCommand OpenSickLeaveAsyncCommand { get; }
        public AsyncCommand CloseSickLeaveAsyncCommand { get; }
        public AsyncCommand CloseSickLeaveWithCodeAsyncCommand { get; }

        public PatientCardDetailViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();

            OpenSickLeaveAsyncCommand = new AsyncCommand(OnOpenSickLeave);
            CloseSickLeaveAsyncCommand = new AsyncCommand(OnCloseSickLeave);
            CloseSickLeaveWithCodeAsyncCommand = new AsyncCommand(OnCloseSickLeaveWithCode);
        }

        private async void LoadPatientCard(string patientCardId)
        {
            try
            {
                var patientCard = await _patientCardAppService.GetAsync(Guid.Parse(patientCardId));
                
                Id = patientCard.Id;
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

        private async void LoadSickLeave(string patientCardId)
        {
            try
            {
                var sickLeave = await _sickLeaveAppService.GetActiveSickLeaveOrDefaultByPatientCardId(Guid.Parse(patientCardId));
                
                if (sickLeave == null)
                {
                    OpenSickLeaveButtonIsEnabled = true;
                    SickLeaveVisible = false;
                }
                else
                {
                    OpenSickLeaveButtonIsEnabled = false;
                    SickLeaveVisible = true;

                    var lastTerm = sickLeave.LastTerm();

                    SickLeaveId = sickLeave.Id;
                    Number = sickLeave.Number;
                    StartDate = lastTerm.StartDate;
                    EndDate = lastTerm.EndDate;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load patient Card");
            }
        }

        public async Task OnOpenSickLeave()
        {
            await Shell.Current.GoToAsync($"{nameof(OpenSickLeavePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }

        private async Task OnCloseSickLeave()
        {
            try
            {
                IsBusy = true;

                if (SickLeaveId != null)
                {
                    await _sickLeaveAppService.CloseSickLeave(SickLeaveId.Value);
                    
                    SickLeaveVisible = false;
                    OpenSickLeaveButtonIsEnabled = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnCloseSickLeaveWithCode()
        {
            await Shell.Current.GoToAsync($"{nameof(CloseSickLeaveWithCodePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }
    }
}