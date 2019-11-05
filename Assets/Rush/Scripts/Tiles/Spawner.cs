///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 22/10/2019 12:15
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {
    public class Spawner : MonoBehaviour {
        static private List<Spawner> list = new List<Spawner>();

        [SerializeField] private GameObject cubePrefab;
        [SerializeField, Range(1, 10)] private int tickBetweenSpawn;
        [SerializeField] private int nToSpawn;
        [SerializeField] private int tickBeforeFirstSpawn;
        [SerializeField] private Color color;

        private MaterialPropertyBlock block;

        private int elapsedTick;
        private int nSpawned = 0;

        private Action doAction;

        private void OnValidate() {
            ChangeColor();
        }


        private void ChangeColor() {
            block = new MaterialPropertyBlock();
            block.SetColor("_Color", color);
            block.SetColor("_EmissionColor", color);

            transform.GetComponentInChildren<Renderer>().SetPropertyBlock(block);
        }

        public static void ResetAll() {
            for (int i = 0; i < list.Count; i++) {
                list[i].Reset();
            }
        }

        private void Reset() {
            nSpawned = 0;
            elapsedTick = tickBeforeFirstSpawn;

            TimeManager.Instance.OnTick -= Tick;
            TimeManager.Instance.OnTick += Tick;

        }

        private void Awake() {
            list.Add(this);
            Debug.Log("<color=red>" + list.Count + "</color>");
        }

        private void Start() {
            TimeManager.Instance.OnTick += Tick;

            ChangeColor();

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

        private void Update() {
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

            cube.GetComponent<Renderer>().SetPropertyBlock(block);
            cube.GetComponent<Renderer>().SetPropertyBlock(block);
            cube.GetComponent<Cube>().secondLight.color = color;
            cube.GetComponent<Cube>().lightHallo.color = color;

            SetModeVoid();
        }
    }
}