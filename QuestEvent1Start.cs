using System.Collections;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class QuestEvent1Start : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private PlayerStats playerStats;

    [SerializeField] private GameObject questUI;
    [SerializeField] private GameObject CenterPanel;
    [SerializeField] private GameObject whispersParticles;
    [SerializeField] private GameObject afterIgnoreEvent1;
    [SerializeField] private GameObject compassUIMarkers;
    [SerializeField] private GameObject NarratorManager;
    [SerializeField] private GameObject CameraTarget;
    [SerializeField] private GameObject MainSign;
    [SerializeField] private GameObject CameraBehindBarrel;

    private AudioSource[] narratorAudioSource;

    private AudioSource audioWhispers;
    private AudioManager AudioManager;
    private Animator animatorCameraTarget;
    private Animator animatorMainSign;
    private Animator animatorTransition;
    private CinemachineVirtualCamera vCamBehindBarrel;
    private Canvas questCanvas;
    private RotateObject RotateScript;

    private bool playerIsTouching = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerStats = player.GetComponent<PlayerStats>();

        AudioManager = FindObjectOfType<AudioManager>();
        animatorTransition = GameObject.Find("Transition").GetComponent<Animator>();

        audioWhispers = this.GetComponent<AudioSource>();
        narratorAudioSource = NarratorManager.GetComponents<AudioSource>();

        questCanvas = questUI.GetComponent<Canvas>();

        RotateScript = CenterPanel.GetComponent<RotateObject>();

        animatorMainSign = MainSign.GetComponent<Animator>();
        animatorCameraTarget = CameraTarget.GetComponent<Animator>();

        vCamBehindBarrel = CameraBehindBarrel.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsTouching = true;
            RotateScript.enabled = true;
            animatorMainSign.SetBool("SignActive", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsTouching = false;
            RotateScript.enabled = false;
            animatorMainSign.SetBool("SignActive", false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsTouching == true)
        {
            StartQuestCutSceene();
        }

        if (playerStats.waitForNarrator == true)
        {
            if (!narratorAudioSource.Any(obj => obj.isPlaying == true))
            {
                questCanvas.enabled = true;
                playerStats.waitForNarrator = false;
            }
        }
        
    }
    private void StartQuestCutSceene()
    {
        playerController.enabled = false;
        animatorTransition.SetTrigger("FadeOut");
        vCamBehindBarrel.Priority = 11;
        TurnIndicatorsOff();

        StartCoroutine(WaitForCamera());
        afterIgnoreEvent1.SetActive(false); //reset the quest stage if player ignored earlier but came back

        AudioManager.Play("Quest1Intro");

        playerStats.waitForNarrator = true;
    }

    private void TurnIndicatorsOff()
    {
        animatorMainSign.SetTrigger("TurnOff");
        compassUIMarkers.SetActive(false);
        whispersParticles.SetActive(false);
        audioWhispers.enabled = false;
    }

    IEnumerator WaitForCamera()
    {
        yield return new WaitForSeconds(2f);
        animatorCameraTarget.SetTrigger("CameraGo");
    }
}

