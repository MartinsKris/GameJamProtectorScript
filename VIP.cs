using Assets.Scripts.Controller;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VIP : MonoBehaviour
{
    private float _movementX;
    private Rigidbody2D _myBody;
    private Animator _anime;
    private string _fleeAnimation = "Flee";
    private string _deathAnimation = "Dead";
    private string _enemy = "Enemy";
    private string _saw = "Saw";
    private SpriteRenderer sr;
    int layerOfDead = 7;
    AnimationController vipAnimation;

    void Awake()
    {
        _myBody = GetComponent<Rigidbody2D>();
        _anime = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        vipAnimation = gameObject.AddComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer != layerOfDead)
        {
            AvoidEnemy();
        }
    }

    void AvoidEnemy()
    {
        var enemyObjectArray = GameObject.FindGameObjectsWithTag("Enemy");
        decimal VIPObjectPosition = new decimal(gameObject.transform.position.x);
        decimal fleeCoef = 5m;

        Vector3 runVector3 = new Vector3(_movementX, 0f, 0f);
        vipAnimation.PlayerAnimation(_anime, _fleeAnimation, false);
        for (var i = 0; i < enemyObjectArray.Length; i++)
        {
            decimal enemyObjectPosition = new decimal(enemyObjectArray[i].transform.position.x);

            if (VIPObjectPosition < enemyObjectPosition)
            {
                if (enemyObjectPosition - VIPObjectPosition < fleeCoef)
                {
                    Vector2 velocityVector2 = new Vector2(-2f, _myBody.velocity.y);
                    _myBody.velocity = velocityVector2;
                    vipAnimation.PlayerAnimation(_anime, _fleeAnimation, true);
                    vipAnimation.FlipPlayer(sr, true);
                }
            }
            else
            {
                if (VIPObjectPosition - enemyObjectPosition < fleeCoef)
                {
                    Vector2 velocityVector2 = new Vector2(2f, _myBody.velocity.y);
                    _myBody.velocity = velocityVector2;
                    vipAnimation.PlayerAnimation(_anime, _fleeAnimation, true);
                    vipAnimation.FlipPlayer(sr, false);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_enemy) || collision.gameObject.CompareTag(_saw))
        {
            gameObject.layer = layerOfDead;
            vipAnimation.PlayerAnimation(_anime, _deathAnimation, true);
            StartCoroutine(RestarGame());
        }
    }

    IEnumerator RestarGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
