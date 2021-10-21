using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LanguageSistem : MonoBehaviour
{
    private string json;
    public static languages lng = new languages();
    public TextAsset test1;

    private AchievementSave AS = new AchievementSave();

    private string currentLanguage;
    private Dictionary<string, string> localizedText;
    private static bool isReady = false;

    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetString("Language"));
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
                AS.WhatIsLanguage = 5;
            }
            else if (Application.systemLanguage == SystemLanguage.German)
            {
                PlayerPrefs.SetString("Language", "de_GE");
                AS.WhatIsLanguage = 3;
            }
            else if (Application.systemLanguage == SystemLanguage.Portuguese)
            {
                PlayerPrefs.SetString("Language", "pt_PO");
                AS.WhatIsLanguage = 0;
            }
            else
            {
                PlayerPrefs.SetString("Language", "en_US");
                AS.WhatIsLanguage = 2;
            }
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
        LanguageLoad();
    }

    public void LanguageLoad()
    {
#if UNITY_ANDROID
        string path = Path.Combine(Application.streamingAssetsPath + "/Language/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(path);
        //UnityWebRequest unityWebRequest = new UnityWebRequest(path);
        while (!reader.isDone) { }
        json = reader.text;
        lng = JsonUtility.FromJson<languages>(json);
#endif
#if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/Language/" + PlayerPrefs.GetString("Language") + ".json");
        lng = JsonUtility.FromJson<languages>(json);
#endif
    }


}


public class languages
{
    public string PlayText;
    public string MusicSettingsText;
    public string SoundSettingsText;
    public string SaveSettingsText;
    public string CallSettingsText;
    public string SoonText;
    public string BaffTimeText;
    public string[] SkillWay = new string[10];
    public string[] SkillName = new string[50];
    public string[] SkillDiscription = new string[50];
    public string[] AchievementName = new string[50];
    public string[] AchievementChallenge0 = new string[50];
    public string[] AchievementChallenge1 = new string[50];
    public string[] AchievementChallenge2 = new string[50];
    public string[] AchievementChallenge3 = new string[50];
    public string[] UpgradeNameText = new string[20];
    public string[] UpgradeDoItText = new string[20];
    public string[] UpgradeBuyText = new string[20];
    public string[] ShopBuyText = new string[20];
    public string[] LootPrizeText = new string[20];
    public string[] LootInfoText = new string[20];
    public string[] TitlesText = new string[7];
    public string[] LoadingText = new string[25];
    public string[] DeathScreenText = new string[10];
}
