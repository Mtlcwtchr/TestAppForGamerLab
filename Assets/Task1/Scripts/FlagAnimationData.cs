using UnityEngine;

namespace Task1.Scripts
{
    [CreateAssetMenu(fileName = "FlagAnimationData", menuName = "Task1/FlagAnimationData")]
    public class FlagAnimationData : ScriptableObject
    {
        public float speed;
        public float freq;
        public float power;
        public float damping;
        public float phaseShift;
        public Vector3 windDirection;
        public float windSpeed;
        public AnimationCurve curve;

        public float scrollSpeed;
        public Vector2 scrollDirection;
    }
}
