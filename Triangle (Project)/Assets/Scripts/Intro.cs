using UnityEngine;

public class Intro : MonoBehaviour {
    public GameObject triangle;
    public GameObject visualThings;
    public Texture2D blackFone;

    Rect r_fone;

    Vector4 introColor;
    float timer;
    float localtimer;
    GUIStyle myStyle;
    int w;
    int h;

    void InitiateGUI()
    {
        w = Screen.width;
        h = Screen.height;

        r_fone.size = new Vector2(2 * w, 2 * h);
        r_fone.center = new Vector2(w / 2, h / 2);

        myStyle = new GUIStyle();
        myStyle.fontSize = h / 10;
        myStyle.alignment = TextAnchor.MiddleCenter;
    }

	void Start () {
        timer = 0;
        InitiateGUI();
        GameObject.Find("Player").GetComponent<PlayerScript>().pause = true;
        visualThings.SetActive(false);

    }
	void Update () {
        if (Time.deltaTime < 0.05) timer += Time.deltaTime;
        else timer += 0.05f;

        if (timer > 0.1 && timer < 0.885)
        {
            localtimer = timer - 0.1f;
            introColor = new Vector4(1, 1, 1, Mathf.Sin(2 * localtimer));
            myStyle.normal.textColor = introColor;
        }

        if (timer > 1 && timer < 1.785)
        {
            localtimer = timer - 0.215f;
            introColor = new Vector4(1, 1, 1, Mathf.Sin(2 * localtimer));
            myStyle.normal.textColor = introColor;
        }

        if (timer > 1 && timer < 2)
        {
            localtimer = timer - 1;
            r_fone.size = new Vector2(2 * w * (1 - localtimer), 2 * w * (1 - localtimer));
            r_fone.center = new Vector2(w / 2, h / 2);
        }

        if (timer > 2 && timer < 3)
        {
            r_fone.size = Vector2.zero;
            localtimer = timer - 2;
            if (!triangle.activeSelf)
                triangle.SetActive(true);
            if (!visualThings.activeSelf)
            {
                visualThings.SetActive(true);
                for (int i = 0; i < 3; i++)
                    visualThings.transform.GetChild(i).localScale = Vector3.zero;
            }
            triangle.transform.localScale = 0.5f * new Vector3(localtimer, localtimer, 2);
            triangle.transform.eulerAngles = new Vector3(0, 0, localtimer * -360);
            for (int i = 0; i < 3; i++)
                visualThings.transform.GetChild(i).localScale = new Vector3(localtimer, localtimer, 1);
        }

        if (timer > 3)
        {
            triangle.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            triangle.transform.eulerAngles = Vector3.zero;
            triangle.transform.position = Vector3.zero;
            GameObject.Find("Player").GetComponent<PlayerScript>().pause = false;
            gameObject.SetActive(false);
        }
    }
    void OnGUI()
    {
        GUI.DrawTexture(r_fone, blackFone);
        if (timer > 0.1 && timer < 1.785)
        {
            GUI.Label(new Rect(0, 0, w, h / 2), "YaX", myStyle);
            GUI.Label(new Rect(0, h / 2, w, h / 2), "PRESENT", myStyle);
        }
    }
}
