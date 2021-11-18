using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Views.SickLeaves;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.SickLeaves
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class SickLeavesOfThePatientCardViewModel : BaseViewModel
    {
        private Term _termForCloseSickLeaveWithCodeThirtyOne;
        private bool _editButtonIsEnabled;
        private SickLeave _selectedSickLeave;
        private readonly ISickLeaveAppService _sickLeaveAppService;

        public string PatientCardId { get; set; }

        public Term TermForCloseSickLeaveWithCodeThirtyOne
        {
            get => _termForCloseSickLeaveWithCodeThirtyOne;
            set => SetProperty(ref _termForCloseSickLeaveWithCodeThirtyOne, value);
        }

        public bool EditButtonIsEnabled
        {
            get => _editButtonIsEnabled;
            set => SetProperty(ref _editButtonIsEnabled, value);
        }
        
        public SickLeave SelectedSickLeave
        {
            get => _selectedSickLeave;
            set
            {
                SetProperty(ref _selectedSickLeave, value);
                OnSickLeaveSelected(value);
            }
        }
        
        public ObservableRangeCollection<SickLeave> SickLeaves { get; }
        public AsyncCommand LoadSickLeavesAsyncCommand { get; }
        public AsyncCommand OpenSickLeaveAsyncCommand { get; }
        

        public SickLeavesOfThePatientCardViewModel()
        {
            SickLeaves = new ObservableRangeCollection<SickLeave>();
            
            LoadSickLeavesAsyncCommand = new AsyncCommand(async () => await ExecuteLoadSickLeavesCommand());
            OpenSickLeaveAsyncCommand = new AsyncCommand(OnOpenSickLeave);
            
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedSickLeave = null;
        }
        
        private async Task ExecuteLoadSickLeavesCommand()
        {
            IsBusy = true;

            try
            {
                SickLeaves.Clear();
                
                var sickLeaves = await _sickLeaveAppService.GetSickLeavesByPatientCardId(Guid.Parse(PatientCardId));
                
                foreach (var sickLeave in sickLeaves)
                {
                    SickLeaves.Add(sickLeave);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnOpenSickLeave()
        {
            await Shell.Current.GoToAsync($"{nameof(OpenSickLeavePage)}?{nameof(PatientCardId)}={PatientCardId}");
        }

        private async Task OnSickLeaveSelected(SickLeave sickLeave)
        {
            if (sickLeave.IsActive)
            {
                EditButtonIsEnabled = true;
            }
        }
    }
}