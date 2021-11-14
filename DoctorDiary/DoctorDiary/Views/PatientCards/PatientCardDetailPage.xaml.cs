using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiary.ViewModels;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    public partial class PatientCardDetailPage : ContentPage
    {
        public PatientCardDetailPage()
        {
            InitializeComponent();
            BindingContext = new PatientCardDetailViewModel();
        }
    }
}