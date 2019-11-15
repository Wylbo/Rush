///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 22/10/2019 12:15
///-----------------------------------------------------------------

using Com.IsartDigital.Common;
using Com.IsartDigital.Rush.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush.Tiles {


    public class Spawner : MonoBehaviour {
        static public List<Spawner> list = new List<Spawner>();

        [SerializeField] private GameObject cubePrefab;
        [SerializeField, Range(1, 10)] private int tickBetweenSpawn;
        [SerializeField] private int nToSpawn;
        [SerializeField] private int tickBeforeFirstSpawn;
        [SerializeField] private ColorChanger.EColor color;

        private ColorChanger colorChanger;

        private MaterialPropertyBlock block;

        private int elapsedTick;
        private int nSpawned = 0;

        private Action doAction;

        public bool hasSpawnAllCube { get; private set; } = false;

        private void OnValidate() {
            ChangeColor();
        }


        private void ChangeColor() {
            colorChanger = GetComponent<ColorChanger>();
            block = colorChanger.ChangeColor(color);

            transform.GetComponentInChildren<Renderer>().SetPropertyBlock(block);
        }

        public static bool AllSpawnedAllCube() {
            for (int i = list.Count - 1; i >= 0; i--) {
                if (!list[i].hasSpawnAllCube) {
                    return false;
                }
            }

            return true;
        }

        public static void ResetAll() {
            for (int i = 0; i < list.Count; i++) {
                list[i].Reset();
            }
        }

        private void Reset() {
            nSpawned = 0;
            elapsedTick = tickBeforeFirstSpawn;
            hasSpawnAllCube = false;

            TimeManager.Instance.OnTick -= Tick;
            TimeManager.Instance.OnTick += Tick;

        }

        private void Awake() {
            list.Add(this);
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
                hasSpawnAllCube = true;
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

            Cube cubeScript = cube.GetComponent<Cube>();

            cube.GetComponent<Renderer>().SetPropertyBlock(block);
            cube.GetComponent<Renderer>().SetPropertyBlock(block);
            cubeScript.secondLight.color = block.GetColor("_Color");
            cubeScript.lightHallo.color = block.GetColor("_Color");
            cubeScript.colorIndex = (int)color;

            SetModeVoid();
        }
    }
}