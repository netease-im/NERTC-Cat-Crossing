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

public class CatCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void RotateRound(Direction target, float t, float speed)
    {
        float r = 0;

        switch (target)
        {
            case Direction.Left:
                r = t * 50 * speed * -1;
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

        gameObject.transform.Rotate(0f, r, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
