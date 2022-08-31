using UnityEngine;

public class MyTechTreeUIControler : MonoBehaviour
{
    [SerializeField] private GameObject evilBro;

    private GameObject player;
    private PlayerController playerControllerScript;
    private PlayerStats playerStatsScript;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        playerStatsScript = player.GetComponent<PlayerStats>();
    }

    public void CloseUpUI()
    {
        this.GetComponent<Canvas>().enabled = false;
        playerControllerScript.enabled = true;
    }
    public void AddNpcBro()
    {
        if (playerStatsScript.cultistsAmount >= 1 && playerStatsScript.broDownPerk == 0 && playerStatsScript.speedMeUpPerk == 1)
        {
            playerStatsScript.broDownPerk = 1;
            playerStatsScript.cultistsAmount -= 1;
            evilBro.SetActive(true);
            Debug.Log("+1 BRO");
        }
        else
        {
            Debug.Log("build more ziggurats");
        }
    }
    public void SpeedMeUp()
    {
        if (playerStatsScript.cultistsAmount >= 1 && playerStatsScript.speedMeUpPerk == 0)
        {
            playerStatsScript.speedMeUpPerk = 1;
            playerControllerScript.playerSpeed += 6f;
            playerStatsScript.cultistsAmount -= 1;
            Debug.Log("+6 speed, -1 Cultist");
        }
        else
        {
            Debug.Log("build more ziggurats");
        }
    }

}
