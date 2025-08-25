using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Region : MonoBehaviour
{
    [Serializable]
    public class Response
    {
        public bool isSuccess;
        public string notfication;
        public List<RegionData> data;
    }
    [Serializable]
    public class RegionData
    {
        public int regionId;
        public string name;
    }
}
