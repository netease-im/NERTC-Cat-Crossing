/*
 * Copyright (C) 2021 Netease RTC CatCrossing
 *
 * Licensed under the MIT License
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      https://opensource.org/licenses/MIT
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using NERTC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    private InputField txtChannelID;
    private InputField txtUserID;
    //private InputField txtToken;

    // Start is called before the first frame update
    void Start() {
#if UNITY_ANDROID
        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass utilityClass = new AndroidJavaClass("com.netease.nertc.unity.UnityUtility");
        utilityClass.CallStatic("initialize", new object[1] { activity });
#endif
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        txtChannelID = GameObject.Find("InputField1").GetComponent<InputField>();
        txtUserID = GameObject.Find("InputField2").GetComponent<InputField>();
        //txtToken = GameObject.Find("InputField3").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RtcService.State >= RtcService.Status.JOINED) {
            SceneManager.LoadScene("PlaygroundScene");
        }
    }

    public void OnStartClick() {
        RtcService.ChannelID = txtChannelID.text;
        RtcService.UserID = txtUserID.text;
        //RtcService.Token = txtToken.text;

        RtcService.Start();
    }
}
