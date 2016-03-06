using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.IO;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;

    public float moveSpeed = 1;
    public float rotSpeed = 1;

    private RaycastHit hit;
    public int range = 1;
    public int dmg = 1;

    public float rateOfFire = 1;
    private float lastShot = 0.0f;

    public GameObject projectile;
    public GameObject firePosition;

    public bool canShoot = false;

    public Text dataSentText;
    public Text positionText;

    byte[] bytePos = new byte[2];

    float delayTime = 0.3f;
    float timeDelayed = 0;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        dataSentText = GameObject.FindGameObjectWithTag("DataSentText").GetComponent<Text>();
        positionText = GameObject.FindGameObjectWithTag("PositionText").GetComponent<Text>();
	}

    

    void Update()
    {
        //timeDelayed += Time.deltaTime;
        //if (timeDelayed >= delayTime)
        //{

            
           // sendRotation();

            //sendTransform();
            //sendRotation();

            //timeDelayed = 0;
        //}
        Rotate();
    }

    public void sendTransform()
    {
        byte[] byteArray = new byte[16];

        MemoryStream stream = new MemoryStream();
        BinaryWriter bw = new BinaryWriter(stream);
        bw.Write(transform.position.ToString());
        bw.Flush();
        byte[] floatBytes = stream.ToArray();

        byte[] x = System.BitConverter.GetBytes(transform.position.x);
        byte[] y = System.BitConverter.GetBytes(transform.position.y);
        byte[] z = System.BitConverter.GetBytes(transform.position.z);

        byteArray[0] = 0;
        byteArray[1] = 0;
        byteArray[2] = 0;
        byteArray[3] = 0;

        byteArray[4] = x[0];
        byteArray[5] = x[1];
        byteArray[6] = x[2];
        byteArray[7] = x[3];

        byteArray[8] = y[0];
        byteArray[9] = y[1];
        byteArray[10] = y[2];
        byteArray[11] = y[3];

        byteArray[12] = z[0];
        byteArray[13] = z[1];
        byteArray[14] = z[2];
        byteArray[15] = z[3];

        Debug.Log(transform.position);

        Debug.Log(byteArray[0].ToString() + byteArray[1].ToString() + byteArray[2].ToString());
        
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, byteArray);
        
        dataSentText.text = "Data Sent: " + byteArray[4].ToString() + byteArray[5].ToString() + byteArray[6].ToString() + byteArray[7].ToString();
    }

    void sendRotation()
    {
        byte[] byteArray = new byte[16];

        MemoryStream stream = new MemoryStream();
        BinaryWriter bw = new BinaryWriter(stream);
        bw.Write(transform.rotation.ToString());
        bw.Flush();
        byte[] floatBytes = stream.ToArray();

        byte[] x = System.BitConverter.GetBytes(transform.rotation.x);
        byte[] y = System.BitConverter.GetBytes(transform.rotation.y);
        byte[] z = System.BitConverter.GetBytes(transform.rotation.z);

        byteArray[0] = 1;
        byteArray[1] = 1;
        byteArray[2] = 1;
        byteArray[3] = 1;

        byteArray[4] = x[0];
        byteArray[5] = x[1];
        byteArray[6] = x[2];
        byteArray[7] = x[3];

        byteArray[8] = y[0];
        byteArray[9] = y[1];
        byteArray[10] = y[2];
        byteArray[11] = y[3];

        byteArray[12] = z[0];
        byteArray[13] = z[1];
        byteArray[14] = z[2];
        byteArray[15] = z[3];

        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, byteArray);
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Shoot()
    {
        if(Time.time > rateOfFire + lastShot)
        {  
            GameObject newProjectile = Instantiate(projectile, firePosition.transform.position, firePosition.transform.rotation) as GameObject;
            lastShot = Time.time;
        }
    }

    void Rotate()
    {
        transform.LookAt(transform.position + new Vector3(CrossPlatformInputManager.GetAxis("Horizontal_Rotate"), 0, CrossPlatformInputManager.GetAxis("Vertical_Rotate")), Vector3.up);
        sendRotation();
        
        if (CrossPlatformInputManager.GetAxis("Horizontal_Rotate") != 0.0f || CrossPlatformInputManager.GetAxis("Vertical_Rotate") != 0.0f)
        {
            Shoot();
        }
    }

    void Move()
    {
        rb.velocity = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed, 0, CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed);
        sendTransform();
    }
}
