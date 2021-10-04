using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private Camera farCamera;
    private Camera closeCamera;
    private Canvas canvas;
    public MainCharacter player;

    // Start is called before the first frame update
    void Start()
    { 
        farCamera = (Camera) GameObject.Find("Far Camera").GetComponent<Camera>();
        closeCamera = (Camera) GameObject.Find("Close Camera").GetComponent<Camera>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        canvas.worldCamera = farCamera.gameObject.GetComponent<Camera>();
        farCamera.gameObject.SetActive(true);
        closeCamera.gameObject.SetActive(false);
        farCamera.enabled = true;
        closeCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.CloseToGround())
        {
            canvas.worldCamera = closeCamera;
            farCamera.gameObject.SetActive(false);
            closeCamera.gameObject.SetActive(true);
            farCamera.enabled = false;
            closeCamera.enabled = true;
        } else
        {
            canvas.worldCamera = farCamera;
            farCamera.gameObject.SetActive(true);
            closeCamera.gameObject.SetActive(false);
            farCamera.enabled = true;
            closeCamera.enabled = false;
        }
    }
}
