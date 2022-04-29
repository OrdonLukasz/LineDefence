using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private float totalScore;
    [SerializeField] private TMP_Text totalScoreText;

    public void Start()
    {
        totalScoreText.text = totalScore.ToString();
    }
    

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }


}
