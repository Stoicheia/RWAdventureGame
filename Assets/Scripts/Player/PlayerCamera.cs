using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Follows the player (object with tag "Player") maintaining the camera within the set bounds.
    /// </summary>
    public class PlayerCamera : MonoBehaviour
    {
        private GameObject _playerObject;
        
        public float MovementSpeed { get; set; }

        // Sets the size of the region we try to keep the player in in camera coordinates
        public Vector2 MovementBounds { get; set; }
        
        private void Awake()
        {
            MovementSpeed = 1.0f;
            MovementBounds = new Vector2(1.0f, 1.0f);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (_playerObject == null)
            {
                _playerObject = GameObject.FindWithTag("Player");
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 thisPosition = transform.position;
            Vector3 playerPosition = _playerObject.transform.position;
            //FIXME: this is a hack.  Should properly raycast/confine to the player plane.
            Vector3 playerPlaneCameraPosition = new Vector3(thisPosition.x, thisPosition.y, playerPosition.z);
            Vector3 planeCorrectionVector = playerPlaneCameraPosition - thisPosition;
            
            Vector3 diff = playerPosition - thisPosition; // this gives the vector in R^3 to the destination.

            if (Math.Abs(diff.x) > MovementBounds.x || Math.Abs(diff.y) > MovementBounds.y)
            {
                diff -= planeCorrectionVector;

                float distance = diff.magnitude;
                diff.Normalize();

                Vector3 finalShift = diff * Math.Min(distance, MovementSpeed * Time.deltaTime);

                transform.position = thisPosition + finalShift;
            }
        }
    }
}