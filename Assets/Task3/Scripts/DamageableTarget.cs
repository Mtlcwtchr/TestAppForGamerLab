using System;
using UnityEngine;

namespace Task3.Scripts
{
    public abstract class DamageableTarget : MonoBehaviour, ITarget, IDamageable
    {
        public abstract event Action<Vector3> OnPositionChanged;
        public abstract Vector3 Position { get; }

        public abstract event Action OnDestroyed;
        public abstract void Damage(float damage);
    }
}