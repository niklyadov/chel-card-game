using UnityEngine;

public class GameOptionsController
{
    public const string KeyName = "__options";
    private GameOptions _gameOptions;

    public GameOptions Options 
    {
        get => _gameOptions;
        set 
        {
            // TODO: CHECK VALUES
            _gameOptions = value;
        }    
    }

    public GameOptionsController()
    {
        _gameOptions = PlayerPrefs.HasKey(KeyName) ? 
            GameOptions.CreateFromJSON(PlayerPrefs.GetString(KeyName)) : new GameOptions();
    }

    public void WriteOptions(GameOptions gameOptions = null) 
    {
        if (gameOptions == null) gameOptions = _gameOptions; // current options

        PlayerPrefs.SetString(KeyName, gameOptions.CreateJson());
        Debug.Log(gameOptions.CreateJson());
    }

    public void ResetOptions()
    {
        WriteOptions(new GameOptions());
    }
}