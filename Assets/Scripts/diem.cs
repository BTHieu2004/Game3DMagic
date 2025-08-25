using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class diem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TextMeshPro;
    [SerializeField] private TextMeshProUGUI diemhientai;
    [SerializeField] private TextMeshProUGUI _diem;

    private int diemvao = 0;    
    private void Start()
    {        
        _TextMeshPro.text = "Điểm cao nhât : " + PlayerPrefs.GetInt("kiluc");
        if (PlayerPrefs.HasKey("kiluc"))
        {
        }
        else { PlayerPrefs.SetInt("kiluc",0); }
    }
    public void tong(int diem)
    {
        diemvao += diem;                
        _diem.text = "Điểm : "+diemvao;
        diemhientai.text = "Điểm : " + diemvao;        
        if (PlayerPrefs.GetInt("kiluc")<diemvao)
        {            
            PlayerPrefs.SetInt("kiluc",diemvao);
        }
        _TextMeshPro.text = "Điểm cao nhât : " + PlayerPrefs.GetInt("kiluc");
    }
}
