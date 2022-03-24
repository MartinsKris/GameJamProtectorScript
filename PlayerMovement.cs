using Assets.Scripts.Controller;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveForce = 10f;

    [SerializeField]
    private float _jumpForce = 11f;

    [SerializeField]
    private GameObject bulletReference;

    private float _movementX;
    private Rigidbody2D _myBody;
    private Animator _anime;
    private string _runAnimation = "Run";
    private string _jumpAnimation = "Jump";
    private string _deathAnimation = "Dead";
    private string _ground = "Ground";
    private string _enemy = "Enemy";
    private string _saw = "Saw";
    private string _bullet = "Bullet";
    private string _shootAnimation = "Shoot";
    private SpriteRenderer sr;
    private bool _isOnGround = true;
    private int _deathLayer = 7;
    GameObject bulletObject;
    private float _bulletSize = 0.5f;
    private int _bulletSpeed = 15;
    private float _bulletOfset = 1f;
    AnimationController playerAnimation;

    private void Awake()
    {
        _myBody = GetComponent<Rigidbody2D>();
        _anime = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerAnimation = gameObject.AddComponent<AnimationController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (gameObject.layer != _deathLayer)
        {
            PlayerMoveKeyboard();
            AnimatePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (gameObject.layer != _deathLayer)
        {
            PlayerJump();
            PlayerShoot();
        }
    }

    void PlayerMoveKeyboard()
    {
        Vector3 runVector3 = new Vector3(_movementX, 0f, 0f);
        _movementX = Input.GetAxisRaw("Horizontal");
        transform.position += runVector3 * Time.deltaTime * _moveForce;
    }

    void AnimatePlayer()
    {
        if (_movementX > 0)
        {
            if (!_isOnGround)
            {
                playerAnimation.PlayerAnimation(_anime, _jumpAnimation, true);
                playerAnimation.FlipPlayer(sr, false);
            }
            else
            {
                playerAnimation.PlayerAnimation(_anime, _runAnimation, true);
                playerAnimation.FlipPlayer(sr, false);
            }

        }
        else if (_movementX < 0)
        {
            if (!_isOnGround)
            {
                playerAnimation.PlayerAnimation(_anime, _jumpAnimation, true);
                playerAnimation.FlipPlayer(sr, true);
            }
            else
            {
                playerAnimation.PlayerAnimation(_anime, _runAnimation, true);
                playerAnimation.FlipPlayer(sr, true);
            }
        }
        else
        {
            if (!_isOnGround)
            {
                playerAnimation.PlayerAnimation(_anime, _jumpAnimation, true);
            }
            else
            {
                playerAnimation.PlayerAnimation(_anime, _jumpAnimation, false);
                playerAnimation.PlayerAnimation(_anime, _runAnimation, false);
            }
        }
    }

    void PlayerJump()
    {
        Vector2 jumpVector2 = new Vector2(0f, _jumpForce);
        if (Input.GetButtonDown(_jumpAnimation) && _isOnGround)
        {
            _isOnGround = false;
            _myBody.AddForce(jumpVector2, ForceMode2D.Impulse);
        }
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerAnimation.PlayerAnimation(_anime, _shootAnimation, true);
            bulletObject = Instantiate(bulletReference);

            if (sr.flipX == false)
            {
                bulletObject.transform.position = new Vector3(gameObject.transform.position.x + _bulletOfset, gameObject.transform.position.y, gameObject.transform.position.z);
                bulletObject.GetComponent<Bullet>().speed = _bulletSpeed;
                bulletObject.transform.localScale = new Vector3(_bulletSize, _bulletSize, _bulletSize);
            }
            else
            {
                bulletObject.transform.position = new Vector3(gameObject.transform.position.x - _bulletOfset, gameObject.transform.position.y, gameObject.transform.position.z);
                bulletObject.GetComponent<Bullet>().speed = -_bulletSpeed;
                bulletObject.transform.localScale = new Vector3(-_bulletSize, _bulletSize, _bulletSize);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_ground))
        {
            _isOnGround = true;
        }

        if (collision.gameObject.CompareTag(_enemy) || collision.gameObject.CompareTag(_saw))
        {
            playerAnimation.PlayerAnimation(_anime, _deathAnimation, true);
            gameObject.layer = _deathLayer;
        }
    }
}
