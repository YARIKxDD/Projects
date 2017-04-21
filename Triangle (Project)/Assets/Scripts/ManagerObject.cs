using UnityEngine;

public class ManagerObject : MonoBehaviour{
    public GameObject obj;
    public Sprite s_red;
    public Sprite s_green;
    public Sprite s_blue;
    float timer;
    float timeBetweenObjects;
    int currentRandomNumber;
    int numberOfLastObject;

    void DestroyChilds()
    {
        if (transform.childCount > 0)
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
    }
    void MakeRandom()
    {
        int lastRandomNumber = currentRandomNumber;
        while (currentRandomNumber == lastRandomNumber)
        {
            currentRandomNumber = Random.Range(1, 4);
        }
        lastRandomNumber = currentRandomNumber;
    }
    void MakeNewObject()
    {
        MakeRandom();
        GameObject GO = Instantiate(obj);
        GO.name = "GO";
        GO.transform.parent = transform;
        
        switch (currentRandomNumber)
        {
            case 1:
                GO.GetComponent<SpriteRenderer>().sprite = s_red;
                GO.GetComponent<Move>().typeOfObject = 1;                
                break;
            case 2:
                GO.GetComponent<SpriteRenderer>().sprite = s_green;
                GO.GetComponent<Move>().typeOfObject = 2;
                break;
            case 3:
                GO.GetComponent<SpriteRenderer>().sprite = s_blue;
                GO.GetComponent<Move>().typeOfObject = 3;
                break;
        }
        if (numberOfLastObject < 40)
        {
            timeBetweenObjects = 2f - numberOfLastObject * 0.03f;
            GO.GetComponent<Move>().speed = 3.5f + numberOfLastObject * 0.03f;
            GameObject.Find("Player").GetComponent<PlayerScript>().rotspeed = 5 + numberOfLastObject * 0.1f;
        }
        else
            GO.GetComponent<Move>().speed = 3.5f + 40 * 0.03f;
        numberOfLastObject++;
    }

    public void Start()
    {
        DestroyChilds();
        
        currentRandomNumber = 0;
        timer = 0;

        numberOfLastObject = 0;
        timeBetweenObjects = 3;
        GameObject.Find("Player").GetComponent<PlayerScript>().rotspeed = 5;
    }
	void Update () {
        if (!GameObject.Find("Player").GetComponent<PlayerScript>().pause)
        {
            if (timer <= 0)
            {
                timer = timeBetweenObjects;
                MakeNewObject();
            }
            timer -= Time.deltaTime;
        }
	}
}
