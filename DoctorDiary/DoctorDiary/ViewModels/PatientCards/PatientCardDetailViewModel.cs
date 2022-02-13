using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.MessageBox;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Views.PatientCards;
using DoctorDiary.Views.SickLeaves;
using DoctorDiary.Views.Visits;
using Xamarin.CommunityToolkit.ObjectModel;
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
        private DateTime? _birthday;
        private string _snils;
        private string _phoneNumber;
        private string _description;
        private string _gender;
        private string _insurancePolicy;
        private string _placeOfWork;
        private int? _precinct;
        
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

        public DateTime? Birthday 
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
        
        public int? Precinct 
        {
            get => _precinct;
            set => SetProperty(ref _precinct, value);
        }
        
        #endregion
        
        #region SickLeave
        private Guid? _sickLeaveId;
        private long? _number;
        private List<Term> _terms;
        private int _totalDaysOnSickLeave;
        
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

        public List<Term> Terms
        {
            get => _terms;
            set => SetProperty(ref _terms, value);
        }

        public int TotalDaysOnSickLeave
        {
            get => _totalDaysOnSickLeave;
            set => SetProperty(ref _totalDaysOnSickLeave, value);
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
        private bool _sickLeaveIsVisible;
        private DateTime? _firstStartDate;
        private DateTime? _firstEndDate;
        private int? _firstDays;
        private DateTime? _secondStartDate;
        private DateTime? _secondEndDate;
        private int? _secondDays;
        private DateTime? _thirdStartDate;
        private DateTime? _thirdEndDate;
        private int? _thirdDays;

        public bool SickLeaveIsVisible
        {
            get => _sickLeaveIsVisible;
            set => SetProperty(ref _sickLeaveIsVisible, value);
        }
        
        public DateTime? FirstStartDate
        {
            get => _firstStartDate;
            set => SetProperty(ref _firstStartDate, value);
        }
        
        public DateTime? FirstEndDate
        {
            get => _firstEndDate;
            set => SetProperty(ref _firstEndDate, value);
        }
        
        public int? FirstDays
        {
            get => _firstDays;
            set => SetProperty(ref _firstDays, value);
        }
        
        public DateTime? SecondStartDate
        {
            get => _secondStartDate;
            set => SetProperty(ref _secondStartDate, value);
        }
        
        public DateTime? SecondEndDate
        {
            get => _secondEndDate;
            set => SetProperty(ref _secondEndDate, value);
        }
        
        public int? SecondDays 
        {
            get => _secondDays;
            set => SetProperty(ref _secondDays, value);
        }
        
        public DateTime? ThirdStartDate
        {
            get => _thirdStartDate;
            set => SetProperty(ref _thirdStartDate, value);
        }
        
        public DateTime? ThirdEndDate
        {
            get => _thirdEndDate;
            set => SetProperty(ref _thirdEndDate, value);
        }
        
        public int? ThirdDays
        {
            get => _thirdDays;
            set => SetProperty(ref _thirdDays, value);
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
        public AsyncCommand CreateDoctorVisitCommand { get; }
        public Command OpenPhoneDialerCommand { get; }
        
        public PatientCardDetailViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();
            _messageBoxAppService = DependencyService.Get<IMessageBoxAppService>();
            
            OpenSickLeaveCommand = new AsyncCommand(OpenSickLeave);
            CloseSickLeaveCommand = new AsyncCommand(CloseSickLeave);
            CloseSickLeaveWithCodeCommand = new AsyncCommand(CloseSickLeaveWithCode);
            ExtendSickLeaveCommand = new AsyncCommand(ExtendSickLeave);
            EditPatientCardCommand = new AsyncCommand(EditPatientCard);
            EditSickLeaveCommand = new AsyncCommand(EditSickLeave);
            DeletePatientCardCommand = new AsyncCommand(DeletePatientCard);
            DeleteSickLeaveCommand = new AsyncCommand(DeleteSickLeave);
            CreateDoctorVisitCommand = new AsyncCommand(CreateDoctorVisit);
            OpenPhoneDialerCommand = new Xamarin.Forms.Command(OnOpenPhoneDialer);
        }
        
        public void OnOpenPhoneDialer()
        {
            try
            {
                if (!string.IsNullOrEmpty(PhoneNumber))
                {
                    PhoneDialer.Open(Models.PatientCards.ValueObjects.PhoneNumber.ClearFromFormat(PhoneNumber));
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
        
        public async Task EditPatientCard()
        {
            await Shell.Current.GoToAsync($"{nameof(EditPatientCardPage)}?{nameof(PatientCardId)}={PatientCardId}");
        }

        public async Task EditSickLeave()
        {
            if (SickLeaveId == null)
                return;
            
            await Shell.Current.GoToAsync($"{nameof(EditSickLeavePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }

        public async Task DeletePatientCard()
        {
            SickLeaveId = null;
            await _patientCardAppService.DeleteAsync(id: Id);
            await Shell.Current.GoToAsync("..");
        }

        public async Task DeleteSickLeave()
        {
            if (SickLeaveId == null)
                return;

            await _sickLeaveAppService.DeleteAsync(SickLeaveId.Value);
            ResetSickLeave();
        }

        public async Task CreateDoctorVisit()
        {
            await Shell.Current.GoToAsync($"{nameof(CreateDoctorVisitPage)}?{nameof(PatientCardId)}={PatientCardId}");
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
                Address = patientCard.Address?.ToString();
                Birthday = patientCard?.Birthday;
                Snils = patientCard.Snils?.ToReadableFormat();
                PhoneNumber = patientCard.PhoneNumber.ToReadableFormat();
                Description = patientCard.Description;
                Gender = patientCard.Gender;
                InsurancePolicy = patientCard.InsurancePolicy?.ToReadableFormat();
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
                    SickLeaveIsVisible = false;
                    ExtendSickLeaveButtonIsEnabled = false;
                }
                else
                {
                    OpenSickLeaveButtonIsEnabled = false;
                    SickLeaveIsVisible = true;
                    ExtendSickLeaveButtonIsEnabled = true;
                    
                    SickLeaveId = sickLeave.Id;
                    Number = sickLeave.Number;
                    TotalDaysOnSickLeave = sickLeave.TotalDaysOnSickLeave();
                    
                    // TODO: Removed this shit!
                    if (sickLeave.Terms.Count == 3)
                    {
                        ExtendSickLeaveButtonIsEnabled = false;
                        CloseSickLeaveWithCodeIsEnabled = true;
                        
                        FirstStartDate = sickLeave.Terms[0].StartDate;
                        FirstEndDate = sickLeave.Terms[0].EndDate;
                        FirstDays = sickLeave.Terms[0].Days;
                        
                        SecondStartDate = sickLeave.Terms[1].StartDate;
                        SecondEndDate = sickLeave.Terms[1].EndDate;
                        SecondDays = sickLeave.Terms[1].Days;
                        
                        ThirdStartDate = sickLeave.Terms[2].StartDate;
                        ThirdEndDate = sickLeave.Terms[2].EndDate;
                        ThirdDays = sickLeave.Terms[2].Days;
                    }
                    else if (sickLeave.Terms.Count == 2)
                    {
                        FirstStartDate = sickLeave.Terms[0].StartDate;
                        FirstEndDate = sickLeave.Terms[0].EndDate;
                        FirstDays = sickLeave.Terms[0].Days;
                        
                        SecondStartDate = sickLeave.Terms[1].StartDate;
                        SecondEndDate = sickLeave.Terms[1].EndDate;
                        SecondDays = sickLeave.Terms[1].Days;
                    }
                    else if (sickLeave.Terms.Count == 1)
                    {
                        FirstStartDate = sickLeave.Terms[0].StartDate;
                        FirstEndDate = sickLeave.Terms[0].EndDate;
                        FirstDays = sickLeave.Terms[0].Days;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to Load patient Card");
            }
        }

        private void ResetSickLeave()
        {
            SickLeaveId = null;
            SickLeaveIsVisible = false;
            ExtendSickLeaveButtonIsEnabled = false;
            CloseSickLeaveWithCodeIsEnabled = false;
            OpenSickLeaveButtonIsEnabled = true;

            FirstStartDate = null;
            FirstEndDate = null;
            FirstDays = null;
                        
            SecondStartDate = null;
            SecondEndDate = null;
            SecondDays = null;
                        
            ThirdStartDate = null;
            ThirdEndDate = null;
            ThirdDays = null;
        }

        public async Task OpenSickLeave()
        {
            await Shell.Current.GoToAsync($"{nameof(OpenSickLeavePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }

        private async Task CloseSickLeave()
        {
            try
            {
                IsBusy = true;

                if (SickLeaveId != null)
                {
                    await _sickLeaveAppService.CloseSickLeave(SickLeaveId.Value);
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
                ResetSickLeave();
            }
        }

        private async Task CloseSickLeaveWithCode()
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
                    case "Отменить":
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                ResetSickLeave();
            }
        }

        private async Task ExtendSickLeave()
        {
            await Shell.Current.GoToAsync($"{nameof(ExtendSickLeavePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }
    }
}