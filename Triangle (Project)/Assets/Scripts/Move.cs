using UnityEngine;

public class Move : MonoBehaviour
{
    PlayerScript Player;
    public float speed;
    Vector2 direction;
    public int typeOfObject;

    void SetStartPosition()
    {
        int startDistance = 10;
        float startAngle = Random.Range(0, 360);
        float startX = -startDistance * Mathf.Sin(startAngle * Mathf.Deg2Rad);
        float startY = startDistance * Mathf.Cos(startAngle * Mathf.Deg2Rad);
        transform.position = new Vector2(startX, startY);
        direction = new Vector2(-startX / startDistance, -startY / startDistance);
    }
    
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        SetStartPosition();
    }
    void Update()
    {
        if (!Player.pause)
            transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        switch (coll.gameObject.name)
        {
            case "RedSide":
                Player.Catch(1, typeOfObject);
                Destroy(gameObject);
                break;
            case "GreenSide":
                Player.Catch(2, typeOfObject);
                Destroy(gameObject);
                break;
            case "BlueSide":
                Player.Catch(3, typeOfObject);
                Destroy(gameObject);
                break;
            default:
                Player.Catch(0, typeOfObject);
                Destroy(gameObject);
                break;
        }
    }
}
