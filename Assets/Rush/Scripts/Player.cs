///-----------------------------------------------------------------
/// Author : Maximilien Galea
/// Date : 05/11/2019 11:33
///-----------------------------------------------------------------

using Com.IsartDigital.Rush.Manager;
using Com.IsartDigital.Rush.Tiles;
using Pixelplacement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Rush {
    public class Player : MonoBehaviour {

        static public Player Instance { get; private set; }

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private GameObject previewPrefab;
        [NonSerialized]public GameObject level;

        private GameObject preview;
        private int inventoryIndex = 0;
        private List<ElementInventory> inventory;
        private ElementInventory elementInHand;
        private GameObject objectInHand;

        private CameraController controller;

        private bool isInit = false;


        private void Awake() {
            if (Instance) {
                Destroy(gameObject);
                return;
            }

            controller = GetComponent<CameraController>();

            Instance = this;
        }

        private void Start() {

            LevelManager.Instance.OnLevelLoading += Init;
            LevelManager.Instance.OnLevelUnload += UnIinit;
            
        }

        public void Init(GameObject level) {

            this.level = level;
            inventory = this.level.GetComponent<Level>().list;
            preview = Instantiate(previewPrefab, level.transform);
            isInit = true;
            inventoryIndex = 0;
        }

        public void UnIinit() {
            isInit = false;
            preview = null;
            level = null;
            inventory = null;
        }

        private void Move() {
            transform.position = controller.Position();

            transform.LookAt(controller.cameraPivot);
        }

        private void Update() {
            Move();

            if (!isInit) {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                GameManager.Instance.SwitchMode();
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                GameManager.Instance.PlayPauseGame(GameManager.Instance.IsPause);
            }

            if (GameManager.Instance.IsPause || GameManager.Instance.IsInActionPhase) {
                elementInHand = null;
                preview.SetActive(false);
                return;
            }

            RaycastToGround();


        }

        public void OnHudButtonClick(int index) {
            inventoryIndex = index;
            elementInHand = null;
            NewElemInHand(index);
            ResetPreview();
        }

        public Action<int> NewElemInHand;
        private void GetElementInHand() {
            if (elementInHand == null) {
                elementInHand = inventory[inventoryIndex];
                //NewElemInHand?.Invoke(inventoryIndex);

                if (elementInHand.Tiles.Count > 0) {

                    preview.transform.rotation = elementInHand.Direction;

                    for (int i = preview.transform.childCount - 1; i >= 0; i--) {
                        if (elementInHand.CompareType(preview.transform.GetChild(i).gameObject)) {
                            preview.transform.GetChild(i).gameObject.SetActive(true);
                            break;
                        }
                    }
                }

            }
        }

        private void RaycastToGround() {
            Ray ray = controller.RaycastToGround();
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, groundMask)) {

                Transform ground = hit.collider.transform;

                Ray rayAboveGround = new Ray(ground.position, Vector3.up);
                RaycastHit hitAbove;
                bool isFree = !Physics.Raycast(rayAboveGround, out hitAbove, 1);

                GetElementInHand();

                preview.transform.position = ground.position + Vector3.up / 2;
                if (controller.OnClick()) {
                    OnClick(isFree, hitAbove);
                }


                if (!isFree) {
                    preview.SetActive(false);
                } else {
                    preview.SetActive(true);

                }
            } else {
                preview.SetActive(false);
            }

        }

        private void ResetPreview() {
            for (int i = preview.transform.childCount - 1; i >= 0; i--) {
                preview.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void OnClick(bool isFree, RaycastHit above) {
            if (isFree) {
                PutTileDown();
            } else if (above.collider.GetComponent<ADraggableTile>()) {
                RemoveTile(above.collider.gameObject);
            }
            elementInHand = null;

            ResetPreview();
        }

        public event Action<int, int> OnElementPlaced;

        private void PutTileDown() {
            if (elementInHand.Tiles.Count > 0) {
                Instantiate(elementInHand.Tiles[0], preview.transform.position, preview.transform.rotation, level.transform);
                elementInHand.Tiles.RemoveAt(0);
                OnElementPlaced?.Invoke(elementInHand.Tiles.Count, inventoryIndex);
                GetComponent<AudioSource>().Play();

                if (elementInHand.Tiles.Count == 0) {
                    findNext();

                }
            }
        }

        private void RemoveTile(GameObject above) {
            for (int i = 0; i < inventory.Count; i++) {
                if (inventory[i].CompareType(above)) {
                    inventory[i].AddOneToList();
                    inventoryIndex = i;
                    OnElementPlaced?.Invoke(inventory[inventoryIndex].Tiles.Count, inventoryIndex);
                    NewElemInHand(inventoryIndex);
                    Tween.LocalPosition(above.transform,above.transform.position + Vector3.up/2 , 0.2f, 0f, Tween.EaseInOutStrong);
                    Tween.LocalPosition(above.transform,above.transform.position + Vector3.down/2, 0.2f, 0.3f, Tween.EaseInOutStrong, Tween.LoopType.None, null,() => Destroy(above));
                    
                }
            }
        }

        private void findNext() {
            for (int i = 0; i < inventory.Count; i++) {
                if (inventory[i].Tiles.Count > 0) {
                    inventoryIndex = i;
                    NewElemInHand(inventoryIndex);
                    break;
                }
            }
        }
    }
}