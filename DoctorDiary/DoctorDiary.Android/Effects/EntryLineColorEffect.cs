using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.ComponentModel;
using System.Diagnostics;
using DoctorDiary.Droid.Effects;
using DoctorDiary.Shared.Behaviors;

[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]
namespace DoctorDiary.Droid.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        private EditText _control;

        protected override void OnAttached()
        {
            try
            {
                _control = Control as EditText;
                UpdateLineColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            _control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorBehavior.LineColorProperty.PropertyName)
            {
                UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            try
            {
                if (_control != null)
                {
                    _control.Background.SetColorFilter(LineColorBehavior.GetLineColor(Element).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}