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

using System;
using System.Collections;
using System.Collections.Generic;
using NERTC;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlaygroundSceneController : MonoBehaviour {
    public Cat Cat { get; set; }
    public GameObject baseCat;
    public Atomic<bool> IsFirst;
    private Camera mainCam;
    private Camera catCam;
    private Toggle toggle;
    
    private RtcEngineEventHandler engineEventListener;
    public bool Joined { get; set; }
    private AtomicDictionary<UInt64, GameObject> Cats { get; set; }
    private Vector3 catOriginPosition;
    private Quaternion catOriginRotation;
    private float counter;

    // Start is called before the first frame update
    void Start() {
        Joined = false;

        baseCat = GameObject.Find("CatA");
        catOriginPosition = baseCat.transform.position;
        catOriginRotation = baseCat.transform.rotation;
        baseCat.transform.position = new Vector3(catOriginPosition.x, -10, catOriginPosition.z);
        
        GameObject newCatObj = Instantiate(baseCat, catOriginPosition, catOriginRotation);
        newCatObj.AddComponent<Cat>();

        baseCat.SetActive(false);

        Cat = newCatObj.GetComponent<Cat>();
        Cat.Attached.Value = true;

        Camera[] camObj = FindObjectsOfType<Camera>();

        foreach (var cam in camObj) {
            Cons.Log(cam.name);
            Cons.Log(cam.gameObject.GetComponent<AudioListener>());

            if (cam.name == "MainCam") {
                mainCam = cam;

            } else if (cam.name == "CatCam") {
                catCam = cam;
            }
        }

        Cats = new AtomicDictionary<UInt64, GameObject>();

        counter = 0;
        IsFirst = new Atomic<bool>(false);

        toggle = GameObject.Find("ToggleCamera").GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((bool v) => {
            IsFirst.Value = v;
            mainCam.enabled = !mainCam.enabled;
            catCam.enabled = !catCam.enabled;
            });

        mainCam.enabled = !mainCam.enabled;
        catCam.enabled = !catCam.enabled;
    }

    void UpdatePosition(Cat c) {
        RtcPosition position = new RtcPosition();
        position.x = c.gameObject.transform.position.x;
        position.y = c.gameObject.transform.position.y;
        position.z = c.gameObject.transform.position.z;
        position.rx = c.gameObject.transform.rotation.x;
        position.ry = c.gameObject.transform.rotation.y;
        position.rz = c.gameObject.transform.rotation.z;
        position.rw = c.gameObject.transform.rotation.w;

        RtcService.ReportPosition(position);
    }

    // Update is called once per frame
    void Update() {
        counter += 1;

        if (counter > 0) {
            //Cons.Log("5s....");
            counter = 0;

            if (RtcService.State > RtcService.Status.START) {
                //Cons.Log("Do update position...");
                UpdatePosition(Cat);
            }
        }

        if (Input.GetKey(KeyCode.Alpha1)) {
            mainCam.enabled = true;
            catCam.enabled = false;

        } else if (Input.GetKey(KeyCode.Alpha2)) {
            mainCam.enabled = false;
            catCam.enabled = true;

            // CatCam catCamComponent = catCam.GetComponent<CatCam>();

            // if (catCamComponent) {

            // }
        }

        while (RtcService.JoinQueue.Count > 0) {
            catOriginPosition.x -= 2;
            catOriginPosition.z -= 2;
            baseCat.SetActive(true);

            GameObject newCatObj = Instantiate(baseCat, catOriginPosition, catOriginRotation);
            // newCatObj.AddComponent<Cat>();

            baseCat.SetActive(false);
            newCatObj.SetActive(true);

            Destroy(newCatObj.GetComponent<BoxCollider>());
            Destroy(newCatObj.GetComponent<Rigidbody>());

            //Cat c = newCatObj.GetComponent<Cat>();

            UInt64 uid = RtcService.JoinQueue.Dequeue();

            Cons.Log("Cat join room");

            Cats.Add(uid, newCatObj);
        }

        while (RtcService.LeaveQueue.Count > 0) {
            UInt64 uid = RtcService.LeaveQueue.Dequeue();

            GameObject c = Cats[uid];

            Cats.Remove(uid);
            Cons.Log("Cat leave room");

            Destroy(c.gameObject);
        }

        while (RtcService.PosRefreshQueue.Count > 0) {
            RtcPosition position = RtcService.PosRefreshQueue.Dequeue();

            if (!Cats.ContainsKey(position.userId)) {
                continue;
            }

            GameObject c = Cats[position.userId];
            c.transform.position = new Vector3(position.x, position.y, position.z);
            c.transform.rotation = new Quaternion(position.rx, position.ry, position.rz, position.rw);
            c.GetComponent<Animator>().Play("walk");

            Cons.Log("Cat {0} move to x[{1}], y[{2}], z[{3}]",
                     c,
                     position.x,
                     position.y,
                     position.z);
        }
    }
}
