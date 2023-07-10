using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using Cysharp.Threading.Tasks;
using System;

public class PlayfabLoginAsyncExample : MonoBehaviour
{
    void Start()
    {
        Login().Forget();
    }

    private async UniTaskVoid Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        var tcs = new UniTaskCompletionSource<LoginResult>();

        PlayFabClientAPI.LoginWithCustomID(
            request,
            result => tcs.TrySetResult(result),
            error => tcs.TrySetException(
                new Exception(error.GenerateErrorReport())));

        try
        {
            var result = await tcs.Task;
        }
        catch(Exception exception)
        {
            Debug.LogError($"LoginFailure!!  {exception}");
        }

    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log($"LoginSuccess!!  {result}");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log($"LoginFailure!!  {error.GenerateErrorReport()}");
    }
}
