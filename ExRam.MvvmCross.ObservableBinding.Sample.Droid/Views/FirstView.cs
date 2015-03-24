using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace ExRam.MvvmCross.ObservableBinding.Sample.Views
{
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.FirstView);
        }
    }
}