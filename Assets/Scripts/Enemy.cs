using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering;
public class Enemy : MonoBehaviour
{
    public delegate void EnemyDeathHandler(); // Định nghĩa kiểu delegate cho sự kiện
    public event EnemyDeathHandler OnEnemyDeath; // Sự kiện khi enemy chết
    [SerializeField] private int healthbar = 0;    
    private NavMeshAgent agent;    
    [SerializeField] private Transform player;       
    private Vector3 vitri;
    private float distance;
    private Animator animator;
    private Collider cl;
    //private int time = 0;
    public ParticleSystem ptc;
    [SerializeField] private float AttackDistance;
    [SerializeField] private float MoveDistance;
    [SerializeField] private bool enm = false;
    [SerializeField] private GameObject Electric;
    //[SerializeField] private AudioSource audioDie;
    //[SerializeField] private AudioClip dieClip;
    //[SerializeField] private AudioClip Attack;
    private bool isAttacking = false;
    private diem Diem;    
    [SerializeField] private int diem;
    [SerializeField] private Slider Slider;
    private Quaternion rotate_face;
    private int hp_bd;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {        
        hp_bd = healthbar;
        Slider.value = healthbar;
        rotate_face = transform.rotation;
        Diem = FindObjectOfType<diem>();
        cl= GetComponent<Collider>();
        animator = GetComponent<Animator>();
        vitri = transform.position;
        agent = GetComponent<NavMeshAgent>();                
        distance = Vector3.Distance(transform.position, player.position);
        animator.SetTrigger("Idle");   
        //audioDie = GetComponent<AudioSource>();
        ptc.Stop();
    }
    
    void Update()
    {                           
        if (healthbar == 0)
        {
            gameObject.tag = "Potal";
            if (!ptc.isPlaying)
            {
                if (OnEnemyDeath != null)
                {
                    OnEnemyDeath?.Invoke();
                }
                //audioDie.PlayOneShot(dieClip);
                _audioManager.PlaySFX(_audioManager.die);
                Diem.tong(diem);
                ptc.Play();
                agent.isStopped = true;
            }                
                animator.SetBool("die", true);                
                cl.enabled = false;
                Destroy(gameObject, 5f);            
            return;            
        }
        if (healthbar > 0) 
        {                        
            distance = Vector3.Distance(transform.position, player.position);
            if (distance < MoveDistance)
            {
                //Fire.Action();
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetTrigger("walk");
                if (distance < AttackDistance)
                {
                    if (enm == false)
                    {
                        agent.isStopped = true;
                        animator.SetTrigger("attack");
                      //  audioDie.PlayOneShot(Attack);
                    }
                    else 
                    {
                        agent.isStopped = true;
                        transform.LookAt(player);
                        if (isAttacking == false && healthbar>0)
                        {
                            isAttacking = true;
                            
                            //  audioDie.PlayOneShot(Attack);
                            
                            StartCoroutine(InstantiateElectric());
                        }
                    }
                }
            }
            else
            {
                healthbar = hp_bd;
                agent.SetDestination(vitri);
                if (Vector3.Distance(transform.position, vitri) < 1f)
                {
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.rotation = rotate_face;
                    agent.isStopped = true;
                    animator.SetTrigger("Idle");
                    //Fire.reset();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kiem")||other.CompareTag("Potal"))
        {
            if (healthbar > 0)
            {
                healthbar -= 1;
                Slider.value = healthbar;
                animator.SetTrigger("Gethit");                
                if (other.CompareTag("Potal"))
                {
                    Destroy(other.gameObject);
                }
                //Destroy(other.gameObject);
            }            
        }       
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Kiem")
    //    {                          
    //    }
    //}
    private IEnumerator InstantiateElectric()
    {             
        yield return new WaitForSeconds(2f);        
        animator.SetTrigger("attack");
        //GameObject elec= Instantiate(Electric,transform.position, Quaternion.LookRotation(transform.forward));                                                                             
        GameObject elec = Instantiate(Electric, transform.position, transform.rotation);
        Rigidbody rbc = elec.GetComponent<Rigidbody>();
        elec.GetComponent<Collider>();
        Vector3 forcedirection = transform.forward + new Vector3(0, 0.1f, 0);
        if (rbc != null)
        {
            rbc.AddForce(forcedirection * 300, ForceMode.Acceleration);
        }
        _audioManager.PlaySFX(_audioManager.Electric);
        Destroy(elec, 4f);
        isAttacking = false;
    }
}
