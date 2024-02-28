using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;

    private int score;
    public int Score => score;

    public Text Dev;

    public Text pause,resume;

    public GameObject[] secondSkybox;

    public AudioSource audio;
    public AudioClip JumpClip;
    public AudioClip BgClip;
    
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
            Pause();
        }
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Dev.gameObject.SetActive(false);

        secondSkybox[0].SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }

        
        pause.gameObject.SetActive(false);
        resume.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Dev.gameObject.SetActive(true);

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    // var isPaused : boolean = false;

    public void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            Time.timeScale = 0;
            audio.clip = JumpClip;
            audio.Play();

            pause.gameObject.SetActive(true);
            resume.gameObject.SetActive(true); 
        }
        if(Input.GetKeyDown("o"))
        {
            Time.timeScale = 1;
            audio.clip = BgClip;
            audio.Play();
            
            pause.gameObject.SetActive(false);
            resume.gameObject.SetActive(false);
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        secondSkybox[0].SetActive(false);

        if (score<=5) {
	        secondSkybox[0].SetActive(true);
	    }
	    else if(score>5 && score<11)
	    {
		    secondSkybox[1].SetActive(true);
	    }
        else if(score>10)
	    {
		    secondSkybox[2].SetActive(true);
	    }

    }

}
