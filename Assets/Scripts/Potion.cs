using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // Effets de la potion
    public List<Effect> Effects;
    public GameObject liquide;
    public GameObject bouchon;

    void Start(){
        liquide.SetActive(false);
        bouchon.SetActive(false);
    }

    public void ShowLiquide(bool val){
        liquide.SetActive(val);
    }

    public void ShowBouchon(bool val){
        bouchon.SetActive(val);
    }
}
