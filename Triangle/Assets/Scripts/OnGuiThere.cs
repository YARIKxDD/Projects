using UnityEngine;
public class OnGuiThere : MonoBehaviour {
    PlayerScript Player;

    public Texture2D PlayTexture;
    public Texture2D RestartTexture;
    public Texture2D ExitTexture;
    public Texture2D PauseFone;

    int W;
    int H;

    Rect FoneRect;
    Rect PlayRect;
    Rect ExitRect;
    Rect RestartRect;

    Rect ScoreRect;
    Rect BestScoreRect;
    Rect LifeRect;

    GUIStyle MyStyle;

    void Initiate()
    {
        W = Screen.width;
        H = Screen.height;

        int offset = H / 15;
        int Size = H / 9;
        Vector2 Center = new Vector2(W / 2, H / 2);

        FoneRect.size =     new Vector2(Size, Size) * 3f;
        PlayRect.size =     new Vector2(Size, Size) * 1.5f;
        ExitRect.size =     new Vector2(Size, Size);
        RestartRect.size =  new Vector2(Size, Size);

        FoneRect.center =       new Vector2(Center.x, Center.y);
        PlayRect.center =       new Vector2(Center.x, Center.y - offset);
        ExitRect.center =       new Vector2(Center.x + offset, Center.y + offset);
        RestartRect.center =    new Vector2(Center.x - offset, Center.y + offset);

        LifeRect =      new Rect(Center.x - H * 8 / 18, H * 2 / 18, H / 9, H / 18);
        ScoreRect =     new Rect(Center.x - H * 8 / 18, H * 1 / 36, H / 9, H / 18);
        BestScoreRect = new Rect(Center.x + H * 7 / 18, H * 2 / 18, H / 9, H / 18);

        MyStyle.alignment = TextAnchor.MiddleCenter;
        MyStyle.fontSize = H / 18;
    }

	void Start ()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        MyStyle = new GUIStyle();
        Initiate();
	}
    void OnGUI()
    {
        if (W != Screen.width || H != Screen.height)
            Initiate();
        
        if (!GameObject.Find("Intro"))
        {
            MyStyle.normal.textColor = Color.red;
            GUI.Label(LifeRect, Player.Life.ToString(), MyStyle);
            MyStyle.normal.textColor = Color.yellow;
            GUI.Label(ScoreRect, Player.Score.ToString(), MyStyle);
            GUI.Label(BestScoreRect, Player.BestScore.ToString(), MyStyle);
        }
        if (Player.Pause && !GameObject.Find("Intro"))
        {
            GUI.DrawTexture(FoneRect, PauseFone);

            if (Player.GetComponent<PlayerScript>().Life > 0)
            {
                if (GUI.Button(PlayRect, PlayTexture, MyStyle))
                {
                    Player.Pause = true;
                }
            }
            else
            {
                MyStyle.normal.textColor = Color.black;
                GUI.Label(PlayRect, "G. O.", MyStyle);
            }

            if (GUI.Button(RestartRect, RestartTexture, MyStyle))
            {
                Player.Pause = false;
                GameObject.Find("Player").GetComponent<PlayerScript>().Start();
                GameObject.Find("ManagerObject").GetComponent<ManagerObject>().Start();
            }

            if (GUI.Button(ExitRect, ExitTexture, MyStyle))
                Application.Quit();

        }
    }
}
