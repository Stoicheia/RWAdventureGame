using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Player
{
    public class ClickToMoveController : MonoBehaviour
    {
        private GameObject _playerObject;
        private Camera _gameCamera;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Start is called before the first frame update
        void Start()
        {
            if (_playerObject == null)
            {
                _playerObject = GameObject.FindWithTag("Player");
            }

            if (_gameCamera == null)
            {
                _gameCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 worldPoint = _gameCamera.ScreenToWorldPoint(Input.mousePosition);

                worldPoint.z = 0.0f;
                _navMeshAgent.destination = worldPoint;
            }
        }
    }
}