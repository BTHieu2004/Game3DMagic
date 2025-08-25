using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scripts : MonoBehaviour
{
    [Serializable]
    public class ResponseAPI
    {
        public bool isSuccess;
        public string notfication;
        public List<GameLevels> data;
    }
    [Serializable]
    public class GameLevels
    {
        public int levelId;
        public string title;
        public string description;
    }
}
