using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class specialColor : MonoBehaviour
{
    public GameObject player,UICanvas;
    public Slider timeSlider;
    Movement movementcolor;
    GameManager gameManager;
    float time = 1;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        movementcolor = player.GetComponent<Movement>();
        time = 1;
        Time.timeScale = 0.2f;
    }
    private void Update()
    {
        if (time > 0)
        {
            time -= 0.004f;
            timeSlider.value = time;

        }
        if (time < 0)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            UICanvas.SetActive(true);
        }
    }
    public void color(string color)
    {
        switch (color)
        {
            case "blue":
                {
                    if (movementcolor.color == "black")
                    {
                        movementcolor.color = "blue";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[6];
                    }
                    if (movementcolor.color == "red")
                    {
                        movementcolor.color= "purple";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[3];
                    }
                    if (movementcolor.color == "yellow")
                    {
                        movementcolor.color = "green";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[5];
                    }
                    if (movementcolor.color == "orange")
                    {
                        movementcolor.color = "white";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[1];
                    }
                    break;
                }
            case "red":
                {
                    if (movementcolor.color == "black")
                    {
                        movementcolor.color = "red";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[2];
                    }
                    if (movementcolor.color == "blue")
                    {
                        movementcolor.color = "purple";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[3];
                    }
                    if (movementcolor.color == "yellow")
                    {
                        movementcolor.color = "orange";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[4];
                    }
                    if (movementcolor.color == "green")
                    {
                        movementcolor.color = "white";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[1];
                    }
                    break;
                }
            case "yellow":
                {
                    if (movementcolor.color == "black")
                    {
                        movementcolor.color = "yellow";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[0];
                    }
                    if (movementcolor.color == "red")
                    {
                        movementcolor.color = "orange";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[4];
                    }
                    if (movementcolor.color == "blue")
                    {
                        movementcolor.color = "green";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[5];
                    }
                    if (movementcolor.color == "purple")
                    {
                        movementcolor.color = "white";
                        player.GetComponent<Renderer>().material = gameManager.cubematerials[1];
                    }
                    break;
                }
            case "black":
                {
                    movementcolor.color = "black";
                    player.GetComponent<Renderer>().material = gameManager.cubematerials[7];
                    break;
                }

        }
        time = 0;
        UICanvas.SetActive(true);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
