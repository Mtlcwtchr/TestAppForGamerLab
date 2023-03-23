using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Task3.Scripts.Weapons
{
    public class WeaponsHandler: MonoBehaviour
    {
        [SerializeField] private int weaponsCapacity;
        [SerializeField] private List<Transform> weaponsRoots;

        private List<Weapon> _weapons;

        private List<Transform> _occupiedSlots;
        private List<Transform> _freeSlots;

        private void Awake()
        {
            _weapons = new List<Weapon>(weaponsCapacity);
            _occupiedSlots = new List<Transform>(weaponsCapacity);
            _freeSlots = new List<Transform>(weaponsCapacity);
            _freeSlots.AddRange(weaponsRoots);
        }

        public bool TryAttachWeapon(Weapon weapon)
        {
            if (!CanAttachWeapon())
            {
                return false;
            }

            if (_weapons.Contains(weapon))
            {
                return false;
            }

            _weapons.Add(weapon);
            var root = _freeSlots.FirstOrDefault();
            if (root)
            {
                _freeSlots.Remove(root);
                _occupiedSlots.Add(root);
                
                weapon.transform.SetParent(root);
                weapon.transform.position = root.position;
                weapon.transform.rotation = root.rotation;
            }

            return true;
        }

        public bool TryDetachWeapon(Weapon weapon)
        {
            if (_weapons.Remove(weapon))
            {
                var weaponRoot = weapon.transform.parent;
                _occupiedSlots.Remove(weaponRoot);
                _freeSlots.Add(weaponRoot);

                weapon.transform.ToWorld();
                return true;
            }

            return false;
        }

        public bool CanAttachWeapon()
        {
            return _weapons.Count < weaponsCapacity;
        }

        public void ModifyWeaponsRechargeSpeed(float factor)
        {
            foreach (var weapon in _weapons)
            {
                weapon.ModifyRechargeTime(factor);
            }
        }

        public void FireTheWeapons(DamageableTarget target)
        {
            foreach (var weapon in _weapons)
            {
                if (!weapon.TryFire(target))
                {
                    weapon.TryRecharge();
                }
            }
        }
    }
}