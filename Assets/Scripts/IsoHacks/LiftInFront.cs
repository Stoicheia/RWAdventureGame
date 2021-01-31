using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsoHacks
{
    // Lift in front is used to lift an isometric object to a clear foreground layer to prevent it having incorrect draworder when something passes behind.
    //
    // it should be attached to a collider that is a child to a sprite-renderer.  If the collider emits a Collision event, it'll raise the render order.
    public class LiftInFront : MonoBehaviour
    {
        private int _startLayer = 5;
        public int sortLift = 10;
        private SpriteRenderer _ourSprite;

        private ISet<SpriteRenderer> _overlapping;
        
        private void Awake()
        {
            _overlapping = new HashSet<SpriteRenderer>();
        }

        private static ISet<SpriteRenderer> overlapsWith(ISet<SpriteRenderer> renderers, SpriteRenderer root)
        {
            ISet<SpriteRenderer> returnSet = new HashSet<SpriteRenderer>();
            int sortOrder = root.sortingOrder;
            int sortLayer = root.sortingLayerID;

            Vector3 rootPosition = root.transform.position;

            ISet<SpriteRenderer> remainingRenderers = new HashSet<SpriteRenderer>(renderers);
            foreach (SpriteRenderer aRenderer in renderers)
            {
                if (aRenderer == root)
                {
                    remainingRenderers.Remove(aRenderer);
                    continue;
                }
                // not in our layer? doesn't matter.
                if (aRenderer.sortingLayerID != sortLayer)
                {
                    remainingRenderers.Remove(aRenderer);
                    continue;
                }
                // eliminate objects that would render behind normally - they won't need elevation.
                if (aRenderer.sortingOrder < sortOrder)
                {
                    remainingRenderers.Remove(aRenderer);
                    continue;
                }
                if (aRenderer.transform.position.y > rootPosition.y)
                {
                    remainingRenderers.Remove(aRenderer);
                    continue;
                }
                // overlaps our bounding box?
                if (root.bounds.Intersects(aRenderer.bounds))
                {
                    returnSet.Add(aRenderer);
                    remainingRenderers.Remove(aRenderer);
                    ISet<SpriteRenderer> overlappingChildren = overlapsWith(remainingRenderers, aRenderer);
                    returnSet.UnionWith(overlappingChildren);
                    remainingRenderers.ExceptWith(overlappingChildren);
                }
            }
            return returnSet;
        }

        // Start is called before the first frame update
        void Start()
        {
            _ourSprite = GetComponentInParent<SpriteRenderer>();
            _startLayer = _ourSprite.sortingOrder;

            Bounds ourBounds = _ourSprite.bounds;

            SpriteRenderer[] allRenderers = GameObject.FindObjectsOfType<SpriteRenderer>();
            ISet<SpriteRenderer> allRenderersSet = new HashSet<SpriteRenderer>(allRenderers);
            _overlapping.UnionWith(overlapsWith(allRenderersSet, _ourSprite));
            Debug.LogFormat(this, "Found {0} objects in front", _overlapping.Count);
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void LiftOverlapping()
        {
            _ourSprite.sortingOrder += sortLift;
            foreach (SpriteRenderer overlappedRenderer in _overlapping)
            {
                overlappedRenderer.sortingOrder += sortLift;
            }
        }

        public void LowerOverlapping()
        {
            _ourSprite.sortingOrder -= sortLift;
            foreach (SpriteRenderer overlappedRenderer in _overlapping)
            {
                overlappedRenderer.sortingOrder -= sortLift;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Lifting Objects", this);
            LiftOverlapping();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Lowering Objects", this);
            LowerOverlapping();
        }
    }
}