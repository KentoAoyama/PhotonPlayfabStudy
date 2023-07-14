//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using PlayFab;
//using PlayFab.ClientModels;

//public class PlayfabLoginExampleUnitySDK : MonoBehaviour
//{
//    void Start()
//    {
//        Login();
//    }

//    private void Login()
//    {
//        var request = new LoginWithCustomIDRequest
//        {
//            CustomId = SystemInfo.deviceUniqueIdentifier,
//            CreateAccount = true
//        };

//        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
//    }

//    private void OnLoginSuccess(LoginResult result)
//    {
//        Debug.Log($"LoginSuccess!!  {result}");
//    }

//    private void OnLoginFailure(PlayFabError error)
//    {
//        Debug.Log($"LoginFailure!!  {error.GenerateErrorReport()}");
//    }
//}
