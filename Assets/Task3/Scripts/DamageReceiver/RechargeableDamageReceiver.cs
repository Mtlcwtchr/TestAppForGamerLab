using Task3.Scripts.Game;
using UnityEngine;

namespace Task3.Scripts
{
    public class RechargeableDamageReceiver : DamageReceiver
    {
        [SerializeField] private float baseRechargeValue;

        private float _rechargeValue;

        private const float RechargeDeltaTime = 1.0f;
        private float _dT;

        public void ModifyRechargeValue(float factor)
        {
            _rechargeValue *= factor;
        }

        protected override void Awake()
        {
            base.Awake();

            _rechargeValue = baseRechargeValue;
        }

        private void Update()
        {
            if (GameState.IsPaused)
            {
                return;
            }
            
            if (MaxCapacity - _currentCapacity <= Mathf.Epsilon)
            {
                return;
            } 
            
            _dT += Time.deltaTime;
            if (_dT >= RechargeDeltaTime)
            {
                _dT = 0;
                _currentCapacity = Mathf.Min(_currentCapacity + _rechargeValue, MaxCapacity);
                RaiseCapacityChanged();
            }
        }
    }
}