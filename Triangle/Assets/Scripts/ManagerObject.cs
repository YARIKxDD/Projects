using UnityEngine;

public class ManagerObject : MonoBehaviour{
    public GameObject Obj;
    public Sprite Red;
    public Sprite Green;
    public Sprite Blue;
    float Timer;
    float TimeBetweenObjects;
    int CurrentRandomNumber;
    int NumberOfLastObject;

    void DestroyChilds()
    {
        if (transform.childCount > 0)
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
    }
    void MakeRandom()
    {
        int LastRandomNumber = CurrentRandomNumber;
        while (CurrentRandomNumber == LastRandomNumber)
        {
            CurrentRandomNumber = Random.Range(1, 4);
        }
        LastRandomNumber = CurrentRandomNumber;
    }
    void MakeNewObject()
    {
        MakeRandom();
        GameObject GO = Instantiate(Obj);
        GO.name = "GO";
        GO.transform.parent = transform;
        
        switch (CurrentRandomNumber)
        {
            case 1:
                GO.GetComponent<SpriteRenderer>().sprite = Red;
                GO.GetComponent<Move>().TypeOfObject = 1;                
                break;
            case 2:
                GO.GetComponent<SpriteRenderer>().sprite = Green;
                GO.GetComponent<Move>().TypeOfObject = 2;
                break;
            case 3:
                GO.GetComponent<SpriteRenderer>().sprite = Blue;
                GO.GetComponent<Move>().TypeOfObject = 3;
                break;
        }
        if (NumberOfLastObject < 40)
        {
            TimeBetweenObjects = 2f - NumberOfLastObject * 0.03f;
            GO.GetComponent<Move>().FlySpeed = 3.5f + NumberOfLastObject * 0.03f;
            GameObject.Find("Player").GetComponent<PlayerScript>().RotationSpeed = 5 + NumberOfLastObject * 0.1f;
        }
        else
            GO.GetComponent<Move>().FlySpeed = 3.5f + 40 * 0.03f;
        NumberOfLastObject++;
    }

    public void Start()
    {
        DestroyChilds();
        
        CurrentRandomNumber = 0;
        Timer = 0;

        NumberOfLastObject = 0;
        TimeBetweenObjects = 3;
        GameObject.Find("Player").GetComponent<PlayerScript>().RotationSpeed = 5;
    }
	void Update () {
        if (!GameObject.Find("Player").GetComponent<PlayerScript>().Pause)
        {
            if (Timer <= 0)
            {
                Timer = TimeBetweenObjects;
                MakeNewObject();
            }
            Timer -= Time.deltaTime;
        }
	}
}
