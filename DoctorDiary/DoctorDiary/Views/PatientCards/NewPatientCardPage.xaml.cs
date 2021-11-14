using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    public partial class NewPatientCardPage : ContentPage
    {
        public PatientCard PatientCard { get; set; }

        public NewPatientCardPage()
        {
            InitializeComponent();
            BindingContext = new NewPatientCardViewModel();
        }
    }
}