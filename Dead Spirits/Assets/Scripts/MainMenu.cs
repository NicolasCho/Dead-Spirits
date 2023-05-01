using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject player;

    public void PlayGame()
    {
        SceneManager.LoadScene("RoomStart");
    }

    public void QuitGame()
    {
        Debug.Log("Quit fui");
        Application.Quit();
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        Time.timeScale = 1.0f;
    }

    public void RestartGame()
    {
        gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        Time.timeScale = 1.0f;
        player.GetComponent<PlayerManager>().RespawnPlayer();
    }
}
