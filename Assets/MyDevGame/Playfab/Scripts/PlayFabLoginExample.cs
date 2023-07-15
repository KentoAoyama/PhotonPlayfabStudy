
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
public class PlayFabLogin : MonoBehaviour
{
    private void Start()
    {
        LoginStart().Forget();
    }

    private async UniTask LoginStart()
    {
        PlayFabSettings.staticSettings.TitleId = PlayfabTitleID.ID;

        var request = new LoginWithCustomIDRequest
        {
            CustomId = "GettingStartedGuide",
            CreateAccount = true
        };

        var result = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

        var message = result.Error is null
            ? $"Login success! My PlayFabID is {result.Result.PlayFabId}"
            : $"Login failed... {result.Error.ErrorMessage}";
         
        Debug.Log(message);
    }
}