using UnityEngine;

namespace Task3.Scripts
{
    public class RechargeableDamageReceiver : DamageReceiver
    {
        [SerializeField] private float rechargeValue;

        private const float RechargeDeltaTime = 1.0f;
        private float _dT;

        private void Update()
        {
            if (MaxCapacity - _currentCapacity <= Mathf.Epsilon)
            {
                return;
            } 
            
            _dT += Time.deltaTime;
            if (_dT >= RechargeDeltaTime)
            {
                _dT = 0;
                _currentCapacity = Mathf.Min(_currentCapacity + rechargeValue, MaxCapacity);
                RaiseCapacityChanged();
            }
        }
    }
}