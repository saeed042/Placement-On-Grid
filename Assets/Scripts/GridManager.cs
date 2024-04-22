using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GridPlacement
{
    
    public class GridManager : MonoBehaviour
    {
        public GridTile TilePrefab;
        public GameObject HorizontalTable;
        public GameObject VerticalTable;
        public Sprite[] sprites;
        [SerializeField] JsonLoader loader;

        List<List<TerrainTile>> terrainGridTiles;
        private GridTile[,] grid;
        
        // Start is called before the first frame update
        void Start()
        {
            terrainGridTiles = loader.LoadDataFromJSON();
            GenerateGrid();
        }

        //// Update is called once per frame
        //void Update()
        //{

        //}

        void GenerateGrid()
        {
            grid = new GridTile[terrainGridTiles.Count, terrainGridTiles[0].Count];

            for (int x = 0; x < terrainGridTiles.Count; x++)
            {
                for (int y = 0; y < terrainGridTiles[x].Count; y++)
                {
                    var tile = Instantiate(TilePrefab, new Vector2(x, y), Quaternion.identity);


                    tile.X = x;
                    tile.Y = y;
                    //tile.TileType = terrainGridTiles[x][y].TileType;
                    tile.Tile_Type = (TileType)terrainGridTiles[x][y].TileType;
                    tile.SpriteRenderer.sprite = sprites[terrainGridTiles[x][y].TileType];
                    tile.IsAvailable = true;

                    if ((TileType)terrainGridTiles[x][y].TileType == TileType.Wood)
                        tile.OnClick += GetNeighbors;

                    tile.transform.parent = this.transform;

                    grid[x, y] = tile;
                    
                }
            }
        }

        private void GetNeighbors(int x, int y)
        {
            //List<Node> neighbors = new List<Node>();
            //int x = tile.X;
            //int y = tile.Y;

            // Check the four cardinal directions (up, down, left, right)
            // Add neighbors only if they are within the grid bounds and are walkable

            // Up
            if (IsWithinGridBounds(x, y + 1) && grid[x, y + 1].IsAvailable && grid[x, y + 1].Tile_Type == TileType.Wood)
            {

                Instantiate(VerticalTable, new Vector2(x, y + 1), Quaternion.identity);
                grid[x, y + 1].IsAvailable = false;
                grid[x, y].IsAvailable = false;
                Debug.Log("UP is wood..... ");
                return;
            }

            // Down
            if (IsWithinGridBounds(x, y - 1) && grid[x, y - 1].IsAvailable && grid[x, y - 1].Tile_Type == TileType.Wood)
            {
                Instantiate(VerticalTable, new Vector2(x, y), Quaternion.identity);
                grid[x, y - 1].IsAvailable = false;
                grid[x, y].IsAvailable = false;
                Debug.Log("Down is wood..... ");
                return;
            }

            // Left
            if (IsWithinGridBounds(x - 1, y) && grid[x - 1, y].IsAvailable && grid[x - 1, y].Tile_Type == TileType.Wood)
            {
                Instantiate(HorizontalTable, new Vector2(x - 1, y), Quaternion.identity);
                grid[x - 1, y].IsAvailable = false;
                grid[x, y].IsAvailable = false;
                Debug.Log("Lef is wood..... ");
                return;
            }

            // Right
            if (IsWithinGridBounds(x + 1, y) && grid[x + 1, y].IsAvailable && grid[x + 1, y].Tile_Type == TileType.Wood)
            {
                Instantiate(HorizontalTable, new Vector2(x, y), Quaternion.identity);
                grid[x + 1, y].IsAvailable = false;
                grid[x, y].IsAvailable = false;
                Debug.Log("Right is wood..... ");
                return;
            }


        }

        private bool IsWithinGridBounds(int x, int y)
        {
            int maxX = grid.GetLength(0);
            int maxY = grid.GetLength(1);
            return x >= 0 && x < maxX && y >= 0 && y < maxY;
        }


        public void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public enum TileType { Dirt, Grass, Stone, Wood }
}

