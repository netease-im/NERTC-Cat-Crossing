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
using System.Collections.Generic;
using System.Threading;
using System.IO;
using UnityEngine;

namespace NERTC
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute(Type type) { }
    }

    public class Atomic<T>
    {
        private Mutex mutex;
        private T _value;

        public T Value
        {
            get
            {
                mutex.WaitOne();
                T v = _value;
                mutex.ReleaseMutex();

                return v;
            }
            set
            {
                mutex.WaitOne();
                _value = value;
                mutex.ReleaseMutex();
            }
        }

        public Atomic(T t)
        {
            mutex = new Mutex();
            Value = t;
        }
    }


    public class AtomicQueue<T> {
        private Mutex mutex;
        private Queue<T> queue;

        public int Count {
            get {
                int count;

                mutex.WaitOne();

                count = queue.Count;

                mutex.ReleaseMutex();

                return count;
            }
        }

        public AtomicQueue() {
            mutex = new Mutex();
            queue = new Queue<T>();
        }

        public void Enqueue(T t) {
            mutex.WaitOne();

            queue.Enqueue(t);

            mutex.ReleaseMutex();
        }

        public T Dequeue() {
            T t;

            mutex.WaitOne();

            t = queue.Dequeue();

            mutex.ReleaseMutex();

            return t;
        }
    }

    public class AtomicDictionary<M, N> {
        private Mutex mutex;
        private Dictionary<M, N> dict;

        public int Count {
            get {
                int count;

                mutex.WaitOne();

                count = dict.Count;

                mutex.ReleaseMutex();

                return count;
            }
        }

        public AtomicDictionary() {
            mutex = new Mutex();
            dict = new Dictionary<M, N>();
        }

        public void Add(M key, N value) {
            mutex.WaitOne();

            dict.Add(key, value);

            mutex.ReleaseMutex();
        }

        public void Remove(M key) {
            mutex.WaitOne();

            dict.Remove(key);

            mutex.ReleaseMutex();
        }

        public bool ContainsKey(M key) {
            bool check;

            mutex.WaitOne();

            check = dict.ContainsKey(key);

            mutex.ReleaseMutex();

            return check;
        }

        public N this[M key] {
            get {
                N value;

                mutex.WaitOne();

                value = dict[key];

                mutex.ReleaseMutex();

                return value;
            }
            set {
                mutex.WaitOne();

                dict[key] = value;

                mutex.ReleaseMutex();
            }
        }

        public void Each(Func<M, N, int> func) {
            mutex.WaitOne();

            foreach (var item in dict) {
                func(item.Key, item.Value);
            }

            mutex.ReleaseMutex();
        }
    }

    public class Cons {
        public static void Log(string format, params object[] args) {
#if UNITY_EDITOR
            Debug.LogFormat(format, args);
#elif UNITY_STANDALONE
        Debug.LogFormat(format, args);
#elif UNITY_ANDROID
        Debug.LogFormat(format, args);
#elif UNITY_IPHONE
        Debug.LogFormat(format, args);
#else
        Console.WriteLine(format, args);
#endif
        }

        public static void Log(object message) {
#if UNITY_EDITOR
            Debug.Log(message);
#elif UNITY_STANDALONE
        Debug.LogFormat(message.ToString());
#elif UNITY_ANDROID
        Debug.LogFormat(message.ToString());
#elif UNITY_IPHONE
        Debug.LogFormat(message.ToString());
#else
        Console.WriteLine(message);
#endif
        }
    }

    public class Helper {
        public static string AppDataPath {
            get {
                string path;
#if UNITY_IPHONE
                // path = Application.persistentDataPath + "/NimUnityDemo";
                path = "";
#else
                path = Application.persistentDataPath;
#endif
                return path;
            }
        }
    }
}