using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

// iOS
// https://developer.apple.com/documentation/localauthentication/logging_a_user_into_your_app_with_face_id_or_touch_id


// Android
// https://developer.android.com/reference/android/hardware/biometrics/package-summary

namespace BioLoginExample
{
    public partial class LoginPage : ContentPage
    {
        IBioAuth biometrics;

        public LoginPage()
        {
            InitializeComponent();
            biometrics = DependencyService.Get<IBioAuth>();
        }

        protected async void btnLogin_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new FingerprintPopup(), true);
        }
    }
}
