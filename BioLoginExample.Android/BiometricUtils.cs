using System;
using System.Threading.Tasks;
using Android;
using Android.Content;
using Android.Support.V4.Content;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Support.V4.OS;
using BioLoginExample.Droid;
using BioLoginExample.Droid.BiometricHelpers;

[assembly: Xamarin.Forms.Dependency(typeof(BiometricUtils))]
namespace BioLoginExample.Droid
{
    public class BiometricUtils : IBioAuth
    {
        Context context = Android.App.Application.Context;
        CryptoObjectHelper CryptObjectHelper { get; set; }
        CancellationSignal _cancellationSignal;

        public BiometricUtils()
        {
        }

        public bool HasBiometrics()
        {
            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(context);
            return fingerprintManager.IsHardwareDetected;
        }

        public bool HasFacial()
        {
            return false;
        }

        public bool HasFingerprint()
        {
            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(context);
            return fingerprintManager.HasEnrolledFingerprints;
        }

        public bool Login()
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginAsync()
        {

            var retVal = new TaskCompletionSource<bool>();

            Android.Content.PM.Permission permissionResult = ContextCompat.CheckSelfPermission(context, Manifest.Permission.UseFingerprint);
            if (permissionResult == Android.Content.PM.Permission.Granted)
            {
                
            }
            else
            {
                // No permission. Go and ask for permissions and don't start the scanner. See
                // https://developer.android.com/training/permissions/requesting.html
            }



            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(context);

            const int flags = 0;

            
            CryptoObjectHelper cryptoHelper = new CryptoObjectHelper();

            // cancellationSignal can be used to manually stop the fingerprint scanner. 
            _cancellationSignal = new Android.Support.V4.OS.CancellationSignal();

            fingerprintManager = FingerprintManagerCompat.From(context);

            // AuthenticationCallback is a base class that will be covered later on in this guide.
            FingerprintManagerCompat.AuthenticationCallback authenticationCallback = new MyAuthCallbackSample();

            // Start the fingerprint scanner.
            fingerprintManager.Authenticate(cryptoHelper.BuildCryptoObject(), flags, _cancellationSignal, authenticationCallback, null);

            retVal.SetResult(true);

            return retVal.Task;
        }
    }


}
