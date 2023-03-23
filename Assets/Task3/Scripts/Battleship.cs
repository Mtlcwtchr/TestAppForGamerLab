using System;
using System.Linq;
using Task3.Scripts.Game;
using Task3.Scripts.Modules;
using Task3.Scripts.Weapons;
using UnityEngine;

namespace Task3.Scripts
{
    public class Battleship : DamageableTarget
    {
        [SerializeField] private RechargeableDamageReceiver shieldDamageReceiver;
        [SerializeField] private DamageReceiver hullDamageReceiver;

        [SerializeField] private ModulesHandler modulesHandler;
        [SerializeField] private WeaponsHandler weaponsHandler;
        [SerializeField] private Radar radar;

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
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }

        private void Update()
        {
            if (GameState.IsPaused)
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

            void TargetDestroyed()
            {
                _target.OnDestroyed -= TargetDestroyed;
                _target = null;
            }
        }

        public void TryAddModule(IModule module)
        {
            if (modulesHandler.TryAddModule(module))
            {
                module.Apply(this);
            }
        }

        public void TryRemoveModule(IModule module)
        {
            if (modulesHandler.TryRemoveModule(module))
            {
                module.Remove(this);
            }
        }

        public bool TryAttachWeapon(Weapon weapon)
        {
            return weaponsHandler.TryAttachWeapon(weapon);
        }

        public bool TryDetachWeapon(Weapon weapon)
        {
            return weaponsHandler.TryDetachWeapon(weapon);
        }

        public void FireTheWeapons(DamageableTarget target)
        {
            weaponsHandler.FireTheWeapons(target);
        }

        public override event Action<Vector3> OnPositionChanged;
        public override Vector3 Position => transform.position;
        public override event Action OnDestroyed;

        public override void Damage(float damage)
        {
            shieldDamageReceiver.ReceiveDamage(damage);
        }
    }
}