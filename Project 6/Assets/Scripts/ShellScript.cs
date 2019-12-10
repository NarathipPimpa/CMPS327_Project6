using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShellScript : MonoBehaviour
{
    public GameObject Shell;

    public float ShellForce = 50.0f;

    public float destroyTime = 3.0f;
    AudioSource myaudio;

    // Start is called before the first frame update
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //create a bullet instance
            GameObject currentBullet = Instantiate(Shell, this.transform.position, this.transform.rotation) as GameObject;
            //fix scale
            currentBullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //add force to shoot
            currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * ShellForce);
            myaudio.Play();
            //Destroy it after a certain time
            Destroy(currentBullet, destroyTime);
        }
    }
}
