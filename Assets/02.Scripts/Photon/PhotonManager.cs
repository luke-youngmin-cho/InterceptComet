using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0";
    private string userId = "Luke";
    private void Awake()
    {
        // ������ Ŭ���̾�Ʈ�� �� �ڵ� ����ȭ�ɼ�
        PhotonNetwork.AutomaticallySyncScene = true;
        // ���� ���� ����
        PhotonNetwork.GameVersion = version;
        // ���� ���� �г��� ����
        PhotonNetwork.NickName = userId;

        // ���� �������� �������� �ʴ� ����Ƚ��
        Debug.Log(PhotonNetwork.SendRate);

        // ���漭�� ����
        PhotonNetwork.ConnectUsingSettings(); 
    }
        // ���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        Debug.Log($"PhotonNetwork.IsConnected = {PhotonNetwork.IsConnected}");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JointRandom Failed {returnCode} : {message}");

        RoomOptions ro = new RoomOptions();

    }
}
