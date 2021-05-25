using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Enums.SceneName mainMenuScene = Enums.SceneName.MainMenu;
    public Enums.SceneName loadGameScene = Enums.SceneName.LoadingScene;

    private void Start() 
    {
        AudioManager.instance.PlayBGM(4);

        if(PlayerController.playerInstance.gameObject != null)
        {
            PlayerController.playerInstance.gameObject.SetActive(false);
        }

        // if(GameMenu.instance.gameObject != null)
        // {
        //     GameMenu.instance.gameObject.SetActive(false);
        // }

        // if(BattleManager.instance.gameObject != null)
        // {
        //     BattleManager.instance.gameObject.SetActive(false);
        // }  

    }

    public void QuitToMain()
    {
        if(GameManager.instance.gameObject != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        if(PlayerController.playerInstance.gameObject != null)
        {
            Destroy(PlayerController.playerInstance.gameObject);
        }

        if(GameMenu.instance.gameObject != null)
        {
            Destroy(GameMenu.instance.gameObject);
        }

        if(AudioManager.instance.gameObject != null)
        {
            Destroy(AudioManager.instance.gameObject);
        }

        // if(BattleManager.instance.gameObject != null)
        // {
        //     Destroy(BattleManager.instance.gameObject);
        // }        

        SceneManager.LoadScene(mainMenuScene.ToString());

    }

    public void LoadLastSave()
    {

        if(GameManager.instance.gameObject != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        if(PlayerController.playerInstance.gameObject != null)
        {
            Destroy(PlayerController.playerInstance.gameObject);
        }

        if(GameMenu.instance.gameObject != null)
        {
            Destroy(GameMenu.instance.gameObject);
        }

        // if(BattleManager.instance.gameObject != null)
        // {
        //     Destroy(BattleManager.instance.gameObject);
        // }

        SceneManager.LoadScene(loadGameScene.ToString());

    }

}
