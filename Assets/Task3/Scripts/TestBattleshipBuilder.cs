using System.Collections;
using Task3.Scripts.Modules;
using Task3.Scripts.Weapons;
using UnityEngine;

namespace Task3.Scripts
{
    public class TestBattleshipBuilder : MonoBehaviour
    {
        [SerializeField] private Battleship battleshipA;
        [SerializeField] private Battleship battleshipB;

        private void Start()
        {
            StartCoroutine(BuildShip());
        }

        private IEnumerator BuildShip()
        {
            yield return null;

            Weapon weaponA = WeaponFactory.Instance.Create(WeaponClass.A);
            Weapon weaponB = WeaponFactory.Instance.Create(WeaponClass.B);
            Weapon weaponC = WeaponFactory.Instance.Create(WeaponClass.C);
            Weapon weaponBAgain = WeaponFactory.Instance.Create(WeaponClass.C);

            if (!battleshipA.TryAttachWeapon(weaponA))
            {
                Destroy(weaponA.gameObject);
            }

            if (!battleshipA.TryAttachWeapon(weaponB))
            {
                Destroy(weaponB.gameObject);
            }

            if (!battleshipA.TryAttachWeapon(weaponC))
            {
                Destroy(weaponC.gameObject);
            }

            if (!battleshipA.TryAttachWeapon(weaponBAgain))
            {
                Destroy(weaponBAgain.gameObject);
            }

            if (battleshipA.TryDetachWeapon(weaponB))
            {
                Destroy(weaponB.gameObject);
            }

            Weapon newWeaponC = WeaponFactory.Instance.Create(WeaponClass.C);
            if (!battleshipA.TryAttachWeapon(newWeaponC))
            {
                Destroy(newWeaponC.gameObject);
            }

            battleshipA.TryAddModule(ModuleFactory.Instance.Create(Module.ShieldCapacity));
            battleshipA.TryAddModule(ModuleFactory.Instance.Create(Module.ShieldCapacity));
            
            Weapon weaponC1 = WeaponFactory.Instance.Create(WeaponClass.C);
            Weapon weaponC2 = WeaponFactory.Instance.Create(WeaponClass.C);

            if (!battleshipB.TryAttachWeapon(weaponC1))
            {
                Destroy(weaponC1.gameObject);
            }

            if (!battleshipB.TryAttachWeapon(weaponC2))
            {
                Destroy(weaponC2.gameObject);
            }

            battleshipB.TryAddModule(ModuleFactory.Instance.Create(Module.WeaponRecharge));
            battleshipB.TryAddModule(ModuleFactory.Instance.Create(Module.ShieldRecharge));
            battleshipB.TryAddModule(ModuleFactory.Instance.Create(Module.HullCapacity));
        }
    }
}