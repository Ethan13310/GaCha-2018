using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour {

    public Button startButton;
    public Button creditButton;

    public Text totalScoreText;

    void Start()
    {
        totalScoreText.text = "";

        if (Global.s.spellBook == null)
        {
            FinalCounts();
        }

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(delegate {
            SceneManager.LoadScene(Global.s.SceneIndex_Menu);
        });

        creditButton.onClick.RemoveAllListeners();
        creditButton.onClick.AddListener(delegate {
            SceneManager.LoadScene(Global.s.SceneIndex_Credits);
        });

    }

    void FinalCounts()
    {
        int totalScore = Global.s.spellBook.GetScore();

        updateTotalScoreText(totalScore);

    }

    void updateTotalScoreText(int totalScore)
    {
        totalScoreText.text = totalScore + " / 7";
    }
}
