using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input_OTP : MonoBehaviour
{
    [Serializable]
    public class ipOTP
    {
        public int OTP;
        public string newPassword;
        public string Email;
        public ipOTP(int otp, string password,string em)
        {
            this.OTP = otp;
            this.newPassword = password;
            this.Email = em;
        }
    }
    [Serializable]   
    public class htt
    {
        public bool isSuccess;
        public string notification;
    }
}
