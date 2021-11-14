using DoctorDiary.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DoctorDiary.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}