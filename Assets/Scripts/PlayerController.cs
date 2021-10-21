using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;
using System;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    //variables
    public float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private bool isRunning;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float animSpeed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minCamVeiw, maxCamVeiw;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private Vector3 velocity;
    public Camera cam;
    private float xRot;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject FPCam;
    [SerializeField] private GameObject TPCam;
    [SerializeField] private float jumpHeeight;
    //references
    public CharacterController controller;
    public AudioSource s;
    public Slider stamina;
    public TMP_Text staminaText;

    private Animator anim;
    private float animrunSpeed;
    public InventoryItemm inventory;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        FPCam.SetActive(false);
        TPCam.SetActive(true);
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        stamina.gameObject.SetActive(false);
        staminaText.gameObject.SetActive(false);
        staminaText.text = runTime.ToString();
    }

    private void Update()
    {
        if (runTime <= 0)
        {
             runTime += Time.deltaTime;
             if (runTime > 0)
             {
                runTime = 8;
             }
        }
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, minCamVeiw, maxCamVeiw);
        if(runTime > 0)
        {
          cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
          transform.Rotate(Vector3.up, mouseX);
        }
        isRunning = Input.GetKey(KeyCode.LeftShift);
        
        if(!isRunning)
        {
            staminaText.gameObject.SetActive(false);
            stamina.gameObject.SetActive(false);
        }
        else
        {
            staminaText.gameObject.SetActive(true);
            stamina.gameObject.SetActive(true);
        }
        Move();

        CamToggle();


    }

    private void FixedUpdate()
    {
        if(runTime > 0)
        {
          float moveX = Input.GetAxis("Horizontal");
          float moveZ = Input.GetAxis("Vertical");
          moveDirection = transform.TransformDirection(moveX / turnSpeed, 0, 0).normalized + transform.TransformVector(0, 0, controller.velocity.z + moveZ).normalized;

        }
       

    }
    private void OnLevelWasLoaded(int level)
    {
        FindSpawn();
    }


    void FindSpawn()
    {
        transform.position = GameObject.FindWithTag("Spawns").transform.position;
        transform.rotation = GameObject.FindWithTag("Spawns").transform.rotation;
    }

    private void CamToggle()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FPCam.SetActive(true);
            TPCam.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TPCam.SetActive(true);
            FPCam.SetActive(false);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        var amount = 1;
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, amount);
            if (item.item.type == ItemType.Gold)
            {
                GoldCounter.instance.CoinCoollect();
                amount = GoldCounter.instance.currency;
                inventory.AddItem(item.item, amount);
                return;
            }
            item.gameObject.SetActive(false);
            //  inventory.AddItem(item.item, amount);
        }

    }
    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }

    private void Move()
    {

        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        if(runTime > 0)
        {
         controller.Move(moveDirection * Time.deltaTime);
        }
       

        anim.SetFloat("Speed", moveDirection == Vector3.zero ? 0f : isRunning ? runSpeed : walkSpeed, animSpeed, Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Rotate(moveDirection);
        }



        if (isGrounded)
        {

            if (moveDirection != Vector3.zero && !isRunning && runTime > 0)
            //walk
            {

                   Walk();
                 
            }
            else if (moveDirection != Vector3.zero && isRunning && runTime > 0)
            //run
            {
                Run();
                stamina.value = runTime;
                staminaText.text = stamina.value.ToString("0");
            }
            else if (moveDirection == Vector3.zero && isRunning == false)
            //idle
            {
                Idle();
            }
           moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

        }
        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    void Rotate(Vector3 moveDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        transform.rotation = newRotation;
    }

    private void Idle()
    {
        anim.SetFloat("Speed", .333f);//, .1f, Time.deltaTime);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        if(animrunSpeed < 0.6f || animrunSpeed > 0.7f)
        {
            animrunSpeed = Mathf.Lerp(animrunSpeed, 0.666f, 1f);
        }

        anim.SetFloat("Speed", animrunSpeed);//, .1f, Time.deltaTime);
    }
    
    float runTime = 8;
    private void Run()
    {
        if(runTime <= 0)
        {
            return;
        }
        runTime -= Time.deltaTime;
        moveSpeed = runSpeed;
        if (runTime > 0)
        {
            if (animrunSpeed < 0.9f || animrunSpeed > 1f)
            {
                animrunSpeed = Mathf.Lerp(animrunSpeed, 1f, 1f);
                anim.SetFloat("Speed", animrunSpeed);
            }

        }
        else
        {
            anim.SetTrigger("RunTime");
            s.Play();
            moveSpeed = 0;
            runTime = -10;
            
        }
    }

 

    private void Jump()
    {
        if(runTime > 0)
        {
          velocity.y = Mathf.Sqrt(jumpHeeight * -2 * gravity);
          anim.SetTrigger("Jump");

        }
      
    }

}



