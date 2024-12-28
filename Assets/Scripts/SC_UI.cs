using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI : MonoBehaviour
{
    public SC_PlayerControl player;

    public GameObject menuCamera;
    public GameObject World;
    public GameObject EventSystem;
    public GameObject menuPanel;
    public GameObject gamePanel;

    public Text hpText;
    public Text ammoText;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePanel.activeSelf)
        {
            hpText.text = player.CurrentHp + " / " + player.MaxHp;
            ammoText.text = (player.IsAuto ? " Auto  " : " Single  ") + player.CurrentAmmo + " / " + player.MaxAmmo;
        }
    }

    public void GameStart()
    {
        menuCamera.SetActive(false);
        World.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        EventSystem.SetActive(false);
        player.gameObject.SetActive(true);
        //player.GetComponent<SC_Quit>().enabled = false;
    }

    public void GameQuit()
    {
        Application.Quit(); //∞‘¿”/æ€ ¡æ∑·.
    }
}
