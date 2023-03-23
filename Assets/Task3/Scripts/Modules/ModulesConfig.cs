using UnityEngine;

namespace Task3.Scripts.Modules
{
    [CreateAssetMenu(fileName = "ModulesConfig", menuName = "Task3/ModulesConfig", order = 0)]
    public class ModulesConfig : ScriptableObject
    {
        public float shieldRechargeFactor;
        public float shieldCapacityFactor;
        public float hullCapacityFactor;
        public float weaponsRechargeFactor;
    }
}