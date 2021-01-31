using System;
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

        private void Start()
        {
            HideInternals();
            HideDoor();
        }

        public void ShowInternals()
        {
            InternalSection.gameObject.SetActive(true);
        }


        public void HideInternals()
        {
            InternalSection.gameObject.SetActive(false);
        }

        public void ShowDoor()
        {
            Door.gameObject.SetActive(true);
        }

        public void HideDoor()
        {
            Door.gameObject.SetActive(false);
        }

    }
}