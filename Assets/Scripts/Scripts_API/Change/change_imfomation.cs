using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_imfomation : MonoBehaviour
{
    [Serializable]
    public class chg
    {        
        public string userId;
        public string name;
        public int regionId;
        public chg(string userId, string name, int regionId)
        {
            this.userId = userId;
            this.name = name;
            this.regionId = regionId;
        }
    }
    [Serializable]
    public class thongbao
    {
        public bool isSuccess;
        public string notification;
    }
}
