using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    public class TileLookup : MonoBehaviour
    {
        private Grid _grid;
        private Tilemap _tilemap;
        
        private void Awake()
        {
        }

        private void Start()
        {
            _grid = GetComponentInParent<Grid>();
            _tilemap = GetComponent<Tilemap>();
        }

        public TileBase FindTileAtWorldLocation(Vector3 location)
        {
            Vector3Int gridLoc = _grid.WorldToCell(location);
            return _tilemap.GetTile(gridLoc);
        }
    }
}