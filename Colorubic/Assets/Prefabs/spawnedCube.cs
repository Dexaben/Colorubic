using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedCube : MonoBehaviour {
    Rigidbody rigidBody;
    public GameObject destroypartical;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(Random.Range(0,90), Random.Range(0, 360), Random.Range(0,90)); //rasgele düşme kuvveti
        float speed = 10;
        rigidBody.isKinematic = false;
        Vector3 force = transform.forward;
        force = new Vector3(force.x, 1, force.z);
        rigidBody.AddForce(force * speed);
        sfxAudioSource = GetComponent<AudioSource>();
        StartCoroutine("destroy");
    }
    AudioSource sfxAudioSource;
    public AudioClip[] sfxAudioClips;
    void OnCollisionEnter(Collision collision) //cismin collision ile karşılaştığı anda ses oynatması
    {
            sfxAudioSource.PlayOneShot(sfxAudioClips[Random.Range(0,4)]);
    }
    IEnumerator destroy() //cube destroy
    {
        yield return new WaitForSeconds(10f);
        Destroy(Instantiate(destroypartical,transform.position,transform.rotation), 4f);
        sfxAudioSource.PlayOneShot(sfxAudioClips[5]);
        Destroy(gameObject,0.3f);
    }
}