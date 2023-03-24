using System;
using Task3.Scripts.Modules;
using Task3.Scripts.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Task3.Scripts.Game.UI
{
    public class WeaponBuilderButton : MonoBehaviour
    {
        [SerializeField] private WeaponClass weaponClass;
        [SerializeField] private BattleshipBuilder battleshipBuilder;

        [SerializeField] private Button addButton;
        [SerializeField] private Button removeButton;

        private void OnEnable()
        {
            addButton.onClick.AddListener(AddButtonClick);
            removeButton.onClick.AddListener(RemoveButtonClick);
        }

        private void OnDisable()
        {
            addButton.onClick.RemoveListener(AddButtonClick);
            removeButton.onClick.RemoveListener(RemoveButtonClick);
        }

        private void AddButtonClick()
        {
            battleshipBuilder.TryAttachWeapon(weaponClass);
        }

        private void RemoveButtonClick()
        {
            battleshipBuilder.TryDetachWeapon(weaponClass);
        }
    }
}