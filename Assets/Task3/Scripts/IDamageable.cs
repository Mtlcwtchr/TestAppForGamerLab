using System;

namespace Task3.Scripts
{
    public interface IDamageable<T>
    {
        event Action<T> OnDestroyed;
        void Damage(float damage);
    }
}