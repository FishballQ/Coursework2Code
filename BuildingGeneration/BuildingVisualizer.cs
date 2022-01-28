using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lcities.SimpleVisualizer;

namespace Lcities
{
    public class BuildingVisualizer : MonoBehaviour
    {
        public LSystemGenerator lsystem;
        public GameObject floor;
        List<Vector3> positions = new List<Vector3>();
        public List<Vector3> nodes = new List<Vector3>();
        public HouseHelper houseHelper;

        public int length = 8;
        public float angle;
        public Vector3 currentPosition = Vector3.zero;
        private float turn;

        public int Length
        {
            get
            {
                if (length > 0)
                {
                    return length;
                }
                else
                {
                    return 1;
                }
            }
            set => length = value;
        }

        private void Start()
        {
            var sequence = lsystem.GenerateSentance();
            VisualizeSequence(sequence);
        }

        private void VisualizeSequence(string sequence)
        {
            Stack<AgentParameters> savePoints = new Stack<AgentParameters>();
            // var currentPosition = Vector3.zero;
            Vector3 direction = Vector3.forward;
            Vector3 tempPosition = Vector3.zero;


            foreach (var letter in sequence)
            {
                positions.Add(currentPosition);
                EncodingLetters encoding = (EncodingLetters)letter;
                switch (encoding)
                {
                    case EncodingLetters.save:
                        savePoints.Push(new AgentParameters
                        {
                            position = currentPosition,
                            direction = direction,
                            length = Length
                        });
                        break;

                    case EncodingLetters.load:
                        if (savePoints.Count > 0)
                        {
                            var agentParameter = savePoints.Pop();
                            currentPosition = agentParameter.position;
                            direction = agentParameter.direction;
                            Length = agentParameter.length;
                        }

                        else
                        {
                            throw new System.Exception("Don't have saved point in our stack");
                        }
                        break;

                    case EncodingLetters.draw:
                        tempPosition = currentPosition;
                        currentPosition += direction * length;
                        int floorT = floor.GetComponent<Floor>().floor;
                        var nodePosition = currentPosition + new Vector3(0,1.65f*floorT,0);
                        nodes.Add(nodePosition);
                        // Debug.Log("floor:" + floorT.ToString());
                        houseHelper.PlaceHousePositions(currentPosition, Vector3Int.RoundToInt(direction), length, turn);
                        turn = 0;
                        break;

                    case EncodingLetters.turnRight:
                        turn = angle;
                        direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                        break;

                    case EncodingLetters.turnLeft:
                        turn = -angle;
                        direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
