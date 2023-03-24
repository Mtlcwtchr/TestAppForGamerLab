using Task3.Scripts.Modules;
using Task3.Scripts.Weapons;
using UnityEngine;

namespace Task3.Scripts
{
    public class BattleshipBuilder : MonoBehaviour
    {
        [SerializeField] private Battleship battleship;

        private ModuleFactory ModuleFactory => ModuleFactory.Instance;
        private WeaponFactory WeaponFactory => WeaponFactory.Instance;

        public void TryAddModule(Module module)
        {
            battleship.TryAddModule(ModuleFactory.Create(module));
        }

        public void TryRemoveModule(Module module)
        {
            battleship.TryRemoveModule(ModuleFactory.Create(module));
        }

        public void TryAttachWeapon(WeaponClass weaponClass)
        {
            var weapon = WeaponFactory.Create(weaponClass);
            if (!battleship.TryAttachWeapon(weapon))
            {
                Destroy(weapon.gameObject);
            }
        }

        public void TryDetachWeapon(WeaponClass weaponClass)
        {
            var weapon = battleship.TryDetachWeapon(weaponClass);
            if (weapon)
            {
                Destroy(weapon.gameObject);
            }
        }
    }
}