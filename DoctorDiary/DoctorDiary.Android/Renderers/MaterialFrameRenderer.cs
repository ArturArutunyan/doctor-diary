using System.ComponentModel;
using AndroidX.Core.View;
using DoctorDiary.Droid.Renderers;
using DoctorDiary.Shared.Frames;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MaterialFrame), typeof(MaterialFrameRenderer))]
namespace DoctorDiary.Droid.Renderers
{
    public class MaterialFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;
 
            UpdateElevation();
        }
 
 
        private void UpdateElevation()
        {
            var materialFrame= (MaterialFrame)Element;
 
            // we need to reset the StateListAnimator to override the setting of Elevation on touch down and release.
            Control.StateListAnimator = new Android.Animation.StateListAnimator();
 
            // set the elevation manually
            ViewCompat.SetElevation(this, materialFrame.Elevation);
            ViewCompat.SetElevation(Control, materialFrame.Elevation);
        }
 
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == "Elevation")
            {
                UpdateElevation();
            }
        }
    }
}