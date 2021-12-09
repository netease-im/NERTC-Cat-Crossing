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
using System.Runtime.InteropServices;

namespace NERTC
{
    public class NativeMediaStatsObserverCallback
    { 
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RtcStats(IntPtr self, IntPtr stats);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LocalAudioStats(IntPtr self, IntPtr stats);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RemoteAudioStats(IntPtr self, IntPtr stats, UInt32 user_count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LocalVideoStats(IntPtr self, IntPtr stats);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RemoteVideoStats(IntPtr self, IntPtr stats, UInt32 user_count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NetworkQuality(IntPtr self, IntPtr infos, UInt32 user_count);
    }

    public class MediaStatsObserverCallback
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RtcStats(Stats stats);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LocalAudioStats(AudioSendStats stats);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RemoteAudioStats(AudioRecvStats[] stats, UInt32 user_count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LocalVideoStats(VideoSendStats stats);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RemoteVideoStats(VideoRecvStats[] stats, UInt32 user_count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NetworkQuality(NetworkQualityInfo[] infos, UInt32 user_count);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct NativeMediaStatsObserver
    {
        public IntPtr self;
        public NativeMediaStatsObserverCallback.RtcStats OnRtcStats;
        public NativeMediaStatsObserverCallback.LocalAudioStats OnLocalAudioStats;
        public NativeMediaStatsObserverCallback.RemoteAudioStats OnRemoteAudioStats;
        public NativeMediaStatsObserverCallback.LocalVideoStats OnLocalVideoStats;
        public NativeMediaStatsObserverCallback.RemoteVideoStats OnRemoteVideoStats;
        public NativeMediaStatsObserverCallback.NetworkQuality OnNetworkQuality;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MediaStatsObserver
    {
        public MediaStatsObserverCallback.RtcStats OnRtcStats;
        public MediaStatsObserverCallback.LocalAudioStats OnLocalAudioStats;
        public MediaStatsObserverCallback.RemoteAudioStats OnRemoteAudioStats;
        public MediaStatsObserverCallback.LocalVideoStats OnLocalVideoStats;
        public MediaStatsObserverCallback.RemoteVideoStats OnRemoteVideoStats;
        public MediaStatsObserverCallback.NetworkQuality OnNetworkQuality;
    }

    public class NativeMediaStatsObserverHandler
    {
        private static Dictionary<IntPtr, MediaStatsObserver> Observers { get; }

        static NativeMediaStatsObserverHandler() {
            Observers = new Dictionary<IntPtr, MediaStatsObserver>();
        }

        private static bool GetObserver(IntPtr self, out MediaStatsObserver observer) {
            if (Observers.ContainsKey(self)) {
                observer = Observers[self];
                return true;
            }

            observer = default(MediaStatsObserver);

            return false;
        }

        [MonoPInvokeCallback(typeof(NativeMediaStatsObserverCallback.RtcStats))]
        public static void OnRtcStats(IntPtr self, IntPtr stats) {
            Console.WriteLine("NativeMediaStatsObserverHandler OnRtcStats");

            if (self == null)
            {
                return;
            }

            MediaStatsObserver listener;

            if (!GetObserver(self, out listener)) {
                return;
            }

            if (listener.OnRtcStats == null) {
                return;
            }

            Stats s = (Stats)Marshal.PtrToStructure(stats, typeof(Stats));
            listener.OnRtcStats(s);
        }

        [MonoPInvokeCallback(typeof(NativeMediaStatsObserverCallback.LocalAudioStats))]
        public static void OnLocalAudioStats(IntPtr self, IntPtr stats) {
            Console.WriteLine("NativeMediaStatsObserverHandler OnLocalAudioStats");

            if (self == null)
            {
                return;
            }

            MediaStatsObserver listener;

            if (!GetObserver(self, out listener)) {
                return;
            }

            if (listener.OnLocalAudioStats == null) {
                return;
            }

            AudioSendStats s = (AudioSendStats)Marshal.PtrToStructure(stats, typeof(AudioSendStats));
            listener.OnLocalAudioStats(s);
        }

        [MonoPInvokeCallback(typeof(NativeMediaStatsObserverCallback.RemoteAudioStats))]
        public static void OnRemoteAudioStats(IntPtr self, IntPtr stats, UInt32 user_count) {
            Console.WriteLine("NativeMediaStatsObserverHandler OnRemoteAudioStats");

            if (self == null)
            {
                return;
            }

            MediaStatsObserver listener;

            if (!GetObserver(self, out listener)) {
                return;
            }

            if (listener.OnRemoteAudioStats == null) {
                return;
            }

            AudioRecvStats[] audioRecvStats = new AudioRecvStats[user_count];
            IntPtr cur = stats;

            for (int i = 0; i < user_count; ++i) {
                audioRecvStats[i] = (AudioRecvStats)Marshal.PtrToStructure(cur, typeof(AudioRecvStats));
                cur += Marshal.SizeOf(typeof(AudioVolumeInfo));
            }

            listener.OnRemoteAudioStats(audioRecvStats, user_count);
        }

        [MonoPInvokeCallback(typeof(NativeMediaStatsObserverCallback.LocalVideoStats))]
        public static void OnLocalVideoStats(IntPtr self, IntPtr stats) {
            Console.WriteLine("NativeMediaStatsObserverHandler OnLocalVideoStats");

            if (self == null)
            {
                return;
            }

            MediaStatsObserver listener;

            if (!GetObserver(self, out listener)) {
                return;
            }

            if (listener.OnLocalVideoStats == null) {
                return;
            }

            VideoSendStats s = (VideoSendStats)Marshal.PtrToStructure(stats, typeof(VideoSendStats));
            listener.OnLocalVideoStats(s);
        }

        [MonoPInvokeCallback(typeof(NativeMediaStatsObserverCallback.RemoteVideoStats))]
        public static void OnRemoteVideoStats(IntPtr self, IntPtr stats, UInt32 user_count) {
            Console.WriteLine("NativeMediaStatsObserverHandler OnRemoteVideoStats");

            if (self == null)
            {
                return;
            }

            MediaStatsObserver listener;

            if (!GetObserver(self, out listener)) {
                return;
            }

            if (listener.OnRemoteVideoStats == null) {
                return;
            }

            VideoRecvStats[] videoRecvStats = new VideoRecvStats[user_count];
            IntPtr cur = stats;

            for (int i = 0; i < user_count; ++i) {
                videoRecvStats[i] = (VideoRecvStats)Marshal.PtrToStructure(cur, typeof(VideoRecvStats));
                cur += Marshal.SizeOf(typeof(VideoRecvStats));
            }

            listener.OnRemoteVideoStats(videoRecvStats, user_count);
        }

        [MonoPInvokeCallback(typeof(NativeMediaStatsObserverCallback.NetworkQuality))]
        public static void OnNetworkQuality(IntPtr self, IntPtr infos, UInt32 user_count) {
            Console.WriteLine("NativeMediaStatsObserverHandler OnNetworkQuality");

            if (self == null)
            {
                return;
            }

            MediaStatsObserver listener;

            if (!GetObserver(self, out listener)) {
                return;
            }

            if (listener.OnNetworkQuality == null) {
                return;
            }

            NetworkQualityInfo[] networkQualityInfos = new NetworkQualityInfo[user_count];
            IntPtr cur = infos;

            for (int i = 0; i < user_count; ++i) {
                networkQualityInfos[i] = (NetworkQualityInfo)Marshal.PtrToStructure(cur, typeof(NetworkQualityInfo));
                cur += Marshal.SizeOf(typeof(NetworkQualityInfo));
            }

            listener.OnNetworkQuality(networkQualityInfos, user_count);
        }

        public static NativeMediaStatsObserver CreateNativeMediaStatsObserver(IntPtr ptrEngine, MediaStatsObserver listener)
        {
            NativeMediaStatsObserver nativeObserver = new NativeMediaStatsObserver();

            nativeObserver.self = ptrEngine;
            nativeObserver.OnRtcStats = OnRtcStats;
            nativeObserver.OnLocalAudioStats = OnLocalAudioStats;
            nativeObserver.OnRemoteAudioStats = OnRemoteAudioStats;
            nativeObserver.OnLocalVideoStats = OnLocalVideoStats;
            nativeObserver.OnRemoteVideoStats = OnRemoteVideoStats;
            nativeObserver.OnNetworkQuality = OnNetworkQuality;

            Observers.Add(ptrEngine, listener);

            return nativeObserver;
        }
    }
}