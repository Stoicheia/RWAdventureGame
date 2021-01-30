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

        private bool _handledClick;

        private void Awake()
        {
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
            // don't process input system events if the pointer is pointing at a discrete gameobject.
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
                    }
                    else
                    {
                        Debug.Log(String.Format("No path to {0} found.", worldPoint));
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
    }
}