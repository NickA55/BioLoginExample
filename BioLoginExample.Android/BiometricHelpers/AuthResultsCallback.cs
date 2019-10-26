using System;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Util;
using Java.Lang;
using Javax.Crypto;
using Xamarin.Forms;

namespace BioLoginExample.Droid.BiometricHelpers
{
    public class AuthResultsCallback : FingerprintManagerCompat.AuthenticationCallback
    {
        public AuthResultsCallback()
        {
        }

        public override void OnAuthenticationSucceeded(FingerprintManagerCompat.AuthenticationResult result)
        {
            MessagingCenter.Send("GoodLogin", "GoodLogin");
        }

        public override void OnAuthenticationError(int errMsgId, ICharSequence errString)
        {
            MessagingCenter.Send("BadLogin", "BadLogin");
            
        }

        public override void OnAuthenticationFailed()
        {
            MessagingCenter.Send("BadLogin", "BadLogin");
        }

        public override void OnAuthenticationHelp(int helpMsgId, ICharSequence helpString)
        {
            MessagingCenter.Send("BadLogin", "BadLogin");
        }
    }
}
