using UnityEngine;

public class UiInputControler : MonoBehaviour
{
    [SerializeField] private GameObject audioManager;
    [SerializeField] private GameObject MainSign;
    [SerializeField] private GameObject AmbientManager;

    private Component[] audioSources;

    private Canvas bookUI;
    private Canvas mainMenuUI;
    private Animator animMainSign;
    private PlayerController playerControler;
    private AmbientManager ambientManager;

    private float timedelay = 0.4f;
    private float t = 0f;
    private float doubleClickTime = 0.2f;
    private float doubleClickTimeLeft;
    private float randomNumber;


    private void Awake()
    {
        playerControler = GetComponent<PlayerController>();

        GameObject bookInterface = GameObject.FindWithTag("BookUI");
        bookUI = bookInterface.GetComponent<Canvas>();

        GameObject mainMenu = GameObject.FindWithTag("MainMenu");
        mainMenuUI = mainMenu.GetComponent<Canvas>();

        animMainSign = MainSign.GetComponent<Animator>();

        ambientManager = AmbientManager.GetComponent<AmbientManager>();
    }

    private void Start()
    {
        audioSources = audioManager.GetComponents(typeof(AudioSource));
    }

    private void BookMenuSwap()
    {
        if (bookUI.enabled == false)
        {
            randomNumber = Random.Range(1,3);
            bookUI.enabled = true;
            ambientManager.PlayAmbient("BookOpen"+randomNumber);
            playerControler.enabled = false;
        }
        else
        {
            randomNumber = Random.Range(1, 2);
            bookUI.enabled = false;
            ambientManager.PlayAmbient("BookClose"+randomNumber);
            playerControler.enabled = true;
        }
    }

    private void MainMenuSwap()
    {
        if (mainMenuUI.enabled == false)
        {
            mainMenuUI.enabled = true;
            playerControler.enabled = false;
        }
        else
        {
            mainMenuUI.enabled = false;

            if (bookUI.enabled == false)
            {
                playerControler.enabled = true;
            }
        }
    }

    private void SkipNarrator()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        t -= Time.deltaTime;
        doubleClickTimeLeft += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Escape) && t <= 0)
        {
            MainMenuSwap();
            t = timedelay;
        }

        if (Input.GetKey(KeyCode.I) && t <= 0 && mainMenuUI.enabled == false)
        {
            BookMenuSwap();
            t = timedelay;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (doubleClickTimeLeft <= doubleClickTime)
            {
                SkipNarrator();
            }
            doubleClickTimeLeft = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.E) && animMainSign.GetBool("SignActive"))
        {
            animMainSign.SetBool("SignActive", false);
        }
    }
}
