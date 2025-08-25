using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapondata : MonoBehaviour
{
    [Serializable]
    public class Weapondatas
    {
        public string tenvk;
        public Weapondatas(string name)
        {
            tenvk = name;
        }
    }
    [Serializable]
    public class tbx
    {
        public bool isSuccess;
        public string notification;
        public wap data;
    }
    public class wap
    {
        public int id;
    }
}
