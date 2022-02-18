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
        // 마스터 클라이언트의 씬 자동 동기화옵션
        PhotonNetwork.AutomaticallySyncScene = true;
        // 게임 버전 설정
        PhotonNetwork.GameVersion = version;
        // 접속 유저 닉네임 설정
        PhotonNetwork.NickName = userId;

        // 포톤 서버와의 데이터의 초당 전송횟수
        Debug.Log(PhotonNetwork.SendRate);

        // 포톤서버 접속
        PhotonNetwork.ConnectUsingSettings(); 
    }
        // 포톤 서버에 접속 후 호출되는 콜백 함수
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
