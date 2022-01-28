using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lcities 
{
    public class RoadHelper : MonoBehaviour
    {
        public GameObject roadStraight, roadCorner, road3way, road4way, roadEnd;
        Dictionary<Vector3Int, GameObject> roadDictionary = new Dictionary<Vector3Int, GameObject>();
        HashSet<Vector3Int> fixedRoadCandidates = new HashSet<Vector3Int>();

        public List<Vector3Int> GetRoadPositions()
        {
            return roadDictionary.Keys.ToList();
        }

        public void PlaceStreetPositions(Vector3 startPosition, Vector3Int direction, int length)
        {
            var rotation = Quaternion.identity;
            if (direction.x == 0)
            {
                rotation = Quaternion.Euler(0, 90, 0);
            }
            for (int i = 0; i < length; i++)
            {
                var position = Vector3Int.RoundToInt(startPosition + direction * i);
                if (roadDictionary.ContainsKey(position))
                {
                    continue;
                }
                var road = Instantiate(roadStraight, position, rotation, transform);
                roadDictionary.Add(position, road);
                if (i == 0 || i == length - 1)
                {
                    fixedRoadCandidates.Add(position);
                }
            }
        }

        public void FixRoad()
        {
            foreach (var position in fixedRoadCandidates)
            {
                List<Direction> neighBorDirections = PlacementHelper.FindNeighbor(position, roadDictionary.Keys);

                Quaternion rotation = Quaternion.identity;

                if(neighBorDirections.Count == 1)
                {
                    //Destroy Straight Road and Place Road End
                    Destroy(roadDictionary[position]);

                    if (neighBorDirections.Contains(Direction.Down))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (neighBorDirections.Contains(Direction.Left))
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    }
                    if (neighBorDirections.Contains(Direction.Up))
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }
                    roadDictionary[position] = Instantiate(roadEnd, position, rotation, transform);
                }
                else if(neighBorDirections.Count == 2)
                {
                    if (
                        neighBorDirections.Contains(Direction.Up) && neighBorDirections.Contains(Direction.Down)
                        || neighBorDirections.Contains(Direction.Right) && neighBorDirections.Contains(Direction.Left)
                       )
                    {
                        //Keep straight
                        continue;
                    }

                    Destroy(roadDictionary[position]);

                    if (neighBorDirections.Contains(Direction.Up) && neighBorDirections.Contains(Direction.Right))
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }
                    else if (neighBorDirections.Contains(Direction.Down) && neighBorDirections.Contains(Direction.Right))
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (neighBorDirections.Contains(Direction.Down) && neighBorDirections.Contains(Direction.Left))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }

                    roadDictionary[position] = Instantiate(roadCorner, position, rotation, transform);
                }
                else if (neighBorDirections.Count == 3)
                {
                    Destroy(roadDictionary[position]);

                    if (neighBorDirections.Contains(Direction.Right) && neighBorDirections.Contains(Direction.Left) && neighBorDirections.Contains(Direction.Down))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (neighBorDirections.Contains(Direction.Down) && neighBorDirections.Contains(Direction.Left) && neighBorDirections.Contains(Direction.Up))
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (neighBorDirections.Contains(Direction.Left) && neighBorDirections.Contains(Direction.Right) && neighBorDirections.Contains(Direction.Up))
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }

                    roadDictionary[position] = Instantiate(road3way, position, rotation, transform);
                }
                else
                {
                    Destroy(roadDictionary[position]);
                    roadDictionary[position] = Instantiate(road4way, position, rotation, transform);
                }
            }
        }
    }
}