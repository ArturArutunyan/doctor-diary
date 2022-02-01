using DoctorDiary.Shared.Buttons;
using Xamarin.Forms;

namespace DoctorDiary.Shared.Frames
{
    // Hint: https://alexdunn.org/2018/06/06/xamarin-tip-dynamic-elevation-frames/
    public class MaterialFrame : Frame
    {
        public static BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(float), typeof(MaterialButton), 4.0f);
 
        public float Elevation
        {
            get
            {
                return (float)GetValue(ElevationProperty);
            }
            set
            {
                SetValue(ElevationProperty, value);
            }
        }
    }
}