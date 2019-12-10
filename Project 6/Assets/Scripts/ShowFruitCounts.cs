using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFruitCounts : MonoBehaviour
{
    public Text countText;
    // Start is called before the first frame update
    void Start()
    {
        countText.text = "Fruits Collected = " + Counting.numFruitsCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
