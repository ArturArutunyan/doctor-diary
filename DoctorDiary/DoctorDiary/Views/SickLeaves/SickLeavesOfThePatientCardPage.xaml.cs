using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiary.ViewModels.SickLeaves;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.SickLeaves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SickLeavesOfThePatientCardPage : ContentPage
    {
        private readonly SickLeavesOfThePatientCardViewModel _sickLeavesOfThePatientCardViewModel;

        public SickLeavesOfThePatientCardPage()
        {
            InitializeComponent();
            BindingContext = _sickLeavesOfThePatientCardViewModel = new SickLeavesOfThePatientCardViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _sickLeavesOfThePatientCardViewModel.OnAppearing();
        }
    }
}