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
    public interface IAudioDeviceManager
    {
        int AdjustRecordingSignalVolume(UInt32 volume);
        int AdjustPlaybackSignalVolume(UInt32 volume);
		int SetPlayoutDeviceMute(bool mute);
		bool GetPlayoutDeviceMute();
		int SetRecordDeviceMute(bool mute);
		bool GetRecordDeviceMute();
        int SetRecordDeviceVolume(UInt32 volume);
		UInt32 GetRecordDeviceVolume();
        int SetPlayoutDeviceVolume(UInt32 volume);
		UInt32 GetPlayoutDeviceVolume();
	}

    public class AudioDeviceManager : IAudioDeviceManager
    {
        private IntPtr ptrManager;

		public AudioDeviceManager(IntPtr ptrManager)
		{
			this.ptrManager = ptrManager;
		}

		class API
        {
            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_adjust_recording_signal_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int AdjustRecordingSignalVolume(IntPtr ptrManager, UInt32 volume);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_adjust_playback_signal_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int AdjustPlaybackSignalVolume(IntPtr ptrManager, UInt32 volume);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_set_playout_device_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetPlayoutDeviceMute(IntPtr ptrManager, bool mute);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_get_playout_device_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool GetPlayoutDeviceMute(IntPtr ptrManager);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_set_record_device_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetRecordDeviceMute(IntPtr ptrManager, bool mute);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_get_record_device_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool GetRecordDeviceMute(IntPtr ptrManager);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_set_record_device_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetRecordDeviceVolume(IntPtr ptrManager, UInt32 volume);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_get_record_device_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int GetRecordDeviceVolume(IntPtr ptrManagere);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_set_playout_device_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int SetPlayoutDeviceVolume(IntPtr ptrManager, UInt32 volume);

            [DllImport(NERTC.Config.NativeDLL, EntryPoint = "nertc_audio_device_manager_get_playout_device_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int GetPlayoutDeviceVolume(IntPtr ptrManager);

        }

        public int AdjustRecordingSignalVolume(UInt32 volume)
        {
            return API.AdjustRecordingSignalVolume(this.ptrManager, volume);
        }

        public int AdjustPlaybackSignalVolume(UInt32 volume)
        {
            return API.AdjustPlaybackSignalVolume(this.ptrManager, volume);
        }

        public int SetPlayoutDeviceMute(bool mute) {
            return API.SetPlayoutDeviceMute(ptrManager, mute);
        }

        public bool GetPlayoutDeviceMute() {
			return API.GetPlayoutDeviceMute(ptrManager);
		}

        public int SetRecordDeviceMute(bool mute) {
            return API.SetRecordDeviceMute(ptrManager, mute);
        }

        public bool GetRecordDeviceMute() {
			return API.GetRecordDeviceMute(ptrManager);
		}

        public int SetRecordDeviceVolume(UInt32 volume)
        {
            return API.SetRecordDeviceVolume(ptrManager, volume);
        }

        public UInt32 GetRecordDeviceVolume()
		{
			return Convert.ToUInt32(API.GetRecordDeviceVolume(ptrManager));
		}
        public int SetPlayoutDeviceVolume(UInt32 volume)
        {
            return API.SetPlayoutDeviceVolume(ptrManager, volume);
        }
        public UInt32 GetPlayoutDeviceVolume()
        {
			return Convert.ToUInt32(API.GetPlayoutDeviceVolume(ptrManager));
		}
    }
}