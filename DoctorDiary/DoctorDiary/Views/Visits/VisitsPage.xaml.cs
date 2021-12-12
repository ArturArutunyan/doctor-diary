using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiary.ViewModels.Visits;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.Visits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitsPage : ContentPage
    {
        private readonly VisitsViewModel _visitsViewModel;

        public VisitsPage()
        {
            InitializeComponent();
            BindingContext = _visitsViewModel = new VisitsViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _visitsViewModel.OnAppearing();
        }

        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _visitsViewModel.SetDay(e.NewDate);
        }
    }
}