using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lcities 
{
  public class HouseHelper : MonoBehaviour
  {
    public GameObject wall;
    public int angle = 90;
    private float rotationT = 90;
    Dictionary<Vector3Int, GameObject> wallDictionary = new Dictionary<Vector3Int, GameObject>();
    HashSet<Vector3Int> fixedWallCandidates = new HashSet<Vector3Int>();
    
    void Start()
    {
        rotationT = 90;
    }
    
    public List<Vector3Int> GetWallPositions()
        {
            return wallDictionary.Keys.ToList();
        }

    public void PlaceHousePositions(Vector3 startPosition, Vector3Int direction, int length, float rotate)
        {
            var rotation = Quaternion.identity;
            rotationT += rotate;
            for (int i = 0; i < length; i++)
            {
                var position = Vector3Int.RoundToInt(startPosition);
                if (wallDictionary.ContainsKey(position))
                {
                    continue;
                }
                var _wall = Instantiate(wall, transform);
                _wall.transform.localPosition = position;
                _wall.transform.localRotation = Quaternion.AngleAxis(rotationT, Vector3.up);
                Debug.Log("wall rotation:" + rotationT.ToString());
                // var _wall = Instantiate(wall, position, rotation, transform);
                wallDictionary.Add(position, _wall);
                if (i == 0 || i == length - 1)
                {
                    fixedWallCandidates.Add(position);
                }
            }
        }

        public void FixWall()
        {
            foreach (var position in fixedWallCandidates)
            {
                List<Direction> neighBorDirections = PlacementHelper.FindNeighbor(position, wallDictionary.Keys);
                Quaternion rotation = Quaternion.identity;
            }
        }

    }
}
