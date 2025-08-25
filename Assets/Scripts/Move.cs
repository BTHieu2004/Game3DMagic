using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;    
    [SerializeField] private Transform _camera;
    [SerializeField] private float lookSensitivity = 0.1f;
    private Rigidbody rb;
    private Animator animator;
    private Vector2 move;   
    private Vector2 look;
    private Vector2 click;
    private float xRotation = 0f;//private float yRotation = 0f;
    private int hp = 40;
    public Slider healthbar;
    [SerializeField] private GameObject Electric;
    [SerializeField] private GameObject Potal;
    [SerializeField] private GameObject position_Instan;
    private GameObject elec;
    private bool isUIOpen = false;
    public GameObject pn;
    AudioManager _audioManager;
    [SerializeField] private GameObject die;
    // Start is called before the first frame update
    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        healthbar.maxValue = hp;
        healthbar.value = hp;
        rb= GetComponent<Rigidbody>();        
        animator = GetComponent<Animator>();              
        Cursor.lockState = CursorLockMode.Locked; //khóa trỏ chuột giữa mh        
    }

    // Update is called once per frame
    void Update()
    {                        
         if(hp <= 0)
         {             
             animator.SetTrigger("die");
             StartCoroutine(cd());            
             return;
         }                     
        if (move.y != 0)
        {            
            MovePlayer();
        }
        else
        {            
            animator.SetBool("chay",false);            
        }                
            RotateView();        
    }
    public void OnEsc(InputValue inputValue)
    {
        isUIOpen =true;
        pn.SetActive(true);
        Cursor.lockState = CursorLockMode.None; //mở khóa
        Time.timeScale = 0f;

    }
    public void OnDongchuot(InputValue inputValue)
    {
        isUIOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MovePlayer()        
    {        
        rb.AddRelativeForce(Vector3.forward * move.y * speed);        
        animator.SetBool("chay",true );
        
    } 
    private void RotateView()
    {
        if (isUIOpen == true) return;
        float mouseX = look.x * lookSensitivity;
        transform.Rotate(Vector3.up * mouseX);                        
        float mouseY = look.y * lookSensitivity;
        xRotation -=mouseY;       
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);
        _camera.localRotation=Quaternion.Euler(xRotation,_camera.localRotation.eulerAngles.y,0);             
    }
    public void OnMove(InputValue inputValue)
    {        
        move = inputValue.Get<Vector2>();
    }
    public void OnLook(InputValue inputValue)
    {
        look = inputValue.Get<Vector2>();
    }
    public void OnSkils(InputValue inputValue)
    {        
        bool Qclick = inputValue.isPressed;
        if (Qclick)
        {           
            animator.SetTrigger("attack2");            
        }
    }
    public void OnRightclick(InputValue inputValue)
    {            
        bool rightclick = inputValue.isPressed;
        if (rightclick)
        {
            animator.SetTrigger("attack");
            elec = Instantiate(Electric,position_Instan.transform.position ,transform.rotation);
            Rigidbody rbc = elec.GetComponent<Rigidbody>();            
            Vector3 forcedirection = transform.forward;
            rbc.AddForce(forcedirection * 400, ForceMode.Acceleration);
            _audioManager.PlaySFX(_audioManager.zzzzz);
            Destroy(elec, 4f);
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Electric")
        {
            hp -= 5;
            healthbar.value = hp;
            animator.SetTrigger("Gethit");

        }
    }
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == "enemy")
        {
            animator.SetTrigger("Gethit");
            hp -= 3; healthbar.value = hp;                        
        }       
    }
    IEnumerator cd()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        die.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }    
}
