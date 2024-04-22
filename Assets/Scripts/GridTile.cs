using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridPlacement 
{
    public class GridTile : MonoBehaviour
    {
        
        public TileType Tile_Type;
        public int X;
        public int Y;

        public SpriteRenderer SpriteRenderer;

        public bool IsAvailable;

        public Action<int, int> OnClick;


        private void OnMouseDown()
        {
            if (Tile_Type == TileType.Wood && IsAvailable)
            {
                //Debug.Log($"Tile SPrite name.... {SpriteRenderer.sprite.name}");
                OnClick?.Invoke(X, Y);
            }
        }
    }
}

