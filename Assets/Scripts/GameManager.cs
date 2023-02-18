using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Meta meta;
    [SerializeField] private PlayerManager player;
    [SerializeField] private PlayerManager player2;
    //[SerializeField] private SoundManager soundManager;
    [SerializeField] private UIManager uiManager;

    private void OnEnable() { Subscription(true); }
    private void OnDisable() { Subscription(false); }

    private void Subscription(bool subscribe)
    {
        if (subscribe)
        {
            if (meta != null)
                addMetaEvents();
            if (player != null)
                addPlayerEvents();
            if (player2 != null)
            addPlayer2Events();
        }
        else
        {
            if (meta != null)
                deleteMetaEvents();
            if (player != null)
                deletePlayerEvents();
            if (player2 != null)
            deletePlayer2Events();
        }
    }

    private void addMetaEvents()
    {
        meta.OnPlayerTouch += nextLevel;
        //meta.OnMetaPlaySound += playVictorySound;
    }

    private void deleteMetaEvents()
    {
        meta.OnPlayerTouch -= nextLevel;
        //meta.OnMetaPlaySound -= playVictorySound;
    }

    private void addPlayerEvents()
    {
        player.OnRestartLevel += restartLevel;
        player.OnNextLevel += nextLevel;
        //player.OnDeathPlaySound += playLoseSound;
        //player.OnChocarPlaySound += playChocarSound;
        player.OnIrAlMenu += goToMenu;
    }

    private void deletePlayerEvents()
    {
        player.OnRestartLevel -= restartLevel;
        player.OnNextLevel -= nextLevel;
        //player.OnDeathPlaySound -= playLoseSound;
        //player.OnChocarPlaySound -= playChocarSound;
        //player.OnMeMuero -= death;
        player.OnIrAlMenu -= goToMenu;
    }

    private void addPlayer2Events()
    {
        player.OnRestartLevel += restartLevel;
        player.OnNextLevel += nextLevel;
        //player.OnDeathPlaySound += playLoseSound;
        //player.OnChocarPlaySound += playChocarSound;
        player.OnIrAlMenu += goToMenu;
    }

    private void deletePlayer2Events()
    {
        player.OnRestartLevel -= restartLevel;
        player.OnNextLevel -= nextLevel;
        //player.OnDeathPlaySound -= playLoseSound;
        //player.OnChocarPlaySound -= playChocarSound;
        //player.OnMeMuero -= death;
        player.OnIrAlMenu -= goToMenu;
    }

    public void death()
    {
        uiManager.diedMenuActivation();
    }

    public void goToMenu()
    {
        uiManager.pauseMenuChangeState();
    }

    public void nextLevel()
    {
        uiManager.nextLevel();
    }

    public void restartLevel()
    {
        uiManager.replayScene();
    }
}
