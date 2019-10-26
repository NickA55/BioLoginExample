using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BioLoginExample
{
    public partial class FingerprintPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        IBioAuth biometrics;

        public FingerprintPopup()
        {
            InitializeComponent();

            biometrics = DependencyService.Get<IBioAuth>();

            MessagingCenter.Subscribe<string>(this, "GoodLogin", (str) =>
            {
                GoodLogin();
            });

            MessagingCenter.Subscribe<string>(this, "BadLogin", (str) =>
            {
                BadLogin();
            });

        }

        private async void GoodLogin()
        {
            biometrics.StopFingerAuth();

            MessagingCenter.Unsubscribe<string>(this, "BadLogin");
            MessagingCenter.Unsubscribe<string>(this, "GoodLogin");

            await DisplayAlert("Login", "Good", "OK");

            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void BadLogin()
        {

            biometrics.StopFingerAuth();

            MessagingCenter.Unsubscribe<string>(this, "BadLogin");
            MessagingCenter.Unsubscribe<string>(this, "GoodLogin");

            await DisplayAlert("Login", "Bad", "OK");

            await PopupNavigation.Instance.PopAsync(true);
        }


        private void LoginUserUsingBio()
        {
            var result = biometrics.Login();
            
        }

        protected async void CancelClicked(object sender, EventArgs e)
        {
            biometrics.StopFingerAuth();

            MessagingCenter.Unsubscribe<string>(this, "BadLogin");
            MessagingCenter.Unsubscribe<string>(this, "GoodLogin");

            await DisplayAlert("Login", "Cancelled", "OK");

            await PopupNavigation.Instance.PopAsync(true);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Biometric prompt
            // TODO - Make sure username/password is saved first
            if (biometrics.HasBiometrics() && (biometrics.HasFacial() || biometrics.HasFingerprint()))
            {
                LoginUserUsingBio();
            }
            else
            {
                await DisplayAlert("No Bueno", $"Sorry, no bio", "OK");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}
