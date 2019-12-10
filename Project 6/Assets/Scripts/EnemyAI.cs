using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public enum EnemyState { CHASE,MOVING,DEFAULT};

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public Text countText;
    public int HP = 100;
    GameObject player;
    NavMeshAgent agent;
    public float chaseDistance = 20.0f;

    protected EnemyState state = EnemyState.DEFAULT;
    protected Vector3 destination = new Vector3(0, 0, 0);

    AudioSource myaudio;

    ParticleSystem explosion;
    bool explosionStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        myaudio = GetComponent<AudioSource>();
        explosion = transform.GetComponent<ParticleSystem>();
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.DEFAULT:
                destination = transform.position + RandomPosition();
                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance )
                {
                    state = EnemyState.CHASE;
                }
                else
                {
                    state = EnemyState.MOVING;
                    agent.SetDestination(destination);
                }
                break;

            case EnemyState.MOVING:
                Debug.Log("Dest = " + destination);
                if (Vector3.Distance(transform.position, destination) < 5)
                {
                    state = EnemyState.DEFAULT;
                }
                if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance )
                {
                    state = EnemyState.CHASE;
                }
                break;

            case EnemyState.CHASE:
                if (Vector3.Distance(transform.position, player.transform.position) > chaseDistance)
                {
                    state = EnemyState.DEFAULT;
                }
                agent.SetDestination(player.transform.position);
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            if (HP <= 0)
            {
                Counting.HP += 20;
                countText.text = "Fruits Collected = " + Counting.numFruitsCollected.ToString() + "\nHP = " + Counting.HP.ToString();
                // Disable all Renderers and Colliders
                Renderer[] allRenderers = gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer c in allRenderers) c.enabled = false;
                Collider[] allColliders = gameObject.GetComponentsInChildren<Collider>();
                foreach (Collider c in allColliders) c.enabled = false;
                gameObject.GetComponent<ParticleSystemRenderer>().enabled = true;
                StartExplosion();
                StartCoroutine(PlayAndDestroy(myaudio.clip.length));
            }
        }
    }

    private IEnumerator PlayAndDestroy(float waitTime)
    {
        myaudio.Play();
        yield return new WaitForSeconds(waitTime);
        StopExplosion();
        Destroy(gameObject);
    }

    private void StartExplosion()
    {
        if (explosionStarted == false)
        {
            explosion.Play();
            explosionStarted = true;
        }
    }

    private void StopExplosion()
    {
        explosionStarted = false;
        explosion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            HP -= 20;
        }
    }
}
