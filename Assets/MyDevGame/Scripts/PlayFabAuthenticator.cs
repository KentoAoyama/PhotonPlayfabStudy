using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabAuthenticator : MonoBehaviour
{

    private string _playFabPlayerIdCache;

    public void Awake()
    {
        AuthenticateWithPlayFab();
        DontDestroyOnLoad(gameObject);
    }

    /*
     * Step 1
     * 現在のPlayFabユーザーを通常の方法で認証します。
     * この場合、シンプルさのためにLoginWithCustomID API呼び出しを使用します。
     * 任意のログイン方法を選択することもできます。
     * デバイスの一意の識別子であるPlayFabSettings.DeviceUniqueIdentifierをカスタムIDとして使用します。
     * アカウントが存在しない場合には新規作成します。
     * 認証が成功した場合、次のステップとしてRequestPhotonTokenをコールバック関数として呼び出します。
     */
    private void AuthenticateWithPlayFab()
    {
        LogMessage("PlayFabを使用してカスタムIDで認証中...");

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CreateAccount = true,
            CustomId = PlayFabSettings.DeviceUniqueIdentifier
        }, RequestPhotonToken, OnPlayFabError);
    }

    /*
    * Step 2
    * PlayFabからPhotonの認証トークンを要求します。
    * これは重要なステップです。なぜなら、PhotonはPlayFabとは異なる認証トークンを使用するため、
    * PlayFabのSessionTicketを直接使用することはできず、明示的にトークンを要求する必要があるからです。
    * このAPI呼び出しでは、Photon App IDを渡す必要があります。
    * App IDはハードコーディングすることもできますが、この例では、
    * 便利なPhotonNetworkクラスの静的フィールドを使用してアクセスしています。
    * 要求が成功した場合、次のステップとしてAuthenticateWithPhotonをコールバックとして渡します。
    * 
    * ====要するに、ここでいう「トークン」とは、PlayFabからの応答として取得した====
    * ====Photonサーバーに対して認証を行うために使用するもの...らしい          ====
    */
    private void RequestPhotonToken(LoginResult obj)
    {
        LogMessage("PlayFabの認証が完了しました。Photonのトークンを要求中です...");
        // PlayFabIdを取得する。これは次のステップで使用
        _playFabPlayerIdCache = obj.PlayFabId;

        // Photonの認証トークンを取得する
        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.NetworkingClient.AppId
        }, AuthenticateWithPhoton, OnPlayFabError);
    }

    /*
     * Step 3
     * これは最終的かつ最も簡単なステップです。新しい AuthenticationValues インスタンスを作成します。
     * このクラスは、Photon環境内でプレイヤーを認証する方法を記述します。
     */
    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        LogMessage("Photonのトークンを取得しました: " + obj.PhotonCustomAuthenticationToken + "  認証完了");

        // AuthTypeをCustomに設定します。これは、PlayFabの認証手順を独自に提供することを意味します
        var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };

        // "username"パラメータを追加します。PlayFabはこのパラメータにプレイヤーのPlayFab ID (!) を含むことを期待しています
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);    // PlayFabのカスタム認証サービスが予期している形式

        // "token"パラメータを追加します。PlayFabは、前のステップで発行されたPhotonの認証トークンを含むことを期待しています。
        customAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

        // 最後に、この認証パラメータをアプリケーション全体で使用するようにPhotonに指示します
        PhotonNetwork.AuthValues = customAuth;
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        LogMessage(obj.GenerateErrorReport());
    }

    public void LogMessage(string message)
    {
        Debug.Log("PlayFab + Photon Example: " + message);
    }

    // Add small button to launch our example code
    public void OnGUI()
    {
        if (GUILayout.Button("Execute Example ")) ExecuteExample();
    }


    // Example code which raises custom room event, then sets custom room property
    private void ExecuteExample()
    {
        // Raise custom room event
        var data = new Dictionary<string, object>() { { "Hello", "World" } };
        var result = PhotonNetwork.RaiseEvent(1, data, new RaiseEventOptions(), new ExitGames.Client.Photon.SendOptions());
        LogMessage("New Room Event Post: " + result);

        // Set custom room property
        var properties = new ExitGames.Client.Photon.Hashtable() { { "CustomProperty", "It's Value" } };
        var expectedProperties = new ExitGames.Client.Photon.Hashtable();
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties, expectedProperties);
        LogMessage("New Room Properties Set");
    }
}