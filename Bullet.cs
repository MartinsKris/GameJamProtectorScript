using Assets.Scripts.Controller;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D _myBody;
    private string _enemy = "Enemy";
    private string _bullet = "Bullet";
    private string _saw = "Saw";
    private bool _isOnGround = true;
    private float _movementX;
    private Animator _anime;
    private string _attackAnimation = "Attack";
    private string _deathAnimation = "Dead";
    private string _idleAnimation = "Idle";
    private string _player = "Player";
    private string _vip = "VIP";
    private SpriteRenderer sr;
    int layerOfDead = 7;
    decimal attackCoef = 1m;
    AnimationController enemyAnimation;
    GameObject vipObject;
    GameObject playerObject;

    // Start is called before the first frame update
    void Awake()
    {
        _myBody = GetComponent<Rigidbody2D>();
        _anime = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        enemyAnimation = gameObject.AddComponent<AnimationController>();
        vipObject = GameObject.FindGameObjectWithTag(_vip);
        playerObject = GameObject.FindGameObjectWithTag(_player);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocityVector2 = new Vector2(speed, _myBody.velocity.y);
        _myBody.velocity = velocityVector2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_enemy) || collision.gameObject.CompareTag(_saw))
        {
            Destroy(gameObject);
        }
    }
}
