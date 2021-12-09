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
    public class InnerRtcEngineEventCallback
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AudioDeviceStateChanged(string deviceId, Int32 deviceType, Int32 deviceState);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AudioHowling(bool howling);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ClientRoleChanged(Int32 oldRole, Int32 newRole); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ConnectionStateChange(Int32 state, Int32 reason); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Disconnect(Int32 reason); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Error(int errorCode, string msg); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FirstAudioDataReceived(UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FirstAudioFrameDecoded(UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JoinChannel(UInt64 cid, UInt64 uid, UInt32 result, UInt64 elapsed); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LeaveChannel(Int32 result); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReconnectingStart(UInt64 cid, UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RejoinChannel(UInt64 cid, UInt64 uid, Int32 result, UInt64 elapsed); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RemoteAudioVolumeIndication(AudioVolumeInfo[] speakers, UInt32 speakerNumber, Int32 totalVolume);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserAudioMute(UInt64 uid, bool mute); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserAudioStart(UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserAudioStop(UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserJoined(UInt64 uid, string userName); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserLeft(UInt64 uid, Int32 reason); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void UserPositionUpdated(UInt64 userId, float x, float y, float z, float rx, float ry, float rz, float rw); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Warning(int warnCode, string msg); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ReleasedHwResources(Int32 result); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void LocalAudioVolumeIndication(Int32 volume); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void AudioDeviceRoutingDidChange(Int32 routing); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void AudioDefaultDeviceChanged(string deviceId, Int32 deviceType);
	}

    public class InnerNativeRtcEngineEventCallback
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AudioDeviceStateChanged(IntPtr self, string deviceId, Int32 deviceType, Int32 deviceState);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AudioHowling(IntPtr self, bool howling);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ClientRoleChanged(IntPtr self, Int32 oldRole, Int32 newRole); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ConnectionStateChange(IntPtr self, Int32 state, Int32 reason); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Disconnect(IntPtr self, Int32 reason); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Error(IntPtr self, int errorCode, string msg); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FirstAudioDataReceived(IntPtr self, UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FirstAudioFrameDecoded(IntPtr self, UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JoinChannel(IntPtr self, UInt64 cid, UInt64 uid, UInt32 result, UInt64 elapsed); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LeaveChannel(IntPtr self, Int32 result); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReconnectingStart(IntPtr self, UInt64 cid, UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RejoinChannel(IntPtr self, UInt64 cid, UInt64 uid, Int32 result, UInt64 elapsed); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RemoteAudioVolumeIndication(IntPtr self, IntPtr speakers, UInt32 speakerNumber, Int32 totalVolume);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserAudioMute(IntPtr self, UInt64 uid, bool mute); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserAudioStart(IntPtr self, UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserAudioStop(IntPtr self, UInt64 uid); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserJoined(IntPtr self, UInt64 uid, string userName); //p0

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserLeft(IntPtr self, UInt64 uid, Int32 reason); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void UserPositionUpdated(IntPtr self, UInt64 userId, float x, float y, float z, float rx, float ry, float rz, float rw); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Warning(IntPtr self, int warnCode, string msg); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ReleasedHwResources(IntPtr self, Int32 result); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void LocalAudioVolumeIndication(IntPtr self, Int32 volume); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void AudioDeviceRoutingDidChange(IntPtr self, Int32 routing); //p0

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void AudioDefaultDeviceChanged(IntPtr self, string deviceId, Int32 deviceType);
	}

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct NativeRtcEngineEventHandler
    {
        public IntPtr self;
        public InnerNativeRtcEngineEventCallback.AudioDeviceStateChanged OnAudioDeviceStateChanged;
        public InnerNativeRtcEngineEventCallback.AudioHowling OnAudioHowling;
        public InnerNativeRtcEngineEventCallback.ClientRoleChanged OnClientRoleChanged;
        public InnerNativeRtcEngineEventCallback.ConnectionStateChange OnConnectionStateChange;
        public InnerNativeRtcEngineEventCallback.Disconnect OnDisconnect;
        public InnerNativeRtcEngineEventCallback.Error OnError;
        public InnerNativeRtcEngineEventCallback.FirstAudioDataReceived OnFirstAudioDataReceived;
        public InnerNativeRtcEngineEventCallback.FirstAudioFrameDecoded OnFirstAudioFrameDecoded;
        public InnerNativeRtcEngineEventCallback.JoinChannel OnJoinChannel;
        public InnerNativeRtcEngineEventCallback.LeaveChannel OnLeaveChannel;
        public InnerNativeRtcEngineEventCallback.ReconnectingStart OnReconnectingStart;
        public InnerNativeRtcEngineEventCallback.RejoinChannel OnRejoinChannel;
        public InnerNativeRtcEngineEventCallback.RemoteAudioVolumeIndication OnRemoteAudioVolumeIndication;
        public InnerNativeRtcEngineEventCallback.UserAudioMute OnUserAudioMute;
        public InnerNativeRtcEngineEventCallback.UserAudioStart OnUserAudioStart;
        public InnerNativeRtcEngineEventCallback.UserAudioStop OnUserAudioStop;
        public InnerNativeRtcEngineEventCallback.UserJoined OnUserJoined;
        public InnerNativeRtcEngineEventCallback.UserLeft OnUserLeft;
		public InnerNativeRtcEngineEventCallback.UserPositionUpdated OnUserPositionUpdated;
		public InnerNativeRtcEngineEventCallback.Warning OnWarning;
		public InnerNativeRtcEngineEventCallback.ReleasedHwResources OnReleasedHwResources;
		public InnerNativeRtcEngineEventCallback.LocalAudioVolumeIndication OnLocalAudioVolumeIndication;
		public InnerNativeRtcEngineEventCallback.AudioDeviceRoutingDidChange OnAudioDeviceRoutingDidChange;
		public InnerNativeRtcEngineEventCallback.AudioDefaultDeviceChanged OnAudioDefaultDeviceChanged;
	}

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RtcEngineEventHandler
    {
        public InnerRtcEngineEventCallback.AudioDeviceStateChanged OnAudioDeviceStateChanged;
        public InnerRtcEngineEventCallback.AudioHowling OnAudioHowling;
        public InnerRtcEngineEventCallback.ClientRoleChanged OnClientRoleChanged;
        public InnerRtcEngineEventCallback.ConnectionStateChange OnConnectionStateChange;
        public InnerRtcEngineEventCallback.Disconnect OnDisconnect;
        public InnerRtcEngineEventCallback.Error OnError;
        public InnerRtcEngineEventCallback.FirstAudioDataReceived OnFirstAudioDataReceived;
        public InnerRtcEngineEventCallback.FirstAudioFrameDecoded OnFirstAudioFrameDecoded;
        public InnerRtcEngineEventCallback.JoinChannel OnJoinChannel;
        public InnerRtcEngineEventCallback.LeaveChannel OnLeaveChannel;
        public InnerRtcEngineEventCallback.ReconnectingStart OnReconnectingStart;
        public InnerRtcEngineEventCallback.RejoinChannel OnRejoinChannel;
        public InnerRtcEngineEventCallback.RemoteAudioVolumeIndication OnRemoteAudioVolumeIndication;
        public InnerRtcEngineEventCallback.UserAudioMute OnUserAudioMute;
        public InnerRtcEngineEventCallback.UserAudioStart OnUserAudioStart;
        public InnerRtcEngineEventCallback.UserAudioStop OnUserAudioStop;
        public InnerRtcEngineEventCallback.UserJoined OnUserJoined;
        public InnerRtcEngineEventCallback.UserLeft OnUserLeft;
		public InnerRtcEngineEventCallback.UserPositionUpdated OnUserPositionUpdated;
		public InnerRtcEngineEventCallback.Warning OnWarning;
		public InnerRtcEngineEventCallback.ReleasedHwResources OnReleasedHwResources;
		public InnerRtcEngineEventCallback.LocalAudioVolumeIndication OnLocalAudioVolumeIndication;
		public InnerRtcEngineEventCallback.AudioDeviceRoutingDidChange OnAudioDeviceRoutingDidChange;
		public InnerRtcEngineEventCallback.AudioDefaultDeviceChanged OnAudioDefaultDeviceChanged;
	}

	public class NativeEngineEventHandler
	{
		private static Dictionary<IntPtr, RtcEngineEventHandler> Handlers { get; }

		static NativeEngineEventHandler() {
			Handlers = new Dictionary<IntPtr, RtcEngineEventHandler>();
		}

		private static bool GetHandler(IntPtr self, out RtcEngineEventHandler eventHandler) {
			if (Handlers.ContainsKey(self)) {
				eventHandler = Handlers[self];
				return true;
            }

			eventHandler = default(RtcEngineEventHandler);

			return false;
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.AudioDeviceStateChanged))]
		public static void OnAudioDeviceStateChanged(IntPtr self, string deviceId, Int32 deviceType, Int32 deviceState) {
			Cons.Log("NativeEngineEventHandler OnAudioDeviceStateChanged");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnAudioDeviceStateChanged == null) {
				return;
            }

			listener.OnAudioDeviceStateChanged(deviceId, deviceType, deviceState);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.AudioHowling))]
		public static void OnAudioHowling(IntPtr self, bool howling) {
			Cons.Log("NativeEngineEventHandler OnAudioHowling");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnAudioHowling == null) {
				return;
			}

			listener.OnAudioHowling(howling);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.ClientRoleChanged))]
		public static void OnClientRoleChanged(IntPtr self, Int32 oldRole, Int32 newRole) {
			Cons.Log("NativeEngineEventHandler OnClientRoleChanged");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnClientRoleChanged == null) {
				return;
			}

			listener.OnClientRoleChanged(oldRole, newRole);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.ConnectionStateChange))]
		public static void OnConnectionStateChange(IntPtr self, Int32 state, Int32 reason) {
			Cons.Log("NativeEngineEventHandler OnConnectionStateChange");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnConnectionStateChange == null) {
				return;
			}

			listener.OnConnectionStateChange(state, reason);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.Disconnect))]
		public static void OnDisconnect(IntPtr self, Int32 reason) {
			Cons.Log("NativeEngineEventHandler OnDisconnect");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnDisconnect == null) {
				return;
			}

			listener.OnDisconnect(reason);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.Error))]
		public static void OnError(IntPtr self, int errorCode, string msg) {
			Cons.Log("NativeEngineEventHandler OnError");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnError == null) {
				return;
            }

			listener.OnError(errorCode, msg);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.FirstAudioDataReceived))]
		public static void OnFirstAudioDataReceived(IntPtr self, UInt64 uid) {
			Cons.Log("NativeEngineEventHandler OnFirstAudioDataReceived");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnFirstAudioDataReceived == null) {
				return;
			}

			listener.OnFirstAudioDataReceived(uid);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.FirstAudioFrameDecoded))]
		public static void OnFirstAudioFrameDecoded(IntPtr self, UInt64 uid) {
			Cons.Log("NativeEngineEventHandler OnFirstAudioFrameDecoded");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnFirstAudioFrameDecoded == null) {
				return;
			}

			listener.OnFirstAudioFrameDecoded(uid);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.JoinChannel))]
		public static void OnJoinChannel(IntPtr self, UInt64 cid, UInt64 uid, UInt32 result, UInt64 elapsed) {
			Cons.Log("NativeEngineEventHandler OnJoinChannel");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnJoinChannel == null) {
				return;
			}

			listener.OnJoinChannel(cid, uid, result, elapsed);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.LeaveChannel))]
		public static void OnLeaveChannel(IntPtr self, Int32 result) {
			Cons.Log("NativeEngineEventHandler OnLeaveChannel");

			if (self == null) {
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnLeaveChannel == null) {
				return;
			}

			listener.OnLeaveChannel(result);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.ReconnectingStart))]
		public static void OnReconnectingStart(IntPtr self, UInt64 cid, UInt64 uid) {
			Cons.Log("NativeEngineEventHandler OnReconnectingStart");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnReconnectingStart == null) {
				return;
			}

			listener.OnReconnectingStart(cid, uid);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.RejoinChannel))]
		public static void OnRejoinChannel(IntPtr self, UInt64 cid, UInt64 uid, Int32 result, UInt64 elapsed) {
			Cons.Log("NativeEngineEventHandler OnRejoinChannel");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnRejoinChannel == null) {
				return;
			}

			listener.OnRejoinChannel(cid, uid, result, elapsed);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.RemoteAudioVolumeIndication))]
		public static void OnRemoteAudioVolumeIndication(IntPtr self, IntPtr speakers, UInt32 speakerNumber, Int32 totalVolume) {
			Cons.Log("NativeEngineEventHandler OnRemoteAudioVolumeIndication");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnRemoteAudioVolumeIndication == null) {
				return;
			}

			AudioVolumeInfo[] audioVolumeInfos = new AudioVolumeInfo[speakerNumber];
			IntPtr cur = speakers;

			for (int i = 0; i < speakerNumber; ++i) {
				audioVolumeInfos[i] = (AudioVolumeInfo)Marshal.PtrToStructure(cur, typeof(AudioVolumeInfo));
				cur += Marshal.SizeOf(typeof(AudioVolumeInfo));
			}

			listener.OnRemoteAudioVolumeIndication(audioVolumeInfos, speakerNumber, totalVolume);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.UserAudioMute))]
		public static void OnUserAudioMute(IntPtr self, UInt64 uid, bool mute) {
			Cons.Log("NativeEngineEventHandler OnUserAudioMute");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnUserAudioMute == null) {
				return;
			}

			listener.OnUserAudioMute(uid, mute);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.UserAudioStart))]
		public static void OnUserAudioStart(IntPtr self, UInt64 uid) {
			Cons.Log("NativeEngineEventHandler OnUserAudioStart");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnUserAudioStart == null) {
				return;
			}

			listener.OnUserAudioStart(uid);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.UserAudioStop))]
		public static void OnUserAudioStop(IntPtr self, UInt64 uid) {
			Cons.Log("NativeEngineEventHandler OnUserAudioStop");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnUserAudioStop == null) {
				return;
			}

			listener.OnUserAudioStop(uid);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.UserJoined))]
		public static void OnUserJoined(IntPtr self, UInt64 uid, string userName) {
			Cons.Log("NativeEngineEventHandler OnUserJoined");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnUserJoined == null) {
				return;
			}

			listener.OnUserJoined(uid, userName);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.UserLeft))]
		public static void OnUserLeft(IntPtr self, UInt64 uid, Int32 reason) {
			Cons.Log("NativeEngineEventHandler OnUserLeft");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnUserLeft == null) {
				return;
			}

			listener.OnUserLeft(uid, reason);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.Warning))]
		public static void OnWarning(IntPtr self, int warnCode, string msg) {
			Cons.Log("NativeEngineEventHandler OnWarning");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnWarning == null) {
				return;
			}

			listener.OnWarning(warnCode, msg);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.UserPositionUpdated))]
		public static void OnUserPositionUpdated(IntPtr self, UInt64 userId, float x, float y, float z, float rx, float ry, float rz, float rw)
		{
			Cons.Log("NativeEngineEventHandler OnUserPositionUpdated");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnUserPositionUpdated == null) {
				return;
			}

			listener.OnUserPositionUpdated(userId, x, y, z, rx, ry, rz, rw);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.LocalAudioVolumeIndication))]
		public static void OnLocalAudioVolumeIndication(IntPtr self, Int32 volume)
		{
			Cons.Log("NativeEngineEventHandler OnLocalAudioVolumeIndication");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnLocalAudioVolumeIndication == null) {
				return;
			}

			listener.OnLocalAudioVolumeIndication(volume);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.AudioDeviceRoutingDidChange))]
		public static void OnAudioDeviceRoutingDidChange(IntPtr self, Int32 routing)
		{
			Cons.Log("NativeEngineEventHandler OnAudioDeviceRoutingDidChange");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnAudioDeviceRoutingDidChange == null) {
				return;
			}

			listener.OnAudioDeviceRoutingDidChange(routing);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.AudioDefaultDeviceChanged))]
		public static void OnAudioDefaultDeviceChanged(IntPtr self, string deviceId, Int32 deviceType)
		{
			Cons.Log("NativeEngineEventHandler OnAudioDefaultDeviceChanged");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnAudioDefaultDeviceChanged == null) {
				return;
			}

			listener.OnAudioDefaultDeviceChanged(deviceId, deviceType);
		}

		[MonoPInvokeCallback(typeof(InnerNativeRtcEngineEventCallback.ReleasedHwResources))]
		public static void OnReleasedHwResources(IntPtr self, Int32 result)
		{
			Cons.Log("NativeEngineEventHandler OnReleasedHwResources");

			if (self == null)
			{
				return;
			}

			RtcEngineEventHandler listener;

			if (!GetHandler(self, out listener)) {
				return;
			}

			if (listener.OnReleasedHwResources == null) {
				return;
			}

			listener.OnReleasedHwResources(result);
		}

		public static NativeRtcEngineEventHandler CreateNativeEngineEventListener(IntPtr ptrEngine, RtcEngineEventHandler listener)
        {
            NativeRtcEngineEventHandler nativeListener = new NativeRtcEngineEventHandler();

			nativeListener.self = ptrEngine;
			nativeListener.OnAudioDeviceStateChanged = OnAudioDeviceStateChanged;
            nativeListener.OnAudioHowling = OnAudioHowling;
            nativeListener.OnClientRoleChanged = OnClientRoleChanged;
            nativeListener.OnConnectionStateChange = OnConnectionStateChange;
            nativeListener.OnDisconnect = OnDisconnect;
            nativeListener.OnError = OnError;
            nativeListener.OnFirstAudioDataReceived = OnFirstAudioDataReceived;
            nativeListener.OnFirstAudioFrameDecoded = OnFirstAudioFrameDecoded;
            nativeListener.OnJoinChannel = OnJoinChannel;
            nativeListener.OnLeaveChannel = OnLeaveChannel;
            nativeListener.OnReconnectingStart = OnReconnectingStart;
            nativeListener.OnRejoinChannel = OnRejoinChannel;
            nativeListener.OnRemoteAudioVolumeIndication = OnRemoteAudioVolumeIndication;
            nativeListener.OnUserAudioMute = OnUserAudioMute;
            nativeListener.OnUserAudioStart = OnUserAudioStart;
            nativeListener.OnUserAudioStop = OnUserAudioStop;
            nativeListener.OnUserJoined = OnUserJoined;
            nativeListener.OnUserLeft = OnUserLeft;
            nativeListener.OnWarning = OnWarning;
			nativeListener.OnLocalAudioVolumeIndication = OnLocalAudioVolumeIndication;
			nativeListener.OnUserPositionUpdated = OnUserPositionUpdated;
			nativeListener.OnAudioDeviceRoutingDidChange = OnAudioDeviceRoutingDidChange;
			nativeListener.OnAudioDefaultDeviceChanged = OnAudioDefaultDeviceChanged;
			nativeListener.OnReleasedHwResources = OnReleasedHwResources;

			Handlers.Add(ptrEngine, listener);

			return nativeListener;
        }
    }
}