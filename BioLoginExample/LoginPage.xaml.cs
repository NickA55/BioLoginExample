using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Essentials;

// iOS
// https://developer.apple.com/documentation/localauthentication/logging_a_user_into_your_app_with_face_id_or_touch_id


// Android - Biometric (Andoird >= 9, AndroidX (coming soon))
// https://developer.android.com/reference/android/hardware/biometrics/package-summary

// FingerprintManager (what this class uses)
// https://developer.android.com/reference/android/hardware/fingerprint/FingerprintManager

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
            // TODO - Make sure username/password is saved first

            if (biometrics.HasBiometrics() && (biometrics.HasFacial() || biometrics.HasFingerprint()))
            {

                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    await PopupNavigation.Instance.PushAsync(new FingerprintPopup(), true);
                }
                else
                {
                    await LoginUserUsingBio();
                }
            }
            else
            {
                await DisplayAlert("No Bueno", $"Sorry, no bio", "OK");
            }
        }

        private async Task<bool> LoginUserUsingBio()
        {
            var result = await biometrics.LoginAsync();

            await DisplayAlert("Result", $"Biometric Resule: {result}", "OK");

            return result;
        }
    }
}