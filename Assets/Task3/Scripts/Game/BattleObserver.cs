using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Task3.Scripts.Game
{
    public class BattleObserver: MonoBehaviour
    {
        [SerializeField] private List<Battleship> ships;

        [SerializeField] private float winnerSpinVelocity;
        [SerializeField] private Camera mainCamera;

        private Camera _camera;
        private Camera Camera => _camera ? _camera : (_camera = mainCamera) ?? (_camera = Camera.main);

        private void Awake()
        {
            foreach (var ship in ships)
            {
                ship.OnDestroyed += ShipDestroyed;
            }
        }

        private void ShipDestroyed(DamageableTarget damageableTarget)
        {
            damageableTarget.OnDestroyed -= ShipDestroyed;

            GameStateProvider.Instance.GoNextState();
            StartCoroutine(SpinWinner(ships.First(ship => ship != damageableTarget)));
        }

        private IEnumerator SpinWinner(DamageableTarget winner)
        {
            Camera.transform.position = winner.transform.position + new Vector3(0, 0, -10);
            while (true)
            {
                if (!winner)
                {
                    yield break;
                }
                
                var spin = winnerSpinVelocity * Time.deltaTime;
                winner.transform.RotateAround(winner.transform.position, winner.transform.up, spin);
                yield return null;
            }
        }
    }
}