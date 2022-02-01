using Xamarin.Forms;

namespace DoctorDiary.Shared.Buttons
{
    // Hint: https://alexdunn.org/2017/05/30/xamarin-tips-adding-dynamic-elevation-to-your-xamarin-forms-buttons/
    public class MaterialButton : Button
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