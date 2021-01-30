using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using World;
using Random = UnityEngine.Random;

namespace Player
{
    [RequireComponent(typeof(Speaker))]
    public class SoundOnNavigationFailure : MonoBehaviour
    {
        public GameObject floor;

        private Speaker _speaker;

        [FormerlySerializedAs("sampleGroup")] public string voidSampleGroup;
        public string nopathSampleGroup;
        
        private TileLookup _tileLookup;
        

        private void Awake()
        {
            _speaker = GetComponent<Speaker>();

            ClickToMoveController ctmc = GetComponentInChildren<ClickToMoveController>();

            ctmc.OnNavigationFailed += NavigationFailedEvent;

            _tileLookup = floor.GetComponent<TileLookup>();
        }

        private void Start()
        {
        }

        private void NavigationFailedEvent(Transform obj, Vector3 worldPosition)
        {
            TileBase tbase = _tileLookup.FindTileAtWorldLocation(worldPosition);
            if (!tbase)
            {
                if (voidSampleGroup.Length > 0)
                    _speaker.PlayOneShot(voidSampleGroup);
                //TODO: display message about not being able to walk into the void.
            }
            else
            {
                if (nopathSampleGroup.Length > 0)
                    _speaker.PlayOneShot(nopathSampleGroup);
                //TODO: some comment about not being able to find a path there.
            }
        }
    }
}