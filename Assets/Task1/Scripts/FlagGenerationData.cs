using UnityEngine;

namespace Task1.Scripts
{
    [CreateAssetMenu(fileName = "FlagGenerationData", menuName = "Task1/FlagGenerationData")]
    public class FlagGenerationData : ScriptableObject
    {
        public Vector2Int size;
        public Material mat;
    }
}
