using Photon.Pun;

using TMPro;

using UnityEngine;

public class Poton_CreateOrJoin : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _roomNameTMP;
    [SerializeField] GameObject _roomLoading;

    #region Methods
    public void Button_CreateRoom()
    {
        PhotonNetwork.CreateRoom(_roomNameTMP.text);
        _roomLoading.SetActive(true);
    }

    public void Button_JoinRoom()
    {
        PhotonNetwork.JoinRoom(_roomNameTMP.text);
        _roomLoading.SetActive(true);
    }

    public void Button_JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        _roomLoading.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("CoreGame");
        _roomLoading.SetActive(false);
    }

    #region Fails
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _roomLoading.SetActive(false);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _roomLoading.SetActive(false);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _roomLoading.SetActive(false);
    }
    #endregion
    #endregion
}
