using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

    public Button backButton;

    void Start()
    {
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(delegate {
            SceneManager.LoadScene(Global.s.SceneIndex_Menu);
        });
    }
}
