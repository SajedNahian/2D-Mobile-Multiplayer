using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathCounter : MonoBehaviour {
    private static Text deathText;
    private static int myDeathCount, enemyDeathCount;
	// Use this for initialization
	void Awake () {
        deathText = GetComponent<Text>();
	}
    
    public static void myDeath ()
    {
        myDeathCount++;
        updateScore();
    }

    public static void enemyDeath ()
    {
        enemyDeathCount++;
        updateScore();
    }

    private static void updateScore ()
    {
        deathText.text = myDeathCount + " : " + enemyDeathCount;
    }
}
