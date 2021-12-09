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
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlPanel : MonoBehaviour {
    public float speed;
    public VariableJoystick variableJoystick;
    private Camera cam;

    // Start is called before the first frame update
    void Start() {
        Camera[] camObj = FindObjectsOfType<Camera>();

        foreach (var cam in camObj) {
            if (cam.name == "CatCam") {
                this.cam = cam;
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void FixedUpdate() {
        if (variableJoystick.Direction.x == 0f && variableJoystick.Direction.y == 0f) {
            return;
        }

        GameObject sceneController = GameObject.Find("SceneController");
        PlaygroundSceneController ctl = sceneController.GetComponent<PlaygroundSceneController>();
        Cat cat = ctl.Cat;

        //Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);


        Direction dir = Direction.Unknown;

        Debug.Log(variableJoystick.Direction);

        float t = Time.deltaTime;
        float rt = 0f;

        if (cam.enabled) {
            if (variableJoystick.Direction.y > 0f) {
                dir |= Direction.Front;

                t = t * variableJoystick.Direction.y;
            }

            if (variableJoystick.Direction.y < 0f) {
                dir |= Direction.Back;

                t = t * (variableJoystick.Direction.y * -1);
            }

            if (variableJoystick.Direction.x < -0.3f) {
                dir |= Direction.Left;
                rt = Time.deltaTime * (variableJoystick.Direction.x * -1) * 0.5f;
            }

            if (variableJoystick.Direction.x > 0.3f) {
                dir |= Direction.Right;
                rt = Time.deltaTime * variableJoystick.Direction.x * 0.5f;
            }

            //CatCam camComponent = cam.GetComponent<CatCam>();

            cat.FirstMove(t, dir, rt);

            //if (camComponent) {
            //    camComponent.CamForward();
            //}

            cam.transform.position = new Vector3(
                gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
            cam.transform.rotation = gameObject.transform.rotation;

            //if (ReportPosition) {
            //    EngineAudioPositionInfo info = new EngineAudioPositionInfo();
            //    info.position = new int[3];
            //    info.quaternion = new float[4];

            //    info.position[0] = (int)gameObject.transform.position.x;
            //    info.position[1] = (int)(gameObject.transform.position.y - 6) * 2;
            //    info.position[2] = (int)gameObject.transform.position.z;

            //    info.quaternion[0] = gameObject.transform.rotation.x;
            //    info.quaternion[1] = gameObject.transform.rotation.y;
            //    info.quaternion[2] = gameObject.transform.rotation.z;
            //    info.quaternion[3] = gameObject.transform.rotation.w;

            //    if (ctl != null) {
            //        ctl.ReportPosition(info);
            //    }
            //}

        } else {
            if (variableJoystick.Direction.y > 0f) {
                dir |= Direction.Front;
            }

            if (variableJoystick.Direction.y < 0f) {
                dir |= Direction.Back;
            }

            if (variableJoystick.Direction.x < -0.3f) {
                dir |= Direction.Left;
            }

            if (variableJoystick.Direction.x > 0.3f) {
                dir |= Direction.Right;
            }

            cat.TraditionalMove(dir, t);
        }
    }
}
