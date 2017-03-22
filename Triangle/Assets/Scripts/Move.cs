using UnityEngine;

public class Move : MonoBehaviour
{
    PlayerScript Player;
    public float FlySpeed;
    Vector2 FlyDirection;
    public int TypeOfObject;
    
    void SetStartPosition()
    {
        int StartDistance = 10;
        float StartAngle = Random.Range(0, 360);
        float StartX = -StartDistance * Mathf.Sin(StartAngle * Mathf.Deg2Rad);
        float StartY = StartDistance * Mathf.Cos(StartAngle * Mathf.Deg2Rad);
        transform.position = new Vector2(StartX, StartY);
        FlyDirection = new Vector2(-StartX / StartDistance, -StartY / StartDistance);
    }
    
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        SetStartPosition();
    }
    void Update()
    {
        if (!Player.Pause)
            transform.Translate(FlyDirection * FlySpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        switch (coll.gameObject.name)
        {
            case "RedSide":
                Player.Catch(1, TypeOfObject);
                Destroy(gameObject);
                break;
            case "GreenSide":
                Player.Catch(2, TypeOfObject);
                Destroy(gameObject);
                break;
            case "BlueSide":
                Player.Catch(3, TypeOfObject);
                Destroy(gameObject);
                break;
            default:
                Player.Catch(0, TypeOfObject);
                Destroy(gameObject);
                break;
        }
    }
}
