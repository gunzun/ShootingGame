/*using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menuBack;
    public GameObject manual;
    public GameObject Story;
    public GameObject Rank;
    public GameObject Setting;
    public GameObject Play;
    public GameObject Exit;
    public Text rankText;

    public Image BackMusic;
    public Image Backsound;

    public Sprite[] OnOff;

    void Start()
    {

        BackMusic.sprite = OnOff[GameDataManager.Instance.isMusic];
        Backsound.sprite = OnOff[GameDataManager.Instance.issound];
    }

    void Update()
    {
        *//*// test close, open animation 
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            OpenMenuBack();
        }
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            CloseMenuBack();
        }*//*


        // Start Animation when click btn
    }

    void OpenRank()
    {
        Rank.SetActive(true);
        Rank.GetComponent<Animator>().SetTrigger("Open");

        if (GameDataManager.Instance.gameDataGroup.rank.Length != 0)
        {
            rankText.text = "";
            for (int i = 0; i < GameDataManager.Instance.gameDataGroup.rank.Length; i++)
            {
                rankText.text += (int)(GameDataManager.Instance.gameDataGroup.rank[i].stageTime / 60.0f) +
                " '' " + (int)(GameDataManager.Instance.gameDataGroup.rank[i].stageTime % 60.0f) +
                "   SCORE : " + GameDataManager.Instance.gameDataGroup.rank[i].stageScore + "\n";
                ;
            }
        }
    }

    public void BtnBackMusic()
    {
        if (GameDataManager.Instance.isMusic == 0)
        {
            GameDataManager.Instance.isMusic = 1;
        }
        else
        {
            GameDataManager.Instance.isMusic = 0;
        }

        PlayerPrefs.SetInt("Music", GameDataManager.Instance.isMusic);
        BackMusic.sprite = OnOff[GameDataManager.Instance.isMusic];
    }

    public void BtnBackSound()
    {
        if (GameDataManager.Instance.issound == 0)
        {
            GameDataManager.Instance.issound = 1;
        }
        else
        {
            GameDataManager.Instance.issound = 0;
        }

        PlayerPrefs.SetInt("Sound", GameDataManager.Instance.issound);
        Backsound.sprite = OnOff[GameDataManager.Instance.issound];
    }


    // MenuBack
    void OpenMenuBack()
    {
        menuBack.gameObject.SetActive(true);
        menuBack.GetComponent<Animator>().SetTrigger("Open");
    }
    void CloseMenuBack()
    {
        // Story.gameObject.SetActive(false);
        // manual.gameObject.SetActive(false);
        menuBack.GetComponent<Animator>().SetTrigger("Close");
    }
    //setting
    void OpenSetting()
    {
        Setting.SetActive(true);
        Setting.GetComponent<Animator>().SetTrigger("Open");
    }
    void CloseSetting()
    {
        Setting.GetComponent<Animator>().SetTrigger("Close");
    }
    public void SettingBtn_OnClick()
    {
        CloseMenuBack();
        Invoke("OpenSetting", 1.5f);
    }
    public void SettingPrevBtn()
    {
        CloseSetting();
        Invoke("OpenMenuBack", 1.5f);
    }

    // story
    void OpenStory()
    {
        Story.SetActive(true);
        Story.GetComponent<Animator>().SetTrigger("Open");
    }
    void CloseStory()
    {
        Story.GetComponent<Animator>().SetTrigger("Close");
    }

    public void StoryBtn_OnClick()
    {
        CloseMenuBack();
        Invoke("OpenStory", 1.5f);
    }
    public void StoryPrevBtn_OnClick()
    {
        CloseStory();
        Invoke("OpenMenuBack", 1.5f);
    }
    // manual
    void OpenManual()
    {
        manual.SetActive(true);
        manual.GetComponent<Animator>().SetTrigger("Open");
    }
    void CloseManual()
    {
        manual.GetComponent<Animator>().SetTrigger("Close");
    }
    public void ManualPrevBtn_OnClick()
    {
        CloseManual();
        Invoke("OpenMenuBack", 1.5f);
    }
    public void ManualBtn_OnClick()
    {
        CloseMenuBack();
        Invoke("OpenManual", 1.5f);

    }
    // Rank
    void OpenRankBtn()
    {
        OpenRank();
        Rank.GetComponent<Animator>().SetTrigger("Open");
    }
    void CloseRankBtn()
    {
        Rank.GetComponent<Animator>().SetTrigger("Close");
    }
    public void RankBtn_OnClick()
    {
        CloseMenuBack();
        Invoke("OpenRankBtn", 1.5f);
    }
    public void RankPrevBtn()
    {
        CloseRankBtn();
        Invoke("OpenMenuBack", 1.5f);
    }

    // play
    public void OpenPlay()
    {
        Play.SetActive(true);
        Play.GetComponent<Animator>().SetTrigger("Open");
    }
    public void ClosePlay()
    {
        Play.GetComponent<Animator>().SetTrigger("Close");
    }
    public void PlayBtn_OnClick()
    {
        CloseMenuBack();
        Invoke("OpenPlay", 1.5f);
    }
    public void ExitBtn_OnClick()
    {
        ClosePlay();
        Invoke("OpenMenuBack", 1.5f);
    }
    // Exit

}*/