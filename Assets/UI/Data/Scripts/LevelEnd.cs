using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EndlevelScene();   
        }
    }

    void EndlevelScene()
    {
        pauseMenuUI2.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitToEndScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("EndScreen");
    }
}
