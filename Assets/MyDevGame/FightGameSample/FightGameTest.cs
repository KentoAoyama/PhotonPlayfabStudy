//using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;

//public class FightGameTest : MonoBehaviourPunCallbacks
//{
//    void Start()
//    {
//        PhotonNetwork.ConnectUsingSettings();
//    }

//    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
//    public override void OnConnectedToMaster()
//    {
//        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
//        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
//    }

//    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
//    public override void OnJoinedRoom()
//    {
//        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
//        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
//        PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
//    }
//}