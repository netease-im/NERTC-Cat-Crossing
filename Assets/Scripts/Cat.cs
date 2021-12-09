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

using NERTC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Unknown = 0,
    Left = 1 << 0,
    Back = 1 << 1,
    Right = 1 << 2,
    Front = 1 << 3
}

public class Cat : MonoBehaviour {
    public float speed;
    public Animator anime;
    public Direction dir;
    private Camera cam;
    private Camera mainCam;
    public bool ReportPosition { get; set; }
    private PlaygroundSceneController ctl;
    public Atomic<bool> Attached { get; set; }

    public Cat() : base() {
        Attached = new Atomic<bool>(false);
    }

    // Start is called before the first frame update
    void Start() {
        speed = 5;
        anime = GetComponent<Animator>();
        dir = Direction.Back;
        ReportPosition = false;

        Camera[] camObj = FindObjectsOfType<Camera>();

        foreach (var cam in camObj) {
            if (cam.name == "CatCam") {
                this.cam = cam;

            } else if (cam.name == "MainCam") {
                this.mainCam = cam;
            }
        }

        GameObject sceneController = GameObject.Find("SceneController");

        if (sceneController != null) {
            ctl = sceneController.GetComponent<PlaygroundSceneController>();
        }
    }

    void Rotate(Direction target) {
        float r = 0;

        if (dir == target) {
            return;
        }

        switch (target) {
            case Direction.Left:
                r = -90;
                //Debug.Log("Rotate to left");
                break;

            case Direction.Back:
                r = 0;
                //Debug.Log("Rotate to back");
                break;

            case Direction.Right:
                r = 90;
                //Debug.Log("Rotate to right");
                break;

            case Direction.Front:
                r = 180;
                //Debug.Log("Rotate to front");
                break;
        }

        gameObject.transform.rotation = Quaternion.AngleAxis(r, Vector3.up);
        dir = target;
    }

    void RotateRound(Direction target, float t) {
        float r = 0;

        switch (target) {
            case Direction.Left:
                r = -1 * t * 50 * speed;
                //Debug.Log("Rotate to left");
                break;

            case Direction.Back:
                r = 0;
                //Debug.Log("Rotate to back");
                break;

            case Direction.Right:
                r = t * 50 * speed;
                //Debug.Log("Rotate to right");
                break;
        }

        //Debug.Log(r);

        gameObject.transform.Rotate(0f, r, 0f);
        dir = Direction.Unknown;
    }

    public void TraditionalMove(Direction direction, float t) {
        if (direction == Direction.Unknown) {
            return;
        }

        if ((direction & Direction.Front) > 0) {
            Rotate(Direction.Back);
            anime.Play("walk");

            if (gameObject.transform.position.z >= 48) {
                return;
            }

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + t * speed);
        }

        if ((direction & Direction.Back) > 0) {
            Rotate(Direction.Front);
            anime.Play("walk");

            if (gameObject.transform.position.z <= -48) {
                return;
            }

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - t * speed);
        }

        if ((direction & Direction.Left) > 0) {
            Rotate(Direction.Left);
            anime.Play("walk");

            if (gameObject.transform.position.x <= -48) {
                return;
            }

            gameObject.transform.position = new Vector3(gameObject.transform.position.x - t * speed, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if ((direction & Direction.Right) > 0) {
            Rotate(Direction.Right);
            anime.Play("walk");

            if (gameObject.transform.position.x >= 48) {
                return;
            }

            gameObject.transform.position = new Vector3(gameObject.transform.position.x + t * speed, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

    bool CheckEdge() {
        if (gameObject.transform.position.x > 48) {
            return true;

        } else if (gameObject.transform.position.x < -48) {
            return true;

        } else if (gameObject.transform.position.z > 48) {
            return true;

        } else if (gameObject.transform.position.z < -48) {
            return true;
        }

        return false;
    }

    public void FirstMove(float t, Direction direction) {
        if (direction == Direction.Unknown) {
            return;
        }

        if ((direction & Direction.Front) > 0) {
            anime.Play("walk");

            gameObject.transform.Translate(Vector3.forward * t * speed);
        }

        if ((direction & Direction.Back) > 0) {
            anime.Play("walk");

            gameObject.transform.Translate(Vector3.back * t * speed);
        }

        if ((direction & Direction.Left) > 0) {
            RotateRound(Direction.Left, t);
            anime.Play("walk");
        }

        if ((direction & Direction.Right) > 0) {
            RotateRound(Direction.Right, t);
            anime.Play("walk");
        }
    }

    public void FirstMove(float t, Direction direction, float rt) {
        if (direction == Direction.Unknown) {
            return;
        }

        if ((direction & Direction.Front) > 0) {
            anime.Play("walk");

            gameObject.transform.Translate(Vector3.forward * t * speed);
        }

        if ((direction & Direction.Back) > 0) {
            anime.Play("walk");

            gameObject.transform.Translate(Vector3.back * t * speed);
        }

        if ((direction & Direction.Left) > 0) {
            RotateRound(Direction.Left, rt);
            anime.Play("walk");
        }

        if ((direction & Direction.Right) > 0) {
            RotateRound(Direction.Right, rt);
            anime.Play("walk");
        }
    }

    void Update() {
        if (!Attached.Value) {
            return;
        }

        if (Input.anyKey) {
            Direction dir = Direction.Unknown;

            if (Input.GetKey(KeyCode.W)) {
                dir |= Direction.Front;
            }

            if (Input.GetKey(KeyCode.S)) {
                dir |= Direction.Back;
            }

            if (Input.GetKey(KeyCode.A)) {
                dir |= Direction.Left;
            }

            if (Input.GetKey(KeyCode.D)) {
                dir |= Direction.Right;
            }

            float t = Time.deltaTime;

            if (cam.enabled) {
                //CatCam camComponent = cam.GetComponent<CatCam>();

                FirstMove(t, dir);

                //if (camComponent) {
                //    camComponent.CamForward();
                //}

            } else {
                TraditionalMove(dir, t);
            }
        }

        if (cam.enabled) {
            cam.transform.position = new Vector3(
                    gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
            cam.transform.rotation = gameObject.transform.rotation;

        } else if (mainCam.enabled) {
            mainCam.transform.position = new Vector3(
                    gameObject.transform.position.x, gameObject.transform.position.y + 15, gameObject.transform.position.z - 15);
        }

        // if (gameObject.transform.rotation.x < 65) {

        // } else if (gameObject.transform.rotation.x > 65) {
        //     gameObject.transform.rotation = new Quaternion(30, gameObject.transform.rotation.y,
        //         gameObject.transform.rotation.z, gameObject.transform.rotation.w);
        // }

        // if (gameObject.transform.rotation.z > 65) {
        //     gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y,
        //         30, gameObject.transform.rotation.w);
        // }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log(name + " collided with: " + collision.gameObject.name);
    }
}
