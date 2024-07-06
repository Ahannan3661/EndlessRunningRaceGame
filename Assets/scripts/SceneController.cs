using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField] GameObject[] homeObjects;
    [SerializeField] GameObject[] introObjects;
    [SerializeField] GameObject[] raceObjects;
    [SerializeField] GameObject[] endObjects;
    public GameObject pauseBG;

    public AudioSource bgMusic;
    public AudioSource crowd;
    public AudioSource buttonClick;
    public AudioSource star;

    public RunnerPlayer player;
    public RunnerAI AI;

    [SerializeField] GameObject[] objectsToKeep;

    public void IntroScreen()
    {
        buttonClick.Play();
        foreach (GameObject go in homeObjects)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in introObjects)
        {
            go.SetActive(true);
        }
    }

    public void RaceScreen()
    {
        buttonClick.Play();
        foreach (GameObject go in introObjects)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in raceObjects)
        {
            go.SetActive(true);
        }
        crowd.Play();
    }
    private GameObject pauseObject;

    public void PauseGame(GameObject pauseScreen)
    {
        star.Play();
        crowd.Pause();
        foreach(GameObject go in raceObjects)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in objectsToKeep)
        {
            go.SetActive(true);
        }
        pauseBG.SetActive(true);
        pauseObject = pauseScreen;
        pauseScreen.SetActive(true);
    }
    public void ContinueGame()
    {
        crowd.Play();
        pauseObject.SetActive(false);
        pauseBG.SetActive(false);
        foreach (GameObject go in raceObjects)
        {
            go.SetActive(true);
        }
        player.Run();
        AI.AIRun();
    }

    public void FinalScreen()
    {
        crowd.Stop();
        foreach(GameObject game in raceObjects)
        {
            game.SetActive(false);
        }
        foreach(GameObject game in endObjects)
        {
            game.SetActive(true);
        }

        StartCoroutine(reloadGame());
    }

    IEnumerator reloadGame()
    {
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene("SampleScene");
    }
}
