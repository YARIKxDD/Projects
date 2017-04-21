using UnityEngine;
public class OnGuiThere : MonoBehaviour {
    PlayerScript Player;

    public Texture2D t_play;
    public Texture2D t_restart;
    public Texture2D t_exit;
    public Texture2D t_fone;

    int w;
    int h;

    Rect r_fone;
    Rect r_play;
    Rect r_exit;
    Rect r_restart;

    Rect r_score;
    Rect r_record;
    Rect r_life;

    GUIStyle myStyle;

    void Initiate()
    {
        w = Screen.width;
        h = Screen.height;

        int offset = h / 15;
        int size = h / 9;
        Vector2 center = new Vector2(w / 2, h / 2);

        r_fone.size =     new Vector2(size, size) * 3f;
        r_play.size =     new Vector2(size, size) * 1.5f;
        r_exit.size =     new Vector2(size, size);
        r_restart.size =  new Vector2(size, size);

        r_fone.center =       new Vector2(center.x, center.y);
        r_play.center =       new Vector2(center.x, center.y - offset);
        r_exit.center =       new Vector2(center.x + offset, center.y + offset);
        r_restart.center =    new Vector2(center.x - offset, center.y + offset);

        r_life =    new Rect(center.x - h * 8 / 18, h * 2 / 18, h / 9, h / 18);
        r_score =   new Rect(center.x - h * 8 / 18, h * 1 / 36, h / 9, h / 18);
        r_record =  new Rect(center.x + h * 7 / 18, h * 2 / 18, h / 9, h / 18);

        myStyle.alignment = TextAnchor.MiddleCenter;
        myStyle.fontSize = h / 18;
    }

	void Start ()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        myStyle = new GUIStyle();
        Initiate();
	}
    void OnGUI()
    {
        if (w != Screen.width || h != Screen.height)
            Initiate();
        
        if (!GameObject.Find("Intro"))
        {
            myStyle.normal.textColor = Color.red;
            GUI.Label(r_life, Player.life.ToString(), myStyle);
            myStyle.normal.textColor = Color.yellow;
            GUI.Label(r_score, Player.score.ToString(), myStyle);
            GUI.Label(r_record, Player.record.ToString(), myStyle);
        }
        if (Player.pause && !GameObject.Find("Intro"))
        {
            GUI.DrawTexture(r_fone, t_fone);

            if (Player.GetComponent<PlayerScript>().life > 0)
            {
                if (GUI.Button(r_play, t_play, myStyle))
                {
                    Player.pause = true;
                }
            }
            else
            {
                myStyle.normal.textColor = Color.black;
                GUI.Label(r_play, "G. O.", myStyle);
            }

            if (GUI.Button(r_restart, t_restart, myStyle))
            {
                Player.pause = false;
                GameObject.Find("Player").GetComponent<PlayerScript>().Start();
                GameObject.Find("ManagerObject").GetComponent<ManagerObject>().Start();
            }

            if (GUI.Button(r_exit, t_exit, myStyle))
                Application.Quit();

        }
    }
}
