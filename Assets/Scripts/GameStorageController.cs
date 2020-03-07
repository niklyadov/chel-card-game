using UnityEngine;

public class GameStorageController
{
    private string KeyName_options      = "__gameOptions";
    private string KeyName_achievements = "__gameAchievements";
    
    private GameOptions _localOptions;
    private GameAchievements _localAchievements;
    
    public GameStorageController()
    {
        _localOptions = ReadOptions();
        _localAchievements = ReadAchievements();
    }
    
    public GameOptions Options 
    {
        get => _localOptions;
        set => _localOptions = value;
    }

    private GameOptions ReadOptions()
    {
        return PlayerPrefs.HasKey(KeyName_options) ? 
            GameOptions.CreateFromJSON(PlayerPrefs.GetString(KeyName_options)) : new GameOptions();
    }
    
    public void WriteOptions(GameOptions gameOptions = null) 
    {
        if (gameOptions == null) gameOptions = _localOptions; // current options

        PlayerPrefs.SetString(KeyName_options, gameOptions.CreateJson());
        Debug.Log(gameOptions.CreateJson());
        _localOptions = ReadOptions();
    }

    public void ResetOptions()
    {
        WriteOptions(new GameOptions());
    }
    
    public GameAchievements Achievements 
    {
        get => _localAchievements;
        set => _localAchievements = value;
    }
    
    private GameAchievements ReadAchievements()
    {
        return PlayerPrefs.HasKey(KeyName_achievements) ? 
            GameAchievements.CreateFromJSON(PlayerPrefs.GetString(KeyName_achievements)) : new GameAchievements();
    }

    public void UpdateAchievements(GameAchievements gameAchievements = null) 
    {
        if (gameAchievements == null) gameAchievements = _localAchievements; // current options

        PlayerPrefs.SetString(KeyName_achievements, gameAchievements.CreateJson());
        Debug.Log(gameAchievements.CreateJson());
        _localAchievements = ReadAchievements();
    }

    public void ResetAchievements()
    {
        UpdateAchievements(new GameAchievements());
    }
}