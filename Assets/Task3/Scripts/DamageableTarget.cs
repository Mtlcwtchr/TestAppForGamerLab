using System;
using UnityEngine;

namespace Task3.Scripts
{
    public abstract class DamageableTarget : MonoBehaviour, ITarget, IDamageable<DamageableTarget>
    {
        public abstract event Action<Vector3> OnPositionChanged;
        public abstract Vector3 Position { get; }

        public abstract event Action<DamageableTarget> OnDestroyed;
        public abstract void Damage(float damage);
    }
}