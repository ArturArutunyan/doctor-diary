using System.ComponentModel;
using DoctorDiary.iOS.Renderers;
using DoctorDiary.Shared.Editors;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace DoctorDiary.iOS.Renderers
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        public static void Init() { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
 
            Control.Layer.BorderWidth = 0;
        }
    }
}