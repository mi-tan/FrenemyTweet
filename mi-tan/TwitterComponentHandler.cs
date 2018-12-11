using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleJSON;

public class TwitterComponentHandler : MonoBehaviour {

    public GameObject inputPINField;
    public GameObject inputTweetField;
    public GameObject TimeLineGet;

    private const string CONSUMER_KEY = "uAWdP7574vZXG95E3YxCxdePq";
    private const string CONSUMER_SECRET = "NIMvj4ImnUfZKFXtDB4GEdYSIA4K6NOHwDG6cZCOf6O0XGfMYm";
    private string user_id = "TwitterUserID";

    Twitter.RequestTokenResponse m_RequestTokenResponse;
    Twitter.AccessTokenResponse m_AccessTokenResponse;
    Twitter.AccessTokenResponse t_AccessTokenResponse;

    const string PLAYER_PREFS_TWITTER_USER_ID = "TwitterUserID";
    const string PLAYER_PREFS_TWITTER_USER_SCREEN_NAME = "TwitterUserScreenName";
    const string PLAYER_PREFS_TWITTER_USER_TOKEN = "TwitterUserToken";
    const string PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET = "TwitterUserTokenSecret";
    const string PLAYER_PREFS_TWITTER_TIMELINE = "TwitterTimeline";

    const string PLAYER_PREFS_TWITTER_TWEETED_IDS = "TwitterTweetedIDs";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickTimeLine()
    {
        StartCoroutine(Twitter.API.GetUserTimeline(m_AccessTokenResponse.UserId, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
              new Twitter.GetUserTimelineCallback(this.OnGetUserTimeline)));
    }

    public void OnClickGetPINButon()
    {
        StartCoroutine(Twitter.API.GetRequestToken(CONSUMER_KEY, CONSUMER_SECRET,
            new Twitter.RequestTokenCallback(this.OnRequestTokenCallback)));
    }

    public void OnClickAuthPINButon()
    {
        string myPIN = inputPINField.GetComponent<InputField>().text;

        StartCoroutine(Twitter.API.GetAccessToken(CONSUMER_KEY, CONSUMER_SECRET, m_RequestTokenResponse.Token, myPIN,
            new Twitter.AccessTokenCallback(this.OnAccessTokenCallback)));
    }

    public void OnClickTweetButon()
    {
        string myTweet = inputTweetField.GetComponent<InputField>().text;

        StartCoroutine(Twitter.API.PostTweet(myTweet, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
            new Twitter.PostTweetCallback(this.OnPostTweet)));


    }

    void OnRequestTokenCallback(bool success, Twitter.RequestTokenResponse response)
    {
        if (success)
        {
            string log = "OnRequestTokenCallback - succeeded";
            log += "\n    Token : " + response.Token;
            log += "\n    TokenSecret : " + response.TokenSecret;
            print(log);

            m_RequestTokenResponse = response;

            print(response.Token);
            print(response.TokenSecret);

            Twitter.API.OpenAuthorizationPage(response.Token);
        }
        else
        {
            print("OnRequestTokenCallback - failed.");
        }
    }

    void OnAccessTokenCallback(bool success, Twitter.AccessTokenResponse response)
    {
        if (success)
        {
            string log = "OnAccessTokenCallback - succeeded";
            log += "\n    UserId : " + response.UserId;
            log += "\n    ScreenName : " + response.ScreenName;
            log += "\n    Token : " + response.Token;
            log += "\n    TokenSecret : " + response.TokenSecret;
            //log += "\n    TweetContents :" + response.TweetContents;
            print(log);

            m_AccessTokenResponse = response;

            PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_ID, response.UserId);
            PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME, response.ScreenName);
            PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN, response.Token);
            PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET, response.TokenSecret);
            //PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_TIMELINE, response.TweetContents);

        }
        else
        {
            print("OnAccessTokenCallback - failed.");
        }
    }

    //public void onClickHomeTimeLineDisplayButton()
    //{
    //    StartCoroutine(Twitter.API.GetHomeTimeline(m_AccessTokenResponse.UserId, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
    //        new Twitter.GetHomeTimelineCallback(this.OnGetHomeTimeline)));
    //}

    void OnPostTweet(bool success, string response)
    {
        print("OnPostTweet - " + (success ? "succedded." : "failed."));

        if (success)
        {
            var json = JSON.Parse(response);
            print(json);
        }
    }

    void OnGetUserTimeline(bool success, string response)
    {
        print("GetUserTimeline- " + (success ? "succedded." : "failed."));

        if (success)
        {
            var json = JSON.Parse(response);
            // イナミュレーターをとってくるよ
            var jsonEnum = json.GetEnumerator();
            while (jsonEnum.MoveNext())
            {
                //Debug.Log(jsonEnum.MoveNext());
                Debug.Log(((JSONNode)jsonEnum.Current)["text"]);
            }
            
        }
    }
}
