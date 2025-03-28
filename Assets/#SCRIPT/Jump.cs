using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jump : MonoBehaviour
{
    private AudioSource _bounceAudio;
    public AudioSource loseAudio, loseAudioAlw;
    private Rigidbody2D _ballRb;
    private SpriteRenderer _ballSpRnd;
    public GameObject Hexagonal;
    public float jumpVar;
    public Color[] colorArr = new Color[6];
    private readonly string[] _objectTag = { "Blue", "Orange", "Green", "Red", "Purple", "Yellow" };
    public TextMeshPro ScoreTxt, HighScoreTxt, GameOverTxt;
    private int _jumpScore, _highScore;
    public Color color10p, color11;
    private TurnAround _turnAround;

    private void Awake()
    {
        _turnAround = Hexagonal.GetComponent<TurnAround>();
    }

    private void Start()
    {
        _ballRb = GetComponent<Rigidbody2D>();
        _ballSpRnd = GetComponent<SpriteRenderer>();
        _bounceAudio = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("highScore"))
        {
            _highScore = PlayerPrefs.GetInt("highScore");
            HighScoreTxt.text = "High  Score : " + _highScore;
        }
        else
        {
            _highScore = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionBall)
    {
        if (collisionBall.gameObject.CompareTag("Floor"))
        {
            _ballRb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }

        foreach (var theTag in _objectTag)
        {
            if (!collisionBall.gameObject.CompareTag(theTag)) continue;
            _ballRb.AddForce(Vector2.up * jumpVar, ForceMode2D.Impulse);
            if (gameObject.CompareTag(collisionBall.gameObject.tag))
            {
                _bounceAudio.Play();
                _jumpScore++;
                if (_jumpScore > _highScore) _highScore = _jumpScore;

                if (_jumpScore % 10 == 0)
                {
                    StartCoroutine(MyEnum());
                }
                else
                {
                    ScoreTxt.text = "Score : " + _jumpScore;
                    HighScoreTxt.text = "High  Score : " + _highScore;
                }

                PlayerPrefs.SetInt("highScore", _highScore);
            }
            else if (!gameObject.CompareTag(collisionBall.gameObject.tag))
            {
                StartCoroutine(WaitGameOver());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collisionTrigger)
    {
        if (!collisionTrigger.gameObject.CompareTag("Trigger")) return;
        int randNumb = Random.Range(0, 6);
        _ballSpRnd.color = colorArr[randNumb];
        gameObject.tag = _objectTag[randNumb];
    }

    private IEnumerator MyEnum()
    {
        ScoreTxt.color = color10p;
        ScoreTxt.text = "Score : " + _jumpScore;
        HighScoreTxt.text = "High  Score : " + _highScore;
        yield return new WaitForSeconds(0.2f);
        ScoreTxt.color = color11;
    }

    private IEnumerator WaitGameOver()
    {
        _turnAround.enabled = false;
        if (_jumpScore == 0)
        {
            loseAudio.Play();
        }
        else
        {
            loseAudioAlw.Play();
        }

        GameOverTxt.text = "Game  Over";
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _turnAround.enabled = true;
    }
}