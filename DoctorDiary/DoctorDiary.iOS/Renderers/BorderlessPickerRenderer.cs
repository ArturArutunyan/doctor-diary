using System.ComponentModel;
using DoctorDiary.iOS.Renderers;
using DoctorDiary.Shared.Pickers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]
namespace DoctorDiary.iOS.Renderers
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
        public static void Init() { }
        
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
 
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}