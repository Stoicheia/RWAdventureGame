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

        private bool _handledClick;

        [SerializeField] private Transform _navigationIcon;
        private Transform _activeNavigationIcon;

        [SerializeField] public float navIconDismissDistance;

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
            if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                _navMeshAgent.remainingDistance < navIconDismissDistance && _activeNavigationIcon)
            {
                HideNavIcon();
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
                        _navMeshAgent.SetPath(path);
                        SpawnNavIcon(worldPoint);
                        OnMove?.Invoke(transform);
                    }
                    else
                    {
                        Debug.LogFormat("No path to {0} found.", worldPoint);
                        // should trigger the can't walk there action.

                        INavigationFailedEvent[] allComponents = GetComponentsInChildren<INavigationFailedEvent>();
                        foreach (INavigationFailedEvent comp in allComponents)
                        {
                            comp.OnNavigationFailed();
                        }
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