using UnityEngine;

namespace Task3.Scripts
{
    public class DamageReceiverView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material material;

        [SerializeField] private Color lowestCapacityColor;
        [SerializeField] private Color highestCapacityColor;

        [SerializeField] private DamageReceiver damageReceiver;

        private Material _mat;

        private void Awake()
        {
            _mat = Instantiate(material);
            meshRenderer.sharedMaterial = _mat;

            damageReceiver.OnCapacityChanged += CapacityChanged;
            CapacityChanged(damageReceiver.MaxCapacity);
        }

        private void CapacityChanged(float cap)
        {
            _mat.color = Color.Lerp(lowestCapacityColor, highestCapacityColor, cap / damageReceiver.MaxCapacity);
        }
    }
}