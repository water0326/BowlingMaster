using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenEasterEgg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int clearedStage = PlayerPrefs.GetInt("ClearedStage", 0);
        if (clearedStage >= 12)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
