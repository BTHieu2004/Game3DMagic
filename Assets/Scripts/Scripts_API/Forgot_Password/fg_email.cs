using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fg_email : MonoBehaviour
{
    [Serializable]
    public class ipem
    {
        public string email;
        public ipem (string email)
        {
            this.email = email;
        }
    }
    [Serializable]
    public class kt
    {
        public bool isSuccess;
        public string notification;
    }
}
