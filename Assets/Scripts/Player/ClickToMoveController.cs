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
        public delegate void MoveAction(Transform t);

        public static event MoveAction OnMove;
        
        private GameObject _playerObject;
        private Camera _gameCamera;
        private NavMeshAgent _navMeshAgent;

        [SerializeField] private Transform _navigationIcon;
        private Transform _activeNavigationIcon;

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
                SpawnNavIcon(worldPoint);
                OnMove?.Invoke(transform);
            }
        }

        void SpawnNavIcon(Vector3 t)
        {
            if(_activeNavigationIcon!=null)
                Destroy(_activeNavigationIcon.gameObject);
            Transform nav = Instantiate(_navigationIcon, t, Quaternion.identity);
            _activeNavigationIcon = nav;
        }
    }
}