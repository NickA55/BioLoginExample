using System;
using System.Threading.Tasks;

namespace BioLoginExample
{
    public interface IBioAuth
    {
        bool HasBiometrics();

        bool HasFacial();
        bool HasFingerprint();

        bool Login();

        Task<bool> LoginAsync();

        void StopFingerAuth();
    }
}
