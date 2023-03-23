using System;
using UnityEngine;

namespace Task3.Scripts
{
    public class DamageReceiver : MonoBehaviour
    {
        public event Action<float> OnCapacityChanged;
        public event Action OnCapacityExceeded;

        [SerializeField] private float baseCapacity;
        [SerializeField] private DamageReceiver nextReceiver;

        private float _maxCapacity;
        protected float _currentCapacity;
        public float MaxCapacity => _maxCapacity;

        protected virtual void Awake()
        {
            _maxCapacity = baseCapacity;
            _currentCapacity = _maxCapacity;
            RaiseCapacityChanged();
        }

        public void ReceiveDamage(float damage)
        {
            if (damage <= 0)
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
                nextReceiver.ReceiveDamage(-capacityDamaged);
            }
        }

        public void AddCapacity(float capacity)
        {
            var currentPercent = _currentCapacity / _maxCapacity;
            _maxCapacity += capacity;
            _currentCapacity = _maxCapacity * currentPercent;
            RaiseCapacityChanged();
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