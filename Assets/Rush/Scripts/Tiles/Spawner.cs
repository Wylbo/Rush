///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 22/10/2019 12:15
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
	public class Spawner : MonoBehaviour {

        [SerializeField] private GameObject cubePrefab;
        [SerializeField, Range(1, 5)] private int tickBetweenSpawn;
        [SerializeField] private int nToSpawn;

        private int elapsedTick;
        private int nSpawned = 0;

        private Action doAction;

		private void Start () {
            TimeManager.Instance.OnTick += Tick;
            doAction = doActionVoid;

            elapsedTick = tickBetweenSpawn;

        }

        private void Tick() {
            if (nSpawned == nToSpawn) {
                TimeManager.Instance.OnTick -= Tick;
            }
            if (elapsedTick == tickBetweenSpawn) {
                SetModeSpawn();
                elapsedTick = 0;
            }
            elapsedTick++;

        }

        private void Update () {
            doAction();
		}

        private void SetModeVoid() {
            doAction = doActionVoid;
        }

        private void doActionVoid() { }

        private void SetModeSpawn() {
            nSpawned++;

            doAction = doActionSpawn;
        }

        private void doActionSpawn() {
            Instantiate(cubePrefab, transform.position, transform.rotation);
            SetModeVoid();
        }
    }
}