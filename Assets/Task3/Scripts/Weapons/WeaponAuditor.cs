using UnityEngine;

namespace Task3.Scripts.Weapons
{
    public class WeaponAuditor : MonoBehaviour
    {
        [SerializeField] private AudioClip idleClip;
        [SerializeField] private AudioClip fireClip;
        [SerializeField] private AudioClip rechargeClip;

        [SerializeField] private AudioSource audioSource;

        public void PlayIdle()
        {
            audioSource.PlayOneShot(idleClip);
        }

        public void PlayFire()
        {
            audioSource.PlayOneShot(fireClip);
        }

        public void PlayRecharge()
        {
            audioSource.PlayOneShot(rechargeClip);
        }
    }
}