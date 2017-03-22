using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject Triangle;
    public int Life;
    public int Score;
    public int BestScore;
    public float RotationSpeed;
    public bool Pause;

    public void Catch(int TypeOfSide, int TypeOfObject)
    {
        if (TypeOfSide != 0)
        {
            if (TypeOfSide == TypeOfObject)
            {
                Score++;
                if (PlayerPrefs.GetInt("BestScore") < Score)
                    PlayerPrefs.SetInt("BestScore", Score);
            }
            else
            {
                Life--;
                if (Life == 0)
                    Pause = true;
            }
        }         
    }
    
	public void Start ()
    {
        //PlayerPrefs.SetInt("BestScore", 0);
        BestScore = PlayerPrefs.GetInt("BestScore");
        Score = 0;
        Life = 3;
	}

    void Update()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Life > 0)
                Pause = !Pause;
        }

        if (!Pause)
            Triangle.transform.eulerAngles += new Vector3(0, 0, -1 * RotationSpeed * Input.GetAxis("Rotation"));
    }
}
