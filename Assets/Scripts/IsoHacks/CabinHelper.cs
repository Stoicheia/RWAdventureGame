using System;
using UnityEngine;

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