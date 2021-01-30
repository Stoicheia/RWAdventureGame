using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Player
{
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMoveController : MonoBehaviour
    {

        public delegate void NavigationEvent(Transform t, Vector3 worldPosition);

        // invoked when we start navigation to a destination.
        public event NavigationEvent OnNavigationStarted;

        // invoked when navigation failed to path-find.  The object that failed to navigate and the position requested are passed.
        public event NavigationEvent OnNavigationFailed;

        // invoked when navigation to a given destination has finished
        public event NavigationEvent OnNavigationArrived;

        private GameObject _playerObject;
        private Camera _gameCamera;
        private NavMeshAgent _navMeshAgent;

        private bool _handledClick;

        [SerializeField] private Transform _navigationIcon;
        private Transform _activeNavigationIcon;

        [MinAttribute(0.0f)] public float navIconDismissDistance;

        private bool enRoute;

        public bool EnRoute
        {
            get => enRoute;
        }

        private void Awake()
        {
            navIconDismissDistance = 0.2f;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _handledClick = false;
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
            if (enRoute &&
                _navMeshAgent.remainingDistance < navIconDismissDistance)
            {
                OnNavigationArrived?.Invoke(transform, _navMeshAgent.pathEndPosition);
                if (_activeNavigationIcon)
                    HideNavIcon();
                enRoute = false;
            }

            // don't process input system events if the pointer is pointing at a discrete gameobject.
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!_handledClick)
                {
                    Vector3 worldPoint = _gameCamera.ScreenToWorldPoint(Input.mousePosition);

                    // snap the point to the game plane.
                    worldPoint.z = 0.0f;

                    // try to solve the path.
                    NavMeshPath path = new NavMeshPath();
                    _navMeshAgent.CalculatePath(worldPoint, path);
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        enRoute = true;
                        _navMeshAgent.SetPath(path);
                        SpawnNavIcon(worldPoint);
                        OnNavigationStarted?.Invoke(transform, worldPoint);
                    }
                    else
                    {
                        Debug.LogFormat("No path to {0} found.", worldPoint);
                        OnNavigationFailed?.Invoke(transform, worldPoint);
                    }

                    _handledClick = true;
                }
            }
            else
            {
                if (_handledClick)
                    _handledClick = false;
            }
        }

        void SpawnNavIcon(Vector3 t)
        {
            HideNavIcon();
            Transform nav = Instantiate(_navigationIcon, t, Quaternion.identity);
            _activeNavigationIcon = nav;
        }

        void HideNavIcon()
        {
            if (_activeNavigationIcon)
            {
                Destroy(_activeNavigationIcon.gameObject);
                _activeNavigationIcon = null;
            }
        }
    }
}