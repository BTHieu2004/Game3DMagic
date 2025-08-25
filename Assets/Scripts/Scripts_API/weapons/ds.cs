using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ds : MonoBehaviour
{
    [Serializable]
    public class inputweapons
    {
        public bool isSuccess;
        public string notification;
        public List<weapons> data;
    }
    public class weapons
    {
        public int id;
        public string name;
        public int damge;
        public bool isDeleted;
    }
}
