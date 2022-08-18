using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public static DailyReward Instance;

    public Image todayRewardImage;
    public Image tomorrowRewardImage;

    public Button claimRewardButton;


    public DateTime expiryTime;
    public GameObject dailyRewardScreen;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        claimRewardButton.onClick.AddListener(ClaimRewardButtonclicked);

        if (!this.ReadTimestamp("timer"))
        {
            this.ScheduleTimer();
        }
        else
        {
            CheckReward();
        }
    }

    protected virtual void OnTimer(EventArgs e)
    {
        GameManager.Instance.RewardUser(DataManager.Instance.mData.rewardCount);
        Debug.Log("Reward user now !");
    }

[ContextMenu("CheckReward")]
public void CheckReward()
{
        #if UNITY_EDITOR
        if (expiryTime.Minute <= DateTime.Now.Minute &&
        DateTime.Now.Minute <= expiryTime.Minute + 1)//reward
        {
            OnTimer(EventArgs.Empty);
            DataManager.Instance.mData.rewardCount++;
            DataManager.Instance.SaveLocal();
            this.ScheduleTimer();
        }
        else if (DateTime.Now.Minute > expiryTime.Minute + 1)//reset
        {
            DataManager.Instance.mData.rewardCount = 0;
            DataManager.Instance.SaveLocal();
            this.ScheduleTimer();
            Debug.Log("NR Reseting: " + expiryTime.Minute.ToString() + ", " + DateTime.Now.Minute);
        }
        else
        {
            Debug.Log("No rewarding no reseting (TIME): "+expiryTime+" , "+DateTime.Now);
        }
#endif
#if !UNITY_EDITOR
        if (expiryTime.Day <= DateTime.Now.Day &&
        DateTime.Now.Day <= expiryTime.Day + 1)//reward
        {
            OnTimer(EventArgs.Empty);
            DataManager.Instance.mData.rewardCount++;
            DataManager.Instance.SaveLocal();
            this.ScheduleTimer();
        }
        else if (DateTime.Now.Day > expiryTime.Day + 1)//reset
        {
            DataManager.Instance.mData.rewardCount = 0;
            DataManager.Instance.SaveLocal();
            this.ScheduleTimer();
            Debug.Log("NR: " + expiryTime.Day.ToString() + ", " + DateTime.Now.Day);
        }
#endif
    }

    void ScheduleTimer()
    {
        Debug.Log("sechedule");

#if UNITY_EDITOR
        expiryTime = DateTime.Now.AddMinutes(1);
#endif
#if !UNITY_EDITOR
        expiryTime = DateTime.Now.AddDays(1);
#endif
        this.WriteTimestamp("timer");
    }

    private bool ReadTimestamp(string key)
    {
        long tmp = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
        if (tmp == 0)
        {
            return false;
        }
        expiryTime = DateTime.FromBinary(tmp);
        return true;
    }

    private void WriteTimestamp(string key)
    {
        PlayerPrefs.SetString(key, expiryTime.ToBinary().ToString());
    }

    public void RewardUser(Sprite todayReward, Sprite tomorrowReward)
    {

        dailyRewardScreen.SetActive(true);
        todayRewardImage.sprite = todayReward;
        tomorrowRewardImage.sprite = tomorrowReward;

    }

    public void ClaimRewardButtonclicked()
    {
        //currently user get rewarded without clicking this this only lose panel

        dailyRewardScreen.SetActive(false);
    }

}
