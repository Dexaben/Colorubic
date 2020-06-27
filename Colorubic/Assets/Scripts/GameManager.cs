using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Materialler")]
    public Material[] floormaterials;
    public Material[] cubematerials;

    [Header("Objeler")]
    public GameObject[] Cubes;
    public GameObject[] specialCubesObject;
    public Transform[] SpawnPoints;
    public GameObject[] floor;
    public GameObject deathMenuCanvas;
    public GameObject howtoPlayCanvas;
    public GameObject player;
    public Transform startspawn;
    public Slider playerHPslider;
    public Slider timeSlider;
    public Text gameScoreText, highScoreText, scoreText;
    public GameObject ui;
    int gameScore = 0;

    [Header("Prefablar")]
    public GameObject hpDownEffect;

    [Header("Audio")]
    public AudioClip[] audioClips;
    AudioSource audioSource;

    Movement playerScript;
    int playerHP = 100;
    float time = 1;
    int index;
    string floorcolor;

    void Start()
    {

        Time.timeScale = 1; //oyun zamanının sahne yüklenince sıfırlanması
        if (PlayerPrefs.GetInt("played") == 1)
        {
            howtoPlayCanvas.SetActive(false);
        }
        else
        {
            howtoPlayCanvas.SetActive(true);
        }
        player.transform.position = startspawn.position;
        player.GetComponent<Renderer>().material = cubematerials[7]; //baslangıcta siyah renk ile baslaması
        audioSource = GetComponent<AudioSource>();
        playerScript = player.GetComponent<Movement>();
        InvokeRepeating("SpawnCubes", 0, 1.5f); //her 1.5 saniyede rastgele noktada rastgele kup canlandırılacak
        InvokeRepeating("specialCubes", 6, 8); //6 saniye sonra 8 saniyede bir rastgele özel küp canlandırılacak 
        playerHPslider.gameObject.SetActive(false);
        timeSlider.value = time;
        playerHPslider.value = playerHP / 100;
        nextColor();
    }
    void nextColor() //random zemin rengi ataması
    {
        index = Random.Range(0, floormaterials.Length);
        floor[0].GetComponent<Renderer>().material = floormaterials[index];
        floor[1].GetComponent<Renderer>().material = floormaterials[index]; //rastgele arka ve ön zemin rengi
        switch (index)
        {
            case 0:
                {
                    floorcolor = "yellow";
                    break;
                }
            case 1:
                {
                    floorcolor = "white";
                    break;
                }
            case 2:
                {
                    floorcolor = "red";
                    break;
                }
            case 3:
                {
                    floorcolor = "purple";
                    break;

                }
            case 4:
                {
                    floorcolor = "orange";
                    break;
                }
            case 5:
                {
                    floorcolor = "green";
                    break;
                }
            case 6:
                {
                    floorcolor = "blue";
                    break;
                }
            case 7:
                {
                    floorcolor = "black";
                    break;
                }
        }
    }
    void specialCubes() //özel topların rastgele spawnlarda rastgele olarak canlandıran fonksiyon Invoke ile 8 saniyede bir cagırılır.
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length);
        int spawnObjectIndex = Random.Range(0, specialCubesObject.Length);

        Destroy(Instantiate(specialCubesObject[spawnObjectIndex], SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation), 12f);
    }
    void SpawnCubes()
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length);
        int spawnObjectIndex = Random.Range(0, Cubes.Length);

        Destroy(Instantiate(Cubes[spawnObjectIndex], SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation), 12f);
    }
    private void Update()
    {
        if (time > 0) //süre barının azalması
        {
            time -= 0.001f;
            timeSlider.value = time;
        }
        if (time < 0) //sure barı 0 dan asagıya duserse
        {
            if (playerScript.color == floorcolor) //küpün materiyali zeminin materyaline eşitse skoru 1 arttırır sureyi sıfırlar materyalleri atar.
            {
                gameScore++;
                gameScoreText.text = "Skor : " + gameScore;
                nextColor();
                time = 1;
            }
            else //küpün materyali zeminin materyaline eşit degilse -20 can düser o canda 0lanmıssa oyunu bitirir.
            {
                playerHP -= 20;
                playerHPslider.value = playerHP / 100f;
                audioSource.PlayOneShot(audioClips[0]);
                StartCoroutine("showHP");
                Destroy(Instantiate(hpDownEffect, new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 2), player.transform.rotation), 4f);
                if (playerHP == 0)
                {
                    scoreText.text = "Senin Skorun : " + gameScore;
                    Debug.Log("death");
                    if (gameScore >= PlayerPrefs.GetInt("highscore") || PlayerPrefs.GetInt("highscore") == null) //playerpref save
                    {
                        PlayerPrefs.SetInt("highscore", gameScore);
                        highScoreText.text = "Yüksek Skor : " + PlayerPrefs.GetInt("highscore");

                    }
                    else
                    {
                        highScoreText.text = "Yüksek Skor : " + PlayerPrefs.GetInt("highscore");
                    }
                    deathMenu();
                }
                nextColor();

                time = 1;
            }

        }
    }
    public void SetTime(float setTime) //zaman ayarlama
    {
        Time.timeScale = setTime;
    }
    IEnumerator showHP() //damage aldıktan sonra 2 saniyeliğine health barı aktif eder.
    {
        playerHPslider.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerHPslider.gameObject.SetActive(false);
    }
    void deathMenu() //hp sıfırlanınca acılacak menu
    {
        Time.timeScale = 0;
        ui.SetActive(false);
        deathMenuCanvas.SetActive(true);

    }
    public void sceneload(string name) //sahne yükleme
    {
        SceneManager.LoadScene(name);
    }
    public void howtoPlay(bool play) //how to play gösterilecekmi?
    {
            PlayerPrefs.SetInt("played", play ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("played") != 0);
    }
}
