using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject player, center, left, right, up, down, camera,UICanvas,specialmenu;

    [Header("Audio")]
    public AudioClip[] impact;

    [Header("Color")]
    public string color = "black";

    [Header("MovementSettings")]
    [SerializeField]int step = 9;
    [SerializeField] float speed = 0.01f;
    [SerializeField] bool input = true;

    private Vector3  cameraPos;
    AudioSource audioSource;
    GameManager gameManager;
    private void Awake()
    {
        cameraPos = player.transform.position - camera.transform.position;
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Update() //küp hareketi kamera ayarlaması
    {
        if (Input.GetKey(KeyCode.UpArrow) && input == true)
        {
            StartCoroutine("moveUp");
          
            input = false;
        }
        if (Input.GetKey(KeyCode.DownArrow) && input == true)
        {
            StartCoroutine("moveDown");
            
            input = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && input == true)
        {
            StartCoroutine("moveLeft");
            
            input = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) && input == true)
        {
            StartCoroutine("moveRight");
         
            input = false;
        }
        if (player.transform.localPosition.x < 20.0f && player.transform.localPosition.x > -20.0f && player.transform.localPosition.z <20.0f && player.transform.localPosition.z > -20.0f)
        {
            camera.transform.position = player.transform.position - cameraPos;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        string tag = other.tag;
        switch(tag)
        {
            case "blue":
                {
                    if(color == "black")
                    {
                        color = "blue";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[6];
                    }
                    if(color == "red")
                    {
                        color = "purple";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[3];
                    }
                    if(color == "yellow")
                    {
                        color = "green";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[5];
                    }
                    if(color == "orange")
                    {
                        color = "white";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[1];
                    }
                    break;
                }
            case "red":
                {
                    if (color == "black")
                    {
                        color = "red";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[2];
                    }
                    if (color == "blue")
                    {
                        color = "purple";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[3];
                    }
                    if (color == "yellow")
                    {
                        color = "orange";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[4];
                    }
                    if(color=="green")
                    {
                        color = "white";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[1];
                    }
                    break;
                }
            case "yellow":
                {
                    if (color == "black")
                    {
                        color = "yellow";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[0];
                    }
                    if (color == "red")
                    {
                        color = "orange";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[4];
                    }
                    if (color == "blue")
                    {
                        color = "green";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[5];
                    }
                    if(color == "purple")
                    {
                        color = "white";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[1];
                    }
                    break;
                }
            case "black":
                {
                    color = "black";
                    player.GetComponent<Renderer>().material = gameManager.cubematerials[7];
                    break;
                }
            case "rainbow": //istedigin rengi secmee menusu
                {
                    UICanvas.SetActive(false);
                    specialmenu.SetActive(true);
                    StartCoroutine("rainbowIE");
                    break;
                }

        }
        Destroy(other);
    }
    IEnumerator moveUp()
    {
        audioSource.PlayOneShot(impact[Random.Range(0,impact.Length)], 0.7F);
        for (int i = 0; i<(90 / step); i++)
        {
            player.transform.RotateAround(up.transform.position,Vector3.right,step);
            yield return new WaitForSeconds(speed);
            
        }
        center.transform.position = player.transform.position;
        input = true;
    }
    IEnumerator moveDown()
    {
        audioSource.PlayOneShot(impact[Random.Range(0, impact.Length)], 0.7F);
        for (int i = 0; i < (90 / step); i++)
        {
            player.transform.RotateAround(down.transform.position, Vector3.left, step);
            yield return new WaitForSeconds(speed);
           
        }
        center.transform.position = player.transform.position;
        input = true;
    }
    IEnumerator moveLeft()
    {
        audioSource.PlayOneShot(impact[Random.Range(0, impact.Length)], 0.7F);
        for (int i = 0; i < (90 / step); i++)
        {
            player.transform.RotateAround(left.transform.position, Vector3.forward, step);
            yield return new WaitForSeconds(speed);
           
        }
        center.transform.position = player.transform.position;
        input = true;
    }
    IEnumerator moveRight()
    {
        audioSource.PlayOneShot(impact[Random.Range(0, impact.Length)], 0.7F);
        for (int i = 0; i < (90 / step); i++)
        {
            player.transform.RotateAround(right.transform.position, Vector3.back, step);
            yield return new WaitForSeconds(speed);
     
        }
        center.transform.position = player.transform.position;
        input = true;
    }
}
