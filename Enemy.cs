using Assets.Scripts.Controller;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    private Rigidbody2D _myBody;
    private string _bullet = "Bullet";
    private string _saw = "Saw";
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
        if (vipObject.layer != layerOfDead && gameObject.layer != layerOfDead)
        {
            Vector2 velocityVector2 = new Vector2(speed, _myBody.velocity.y);
            _myBody.velocity = velocityVector2;
        }
    }

    private void Update()
    {
        if (gameObject.layer != layerOfDead)
        {
            AttackVIP();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_player) || collision.gameObject.CompareTag(_vip))
        {
            enemyAnimation.PlayerAnimation(_anime, _attackAnimation, false);
        }

        if (collision.gameObject.CompareTag(_bullet) || collision.gameObject.CompareTag(_saw))
        {
            gameObject.layer = layerOfDead;
            StartCoroutine(RemoveEnemy());
        }
    }

    void AttackVIP()
    {
        decimal enemyObjectPosition = new decimal(gameObject.transform.position.x);
        decimal vipObjectPosition = new decimal(vipObject.transform.position.x);
        decimal playerObjectPosition = new decimal(playerObject.transform.position.x);

        Vector3 runVector3 = new Vector3(_movementX, 0f, 0f);
        enemyAnimation.PlayerAnimation(_anime, _attackAnimation, false);
        if (vipObject.layer != layerOfDead || playerObject.layer != layerOfDead)
        {
            if (enemyObjectPosition < vipObjectPosition || enemyObjectPosition < playerObjectPosition)
            {
                if (vipObjectPosition - enemyObjectPosition < attackCoef || playerObjectPosition - enemyObjectPosition < attackCoef)
                {
                    Vector2 velocityVector2 = new Vector2(-2f, _myBody.velocity.y);
                    _myBody.velocity = velocityVector2;
                    enemyAnimation.PlayerAnimation(_anime, _attackAnimation, true);
                }
            }
            else
            {
                if (enemyObjectPosition - vipObjectPosition < attackCoef || enemyObjectPosition - playerObjectPosition < attackCoef)
                {
                    Vector2 velocityVector2 = new Vector2(2f, _myBody.velocity.y);
                    _myBody.velocity = velocityVector2;
                    enemyAnimation.PlayerAnimation(_anime, _attackAnimation, true);
                }
            }
        }

        if (vipObject.layer == layerOfDead)
        {
            enemyAnimation.PlayerAnimation(_anime, _idleAnimation, true);
            Vector2 velocityVector2 = new Vector2(0f, _myBody.velocity.y);
            _myBody.velocity = velocityVector2;
            runVector3 = new Vector3(0f, 0f, 0f);
        }
    }

    IEnumerator RemoveEnemy()
    {
        enemyAnimation.PlayerAnimation(_anime, _deathAnimation, true);
        Vector2 velocityVector2 = new Vector2(0f, _myBody.velocity.y);
        _myBody.velocity = velocityVector2;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
