using System;
using System.Linq;
using Task3.Scripts.Game;
using Task3.Scripts.Modules;
using Task3.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace Task3.Scripts
{
    public class Battleship : DamageableTarget
    {
        [SerializeField] private RechargeableDamageReceiver shieldDamageReceiver;
        [SerializeField] private DamageReceiver hullDamageReceiver;

        [SerializeField] private ModulesHandler modulesHandler;
        [SerializeField] private WeaponsHandler weaponsHandler;
        [SerializeField] private Radar radar;

        [SerializeField] private UnityEvent OnModuleAttached;
        [SerializeField] private UnityEvent OnModuleDetached;
        [SerializeField] private UnityEvent OnModuleFailedToAttach;
        
        [SerializeField] private UnityEvent OnWeaponAttached;
        [SerializeField] private UnityEvent OnWeaponDetached;
        [SerializeField] private UnityEvent OnWeaponFailedToAttach;

        [SerializeField] private UnityEvent OnDestroyedEv;

        public RechargeableDamageReceiver Shield => shieldDamageReceiver;
        public DamageReceiver Hull => hullDamageReceiver;
        public WeaponsHandler WeaponsHandler => weaponsHandler;

        private DamageableTarget _target;

        private void Awake()
        {
            hullDamageReceiver.OnCapacityExceeded += Destroy;
        }

        private void Destroy()
        {
            OnDestroyedEv?.Invoke();
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        private void Update()
        {
            if (GameStateProvider.Instance.IsPaused)
            {
                return;
            }

            if (_target == null)
            {
                if (radar.TryFindTargets(out var targets))
                {
                    _target = targets.First();
                    _target.OnDestroyed += TargetDestroyed;
                }

                return;
            }

            FireTheWeapons(_target);

            void TargetDestroyed(DamageableTarget target)
            {
                if (_target != target)
                {
                    return;
                }
                _target.OnDestroyed -= TargetDestroyed;
                _target = null;
            }
        }

        public void TryAddModule(IModule module)
        {
            if (modulesHandler.TryAddModule(module))
            {
                module.Apply(this);
                OnModuleAttached?.Invoke();
            }
            else
            {
                OnModuleFailedToAttach?.Invoke();
            }
        }

        public void TryRemoveModule(IModule module)
        {
            if (modulesHandler.TryRemoveModule(module))
            {
                module.Remove(this);
                OnModuleDetached?.Invoke();
            }
        }

        public bool TryAttachWeapon(Weapon weapon)
        {
            if (weaponsHandler.TryAttachWeapon(weapon))
            {
                OnWeaponAttached?.Invoke();
                return true;
            }

            OnWeaponFailedToAttach?.Invoke();
            return false;
        }

        public bool TryDetachWeapon(Weapon weapon)
        {
            if (weaponsHandler.TryDetachWeapon(weapon))
            {
                OnWeaponDetached?.Invoke();
                return true;
            }

            return false;
        }

        public Weapon TryDetachWeapon(WeaponClass weaponClass)
        {
            var weapon = weaponsHandler.TryDetachWeapon(weaponClass);
            if (weapon)
            {
                OnWeaponDetached?.Invoke();
                return weapon;
            }

            return null;
        }

        public void FireTheWeapons(DamageableTarget target)
        {
            weaponsHandler.FireTheWeapons(target);
        }

        public override event Action<Vector3> OnPositionChanged;
        public override Vector3 Position => transform.position;
        public override event Action<DamageableTarget> OnDestroyed;

        public override void Damage(float damage)
        {
            shieldDamageReceiver.ReceiveDamage(damage);
        }
    }
}