using UnityEngine;

namespace Task3.Scripts.Game.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject launchGUIRoot;
        [SerializeField] private GameObject endgameGUIRoot;

        private static UIManager _instance;
        public static UIManager Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

        public void SetLaunchGUIVisible(bool isVisible)
        {
            launchGUIRoot.SetActive(isVisible);
        }

        public void SetEndgameGUIVisible(bool isVisible)
        {
            endgameGUIRoot.SetActive(isVisible);
        }
    }
}