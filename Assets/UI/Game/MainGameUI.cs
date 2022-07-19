 using UnityEngine;
 using System.Collections;
 
 public class MainGameUI : MonoBehaviour {
     
    public GameObject GameCanvas;
    public GameObject MenuCanvas;
    public GameObject GameCamera;
    bool Paused = false;
 
    void Start()
    {
        GameCamera.gameObject.SetActive (true);
        GameCanvas.gameObject.SetActive (true);
        MenuCanvas.gameObject.SetActive (false);
    }
 
    void Update () 
    {
        if (Input.GetKeyDown ("p"))
            {
            if(Paused == true)
            {
                Time.timeScale = 1.0f;
                GameCamera.gameObject.SetActive (true);
                GameCanvas.gameObject.SetActive (true);
                MenuCanvas.gameObject.SetActive (false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Paused = false;
            } 
            else 
            {
                Time.timeScale = 0.0f;
                GameCamera.gameObject.SetActive (false);
                GameCanvas.gameObject.SetActive (false);
                MenuCanvas.transform.GetChild(0).gameObject.SetActive (true);
                MenuCanvas.transform.GetChild(1).gameObject.SetActive (false);
                MenuCanvas.gameObject.SetActive (true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Paused = true;
            }
        }
    }

    public void KeepPlaying()
    {
        Time.timeScale = 1.0f;
        GameCamera.gameObject.SetActive (true);
        GameCanvas.gameObject.SetActive (true);
        MenuCanvas.gameObject.SetActive (false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
 } 

