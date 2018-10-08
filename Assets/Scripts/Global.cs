using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

    public readonly int SceneIndex_Menu = 0;
    public readonly int SceneIndex_Game = 1;
    public readonly int SceneIndex_ScoreScreen = 2;
    public readonly int SceneIndex_Credits = 3;

    public static Global s;

    void Awake()
    {
        if (s != null)
        {
            Destroy(this.gameObject);
            return;
        }
        s = this;
        DontDestroyOnLoad(this);
    }

    public MusicManager sound;

    public SpellBook spellBook;



}
