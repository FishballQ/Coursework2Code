﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lcities
{
    [Serializable]
    public class BuildingTypes
    { 
        [SerializeField]
        private GameObject[] prefabs;
        public int sizeRequired;
        public int quantity;
        public int quantityAlreadyPlaced;

        public GameObject GetPrefab()
        {
            quantityAlreadyPlaced++;
            if (prefabs.Length > 1)
            {
                var random = UnityEngine.Random.Range(0, prefabs.Length);
                return prefabs[random];
            }
            return prefabs[0];
        }

        public bool IsBuildingAvailable()
        {
            return quantityAlreadyPlaced < quantity;
        }

        public void Reset()
        {
            quantityAlreadyPlaced = 0;
        }
    }
}