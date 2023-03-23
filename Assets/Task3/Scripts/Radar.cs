using System.Collections.Generic;
using UnityEngine;

namespace Task3.Scripts
{
    public class Radar: MonoBehaviour
    {
        [SerializeField] private int maxTargetsPerScan;
        [SerializeField] private float range;
        [SerializeField] private Transform radarRoot;

        [SerializeField] private Battleship ship;

        public bool TryFindTargets(out List<DamageableTarget> targets)
        {
            targets = new List<DamageableTarget>();
            var hits = new RaycastHit[maxTargetsPerScan];
            var size = Physics.SphereCastNonAlloc(radarRoot.position, range, radarRoot.up, hits, range);
            if (size > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.collider &&
                        hit.collider.TryGetComponent<DamageableTarget>(out var component) &&
                        component != ship)
                    {
                        targets.Add(component);
                    }
                }
            }

            return targets.Count > 0;
        }
    }
}