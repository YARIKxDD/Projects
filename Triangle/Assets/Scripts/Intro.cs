using UnityEngine;

public class Intro : MonoBehaviour {
    public GameObject Triangle;
    public GameObject VisualThings;
    public Texture2D BlackFone;

    Rect FoneRect;

    Vector4 IntroColor;
    float Timer;
    float LocalTimer;
    GUIStyle Style1;
    int W;
    int H;

    void InitiateGUI()
    {
        W = Screen.width;
        H = Screen.height;

        FoneRect.size = new Vector2(2 * W, 2 * H);
        FoneRect.center = new Vector2(W / 2, H / 2);

        Style1 = new GUIStyle();
        Style1.fontSize = H / 10;
        Style1.alignment = TextAnchor.MiddleCenter;
    }

	void Start () {
        Timer = 0;
        InitiateGUI();
        GameObject.Find("Player").GetComponent<PlayerScript>().Pause = true;
        VisualThings.SetActive(false);

    }
	void Update () {
        if (Time.deltaTime < 0.05) Timer += Time.deltaTime;
        else Timer += 0.05f;

        if (Timer > 0.1 && Timer < 0.885)
        {
            LocalTimer = Timer - 0.1f;
            IntroColor = new Vector4(1, 1, 1, Mathf.Sin(2 * LocalTimer));
            Style1.normal.textColor = IntroColor;
        }

        if (Timer > 1 && Timer < 1.785)
        {
            LocalTimer = Timer - 0.215f;
            IntroColor = new Vector4(1, 1, 1, Mathf.Sin(2 * LocalTimer));
            Style1.normal.textColor = IntroColor;
        }

        if (Timer > 1 && Timer < 2)
        {
            LocalTimer = Timer - 1;
            FoneRect.size = new Vector2(2 * W * (1 - LocalTimer), 2 * W * (1 - LocalTimer));
            FoneRect.center = new Vector2(W / 2, H / 2);
        }

        if (Timer > 2 && Timer < 3)
        {
            FoneRect.size = Vector2.zero;
            LocalTimer = Timer - 2;
            if (!Triangle.activeSelf)
                Triangle.SetActive(true);
            if (!VisualThings.activeSelf)
            {
                VisualThings.SetActive(true);
                for (int i = 0; i < 3; i++)
                    VisualThings.transform.GetChild(i).localScale = Vector3.zero;
            }
            Triangle.transform.localScale = 0.5f * new Vector3(LocalTimer, LocalTimer, 2);
            Triangle.transform.eulerAngles = new Vector3(0, 0, LocalTimer * -360);
            for (int i = 0; i < 3; i++)
                VisualThings.transform.GetChild(i).localScale = new Vector3(LocalTimer, LocalTimer, 1);
        }

        if (Timer > 3)
        {
            Triangle.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Triangle.transform.eulerAngles = Vector3.zero;
            Triangle.transform.position = Vector3.zero;
            GameObject.Find("Player").GetComponent<PlayerScript>().Pause = false;
            gameObject.SetActive(false);
        }
    }
    void OnGUI()
    {
        GUI.DrawTexture(FoneRect, BlackFone);
        if (Timer > 0.1 && Timer < 1.785)
        {
            GUI.Label(new Rect(0, 0, W, H / 2), "YaX", Style1);
            GUI.Label(new Rect(0, H / 2, W, H / 2), "PRESENT", Style1);
        }
    }
}
