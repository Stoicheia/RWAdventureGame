using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using World;
using Random = UnityEngine.Random;

namespace Player
{
    public class SoundOnNavigationFailure : MonoBehaviour
    {
        public AudioClip[] voidSounds;

        public GameObject floor;

        public AudioSource audioSource;
        
        private TileLookup _tileLookup;
        

        private void Awake()
        {
            ClickToMoveController ctmc = GetComponentInChildren<ClickToMoveController>();

            ctmc.OnNavigationFailed += NavigationFailedEvent;

        }

        private void Start()
        {
            _tileLookup = floor.GetComponent<TileLookup>();
            if (!audioSource)
            {
                audioSource = GetComponentInChildren<AudioSource>();
            }
        }

        private void NavigationFailedEvent(Transform obj, Vector3 worldPosition)
        {
            TileBase tbase = _tileLookup.FindTileAtWorldLocation(worldPosition);
            if (!tbase)
            {
                int soundIndex = Random.Range(0, voidSounds.Length);
                audioSource.PlayOneShot(voidSounds[soundIndex], 1.0f);
                
                //TODO: display message about not being able to walk into the void.
            }
        }
    }
}