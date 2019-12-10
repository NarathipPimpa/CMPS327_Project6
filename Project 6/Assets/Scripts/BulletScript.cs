using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletScript : MonoBehaviour
{
    public GameObject Bullet;

    public float BulletForce = 100.0f;

    public float destroyTime = 3.0f;
    AudioSource myaudio;
    private ParticleSystem gunEffect;

    // Start is called before the first frame update
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
        gunEffect = GameObject.Find("BulletEffect").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //create a bullet instance
            GameObject currentBullet = Instantiate(Bullet, this.transform.position, this.transform.rotation) as GameObject;
            //fix scale
            currentBullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //add force to shoot
            currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * BulletForce);
            myaudio.Play();
            gunEffect.Play();
            //Destroy it after a certain time
            Destroy(currentBullet, destroyTime);
        }
    }
}
