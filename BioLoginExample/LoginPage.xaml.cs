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


        private async Task<bool> LoginUserUsingBio()
        {
            var result = await biometrics.LoginAsync();

            //await DisplayAlert("Result", $"Done with login {result}", "OK");

            return result;

        }

        protected async void btnLogin_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FingerprintPopup());

            

            return;

            // Biometric prompt
            // TODO - Make sure username/password is saved first
            if (biometrics.HasBiometrics() && (biometrics.HasFacial() || biometrics.HasFingerprint()))
            {
                await LoginUserUsingBio();
            } else
            {
                await DisplayAlert("No Bueno", $"Sorry, no bio", "OK");
            }
        }
    }
}
