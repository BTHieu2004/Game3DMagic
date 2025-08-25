using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ditheo : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    private Animator animatorComponent;
    private int hpEnemy;
    private bool run = true;
    private string CurrentAni;
    private float lifeAtack1 = 5f;    
    private void Start()
    {
        animatorComponent = GetComponent<Animator>();
    }
    void Update()
    {
        agent.SetDestination(player.transform.position);
        dichuyen();
        StartCoroutine(attack());
    }
    private void dichuyen()
    {
        if (Vector3.Distance(transform.position,player.transform.position)>30&&run==true)
        {            
            ani("wound");
        }        
    }        
    IEnumerator attack() 
    {
        if (lifeAtack1 > 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 30 )
            {
                run = false;
                ani("atack1");
               // animatorComponent.SetTrigger("atack3");
                StartCoroutine(CountDown());
            }
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= 15)
        {
            run = false;
            StartCoroutine(NonLoopAni());
        }
        else
        {
            yield return null;
        }
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(5);
        lifeAtack1 = 0;
        run = true;
    }
    IEnumerator NonLoopAni()
    {
        ani("atack3");
        //animatorComponent.SetTrigger("atack3");
        var CurrentStateAni = animatorComponent.GetCurrentAnimatorStateInfo(0);
        //yield return new WaitForEndOfFrame();
        if (CurrentStateAni.IsName("atack3") == true)
        {
            var CurrentLifeTimeAni = CurrentStateAni.length;
            yield return new WaitForSeconds(CurrentLifeTimeAni);
            Debug.Log("tiem : " + CurrentLifeTimeAni);
            run = true;
        }       
    }
    public void TakeDamge(int dam)
    {
        hpEnemy -=dam;
        if (hpEnemy <= 0)
        {
            ani("death");
            Destroy(gameObject,5f);
        }
    }
    private void ani(string ani)
    {
        if (CurrentAni != ani)
        {
            CurrentAni = ani;
            animatorComponent.Play(CurrentAni);
        }
    }
}
