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

public class StartSceneCam : MonoBehaviour
{
    private bool direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + Time.deltaTime / 2, gameObject.transform.position.y, gameObject.transform.position.z + Time.deltaTime / 2);

        } else {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - Time.deltaTime / 2, gameObject.transform.position.y, gameObject.transform.position.z - Time.deltaTime / 2);
        }
        
        if (gameObject.transform.position.x > 1) {
            direction = false;
        }

        if (gameObject.transform.position.x < -1) {
            direction = true;
        }
    }
}
