using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using UnityEngine.UI;

public class NetworkManager : GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
{

    private static NetworkManager sInstance = new NetworkManager();
    private bool showingWaitingRoom = false;

    public GameObject OtherPlayer;
    public GameObject PlayerClone;

    public Text debugText;
    public Text dataReceivedText;

    float speed = 8f;
    float startTime;
    float journeyLength;

    public Vector3 pos;
    public Vector3 rot;

    // Use this for initialization
    void Start () {

    }

    //void Update()
    //{
    //    
    //
    //    //float distCovered = (Time.time - startTime) * speed;
    //    //float fracJourney = distCovered / journeyLength;
    //
    //    //OtherPlayer.transform.position = Vector3.Lerp(OtherPlayer.transform.position, pos, fracJourney);
    //}

    public static NetworkManager Instance
    {
        get { return sInstance; }
    }

    public static void CreateQuickMatch()
    {

    }

    public void OnLeftRoom()
    {
        throw new NotImplementedException();
    }

    public void OnParticipantLeft(Participant participant)
    {
        throw new NotImplementedException();
    }

    public void OnPeersConnected(string[] participantIds)
    {
        throw new NotImplementedException();
    }

    public void OnPeersDisconnected(string[] participantIds)
    {
        throw new NotImplementedException();
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        // Player Transform Data
        if (data[0] == 0) // if the first positon in the array = 0 then it is a transform being sent
        {
            float[] floatArray2 = new float[data.Length / 4];
            System.Buffer.BlockCopy(data, 0, floatArray2, 0, data.Length);

            if (Application.loadedLevelName == "MainGame" && OtherPlayer == null)
            {
               PlayerClone = MultiplayerManager.getInstance.playerDummy;
               OtherPlayer = GameObject.Instantiate(PlayerClone);

               debugText = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();
               dataReceivedText = GameObject.FindGameObjectWithTag("DataReceivedText").GetComponent<Text>();
            }
            // Handle game updating
            pos = new Vector3(floatArray2[1], floatArray2[2], floatArray2[3]);

            debugText.text = "Position: " + pos.ToString();
            dataReceivedText.text = "Data Received: " + data[4].ToString() + data[5].ToString() + data[6].ToString() + data[7].ToString();
        }
        // Player Rotation Data
        else if (data[0] == 1) // if the first positon in the array = 1 then it is a rotation being sent
        {
            float[] floatArray2 = new float[data.Length / 4];
            System.Buffer.BlockCopy(data, 0, floatArray2, 0, data.Length);

            if (Application.loadedLevelName == "MainGame" && OtherPlayer == null)
            {
                PlayerClone = MultiplayerManager.getInstance.playerDummy;
                OtherPlayer = GameObject.Instantiate(PlayerClone);
            }
            // Handle game updating
            rot = new Vector3(floatArray2[1], floatArray2[2], floatArray2[3]);
        }
    }

    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            // Successfully connected to room!
            // ...start playing game...
            Application.LoadLevel("MainGame");
        }
        else
        {
            // Error!
            // ...show error message to user...
        }
    }

    public void OnRoomSetupProgress(float percent)
    {
        if (!showingWaitingRoom)
        {
            showingWaitingRoom = true;
            PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
        }
    }
}
