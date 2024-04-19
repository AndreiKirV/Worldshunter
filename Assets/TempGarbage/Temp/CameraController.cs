using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _speed = 150;
    private Vector2 _duration = Vector2.zero;

    public void Update()
    {
        //Input.GetKeyDown(KeyCode.Escape)
        if (!string.IsNullOrEmpty(Input.inputString))
            switch (Input.inputString)
            {
                case "w":
                case "ц":
                    _duration = new Vector2(_duration.x, 1);
                    Debug.Log("nazhata w");
                    break;
                case "s":
                case "ы":
                    _duration = new Vector2(_duration.x, -1);
                    Debug.Log("nazhata s");
                    break;
                case "a":
                case "ф":
                    _duration = new Vector2(-1, _duration.y);
                    break;
                case "d":
                case "в":
                    _duration = new Vector2(1, _duration.y);
                    break;
                // Добавьте другие клавиши, которые вам нужны
                default:
                    Debug.Log("Нажата другая клавиша: " + Input.inputString);
                    break;
            }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("otzh");
            _duration = new Vector2(_duration.x, 0);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("otzh");
            _duration = new Vector2(0, _duration.y);
        }

        Move(_duration);
    }

    private void Move(Vector2 duration)
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime * duration.x * _speed, transform.position.y + Time.deltaTime * duration.y * _speed, transform.position.z);
    }
}