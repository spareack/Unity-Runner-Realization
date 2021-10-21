using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCheckInGame : MonoBehaviour
{
    public Manager Mng;
    public bike bk;
    public GameObject AchievementPanel;
    public GameObject[] AchievementLevelUp;
    public Animator CloseAchievAnim;
    public Animator OpenAchievAnim;
    public int[,] AchievementChallengeInt = new int[20, 5] { { 25, 1500, 50000, 1000000, 1000000 }, { 1, 5, 25, 100, 100 }, { 2, 5, 9, 12, 12 }, { 2, 15, 111, 777, 777 }, { 13, 91, 666, 4664, 4664 },
                                                             { 100, 1000, 10000, 100000, 100000 }, { 1, 7, 14, 35, 35 }, { 100, 1500, 35000, 250000, 250000}, { 7, 70, 700, 7000, 7000 }, { 47, 470, 4700, 47000, 47000 },
                                                             { 50, 500, 5000, 50000, 50000 }, { 5, 55, 605, 6655, 6655 }, { 20, 100, 500, 2500, 2500 }, { 125, 500, 2000, 8000, 8000 }, { 1, 2, 3, 4, 4 },
                                                             { 300, 900, 3000, 9000, 9000 }, { 3, 7, 21, 30, 30 }, { 1, 7, 20, 100, 100}, { 1, 2, 5, 10, 10 }, { 1, 2, 3, 5, 5 } };
    public int AchievComplitedCount = 0 ; 
    public Sprite[] AchievementIcon;
    public Sprite[] AchievementLevelIcon;

    public AchievementSave AS = new AchievementSave();

    private void Start()
    {
        AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
    }

    public void CheckAchievement()
    {
        for (int i = 0; i < 4; i++)
        {

            if (Mng.AS.AchievementsLevel[0] == i && Mng.AS.corn >= AchievementChallengeInt[0, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[0], 0);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.corn + Mng.corn;
                Mng.AS.AchievementsLevel[0] += 1;
                bk.AchievCheckSave[0] = Mng.AS.AchievementsLevel[0];
                Mng.AS.WhatAchievemntHavePrize[0] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[0], 0));
                ASlider[0].value = Mng.AS.corn + Mng.corn;
                AchivComplitSave(0);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[4] == i && Mng.AS.DeathShit >= AchievementChallengeInt[4, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[4], 4);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.DeathShit;
                Mng.AS.AchievementsLevel[4] += 1;
                bk.AchievCheckSave[4] = Mng.AS.AchievementsLevel[4];
                Mng.AS.WhatAchievemntHavePrize[4] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[4], 4));
                ASlider[0].value = Mng.AS.DeathShit;
                AchivComplitSave(4);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[5] == i && Mng.scoreReal >= AchievementChallengeInt[5, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[5], 5);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = PlayerPrefs.GetInt("Score");
                Mng.AS.AchievementsLevel[5] += 1;
                bk.AchievCheckSave[5] = Mng.AS.AchievementsLevel[5];
                Mng.AS.WhatAchievemntHavePrize[5] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[5], 5));
                ASlider[0].value = PlayerPrefs.GetInt("Score");
                AchivComplitSave(5);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[7] == i && Mng.AS.СatapultMax >= AchievementChallengeInt[7, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[7], 7);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.СatapultMax;
                Mng.AS.AchievementsLevel[7] += 1;
                bk.AchievCheckSave[7] = Mng.AS.AchievementsLevel[7];
                Mng.AS.WhatAchievemntHavePrize[7] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[7], 7));
                ASlider[0].value = Mng.AS.СatapultMax;
                AchivComplitSave(7);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[8] == i && Mng.AS.SecondChanceMax >= AchievementChallengeInt[8, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[8], 8);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.SecondChanceMax;
                Mng.AS.AchievementsLevel[8] += 1;
                bk.AchievCheckSave[8] = Mng.AS.AchievementsLevel[8];
                Mng.AS.WhatAchievemntHavePrize[8] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[8], 8));
                ASlider[0].value = Mng.AS.SecondChanceMax;
                AchivComplitSave(8);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[9] == i && Mng.AS.SupermanMax >= AchievementChallengeInt[9, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[9], 9);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.SupermanMax;
                Mng.AS.AchievementsLevel[9] += 1;
                bk.AchievCheckSave[9] = Mng.AS.AchievementsLevel[9];
                Mng.AS.WhatAchievemntHavePrize[9] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[9], 9));
                ASlider[0].value = Mng.AS.SupermanMax;
                AchivComplitSave(9);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[10] == i && Mng.DivergentCheck == false && Mng.scoreReal >= AchievementChallengeInt[10, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[10], 10);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.DivergentMax;
                Mng.AS.AchievementsLevel[10] += 1;
                bk.AchievCheckSave[10] = Mng.AS.AchievementsLevel[10];
                Mng.AS.WhatAchievemntHavePrize[10] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[10], 10));
                ASlider[0].value = Mng.AS.DivergentMax;
                AchivComplitSave(10);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[11] == i && Mng.AS.ProtectionShit >= AchievementChallengeInt[11, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[11], 11);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.ProtectionShit;
                Mng.AS.AchievementsLevel[11] += 1;
                bk.AchievCheckSave[11] = Mng.AS.AchievementsLevel[11];
                Mng.AS.WhatAchievemntHavePrize[11] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[11], 11));
                ASlider[0].value = Mng.AS.ProtectionShit;
                AchivComplitSave(11);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[12] == i && Mng.AS.LazyMax >= AchievementChallengeInt[12, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[12], 12);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.LazyMax;
                Mng.AS.AchievementsLevel[12] += 1;
                bk.AchievCheckSave[12] = Mng.AS.AchievementsLevel[12];
                Mng.AS.WhatAchievemntHavePrize[12] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[12], 12));
                ASlider[0].value = Mng.AS.LazyMax;
                AchivComplitSave(12);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[13] == i && poo.JesusCheck == false && Mng.scoreReal >= AchievementChallengeInt[13, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[13], 13);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = Mng.AS.JesusMax;
                Mng.AS.AchievementsLevel[13] += 1;
                bk.AchievCheckSave[13] = Mng.AS.AchievementsLevel[13];
                Mng.AS.WhatAchievemntHavePrize[13] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[13], 13));
                ASlider[0].value = Mng.AS.JesusMax;
                AchivComplitSave(13);
                Mng.SaveAchuevEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (Mng.AS.AchievementsLevel[15] == i && PlayerPrefs.GetInt("Money") >= AchievementChallengeInt[15, i])
            {
                AchievementOpen(Mng.AS.AchievementsLevel[15], 15);
                Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
                ASlider[0].value = PlayerPrefs.GetInt("Money");
                Mng.AS.AchievementsLevel[15] += 1;
                bk.AchievCheckSave[15] = Mng.AS.AchievementsLevel[15];
                Mng.AS.WhatAchievemntHavePrize[15] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(Mng.AS.AchievementsLevel[15], 15));
                ASlider[0].value = PlayerPrefs.GetInt("Money");
                AchivComplitSave(15);
                Mng.SaveAchuevEbanoe();
            }
        }
    }
    public void AchievementOpen(int level, int WhatAchiev)
    {
        Image[] APanel = AchievementPanel.GetComponentsInChildren<Image>();
        Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
        Text[] AText = AchievementPanel.GetComponentsInChildren<Text>();
        ASlider[0].maxValue = AchievementChallengeInt[WhatAchiev, level];
        APanel[1].GetComponent<Image>().sprite = AchievementIcon[WhatAchiev];
        AText[1].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[WhatAchiev];
        AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge0[WhatAchiev];
        if (level == 1)
        {
            APanel[5].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge1[WhatAchiev];
        }
        if (level == 2)
        {
            APanel[5].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            APanel[6].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge2[WhatAchiev];
        }
        if (level == 3)
        {
            APanel[5].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            APanel[6].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            APanel[7].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[WhatAchiev];
        }
        OpenAchievAnim.SetTrigger("IsBegin");
    }

    IEnumerator AchievementOpenWithCoroutine(int level, int WhatAchiev)
    {
        yield return new WaitForSeconds(1);
        Image[] APanel = AchievementPanel.GetComponentsInChildren<Image>();
        Slider[] ASlider = AchievementPanel.GetComponentsInChildren<Slider>();
        Text[] AText = AchievementPanel.GetComponentsInChildren<Text>();
        ASlider[0].maxValue = AchievementChallengeInt[WhatAchiev, level];
        if (level == 1)
        {
            AchievementLevelUp[0].SetActive(true);
            yield return new WaitForSeconds(1);
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge1[WhatAchiev];
        }
        if (level == 2)
        {
            AchievementLevelUp[1].SetActive(true);
            yield return new WaitForSeconds(1);
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge2[WhatAchiev];
        }
        if (level == 3)
        {
            AchievementLevelUp[2].SetActive(true);
            yield return new WaitForSeconds(1);
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[WhatAchiev];
        }
        if (level == 4)
        {
            AchievementLevelUp[2].SetActive(true);
            yield return new WaitForSeconds(1);
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[WhatAchiev];
        }
        yield return new WaitForSeconds(1);
        CloseAchievAnim.SetTrigger("IsEnd");
        yield return new WaitForSeconds(1);
        AchievementLevelUp[0].SetActive(false);
        AchievementLevelUp[1].SetActive(false);
        AchievementLevelUp[2].SetActive(false);
        AchievementLevelUp[3].SetActive(false);
        APanel[5].GetComponent<Image>().sprite = AchievementLevelIcon[1];
        APanel[6].GetComponent<Image>().sprite = AchievementLevelIcon[1];
        APanel[7].GetComponent<Image>().sprite = AchievementLevelIcon[1];
        APanel[8].GetComponent<Image>().sprite = AchievementLevelIcon[1];
    }
    public void AchivComplitSave(int num)
    {
        AchievComplitedCount = 0;
        for (int i = 0; i < 20; i++)
        {
            if (Mng.WhatAchievComplited[i] != -1) AchievComplitedCount += 1;
        }
        Mng.WhatAchievComplited[AchievComplitedCount] = num;
    }
}
