using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Task3.Scripts.Weapons
{
    public class WeaponFactory : MonoBehaviour
    {
        [SerializeField] private List<Weapon> weaponPrefabs;

        private static WeaponFactory _instance;
        public static WeaponFactory Instance => _instance;

        private Dictionary<WeaponClass, Weapon> _weaponPrefabsOfClass;

        private void Awake()
        {
            _instance = this;

            _weaponPrefabsOfClass = weaponPrefabs.ToDictionary(weapon => weapon.WeaponClass, weapon => weapon);
        }

        public Weapon Create(WeaponClass weaponClass) => Instantiate(_weaponPrefabsOfClass[weaponClass]);
    }
}