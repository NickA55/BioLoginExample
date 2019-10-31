# Biometric Login Sample App

Sample Xamarin Forms application using DependencyService to access native API's for iOS and Android biometrics.

---

## Permission Setup

#### - iOS
Add this string key to your info.plist file:

	<key>NSFaceIDUsageDescription</key>
	<string>MyApp needs access to to Face/Touch ID to login</string>
	
#### - Android

No special permission setup is necessary.  FingerprintManager is available on Android 6.0 and higher.  NOTE: Starting with Android 9.0, FingerprintManager is deprecrated.  Use BiometricPropmt instead (not implemented in this sample).

You will need to create your own UI in Android for the Fingerprint prompt

---

## Implementation

* Add the IBioAuth.cs interface file to your shared project
* Implement the interface in each of the platform specific projects (BiometricUtils.cs in this sample project)
 
---

## Notes
