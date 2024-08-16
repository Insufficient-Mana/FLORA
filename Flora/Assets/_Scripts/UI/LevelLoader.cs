using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public int levelNumber;
    Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("HighestLevelUnlocked") >= levelNumber)
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }

        if (levelNumber == 1)
        {
            myButton.interactable |= true;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level " +  levelNumber.ToString());
    }

}
