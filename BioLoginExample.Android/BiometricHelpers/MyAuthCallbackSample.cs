﻿using System;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Util;
using Java.Lang;
using Javax.Crypto;

namespace BioLoginExample.Droid.BiometricHelpers
{
    public class MyAuthCallbackSample : FingerprintManagerCompat.AuthenticationCallback
    {
        // Can be any byte array, keep unique to application.
        static readonly byte[] SECRET_BYTES = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        // The TAG can be any string, this one is for demonstration.
        //static readonly string TAG = "X:" + typeof(SimpleAuthCallbacks).Name;

        public MyAuthCallbackSample()
        {
        }

        public override void OnAuthenticationSucceeded(FingerprintManagerCompat.AuthenticationResult result)
        {
            return;

            if (result.CryptoObject.Cipher != null)
            {
                try
                {
                    // Calling DoFinal on the Cipher ensures that the encryption worked.
                    byte[] doFinalResult = result.CryptoObject.Cipher.DoFinal(SECRET_BYTES);

                    // No errors occurred, trust the results.              
                }
                catch (BadPaddingException bpe)
                {
                    // Can't really trust the results.
                    System.Diagnostics.Debug.WriteLine("Failed to encrypt the data with the generated key." + bpe);
                }
                catch (IllegalBlockSizeException ibse)
                {
                    // Can't really trust the results.
                    System.Diagnostics.Debug.WriteLine("Failed to encrypt the data with the generated key." + ibse);
                }
            }
            else
            {
                // No cipher used, assume that everything went well and trust the results.
            }
        }

        public override void OnAuthenticationError(int errMsgId, ICharSequence errString)
        {
            // Report the error to the user. Note that if the user canceled the scan,
            // this method will be called and the errMsgId will be FingerprintState.ErrorCanceled.
            string msg = "";
        }

        public override void OnAuthenticationFailed()
        {
            // Tell the user that the fingerprint was not recognized.
            string msg = "";
        }

        public override void OnAuthenticationHelp(int helpMsgId, ICharSequence helpString)
        {
            // Notify the user that the scan failed and display the provided hint.
            string msg = "";
        }
    }
}
