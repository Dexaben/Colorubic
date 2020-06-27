using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeup : MonoBehaviour {
    public GameObject player, center, left, right, up, down;

    
    [SerializeField] int step = 6;
    [SerializeField] float speed = 0.001f;//mechanics

    [Header("Audio")]
    public AudioClip[] impact;
    AudioSource audioSource;


    public Material[] colors;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("go", 0, Random.Range(1f,1.2f)); //kup hızlarının random atanması ve tekrar eden fonksiyon
        this.GetComponent<Renderer>().material = colors[Random.Range(0, colors.Length)]; //rastgele renk
    }

	void go() {
        StartCoroutine("moveUp");
    }
    IEnumerator moveUp()
    {
        audioSource.PlayOneShot(impact[Random.Range(0, impact.Length)], 0.7F);
        for (int i = 0; i < (90 / step); i++)
        {
            player.transform.RotateAround(up.transform.position, Vector3.right, step);
            yield return new WaitForSeconds(speed);

        }
        center.transform.position = player.transform.position;
   
    }
}
