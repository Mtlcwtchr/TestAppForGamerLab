using System;
using Task3.Scripts.Game;
using UnityEngine;

namespace Task3.Scripts.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;

        public event Action<Projectile> OnHit;

        private DamageableTarget _target;
        private Vector3 _targetPosition;

        private float _damage;

        private void Update()
        {
            if (GameStateProvider.Instance.IsPaused)
            {
                return;
            }
            
            if (_target == null)
            {
                return;
            }

            var direction = (_targetPosition - transform.position);
            if ((direction - Vector3.zero).magnitude <= Mathf.Epsilon)
            {
                Hit();
            }
            transform.position += direction.normalized * (speed * Time.deltaTime);
        }

        public void Fire(DamageableTarget target, float damage)
        {
            _target = target;
            _targetPosition = target.Position;
            _target.OnPositionChanged += TargetPositionChanged;
            _target.OnDestroyed += TargetDestroyed;

            _damage = damage;
        }

        public void Stop()
        {
            Hit();
        }

        private void TargetDestroyed(DamageableTarget target)
        {
            Hit();
        }

        private void TargetPositionChanged(Vector3 pos)
        {
            _targetPosition = pos;
        }

        private void OnCollisionEnter(Collision col)
        {
            Hit(col.gameObject.GetComponent<DamageableTarget>());
        }

        private void Hit(DamageableTarget damageableTarget = null)
        {
            if (damageableTarget)
            {
                damageableTarget.Damage(_damage);
            }

            _target = null;
            OnHit?.Invoke(this);
        }
    }
}