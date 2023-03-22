using Task3.Scripts;
using UnityEngine;

public class Battleship : MonoBehaviour
{
    [SerializeField] private DamageReceiver shieldDamageReceiver;
    [SerializeField] private DamageReceiver hullDamageReceiver;

    [SerializeField] private Material hullMaterial;
    [SerializeField] private Material shieldMaterial;

    void Start()
    {
        shieldDamageReceiver.OnCapacityChanged += ShieldCapacityChanged;
        hullDamageReceiver.OnCapacityChanged += HullCapacityChanged;

        shieldDamageReceiver.ReceiveDamage(160f);
    }

    private void HullCapacityChanged(float cap)
    {
        hullMaterial.color = Color.Lerp(Color.green, Color.red, 1 - cap/hullDamageReceiver.MaxCapacity);
    }

    private void ShieldCapacityChanged(float cap)
    {
        shieldMaterial.color = Color.Lerp(Color.blue, Color.clear, 1 - cap/shieldDamageReceiver.MaxCapacity);
    }
}
