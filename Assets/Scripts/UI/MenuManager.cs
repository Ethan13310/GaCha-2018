using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Button startButton;
    public Button creditsButton;
    public Button quitButton;

    void Start()
    {
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(delegate {
            SceneManager.LoadScene(Global.s.SceneIndex_Game);
        });

        creditsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.AddListener(delegate {
            SceneManager.LoadScene(Global.s.SceneIndex_Credits);
        });

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(delegate {
            //SceneManager
        });
    }
}
