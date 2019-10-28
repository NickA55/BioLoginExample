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
            var retVal = true;

            Android.Content.PM.Permission permissionResult = ContextCompat.CheckSelfPermission(context, Manifest.Permission.UseFingerprint);

            if (permissionResult == Android.Content.PM.Permission.Granted)
            {

            }
            else
            {
                // No permission.
                // https://developer.android.com/training/permissions/requesting.html
            }

            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(context);

            const int flags = 0;

            CryptoObjectHelper cryptoHelper = new CryptoObjectHelper();

            // cancellationSignal - stop scanning
            _cancellationSignal = new Android.Support.V4.OS.CancellationSignal();

            fingerprintManager = FingerprintManagerCompat.From(context);

            // Callback method
            FingerprintManagerCompat.AuthenticationCallback authenticationCallback = new AuthResultsCallback();

            // Start scanning
            fingerprintManager.Authenticate(cryptoHelper.BuildCryptoObject(), flags, _cancellationSignal, authenticationCallback, null);

            return retVal;
        }

        public void StopFingerAuth()
        {
            if (_cancellationSignal != null && !_cancellationSignal.IsCanceled)
            {
                _cancellationSignal.Cancel();
            }
        }

        public Task<bool> LoginAsync()
        {
            var retVal = new TaskCompletionSource<bool>();

            retVal.SetResult(true);

            return retVal.Task;
        }
    }
}
