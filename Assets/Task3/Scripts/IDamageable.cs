using System;

namespace Task3.Scripts
{
    public interface IDamageable
    {
        event Action OnDestroyed;
        void Damage(float damage);
    }
}