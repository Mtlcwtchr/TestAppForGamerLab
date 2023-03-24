using UnityEngine;

namespace Task3.Scripts
{
    public class DamageReceiverViewUpdater : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer meshRenderer;
        [SerializeField] protected Material material;

        [SerializeField] protected Color lowestCapacityColor;
        [SerializeField] protected Color highestCapacityColor;

        [SerializeField] protected DamageReceiver damageReceiver;

        private Material _mat;
        
        protected virtual void Awake()
        {
            SetupMaterial();
            damageReceiver.OnCapacityChanged += CapacityChanged;
            CapacityChanged(damageReceiver.MaxCapacity);
        }

        private void OnDestroy()
        {
            damageReceiver.OnCapacityChanged -= CapacityChanged;
        }

        protected virtual void SetupMaterial()
        {
            _mat = Instantiate(material);
            meshRenderer.sharedMaterial = _mat;
        }

        protected virtual void CapacityChanged(float cap)
        {
            _mat.color = Color.Lerp(lowestCapacityColor, highestCapacityColor, cap / damageReceiver.MaxCapacity);
        }
    }
}