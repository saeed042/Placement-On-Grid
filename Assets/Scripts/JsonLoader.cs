using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridPlacement
{
    public class JsonLoader : MonoBehaviour
    {
        [SerializeField] TextAsset _json;

        List<List<TerrainTile>> terrainGridTiles;
        // Start is called before the first frame update
        

        public List<List<TerrainTile>> LoadDataFromJSON()
        {


            string jsonText = _json.text;
            

            dynamic jsonData = JsonConvert.DeserializeObject(jsonText);


            terrainGridTiles = new List<List<TerrainTile>>();

            // Step 5: Iterate through the JSON data and populate the grid
            foreach (var row in jsonData.TerrainGrid)
            {
                List<TerrainTile> gridRow = new List<TerrainTile>();
                foreach (var tileData in row)
                {
                    TerrainTile tile = new TerrainTile();
                    tile.TileType = tileData.TileType;
                    gridRow.Add(tile);
                }
                terrainGridTiles.Add(gridRow);
            }


            return terrainGridTiles;




            
        }
    }

    public class TerrainTile
    {
        public int TileType { get; set; }
    }

    [SerializeField]
    public class TerrainGrid
    {
        public List<List<TerrainTile>> Grid { get; set; }
    }
}

