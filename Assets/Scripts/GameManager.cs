using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] int score;
    [SerializeField] private GameObject GameOverObj;
    [SerializeField] private GameObject startObj;
    [SerializeField] private AudioSource musicaFundo;
    [SerializeField] private AudioSource efeitosAudio;
    [SerializeField] private AudioClip somScoring;
    bool isGameStarted = false;

    void Start()
    {
        Time.timeScale = 0;
        startObj.SetActive(true);

        if (musicaFundo != null)
        {
            musicaFundo.loop = true;
            musicaFundo.playOnAwake = false;
        }
    }

    public void StartButton()
    {
        Time.timeScale = 1;
        GameOverObj.SetActive(false);
        startObj.SetActive(false);
        isGameStarted = true;

        if (musicaFundo != null && !musicaFundo.isPlaying)
            musicaFundo.Play();
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        GameOverObj.SetActive(true);
        Time.timeScale = 0;
        isGameStarted = false;

        if (musicaFundo != null && musicaFundo.isPlaying)
            musicaFundo.Stop();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void Scoring()
    {
        if (!isGameStarted) return;

        score++;
        scoreText.text = score.ToString();

        if (efeitosAudio && somScoring)
        {
            efeitosAudio.Stop();
            efeitosAudio.PlayOneShot(somScoring);
        }
    }
}
