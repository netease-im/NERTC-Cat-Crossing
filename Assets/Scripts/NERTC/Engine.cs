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
using System.Runtime.InteropServices;

namespace NERTC
{
    public struct NERtcEngineContext
    {
        public string AppKey;                       /**< 用户注册云信的APP Key。如果你的开发包里面缺少 APP Key，请申请注册一个新的 APP Key。*/
        public string LogDirPath;                  /**< 日志目录的完整路径，采用UTF-8 编码。*/
        public int LogLevel;                   /**< 日志级别，默认级别为 k_nERtc_log_level_info。*/
        public int LogFileMaxSizeKbytes;         /**< 指定 SDK 输出日志文件的大小上限，单位为 KB。如果设置为 0，则默认为 20 M。*/
        public RtcEngineEventHandler EventHandler;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct NativeRtcEngineContext
    {
        public string AppKey;                       /**< 用户注册云信的APP Key。如果你的开发包里面缺少 APP Key，请申请注册一个新的 APP Key。*/
        public string LogDirPath;                  /**< 日志目录的完整路径，采用UTF-8 编码。*/
        public Int32 LogLevel;                   /**< 日志级别，默认级别为 k_nERtc_log_level_info。*/
        public Int32 LogFileMaxSizeKbytes;         /**< 指定 SDK 输出日志文件的大小上限，单位为 KB。如果设置为 0，则默认为 20 M。*/
        public NativeRtcEngineEventHandler EventHandler;
    }
    
    public interface IRtcEngine
    {
        int Initialize(NERtcEngineContext param);
        IntPtr QueryAudioDeviceInterface();
        int Release(bool sync);
        int JoinChannel(string token, string channelName, UInt64 uid);
        int LeaveChannel();
        int EnableLocalAudio(bool enabled);

        int SubscribeRemoteAudioStream(UInt64 uid, bool subscribe);

        int SetAudioProfile(Int32 profile, Int32 scenario);

		int SetParameters(string parameters);

        int MuteLocalAudioStream(bool mute);

        int StartAudioDump();

        int StopAudioDump();

        void SetMediaStatsObserver(MediaStatsObserver mediaStatsObserver);

        int EnableAudioVolumeIndication(bool enable, Int32 interval);

        int EnableEarback(bool enabled, Int32 volume);

        int SetEarbackVolume(Int32 volume);

		int UploadSdkInfo();

        int SetChannelProfile(int channelProfile);

        int GetConnectionState();

        int SetClientRole(Int32 role);

        int UpdateAudioRecvRange(int range, int rollOff);

        int UpdateSelfPosition(EngineAudioPositionInfo positionInfo);

        int EnableSpatializer(bool enable, bool applyToTeam);

        int EnableRoomEffects(bool enable);

        int SetRoomProperty(int size, int material, float reflectionScalar, float reverbGain, float reverbTime, float reverbBrightness);

        int SetRenderMode(int mode);
    }
    
    class RtcEngine : IRtcEngine
    {
        private IntPtr ptrEngine;

        public RtcEngine(IntPtr ptrEngine)
        {
            this.ptrEngine = ptrEngine;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DoLog(string message);

        private class NativeRtcAPI
        {
            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "create_nertc_engine", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr Create();

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "destroy_nertc_engine", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Destroy(IntPtr ptrEngine);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_query_audio_interface", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr QueryAudioDeviceInterface(IntPtr ptrEngine);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
			internal static extern int Initialize(IntPtr ptrEngine, [MarshalAs(UnmanagedType.LPStruct)] NativeRtcEngineContext param);
			// internal static extern int Initialize(IntPtr ptrEngine, NativeRtcEngineContext param);

			[DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_release", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int Release(IntPtr ptrEngine, Int32 sync);
            
            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_join_channel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int JoinChannel(IntPtr ptrEngine, string token, string channelName, UInt64 uid);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_leave_channel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int LeaveChannel(IntPtr ptrEngine);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_enable_local_audio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int EnableLocalAudio(IntPtr ptrEngine, bool enabled);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_subscribe_remote_audio_stream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SubscribeRemoteAudioStream(IntPtr ptrEngine, UInt64 uid, bool subscribe);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_audio_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetAudioProfile(IntPtr ptrEngine, Int32 profile, Int32 scenario);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_parameters", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetParameters(IntPtr ptrEngine, string parameters);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_mute_local_audio_stream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int MuteLocalAudioStream(IntPtr ptrEngine, bool mute);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_start_audio_dump", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int StartAudioDump(IntPtr ptrEngine);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_stop_audio_dump", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int StopAudioDump(IntPtr ptrEngine);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_stats_observer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
			internal static extern void SetMediaStatsObserver(IntPtr ptrEngine, [MarshalAs(UnmanagedType.LPStruct)] NativeMediaStatsObserver mediaStatsObserver);

			[DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_enable_audio_volume_indication", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int EnableAudioVolumeIndication(IntPtr ptrEngine, bool enable, Int32 interval);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_enable_earback", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int EnableEarback(IntPtr ptrEngine, bool enabled, Int32 volume);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_earback_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetEarbackVolume(IntPtr ptrEngine, Int32 volume);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_upload_sdk_info", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void UploadSdkInfo(IntPtr ptrEngine);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_channel_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetChannelProfile(IntPtr ptrEngine, int channelProfile);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_get_connection_state", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int GetConnectionState(IntPtr ptrEngine);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_client_role", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetClientRole(IntPtr ptrEngine, Int32 role);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_update_audio_recv_range", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int UpdateAudioRecvRange(IntPtr ptrEngine, int range, int rollOff);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_update_self_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int UpdateSelfPosition(IntPtr ptrEngine, [MarshalAs(UnmanagedType.LPStruct)] EngineAudioPositionInfo positionInfo);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_enable_spatializer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int EnableSpatializer(IntPtr ptrEngine, bool enable, bool applyToTeam);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_enable_room_effects", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int EnableRoomEffects(IntPtr ptrEngine, bool enable);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_room_property", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetRoomProperty(IntPtr ptrEngine, int size, int material, float reflectionScalar, float reverbGain, float reverbTime, float reverbBrightness);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_render_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetRenderMode(IntPtr ptrEngine, int mode);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_engine_set_custom_logger", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetCustomLogger(DoLog logger);
        }

        public static IRtcEngine Create()
        {
            IntPtr ptr = NativeRtcAPI.Create();

            if (ptr == null)
            {
                return null;
            }

            NativeRtcAPI.SetCustomLogger(Log);

            return new RtcEngine(ptr);
        }

        public static void Destroy(IRtcEngine engine)
        {
            RtcEngine rtcEngine = engine as RtcEngine;

            if (rtcEngine != null)
            {
                NativeRtcAPI.Destroy(rtcEngine.ptrEngine);
            }
        }

        private static NativeRtcEngineEventHandler cachedEvent;
        public int Initialize(NERtcEngineContext param)
        {
            NativeRtcEngineContext nativeParam = new NativeRtcEngineContext();

            nativeParam.AppKey = param.AppKey;                       /**< 用户注册云信的APP Key。如果你的开发包里面缺少 APP Key，请申请注册一个新的 APP Key。*/
            nativeParam.LogDirPath = param.LogDirPath;                  /**< 日志目录的完整路径，采用UTF-8 编码。*/
            nativeParam.LogLevel = param.LogLevel;                   /**< 日志级别，默认级别为 k_nERtc_log_level_info。*/
            nativeParam.LogFileMaxSizeKbytes = param.LogFileMaxSizeKbytes;         /**< 指定 SDK 输出日志文件的大小上限，单位为 KB。如果设置为 0，则默认为 20 M。*/
            cachedEvent = NativeEngineEventHandler.CreateNativeEngineEventListener(ptrEngine, param.EventHandler);
            nativeParam.EventHandler = cachedEvent;
			return NativeRtcAPI.Initialize(this.ptrEngine, nativeParam);
        }

        [MonoPInvokeCallback(typeof(DoLog))]
        public static void Log(string message) {
            Cons.Log(message);
        }

        public IntPtr QueryAudioDeviceInterface()
        {
            return NativeRtcAPI.QueryAudioDeviceInterface(this.ptrEngine);
        }

        public int Release(bool sync)
        {
            int s = 1;

            if (sync) {
                s = 0;
            }

            return NativeRtcAPI.Release(this.ptrEngine, s);
        }

        public int JoinChannel(string token, string channelName, UInt64 uid)
        {
			return NativeRtcAPI.JoinChannel(this.ptrEngine, token, channelName, uid);
        }

        public int LeaveChannel()
        {
			return NativeRtcAPI.LeaveChannel(this.ptrEngine);
        }

        public int EnableLocalAudio(bool enabled)
        {
            return NativeRtcAPI.EnableLocalAudio(this.ptrEngine, enabled);
        }

        public int SubscribeRemoteAudioStream(ulong uid, bool subscribe)
        {
            return NativeRtcAPI.SubscribeRemoteAudioStream(this.ptrEngine, uid, subscribe);
        }

        public int SetAudioProfile(int profile, int scenario)
        {
            return NativeRtcAPI.SetAudioProfile(this.ptrEngine, profile, scenario);
        }

        public int SetParameters(string parameters)
        {
            NativeRtcAPI.SetParameters(this.ptrEngine, parameters);
            return 0;
        }

        public int MuteLocalAudioStream(bool mute)
        {
            return NativeRtcAPI.MuteLocalAudioStream(this.ptrEngine, mute);
        }

        public int StartAudioDump()
        {
            return NativeRtcAPI.StartAudioDump(this.ptrEngine);
        }

        public int StopAudioDump()
        {
            return NativeRtcAPI.StopAudioDump(this.ptrEngine);
        }

		private static NativeMediaStatsObserver _mediaStatsObserver;
		public void SetMediaStatsObserver(MediaStatsObserver mediaStatsObserver) {
			_mediaStatsObserver = NativeMediaStatsObserverHandler.CreateNativeMediaStatsObserver(ptrEngine, mediaStatsObserver);
			NativeRtcAPI.SetMediaStatsObserver(this.ptrEngine, _mediaStatsObserver);
        }

        public int EnableAudioVolumeIndication(bool enable, int interval)
        {
            return NativeRtcAPI.EnableAudioVolumeIndication(this.ptrEngine, enable, interval);
        }

        public int EnableEarback(bool enabled, int volume)
        {
            return NativeRtcAPI.EnableEarback(this.ptrEngine, enabled, volume);
        }

        public int SetEarbackVolume(int volume)
        {
            return NativeRtcAPI.SetEarbackVolume(this.ptrEngine, volume);
        }

        public int UploadSdkInfo()
        {
            NativeRtcAPI.UploadSdkInfo(this.ptrEngine);
            return 0;
        }

        public int SetChannelProfile(int channelProfile)
        {
            return NativeRtcAPI.SetChannelProfile(this.ptrEngine, channelProfile);
        }

        public int GetConnectionState()
        {
            return NativeRtcAPI.GetConnectionState(this.ptrEngine);
        }

        public int SetClientRole(int role)
        {
            return NativeRtcAPI.SetClientRole(this.ptrEngine, role);
        }

        public int UpdateAudioRecvRange(int range, int rollOff) {
            return NativeRtcAPI.UpdateAudioRecvRange(ptrEngine, range, rollOff);
        }

        public int UpdateSelfPosition(EngineAudioPositionInfo positionInfo) {
            return NativeRtcAPI.UpdateSelfPosition(ptrEngine, positionInfo);
        }

        public int EnableSpatializer(bool enable, bool applyToTeam) {
            return NativeRtcAPI.EnableSpatializer(ptrEngine, enable, applyToTeam);
        }

        public int EnableRoomEffects(bool enable) {
            return NativeRtcAPI.EnableRoomEffects(ptrEngine, enable);
        }

        public int SetRoomProperty(int size, int material, float reflectionScalar,
            float reverbGain, float reverbTime, float reverbBrightness) {
            return NativeRtcAPI.SetRoomProperty(ptrEngine, size, material, reflectionScalar, reverbGain, reverbTime, reverbBrightness);
        }

        public int SetRenderMode(int mode) {
            return NativeRtcAPI.SetRenderMode(ptrEngine, mode);
        }
    }
}
