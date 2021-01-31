using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace IsoHacks
{
    public class CabinHelper : MonoBehaviour
    {
        [SerializeField]
        public Transform Door;

        [SerializeField]
        public Transform InternalSection;

        public List<Transform> detectionPoints;
        public float openRadius;

        private ClickToMoveController player;
        private Collider2D[] playerCol;
        private Collider2D internalCol;
        private SpriteRenderer playerSprite;

        private void Start()
        {
            player = FindObjectOfType<ClickToMoveController>();
            playerCol = player.GetComponentsInChildren<Collider2D>();
            internalCol = InternalSection.GetComponent<Collider2D>();
            playerSprite = player.GetComponentInChildren<SpriteRenderer>();
            HideInternals();
            HideDoor();
            print(playerCol.Length);
        }

        private void Update()
        {
            bool n = false;
            foreach (var t in detectionPoints)
            {
                if ((player.transform.position - t.position).magnitude <= openRadius)
                    n = true;
            }
            if (n || AnyTouching())
            {
                ShowInternals();
                playerSprite.sortingOrder = 10;
                HideDoor();
            }
            else
            {
                HideInternals();
                playerSprite.sortingOrder = 5;
                ShowDoor();
            }
        }

        public void ShowInternals()
        {
            InternalSection.gameObject.SetActive(true);
        }

        bool AnyTouching()
        {
            foreach (var col in playerCol)
            {
                if (col.IsTouching(internalCol))
                {
                    print("ye");
                    return true;
                }
            }

            return false;
        }


        public void HideInternals()
        {
            InternalSection.gameObject.SetActive(false);
        }

        public void ShowDoor()
        {
            if (Door == null) return;
            Door.gameObject.SetActive(true);
        }

        public void HideDoor()
        {
            if (Door == null) return;
            Door.gameObject.SetActive(false);
        }

    }
}