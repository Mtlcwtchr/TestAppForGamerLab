using UnityEngine;

namespace Task3.Scripts
{
    public class BattleshipAuditor : MonoBehaviour
    {
        [SerializeField] private AudioClip attachModuleClip;
        [SerializeField] private AudioClip failToAttachModuleClip;
        [SerializeField] private AudioClip detachModuleClip;

        [SerializeField] private AudioClip attachWeaponClip;
        [SerializeField] private AudioClip failToAttachWeaponClip;
        [SerializeField] private AudioClip detachWeaponClip;

        [SerializeField] private AudioClip destroyClip;

        [SerializeField] private AudioSource audioSource;

        public void PlayAttachModule()
        {
            audioSource.PlayOneShot(attachModuleClip);
        }

        public void PlayFailToAttachModule()
        {
            audioSource.PlayOneShot(failToAttachModuleClip);
        }

        public void PlayDetachModule()
        {
            audioSource.PlayOneShot(detachModuleClip);
        }
        
        public void PlayAttachWeapon()
        {
            audioSource.PlayOneShot(attachWeaponClip);
        }

        public void PlayFailToAttachWeapon()
        {
            audioSource.PlayOneShot(failToAttachWeaponClip);
        }

        public void PlayDetachWeapon()
        {
            audioSource.PlayOneShot(detachWeaponClip);
        }
        
        public void PlayDestroy()
        {
            audioSource.PlayOneShot(destroyClip);
        }
    }
}