// Copyright (C) 2016-2019 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine.Networking;

using CCGKit;

#pragma warning disable 618

/// <summary>
/// This classes manages the game scene.
/// </summary>
public class GameScene : BaseScene
{
    public PopupChat chatPopup;

    private void Start()
    {
        OpenPopup<PopupLoading>("PopupLoading", popup =>
        {
            popup.text.text = "Waiting for game to start...";
        });
        if (GameNetworkManager.Instance.isSinglePlayer)
        {
            Invoke("AddBot", 1.5f);
        }
    }

    private void AddBot()
    {
        ClientScene.AddPlayer(1);
    }

    public void CloseWaitingWindow()
    {
        ClosePopup();
    }

    /// <summary>
    /// Callback for when the end turn button is pressed.
    /// </summary>
    public void OnEndTurnButtonPressed()
    {
        var localPlayer = NetworkingUtils.GetLocalPlayer() as DemoHumanPlayer;
        if (localPlayer != null)
        {
            localPlayer.StopTurn();
        }
    }

    /// <summary>
    /// Callback for when the exit game button is pressed.
    /// </summary>
    public void OnExitGameButtonPressed()
    {
        OpenPopup<PopupTwoButtons>("PopupTwoButtons", popup =>
        {
            popup.text.text = "Do you want to leave this game?";
            popup.buttonText.text = "Yes";
            popup.button2Text.text = "No";
            popup.button.onClickEvent.AddListener(() =>
            {
                if (NetworkingUtils.GetLocalPlayer().isServer)
                {
                    GameNetworkManager.Instance.StopHost();
                }
                else
                {
                    GameNetworkManager.Instance.StopClient();
                }
            });
            popup.button2.onClickEvent.AddListener(() => { popup.Close(); });
        });
    }

    /// <summary>
    /// Callback for when the chat button is pressed.
    /// </summary>
    public void OnChatButtonPressed()
    {
        chatPopup.Show();
    }
}
