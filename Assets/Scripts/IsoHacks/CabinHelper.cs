using System;
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

        public Transform detectionPoint;
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
        }

        private void Update()
        {
            if ((player.transform.position - detectionPoint.position).magnitude <= openRadius || AnyTouching())
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
                    return true;
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