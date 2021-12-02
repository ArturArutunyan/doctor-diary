using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Shared.SickLeaves;
using DoctorDiary.Views.SickLeaves;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
        private DateTime? _firstRowStartDate;
        private DateTime? _firstRowEndDate;
        private DateTime? _secondRowStartDate;
        private DateTime? _secondRowEndDate;
        private DateTime? _thirdRowStartDate;
        private DateTime? _thirdRowEndDate;

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

        public DateTime? FirstRowStartDate
        {
            get => _firstRowStartDate;
            set => SetProperty(ref _firstRowStartDate, value);
        }
        
        public DateTime? FirstRowEndDate
        {
            get => _firstRowEndDate;
            set => SetProperty(ref _firstRowEndDate, value);
        }
        
        public DateTime? SecondRowStartDate
        {
            get => _secondRowStartDate;
            set => SetProperty(ref _secondRowStartDate, value);
        }
        
        public DateTime? SecondRowEndDate
        {
            get => _secondRowEndDate;
            set => SetProperty(ref _secondRowEndDate, value);
        }
        
        public DateTime? ThirdRowStartDate
        {
            get => _thirdRowStartDate;
            set => SetProperty(ref _thirdRowStartDate, value);
        }
        
        public DateTime? ThirdRowEndDate
        {
            get => _thirdRowEndDate;
            set => SetProperty(ref _thirdRowEndDate, value);
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
        private bool _sickLeaveVisible;
        private bool _extendSickLeaveButtonIsEnabled;

        public bool SickLeaveVisible
        {
            get => _sickLeaveVisible;
            set => SetProperty(ref _sickLeaveVisible, value);
        }

        public bool ExtendSickLeaveButtonIsEnabled
        {
            get => _extendSickLeaveButtonIsEnabled;
            set => SetProperty(ref _extendSickLeaveButtonIsEnabled, value);
        }
        #endregion

        public AsyncCommand OpenSickLeaveAsyncCommand { get; }
        public AsyncCommand CloseSickLeaveAsyncCommand { get; }
        public AsyncCommand ExtendSickLeaveAsyncCommand { get; }

        public PatientCardDetailViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();

            OpenSickLeaveAsyncCommand = new AsyncCommand(OnOpenSickLeave);
            CloseSickLeaveAsyncCommand = new AsyncCommand(OnCloseSickLeave);
            ExtendSickLeaveAsyncCommand = new AsyncCommand(OnExtendSickLeave);
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
                    ExtendSickLeaveButtonIsEnabled = false;
                }
                else
                {
                    OpenSickLeaveButtonIsEnabled = false;
                    SickLeaveVisible = true;
                    ExtendSickLeaveButtonIsEnabled = true;
                    
                    SickLeaveId = sickLeave.Id;
                    Number = sickLeave.Number;

                    // TODO: Removed this shit!
                    if (sickLeave.Terms.Count == 3)
                    {
                        FirstRowStartDate = sickLeave.Terms[2].StartDate;
                        FirstRowEndDate = sickLeave.Terms[2].EndDate;
                        SecondRowStartDate = sickLeave.Terms[1].StartDate;
                        SecondRowEndDate = sickLeave.Terms[1].EndDate;
                        ThirdRowStartDate = sickLeave.Terms[0].StartDate;
                        ThirdRowEndDate = sickLeave.Terms[0].EndDate;
                        ExtendSickLeaveButtonIsEnabled = false;
                    }
                    else if (sickLeave.Terms.Count == 2)
                    {
                        FirstRowStartDate = sickLeave.Terms[1].StartDate;
                        FirstRowEndDate = sickLeave.Terms[1].EndDate;
                        SecondRowStartDate = sickLeave.Terms[0].StartDate;
                        SecondRowEndDate = sickLeave.Terms[0].EndDate;
                    }
                    else if (sickLeave.Terms.Count == 1)
                    {
                        FirstRowStartDate = sickLeave.Terms[0].StartDate;
                        FirstRowEndDate = sickLeave.Terms[0].EndDate;
                    }
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

        private async Task OnExtendSickLeave()
        {
            await Shell.Current.GoToAsync($"{nameof(ExtendSickLeavePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }
    }
}