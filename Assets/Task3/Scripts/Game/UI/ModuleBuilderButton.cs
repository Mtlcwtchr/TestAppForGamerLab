using System;
using Task3.Scripts.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Task3.Scripts.Game.UI
{
    public class ModuleBuilderButton : MonoBehaviour
    {
        [SerializeField] private Module module;
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
            battleshipBuilder.TryAddModule(module);
        }

        private void RemoveButtonClick()
        {
            battleshipBuilder.TryRemoveModule(module);
        }
    }
}