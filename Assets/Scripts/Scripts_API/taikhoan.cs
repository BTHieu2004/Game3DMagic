using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taikhoan : MonoBehaviour
{
    [Serializable]
    public class tk
    {
        public bool isSucess;
        public string notfication;
        public data data;
    }
    public class data
    {
        public string name;
        public int regionId;
        public string avata;
        public bool isdeleted;
        public string id;
    }
}
