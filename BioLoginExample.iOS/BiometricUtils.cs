using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BioLoginExample.iOS;
using Foundation;
using LocalAuthentication;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(BiometricUtils))]
namespace BioLoginExample.iOS
{
    public class BiometricUtils : IBioAuth
    {
        string BiometryType = "";

        public BiometricUtils()
        {
        }

        public bool HasBiometrics()
        {
            bool retVal = false;

            var context = new LAContext();

            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out var authError))
            {
                retVal = true;

                if (authError != null)
                {
                    retVal = false;
                }
            }

            return retVal;
        }

        public bool HasFacial()
        {
            bool retVal = false;

            var context = new LAContext();

            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out var authError1))
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    context.LocalizedReason = "Authorize for access to STATALert"; // iOS 11

                    // 0 = none
                    // 1 = TouchID
                    // 2 = FaceID

                    BiometryType = context.BiometryType == LABiometryType.TouchId ? "Touch ID" : "Face ID";

                    if (context.BiometryType == LABiometryType.FaceId)
                    {
                        retVal = true;
                    }

                }
                else
                {   // No FaceID before iOS 11
                    retVal = false;
                }
            }
            return retVal;
        }

        public bool HasFingerprint()
        {
            bool retVal = false;

            var context = new LAContext();

            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out var authError1))
            {
                context.LocalizedReason = "Authorize for access to STATALert"; // iOS 11

                if (context.BiometryType == LABiometryType.TouchId)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public bool Login()
        {
            bool retVal = false;

            return retVal;
        }

        public Task<bool> LoginAsync()
        {
            LAContextReplyHandler replyHandler;

            var retVal = new TaskCompletionSource<bool>();

            var context = new LAContext();
            NSError AuthError;

            var localizedReason = new NSString("Access to STATAlert");

            // because LocalAuthentication APIs have been extended over time, need to check iOS version before setting some properties
            context.LocalizedFallbackTitle = "Fallback"; // iOS 8

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                context.LocalizedCancelTitle = "Cancel"; // iOS 10
            }
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                context.LocalizedReason = "Authorize for access to STATAlert"; // iOS 11
                BiometryType = context.BiometryType == LABiometryType.TouchId ? "TouchID" : "FaceID";
            }

            //Use canEvaluatePolicy method to test if device is TouchID or FaceID enabled
            //Use the LocalAuthentication Policy DeviceOwnerAuthenticationWithBiometrics
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
            {
                Console.WriteLine("TouchID/FaceID available/enrolled");
                replyHandler = new LAContextReplyHandler((success, error) =>
                {
                    //Make sure it runs on MainThread, not in Background

                    Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(
                    () =>
                    {
                        if (success)
                        {
                            retVal.SetResult(success);
                        }
                        else
                        {
                            Console.WriteLine(error.LocalizedDescription);
                            retVal.SetResult(success);
                        }
                    });

                });

                context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, localizedReason, replyHandler);
            }

            return retVal.Task;
        }
    }
}
