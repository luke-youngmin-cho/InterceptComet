using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "42";
        }
        
    }

    public void Login(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest { Email = email, Password = password};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    public void Login()
    {
        // for devices
    }
    public void Register(string email, string password, string userName)
    {
        var request = new RegisterPlayFabUserRequest { Email = email, Password = password, Username = userName };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result) => print("Registered Successfully");
    private void OnRegisterFailure(PlayFabError error) => print("Failed to register");
}
