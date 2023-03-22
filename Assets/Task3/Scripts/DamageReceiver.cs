using System;
using UnityEngine;

namespace Task3.Scripts
{
    public class DamageReceiver : MonoBehaviour
    {
        public event Action<float> OnCapacityChanged;
        public event Action OnCapacityExceeded;

        [SerializeField] private float maxCapacity;
        [SerializeField] private DamageReceiver nextReceiver;

        protected float _currentCapacity;
        public float MaxCapacity => maxCapacity;

        protected virtual void Awake()
        {
            _currentCapacity = maxCapacity;
            RaiseCapacityChanged();
        }

        public void ReceiveDamage(float damage)
        {
            if (damage == 0)
            {
                return;
            }

            if (_currentCapacity <= 0)
            {
                if (nextReceiver)
                {
                    nextReceiver.ReceiveDamage(damage);
                }

                return;
            }

            var capacityDamaged = _currentCapacity - damage;
            if (_currentCapacity > 0)
            {
                _currentCapacity = Mathf.Max(capacityDamaged, .0f);
                RaiseCapacityChanged();

                if (capacityDamaged <= 0)
                {
                    RaiseCapacityExceeded();
                }
            }

            if (nextReceiver)
            {
                nextReceiver.ReceiveDamage(Mathf.Abs(capacityDamaged));
            }
        }

        protected void RaiseCapacityChanged()
        {
            OnCapacityChanged?.Invoke(_currentCapacity);
        }

        protected void RaiseCapacityExceeded()
        {
            OnCapacityExceeded?.Invoke();
        }
    }
}