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
        [SerializeField] private int tickBeforeFirstSpawn;
        [SerializeField] private Color color;

        private int elapsedTick;
        private int nSpawned = 0;

        private Action doAction;

		private void Start () {
            TimeManager.Instance.OnTick += Tick;
            doAction = doActionVoid;

            elapsedTick = tickBeforeFirstSpawn;

        }

        private void Tick() {
            if (nSpawned == nToSpawn) {
                TimeManager.Instance.OnTick -= Tick;
            }
            if (elapsedTick == 0) {
                SetModeSpawn();
                elapsedTick = tickBetweenSpawn;
            }
            elapsedTick--;

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
            GameObject cube = Instantiate(cubePrefab, transform.position, transform.rotation);
            cube.GetComponent<Renderer>().material.SetColor("_Color", color);
            SetModeVoid();
        }
    }
}