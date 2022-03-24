using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform _player;
    private Vector3 temporaryPosition;
    private string _playerTag = "Player";
    private float minX = 8.8f;
    private float maxX = 56f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag(_playerTag).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!_player)
        {
            return;
        }

        temporaryPosition = transform.position;
        temporaryPosition.x = _player.position.x;

        if (temporaryPosition.x < minX)
        {
            temporaryPosition.x = minX;
        }
        else if (temporaryPosition.x > maxX)
        {
            temporaryPosition.x = maxX;
        }
        else
        {
            transform.position = temporaryPosition;
        }
    }
}
