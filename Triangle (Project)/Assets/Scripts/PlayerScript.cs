using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    GameObject triangle;
    public int life;
    public int score;
    public int record;
    public float rotspeed;
    public bool pause;

    public void Catch(int typeOfSide, int typeOfObject)
    {
        if (typeOfSide != 0)
        {
            if (typeOfSide == typeOfObject)
            {
                score++;
                if (PlayerPrefs.GetInt("record") < score)
                    PlayerPrefs.SetInt("record", score);
            }
            else
            {
                life--;
                if (life == 0)
                    pause = true;
            }
        }         
    }
    
	public void Start ()
    {
        //PlayerPrefs.SetInt("BestScore", 0);
        triangle = transform.GetChild(0).gameObject;
        record = PlayerPrefs.GetInt("record");
        score = 0;
        life = 3;
	}

    void Update()
    {
        if (score > record)
        {
            record = score;
            PlayerPrefs.SetInt("record", record);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            if (life > 0)
                pause = !pause;
        }

        if (!pause)
        {
            if (Input.GetAxis("Rotation") != 0)
                triangle.transform.eulerAngles += new Vector3(0, 0, -1 * rotspeed * Input.GetAxis("Rotation"));

            else if (Input.GetMouseButton(0) && Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x < Screen.width / 2)
                triangle.transform.eulerAngles += new Vector3(0, 0, rotspeed);

            else if (Input.GetMouseButton(0) && Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x > Screen.width / 2)
                triangle.transform.eulerAngles -= new Vector3(0, 0, rotspeed);
        }
    }
}
