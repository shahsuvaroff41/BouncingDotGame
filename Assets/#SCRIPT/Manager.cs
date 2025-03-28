using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject Ball, Hexagonal, TapToPlay;
    public Transform StartTransform;
    private bool _dontRepeat = false;
    private Rigidbody2D _rigidbody2D;
    private TurnAround _turnAround;

    private void Awake()
    {
        _turnAround = Hexagonal.GetComponent<TurnAround>();
        _rigidbody2D = Ball.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Hexagonal.GetComponent<TurnAround>().enabled = false;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0) || _dontRepeat) return;
        _rigidbody2D.linearVelocityY = 0f;
        _turnAround.enabled = true;
        Ball.transform.position = StartTransform.position;
        TapToPlay.SetActive(false);
        _dontRepeat = true;
    }   
}