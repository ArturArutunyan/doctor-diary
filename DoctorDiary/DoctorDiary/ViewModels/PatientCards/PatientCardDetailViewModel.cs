﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Services.MessageBox;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Views.PatientCards;
using DoctorDiary.Views.SickLeaves;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class PatientCardDetailViewModel : BaseViewModel
    {
        private readonly IPatientCardAppService _patientCardAppService;
        private readonly ISickLeaveAppService _sickLeaveAppService;
        private readonly IMessageBoxAppService _messageBoxAppService;

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
        private string _gender;
        private string _insurancePolicy;
        private string _placeOfWork;
        private int _precinct;
        
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
        
        #endregion
        
        #region SickLeave
        private Guid? _sickLeaveId;
        private long? _number;
        private DateTime? _actualTermStartDate;
        private DateTime? _actualTermEndDate;
        
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

        public DateTime? ActualTermStartDate
        {
            get => _actualTermStartDate;
            set => SetProperty(ref _actualTermStartDate, value);
        }
        
        public DateTime? ActualTermEndDate
        {
            get => _actualTermEndDate;
            set => SetProperty(ref _actualTermEndDate, value);
        }
        #endregion

        #region Buttons
        private bool _openSickLeaveButtonIsEnabled;
        private bool _closeSickLeaveWithCodeIsEnabled;
        private bool _extendSickLeaveButtonIsEnabled;

        public bool OpenSickLeaveButtonIsEnabled
        {
            get => _openSickLeaveButtonIsEnabled;
            set => SetProperty(ref _openSickLeaveButtonIsEnabled, value);
        }

        public bool CloseSickLeaveWithCodeIsEnabled
        {
            get => _closeSickLeaveWithCodeIsEnabled;
            set => SetProperty(ref _closeSickLeaveWithCodeIsEnabled, value);
        }

        public bool ExtendSickLeaveButtonIsEnabled
        {
            get => _extendSickLeaveButtonIsEnabled;
            set => SetProperty(ref _extendSickLeaveButtonIsEnabled, value);
        }
        #endregion

        #region Others
        // TODO: Move to another page
        private bool _sickLeaveVisible;

        public bool SickLeaveVisible
        {
            get => _sickLeaveVisible;
            set => SetProperty(ref _sickLeaveVisible, value);
        }
        #endregion

        public AsyncCommand OpenSickLeaveCommand { get; }
        public AsyncCommand CloseSickLeaveCommand { get; }
        public AsyncCommand CloseSickLeaveWithCodeCommand { get; }
        public AsyncCommand ExtendSickLeaveCommand { get; }
        public AsyncCommand EditPatientCardCommand { get; }
        public AsyncCommand EditSickLeaveCommand { get; }
        public AsyncCommand DeletePatientCardCommand { get; }
        public AsyncCommand DeleteSickLeaveCommand { get; }
        public Xamarin.Forms.Command OpenPhoneDialerCommand { get; }
        
        public PatientCardDetailViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();
            _messageBoxAppService = DependencyService.Get<IMessageBoxAppService>();
            
            OpenSickLeaveCommand = new AsyncCommand(OnOpenSickLeave);
            CloseSickLeaveCommand = new AsyncCommand(OnCloseSickLeave);
            CloseSickLeaveWithCodeCommand = new AsyncCommand(OnCloseSickLeaveWithCode);
            ExtendSickLeaveCommand = new AsyncCommand(OnExtendSickLeave);
            EditPatientCardCommand = new AsyncCommand(OnEditPatientCard);
            EditSickLeaveCommand = new AsyncCommand(OnEditSickLeave);
            DeletePatientCardCommand = new AsyncCommand(OnDeletePatientCard);
            DeleteSickLeaveCommand = new AsyncCommand(OnDeleteSickLeave);
            OpenPhoneDialerCommand = new Xamarin.Forms.Command(OnOpenPhoneDialer);
        }

        private void OnOpenPhoneDialer()
        {
            try
            {
                if (!string.IsNullOrEmpty(PhoneNumber))
                {
                    PhoneDialer.Open(PhoneNumber);
                }
            }
            catch (ArgumentNullException anEx)
            {
                Console.WriteLine(anEx);
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        private async Task OnEditPatientCard()
        {
            await Shell.Current.GoToAsync($"{nameof(EditPatientCardPage)}?{nameof(PatientCardId)}={PatientCardId}");
        }

        private async Task OnEditSickLeave()
        {
            if (!SickLeaveId.HasValue)
                return;
            
            await Shell.Current.GoToAsync($"{nameof(EditSickLeavePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }

        private async Task OnDeletePatientCard()
        {
            await _patientCardAppService.DeleteAsync(id: Id);
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnDeleteSickLeave()
        {
            if (!SickLeaveId.HasValue)
                return;

            await _sickLeaveAppService.DeleteAsync(SickLeaveId.Value);

            SickLeaveVisible = false;
            OpenSickLeaveButtonIsEnabled = true;
            ExtendSickLeaveButtonIsEnabled = false;
            CloseSickLeaveWithCodeIsEnabled = false;
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
                Address = patientCard.Address.ToString();
                Birthday = patientCard.Birthday;
                Snils = patientCard.Snils.ToReadableFormat();
                PhoneNumber = patientCard.PhoneNumber;
                Description = patientCard.Description;
                Gender = patientCard.Gender;
                InsurancePolicy = patientCard.InsurancePolicy.ToReadableFormat();
                PlaceOfWork = patientCard.PlaceOfWork;
                Precinct = patientCard.Precinct;
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
                
                CloseSickLeaveWithCodeIsEnabled = false;
                
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

                    ActualTermStartDate = sickLeave.LastTerm().StartDate;
                    ActualTermEndDate = sickLeave.LastTerm().EndDate;
                    
                    // TODO: Removed this shit!
                    if (sickLeave.Terms.Count == 3)
                    {
                        ExtendSickLeaveButtonIsEnabled = false;
                        CloseSickLeaveWithCodeIsEnabled = true;
                    }
                }
            }
            catch (Exception e)
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
            try
            {
                var code = await _messageBoxAppService.ShowActionSheet(
                    title: "Выберите код",
                    cancel: "Отменить",
                    destruction: null,
                    buttons: new[] { "31" });
                
                switch (code)
                {
                    case "31":
                        if (SickLeaveId.HasValue)
                        {
                            await Shell.Current.GoToAsync($"{nameof(CloseSickLeaveWithThirtyOneCodePage)}?{nameof(PatientCardDetailViewModel.SickLeaveId)}={SickLeaveId.Value.ToString()}");
                        }
                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            CloseSickLeaveWithCodeIsEnabled = false;
        }

        private async Task OnExtendSickLeave()
        {
            await Shell.Current.GoToAsync($"{nameof(ExtendSickLeavePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }
    }
}