using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndState : MonoBehaviour
{
    public static EndState Instance { get; private set; }

    #region Inspector Fields
    public List<BackgroundScrollManager> scrollManagers = new List<BackgroundScrollManager>();
    public GameObject gameOverPanel = null;
    public GameObject jumpButton = null;
    #endregion

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnEndState()
    {
        foreach (BackgroundScrollManager bsm in scrollManagers)
        {
            bsm.Pause();
        }

        Player.Instance.Jumping.canAct = false;
        Player.Instance.Running.FreezeMovement();

        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
        }
        if (jumpButton)
        {
            jumpButton.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}