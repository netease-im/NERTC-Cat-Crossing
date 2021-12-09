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

public class CameraControl : MonoBehaviour
{
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.R))
        {
            gameObject.transform.Rotate(new Vector3(0f, Time.deltaTime * (speed / 1.25f), 0f));
            gameObject.transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * (speed / 1.25f) * -1));
        }

        if (Input.GetKey(KeyCode.T))
        {
            gameObject.transform.Rotate(new Vector3(0f, Time.deltaTime * (speed / 1.25f) * -1, 0f));
            gameObject.transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * (speed / 1.25f)));
        }

        if (Input.GetKey(KeyCode.I))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Time.deltaTime * speed, gameObject.transform.position.z);
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (gameObject.transform.position.y <= 15)
            {
                return;
            }

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - Time.deltaTime * speed, gameObject.transform.position.z);
        }
    }
}
