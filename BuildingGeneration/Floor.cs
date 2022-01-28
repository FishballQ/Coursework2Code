using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject building;

    public int floor = 3;
    private int floorAlready = 1;

    void Start()
    {
        // floor = Random.Range(1, 7);
        Invoke("Update", 0.01f);
    }

    void Update()
    {
        if (floorAlready < floor){
            Vector3 floorHeight = new Vector3(0, 1.65f * floorAlready, 0);
            GameObject Instance = Instantiate(building, transform.position + floorHeight, transform.localRotation, transform);
            floorAlready ++;
        }
    }
}
