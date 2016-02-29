using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour {

    const int MinOpponents = 1, MaxOpponents = 1;
    const int GameVariant = 0;

    public Text successText;

    public GameObject playerDummy;

    float moveSpeed = 3f;


    static MultiplayerManager instance = null;
    //byte[] message;
    //bool reliable = false;

    void Awake()
    {
       DontDestroyOnLoad(this);
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        //.EnableSavedGames()
        // registers a callback to handle game invitations received while the game is not running.
        //.WithInvitationDelegate(< callback method >)
        // registers a callback for turn based match notifications received while the
        // game is not running.
        //.WithMatchDelegate(< callback method >)
        // require access to a player's Google+ social graph to sign in
        //.RequireGooglePlus()
        //.Build();

        //PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    public static MultiplayerManager getInstance {
        get {
            if (instance == null) {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                instance =  FindObjectOfType(typeof (MultiplayerManager)) as MultiplayerManager;
            }
 
            // If it is still null, create a new instance
            if (instance == null) {
                GameObject obj = new GameObject("MultiplayerManager");
                instance = obj.AddComponent(typeof (MultiplayerManager)) as MultiplayerManager;
               // Debug.Log ("Could not locate an AManager object. \ AManager was Generated Automaticly.");
            }
 
            return instance;
        }
    }

    void OnApplicationQuit()
    {
        instance = null;
    }

    // Use this for initialization
    void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        //NetworkManager.Instance.OtherPlayer.transform.position = Vector3.MoveTowards(NetworkManager.Instance.OtherPlayer.transform.position, NetworkManager.Instance.pos, speed * Time.deltaTime);
        if (NetworkManager.Instance.OtherPlayer != null)
        {
            NetworkManager.Instance.OtherPlayer.transform.position = Vector3.Lerp(NetworkManager.Instance.OtherPlayer.transform.position, NetworkManager.Instance.pos, moveSpeed * Time.deltaTime);
            NetworkManager.Instance.OtherPlayer.transform.rotation = Quaternion.Euler(NetworkManager.Instance.rot);
        }
    }

    public void CreateQuickMatch()
    {
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(MinOpponents, MaxOpponents,
                GameVariant, NetworkManager.Instance);
    }

    public void CreateInviteMatch()
    {
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
                GameVariant, NetworkManager.Instance);
    }

    public void ConnectToGooglePlay()
    {
        // authenticate user:
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
            if (success)
            {
                successText.text = "Success!";
            }
            else
            {
                successText.text = "Failed!" ;
            }
        });
    }

    //public void sendMessage(bool reliable, byte[] message)
    //{
    //    PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, message);
    //}

}
