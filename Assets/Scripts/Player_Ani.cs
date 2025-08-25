using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player_Ani : StateMachineBehaviour
{
    private GameObject obj;
    [SerializeField] private GameObject _gameObject;    
    private int random;
    private List<GameObject> list = new List<GameObject>();
    private AudioManager _audioManager;
    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public override void OnStateEnter(Animator animator,AnimatorStateInfo stateInfo, int Layer)
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject go in enemy) 
        {
            if (go != null)
            {
                GameObject _player = GameObject.FindGameObjectWithTag("Player");
                if (Vector3.Distance(_player.transform.position, go.transform.position) <= 15)
                {
                    list.Add(go);
                }
            }
        }
        if (list.Count>0)
        {
            random = Random.Range(0, list.Count);
            Vector3 spawm = list[random].transform.position + list[random].transform.up * 0f;
            obj = Instantiate(_gameObject, spawm, Quaternion.identity);
            _audioManager.PlaySFX(_audioManager.potal);
        }
    }
    public override void OnStateExit(Animator animator,AnimatorStateInfo stateInfo,int Layer)
    {
        if (list.Count>0)
        {
            Destroy(obj);
        }
        list.Clear();
    }
}
