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
     * ���݂�PlayFab���[�U�[��ʏ�̕��@�ŔF�؂��܂��B
     * ���̏ꍇ�A�V���v�����̂��߂�LoginWithCustomID API�Ăяo�����g�p���܂��B
     * �C�ӂ̃��O�C�����@��I�����邱�Ƃ��ł��܂��B
     * �f�o�C�X�̈�ӂ̎��ʎq�ł���PlayFabSettings.DeviceUniqueIdentifier���J�X�^��ID�Ƃ��Ďg�p���܂��B
     * �A�J�E���g�����݂��Ȃ��ꍇ�ɂ͐V�K�쐬���܂��B
     * �F�؂����������ꍇ�A���̃X�e�b�v�Ƃ���RequestPhotonToken���R�[���o�b�N�֐��Ƃ��ČĂяo���܂��B
     */
    private void AuthenticateWithPlayFab()
    {
        LogMessage("PlayFab���g�p���ăJ�X�^��ID�ŔF�ؒ�...");

        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CreateAccount = true,
            CustomId = PlayFabSettings.DeviceUniqueIdentifier
        }, RequestPhotonToken, OnPlayFabError);
    }

    /*
    * Step 2
    * PlayFab����Photon�̔F�؃g�[�N����v�����܂��B
    * ����͏d�v�ȃX�e�b�v�ł��B�Ȃ��Ȃ�APhoton��PlayFab�Ƃ͈قȂ�F�؃g�[�N�����g�p���邽�߁A
    * PlayFab��SessionTicket�𒼐ڎg�p���邱�Ƃ͂ł����A�����I�Ƀg�[�N����v������K�v�����邩��ł��B
    * ����API�Ăяo���ł́APhoton App ID��n���K�v������܂��B
    * App ID�̓n�[�h�R�[�f�B���O���邱�Ƃ��ł��܂����A���̗�ł́A
    * �֗���PhotonNetwork�N���X�̐ÓI�t�B�[���h���g�p���ăA�N�Z�X���Ă��܂��B
    * �v�������������ꍇ�A���̃X�e�b�v�Ƃ���AuthenticateWithPhoton���R�[���o�b�N�Ƃ��ēn���܂��B
    * 
    * ====�v����ɁA�����ł����u�g�[�N���v�Ƃ́APlayFab����̉����Ƃ��Ď擾����====
    * ====Photon�T�[�o�[�ɑ΂��ĔF�؂��s�����߂Ɏg�p�������...�炵��          ====
    */
    private void RequestPhotonToken(LoginResult obj)
    {
        LogMessage("PlayFab�̔F�؂��������܂����BPhoton�̃g�[�N����v�����ł�...");
        // PlayFabId���擾����B����͎��̃X�e�b�v�Ŏg�p
        _playFabPlayerIdCache = obj.PlayFabId;

        // Photon�̔F�؃g�[�N�����擾����
        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.NetworkingClient.AppId
        }, AuthenticateWithPhoton, OnPlayFabError);
    }

    /*
     * Step 3
     * ����͍ŏI�I���ł��ȒP�ȃX�e�b�v�ł��B�V���� AuthenticationValues �C���X�^���X���쐬���܂��B
     * ���̃N���X�́APhoton�����Ńv���C���[��F�؂�����@���L�q���܂��B
     */
    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        LogMessage("Photon�̃g�[�N�����擾���܂���: " + obj.PhotonCustomAuthenticationToken + "  �F�؊���");

        // AuthType��Custom�ɐݒ肵�܂��B����́APlayFab�̔F�؎菇��Ǝ��ɒ񋟂��邱�Ƃ��Ӗ����܂�
        var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };

        // "username"�p�����[�^��ǉ����܂��BPlayFab�͂��̃p�����[�^�Ƀv���C���[��PlayFab ID (!) ���܂ނ��Ƃ����҂��Ă��܂�
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);    // PlayFab�̃J�X�^���F�؃T�[�r�X���\�����Ă���`��

        // "token"�p�����[�^��ǉ����܂��BPlayFab�́A�O�̃X�e�b�v�Ŕ��s���ꂽPhoton�̔F�؃g�[�N�����܂ނ��Ƃ����҂��Ă��܂��B
        customAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

        // �Ō�ɁA���̔F�؃p�����[�^���A�v���P�[�V�����S�̂Ŏg�p����悤��Photon�Ɏw�����܂�
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