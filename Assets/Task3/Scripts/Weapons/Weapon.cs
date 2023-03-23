using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Task3.Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponClass weaponClass;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform launchRoot;

        [SerializeField] private float baseDamage;
        [SerializeField] private float baseRechargeTime;

        public WeaponClass WeaponClass => weaponClass;

        public float Damage => baseDamage;

        private float _rechargeTime;
        public float RechargeTime => _rechargeTime;

        private WeaponState _currentState;

        private DamageableTarget _requestedTarget;

        #region ProjectilesPool

        [SerializeField] private Transform projectilesRoot;
        [SerializeField] private int projectilesPoolCapacity;
        [SerializeField] private int projectilesPoolIncreaseStep;

        private Queue<Projectile> _projectiles;
        private HashSet<Projectile> _activeProjectiles;

        private void PoolProjectiles(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                Projectile projectile = Instantiate(projectilePrefab, projectilesRoot);
                projectile.OnHit += ReleaseProjectile;
                projectile.gameObject.SetActive(false);
                _projectiles.Enqueue(projectile);
            }
        }

        private Projectile GetProjectile()
        {
            if (_projectiles.Count <= 0)
            {
                PoolProjectiles(projectilesPoolIncreaseStep);
            }

            Projectile projectile = _projectiles.Dequeue();
            projectile.gameObject.SetActive(true);
            _activeProjectiles.Add(projectile);

            return projectile;
        }

        private void ReleaseProjectile(Projectile projectile)
        {
            if (_activeProjectiles.Remove(projectile))
            {
                _projectiles.Enqueue(projectile);
                projectile.gameObject.SetActive(false);
                projectile.transform.SetParent(projectilesRoot);
                projectile.transform.position = launchRoot.position;
            }
        }

        #endregion

        private void Awake()
        {
            _projectiles = new Queue<Projectile>(projectilesPoolCapacity);
            _activeProjectiles = new HashSet<Projectile>(projectilesPoolCapacity);
            PoolProjectiles(projectilesPoolCapacity);
            _rechargeTime = baseRechargeTime;
            _currentState = WeaponState.Idle;
        }

        private void OnDestroy()
        {
            foreach (Projectile projectile in _projectiles)
            {
                projectile.OnHit -= ReleaseProjectile;
            }

            foreach (Projectile projectile in _activeProjectiles)
            {
                projectile.OnHit -= ReleaseProjectile;
            }
        }

        public bool TryFire(DamageableTarget target)
        {
            _requestedTarget = target;
            return TrySetState(WeaponState.Fire);
        }

        public void TryRecharge()
        {
            TrySetState(WeaponState.Recharging);
        }

        public void ModifyRechargeTime(float factor)
        {
            _rechargeTime *= factor;
        }

        #region StateMachine

        private bool TrySetState(WeaponState state)
        {
            if (CanSwitchState(_currentState, state))
            {
                ProceedState(_currentState = state);
                return true;
            }

            return false;
        }

        private bool CanSwitchState(WeaponState from, WeaponState to) =>
            from switch
            {
                WeaponState.Idle => true,
                WeaponState.Fire => to == WeaponState.Recharging,
                WeaponState.Recharging => to == WeaponState.Idle,
                _ => false
            };

        private void ProceedState(WeaponState state)
        {
            switch (state)
            {
                case WeaponState.Idle:
                    _requestedTarget = null;
                    break;
                case WeaponState.Fire:
                    Fire(_requestedTarget);
                    break;
                case WeaponState.Recharging:
                    Recharge();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Fire(DamageableTarget target)
        {
            Projectile projectile = GetProjectile();
            projectile.transform.ToWorld();
            projectile.transform.position = launchRoot.position;
            projectile.Fire(target, Damage);
        }

        private void Recharge()
        {
            StartCoroutine(Recharge(RechargeTime, () => TrySetState(WeaponState.Idle)));
        }

        private float _dT;

        private IEnumerator Recharge(float rechargeTime, Action onRechargedCallback = null)
        {
            while (_dT < rechargeTime)
            {
                _dT += Time.deltaTime;
                yield return null;
            }

            _dT = 0;
            onRechargedCallback?.Invoke();
        }

        #endregion
    }
}