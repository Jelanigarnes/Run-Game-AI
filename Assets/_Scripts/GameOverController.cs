using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Title");
    }
    public void Quit()
    {
        GameManager.Instance.Quit();
    }
}
