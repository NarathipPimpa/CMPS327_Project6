using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counting : MonoBehaviour
{

    public static int HP = 100;
    public int damage = 50;
    public int zDamage = 50;
    private int badCount;
    public static int numFruitsCollected = 0;
    public Text countText;
    private bool cureFruit = false;

    // Start is called before the first frame update
    void Start()
    {
        countText.text = "Fruits Collected = " + numFruitsCollected.ToString() + "\nHP = " + HP.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (badCount == 0)
        {
            damage = 50;
        }
        if (other.gameObject.CompareTag("Fruit"))
        {
            numFruitsCollected += 1;
            HP += 20;
            countText.text = "Fruits Collected = " + numFruitsCollected.ToString() + "\nHP = " + HP.ToString();
        }

        if (other.gameObject.CompareTag("Invincible"))
        {
            damage = 0;
            badCount = 3;
        }

        if (other.gameObject.CompareTag("Zombie"))
        {
            badCount -= 1;
            HP = HP - damage;
            countText.text = "Fruits Collected = " + numFruitsCollected.ToString() + "\nHP = " + HP.ToString();

            if (HP <= 0)
            {
                EndGame();
            }

        }

        if (other.gameObject.CompareTag("Bad"))
        {
            badCount -= 1;
            numFruitsCollected += 1;
            HP = HP - damage;
            countText.text = "Fruits Collected = " + numFruitsCollected.ToString() + "\nHP = " + HP.ToString();

            if (HP <= 0)
            {
                EndGame();
            }
            
        }

        if (other.gameObject.CompareTag("Cure"))
        {
            Cure();
        }

    }    void Cure()
    {
        SceneManager.LoadScene("CureScene");
    }
    void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }



}
