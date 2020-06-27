using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menucubescript : MonoBehaviour
{
    //main menu kontrol scriptidir.

    public Transform[] spawns; //spawn yerleri
    public GameObject cubepref; //spawnlanacak küp
    Color renk; 
    public float floorColorChangeSpeed = 1.0f;
    public Color[] colors;
    public Camera cam;
    float startTime,t;
    private float timer = 0.0f;
    public float changeColourTime = 2.0f;

    void Start()
    {
        PlayerPrefs.SetInt("played", false ? 1 : 0);
        startTime = Time.time;
        t = 0;
        renk = colors[Random.Range(0, colors.Length)];
        InvokeRepeating("cube", 0, Random.Range(0,6)); //6 saniyede bir küp canlandır
    }
    void cube()
    {
        int index = Random.Range(0, spawns.Length);
        Destroy(Instantiate(cubepref, spawns[index].position,spawns[index].rotation),15);
    }
    void Update() //floor rengi degisme ve timer.
    {
        timer += Time.deltaTime;
        t = (Time.time - startTime) * floorColorChangeSpeed;
        GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, renk, t);
        cam.backgroundColor = GetComponent<Renderer>().material.color;
        if (timer > changeColourTime)
        {
            changecolor();
            timer = 0.0f;

        }
        
  
    }
    void changecolor() 
    {
        renk = colors[Random.Range(0, colors.Length)];
    }
    public void OnApplicationQuit() //oyundan cıkma fonksiyonu
    {
        Application.Quit();
    }
    public void changeQuality(int index) //grafik ayarlama
    {
        QualitySettings.SetQualityLevel(index,true);
    }
    public void changeScene(string name) //sahne degistirme
    {
        SceneManager.LoadScene(name);
    }
   
}
