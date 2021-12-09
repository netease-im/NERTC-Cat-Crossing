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
    /** 系统分类。*/
    public enum OsCategory : Int32
    {
        NERTC_OS_IOS = 1,  /**< IOS 通用设备 */
        NERTC_OS_ANDROID = 2,  /**< ANDROID 通用设备 */
        NERTC_OS_PC = 3,  /**< PC设备 */
        NERTC_OS_WEB_RTC = 4,  /**< WEB_RTC */
    };

    /** 接口ID类型。*/
    public enum InterfaceIdType : Int32
    {
        NERTC_IID_AUDIO_DEVICE_MANAGER = 1,                /**< 获取音频设备管理器的接口ID */
        NERTC_IID_VIDEO_DEVICE_MANAGER = 2,                /**< 获取视频设备管理器的接口ID */
    }

    /** 参会者角色类型 */
    public enum ClientRole : Int32
    {
        NERTC_CLIENT_ROLE_BROADCASTER = 0,            /**< 主播 */
        NERTC_CLIENT_ROLE_AUDIENCE = 1,            /**< 观众 */
    }

    /** 场景模式 */
    public enum ChannelProfileType : Int32
    {
        NERTC_CHANNEL_PROFILE_COMMUNICATION = 0,    /**< 通话场景 */
        NERTC_CHANNEL_PROFILE_LIVE_BROADCASTING = 1,    /**< 直播推流场景 */
    }

    /** 音频属性。设置采样率，码率，编码模式和声道数。*/
    public enum AudioProfileType : Int32
    {
        NERTC_AUDIO_PROFILE_DEFAULT = 0,              /**< 0: 默认设置。SPEECH场景下为 NERTC_AUDIO_PROFILE_STANDARD_EXTEND，MUSIC场景下为 NERTC_AUDIO_PROFILE_HIGH_QUALITY */
        NERTC_AUDIO_PROFILE_STANDARD = 1,             /**< 1: 普通质量的音频编码，16000HZ，20KBPS */
        NERTC_AUDIO_PROFILE_STANDARD_EXTEND = 2,       /**< 2: 普通质量的音频编码，16000HZ，32KBPS */
        NERTC_AUDIO_PROFILE_MIDDLE_QUALITY = 3,        /**< 3: 中等质量的音频编码，48000HZ，32KBPS */
        NERTC_AUDIO_PROFILE_MIDDLE_QUALITY_STEREO = 4,  /**< 4: 中等质量的立体声编码，48000HZ * 2，64KBPS  */
        NERTC_AUDIO_PROFILE_HIGH_QUALITY = 5,          /**< 5: 高质量的音频编码，48000HZ，64KBPS  */
        NERTC_AUDIO_PROFILE_HIGH_QUALITY_STEREO = 6,    /**< 6: 高质量的立体声编码，48000HZ * 2，128KBPS  */
    }

    /** 音频应用场景。不同的场景设置对应不同的音频采集模式（移动平台）、播放模式*/
    public enum AudioScenarioType : Int32
    {
        NERTC_AUDIO_SCENARIO_DEFAULT = 0,    /** 0: 默认设置:NERTC_CHANNEL_PROFILE_COMMUNICATION下为NERTC_AUDIO_SCENARIO_SPEECH，NERTC_CHANNEL_PROFILE_LIVE_BROADCASTING下为NERTC_AUDIO_SCENARIO_MUSIC。 */
        NERTC_AUDIO_SCENARIO_SPEECH = 1,    /** 1: 语音场景. NERTC_AUDIO_PROFILE_TYPE 推荐使用 NERTC_AUDIO_PROFILE_MIDDLE_QUALITY 及以下 */
        NERTC_AUDIO_SCENARIO_MUSIC = 2,    /** 2: 音乐场景。NERTC_AUDIO_PROFILE_TYPE 推荐使用 NERTC_AUDIO_PROFILE_MIDDLE_QUALITY_STEREO 及以上 */
    }

    /** 视频编码配置。用于衡量编码质量。*/
    public enum VideoProfileType : Int32
    {
        NERTC_VIDEO_PROFILE_LOWEST = 0,       /**< 160X_90/120, 15FPS */
        NERTC_VIDEO_PROFILE_LOW = 1,          /**< 320X_180/240, 15FPS */
        NERTC_VIDEO_PROFILE_STANDARD = 2,     /**< 640X_360/480, 30FPS */
        NERTC_VIDEO_PROFILE_HD720P = 3,       /**< 1280X_720, 30FPS */
        NERTC_VIDEO_PROFILE_HD1080P = 4,      /**< 1920X_1080, 30FPS */
        NERTC_VIDEO_PROFILE_NONE = 5,
        NERTC_VIDEO_PROFILE_MAX = NERTC_VIDEO_PROFILE_HD1080P,
    }

    /** 视频流类型。*/
    public enum RemoteVideoStreamType : Int32
    {
        NERTC_REMOTE_VIDEO_STREAM_TYPE_HIGH = 0, /**< 默认大流 */
        NERTC_REMOTE_VIDEO_STREAM_TYPE_LOW = 1, /**< 小流 */
        NERTC_REMOTE_VIDEO_STREAM_TYPE_NONE = 2, /**< 不订阅 */
    }

    /** 音频设备类型。*/
    public enum AudioDeviceType : Int32
    {
        NERTC_AUDIO_DEVICE_UNKNOWN = 0,       /**< 未知音频设备 */
        NERTC_AUDIO_DEVICE_RECORD,            /**< 音频采集设备 */
        NERTC_AUDIO_DEVICE_PLAYOUT,           /**< 音频播放设备 */
    }

    /** 音频设备类型状态。*/
    public enum AudioDeviceState : Int32
    {
        NERTC_AUDIO_DEVICE_ACTIVE = 0,    /**< 音频设备已激活 */
        NERTC_AUDIO_DEVICE_UNACTIVE,      /**< 音频设备未激活 */
    }

    /** 音频设备链接类型。*/
    public enum AudioDeviceTransportType : Int32
    {
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_UNKNOWN = 0,    /**< 未知设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_BLUETOOTH = 1,    /**< 蓝牙设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_BLUETOOTH_A2DP = 2,    /**< 蓝牙立体声设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_BLUETOOTH_LE = 3,    /**< 蓝牙低功耗设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_USB = 4,    /**< USB设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_HDMI = 5,    /**< HDMI设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_BUILT_IN = 6,    /**< 内置设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_THUNDERBOLT = 7,    /**< 雷电接口设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_AIR_PLAY = 8,    /**< AIR_PLAY设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_VIRTUAL = 9,    /**< 虚拟设备 */
        NERTC_AUDIO_DEVICE_TRANSPORT_TYPE_OTHER = 10,   /**< 其他设备 */
    }

    /** 摄像头设备链接类型。*/
    public enum VideoDeviceTransportType : Int32
    {
        NERTC_VIDEO_DEVICE_TRANSPORT_TYPE_UNKNOWN = 0,    /**< 未知设备 */
        NERTC_VIDEO_DEVICE_TRANSPORT_TYPE_USB = 1,    /**< USB设备 */
        NERTC_VIDEO_DEVICE_TRANSPORT_TYPE_VIRTUAL = 2,    /**< 虚拟设备 */
        NERTC_VIDEO_DEVICE_TRANSPORT_TYPE_OTHER = 3,    /**< 其他设备 */
    }

    /** 视频设备类型。*/
    public enum VideoDeviceType : Int32
    {
        NERTC_VIDEO_DEVICE_UNKNOWN = 0,   /**< 未知视频设备 */
        NERTC_VIDEO_DEVICE_CAPTURE,       /**< 视频采集设备 */
    }

    /** 视频设备类型状态。*/
    public enum VideoDeviceState : Int32
    {
        NERTC_VIDEO_DEVICE_ADDED = 0, /**< 视频设备已添加 */
        NERTC_VIDEO_DEVICE_REMOVED,   /**< 视频设备已拔除 */
    }

    /** @enum NertcVideoScalingMode : Int32 设置视频缩放模式。*/
    public enum VideoScalingMode : Int32
    {
        NERTC_VIDEO_SCALE_FIT = 0,   /**< 0: 视频尺寸等比缩放。优先保证视频内容全部显示。因视频尺寸与显示视窗尺寸不一致造成的视窗未被填满的区域填充黑色。*/
        NERTC_VIDEO_SCALE_FULL_FILL = 1,   /**< 1: 视频尺寸非等比缩放。保证视频内容全部显示，且填满视窗。*/
        NERTC_VIDEO_SCALE_CROP_FILL = 2,   /**< 2: 视频尺寸等比缩放。优先保证视窗被填满。因视频尺寸与显示视窗尺寸不一致而多出的视频将被截掉。*/
    }

    /** @enum NertcVideoMirrorMode : Int32 设置镜像模式。*/
    public enum VideoMirrorMode : Int32
    {
        NERTC_VIDEO_MIRROR_MODE_AUTO = 0,    /**< 0: WINDOWS/MAC_OS SDK 启用镜像模式。在 I_OS/ANDROID 平台中：如果你使用前置摄像头，SDK 默认启用镜像模式；如果你使用后置摄像头，SDK 默认关闭镜像模式。*/
        NERTC_VIDEO_MIRROR_MODE_ENABLED = 1,    /**< 1: 启用镜像模式。*/
        NERTC_VIDEO_MIRROR_MODE_DISABLED = 2,    /**< 2: （默认）关闭镜像模式。*/
    }

    /** 连接状态 */
    public enum ConnectionStateType : Int32
    {
        NERTC_CONNECTION_STATE_DISCONNECTED = 1, /**< 没加入频道。*/
        NERTC_CONNECTION_STATE_CONNECTING = 2, /**< 正在加入频道。*/
        NERTC_CONNECTION_STATE_CONNECTED = 3, /**< 加入频道成功。*/
        NERTC_CONNECTION_STATE_RECONNECTING = 4, /**< 正在尝试重新加入频道。*/
        NERTC_CONNECTION_STATE_FAILED = 5, /**< 加入频道失败。*/
    }

    /** 连接状态变更原因 */
    public enum ReasonConnectionChangedType : Int32
    {
        NERTC_REASON_CONNECTION_CHANGED_LEAVE_CHANNEL = 1, //NERTC_CONNECTION_STATE_DISCONNECTED 离开房间
        NERTC_REASON_CONNECTION_CHANGED_CHANNEL_CLOSED = 2, //NERTC_CONNECTION_STATE_DISCONNECTED    房间被关闭
        NERTC_REASON_CONNECTION_CHANGED_BE_KICKED = 3, //NERTC_CONNECTION_STATE_DISCONNECTED 用户被踢
        NERTC_REASON_CONNECTION_CHANGED_TIME_OUT = 4, //NERTC_CONNECTION_STATE_DISCONNECTED    服务超时
        NERTC_REASON_CONNECTION_CHANGED_JOIN_CHANNEL = 5, //NERTC_CONNECTION_STATE_CONNECTING 加入房间
        NERTC_REASON_CONNECTION_CHANGED_JOIN_SUCCEED = 6, //NERTC_CONNECTION_STATE_CONNECTED 加入房间成功
        NERTC_REASON_CONNECTION_CHANGED_RE_JOIN_SUCCEED = 7, //NERTC_CONNECTION_STATE_CONNECTED 重新加入房间成功（重连）
        NERTC_REASON_CONNECTION_CHANGED_MEDIA_CONNECTION_DISCONNECTED = 8, //NERTC_CONNECTION_STATE_RECONNECTING 媒体连接断开
        NERTC_REASON_CONNECTION_CHANGED_SIGNAL_DISCONNECTED = 9, //NERTC_CONNECTION_STATE_RECONNECTING 信令连接断开
        NERTC_REASON_CONNECTION_CHANGED_REQUEST_CHANNEL_FAILED = 10, //NERTC_CONNECTION_STATE_FAILED 请求频道失败
        NERTC_REASON_CONNECTION_CHANGED_JOIN_CHANNEL_FAILED = 11, //NERTC_CONNECTION_STATE_FAILED 加入频道失败
    }

    /** @enum NetworkQualityype : Int32 网络质量类型。*/
    public enum NetworkQualityType : Int32
    {
        NERTC_NETWORK_QUALITY_UNKNOWN = 0,    /**< 0: 网络质量未知。*/
        NERTC_NETWORK_QUALITY_EXCELLENT = 1,  /**< 1: 网络质量极好。*/
        NERTC_NETWORK_QUALITY_GOOD = 2,       /**< 2: 用户主观感觉和 EXCELLENT 差不多，但码率可能略低于 EXCELLENT。*/
        NERTC_NETWORK_QUALITY_POOR = 3,       /**< 3: 用户主观感受有瑕疵但不影响沟通。*/
        NERTC_NETWORK_QUALITY_BAD = 4,        /**< 4: 勉强能沟通但不顺畅。*/
        NERTC_NETWORK_QUALITY_VERY_BAD = 5,    /**< 5: 网络质量非常差，基本不能沟通。*/
        NERTC_NETWORK_QUALITY_DOWN = 6,       /**< 6: 完全无法沟通。*/
    }

    /** @enum NertcVideoCropMode : Int32 视频画面裁剪模式。*/
    public enum VideoCropMode : Int32
    {
        NERTC_VIDEO_CROP_MODE_DEFAULT = 0,     /**< DEVICE DEFALUT */
        NERTC_VIDEO_CROP_MODE_16X_9 = 1,     /**< 16:9 */
        NERTC_VIDEO_CROP_MODE_4X_3 = 2,     /**< 4:3 */
        NERTC_VIDEO_CROP_MODE_1X_1 = 3,     /**< 1:1 */
    }

    /** @enum VideoFramerateype : Int32 视频帧率。*/
    public enum VideoFramerateType : Int32
    {
        NERTC_VIDEO_FRAMERATE_FPS_DEFAULT = 0,    /**< 默认帧率 */
        NERTC_VIDEO_FRAMERATE_FPS_7 = 7,    /**< 7帧每秒 */
        NERTC_VIDEO_FRAMERATE_FPS_10 = 10,   /**< 10帧每秒 */
        NERTC_VIDEO_FRAMERATE_FPS_15 = 15,   /**< 15帧每秒 */
        NERTC_VIDEO_FRAMERATE_FPS_24 = 24,   /**< 24帧每秒 */
        NERTC_VIDEO_FRAMERATE_FPS_30 = 30,   /**< 30帧每秒 */
    }

    /** @enum NertcDegradationPreference : Int32 视频编码策略。*/
    public enum DegradationPreference : Int32
    {
        NERTC_DEGRADATION_DEFAULT = 0,  /**< 使用引擎推荐值。通话场景使用平衡模式，直播推流场景使用清晰优先 */
        NERTC_DEGRADATION_MAINTAIN_FRAMERATE = 1,  /**< 帧率优先 */
        NERTC_DEGRADATION_MAINTAIN_QUALITY = 2,  /**< 清晰度优先 */
        NERTC_DEGRADATION_BALANCED = 3,  /**< 平衡模式 */
    }

    /** 屏幕共享编码参数配置。*/
    public enum ScreenProfileType : Int32
    {
        NERTC_SCREEN_PROFILE_480P = 0,    /**< 640X_480, 5FPS */
        NERTC_SCREEN_PROFILE_HD720P = 1,    /**< 1280X_720, 5FPS */
        NERTC_SCREEN_PROFILE_HD1080P = 2,    /**< 1920X_1080, 5FPS。默认 */
        NERTC_SCREEN_PROFILE_CUSTOM = 3,    /**< 自定义 */
        NERTC_SCREEN_PROFILE_NONE = 4,
        NERTC_SCREEN_PROFILE_MAX = NERTC_SCREEN_PROFILE_HD1080P,
    }

    /** 屏幕共享功能的编码策略倾向

- nertc_sub_stream_content_prefer_motion: 内容类型为动画;当共享的内容是视频、电影或游戏时，推荐选择该内容类型
当用户设置内容类型为动画时，按用户设置的帧率处理
        
- nertc_sub_stream_content_prefer_details: 内容类型为细节;当共享的内容是图片或文字时，推荐选择该内容类型
当用户设置内容类型为细节时，最高允许用户设置到10帧，设置超过10帧时，不生效，按10帧处理

     */
    public enum SubStreamContentPrefer : Int32
    {
        NERTC_SUB_STREAM_CONTENT_PREFER_MOTION = 0,    /**< 动画模式。*/
        NERTC_SUB_STREAM_CONTENT_PREFER_DETAILS = 1,    /**< 细节模式。*/
    }

    /** 录制类型。*/
    public enum RecordType : Int32
    {
        NERTC_RECORD_TYPE_ALL = 0,    /**< 参与混合录制且录制单人文件。*/
        NERTC_RECORD_TYPE_MIX = 1,    /**< 参与混合录制。*/
        NERTC_RECORD_TYPE_SINGLE = 2, /**< 只录单人文件。*/
    }

    /** 音频类型。*/
    public enum AudioType : Int32
    {
        NERTC_AUDIO_TYPE_PCM16 = 0,    /**< PCM 音频格式。*/
    }

    /** 音频帧请求数据的读写模式。*/
    public enum RawAudioFrameOpModeType : Int32
    {
        NERTC_RAW_AUDIO_FRAME_OP_MODE_READ_ONLY = 0,     //AUDIO CALLBACK JUST SUPPORT READ ONLY.
        NERTC_RAW_AUDIO_FRAME_OP_MODE_READ_WRITE,        //AUDIO CALLBACK SUPPORT READ AND WRITE.   
    }

    /** 视频类型。*/
    public enum VideoType : Int32
    {
        NERTC_VIDEO_TYPE_I420 = 0,    /**< I420 视频格式。*/
        NERTC_VIDEO_TYPE_NV12 = 1,    /**< NV12 视频格式。*/
        NERTC_VIDEO_TYPE_NV21 = 2,    /**< NV21 视频格式。*/
        NERTC_VIDEO_TYPE_BGRA = 3,    /**< BGRA 视频格式。*/
        NERTC_VIDEO_TYPE_CVPIXEL_BUFFER = 4,    /**< OC CAPTURE NATIVE视频格式。不支持外部视频输入*/
    }

    /** 视频旋转角度。*/
    public enum VideoRotation : Int32
    {
        NERTC_VIDEO_ROTATION_0 = 0,      /**< 0 度。*/
        NERTC_VIDEO_ROTATION_90 = 90,    /**< 90 度。*/
        NERTC_VIDEO_ROTATION_180 = 180,  /**< 180 度。*/
        NERTC_VIDEO_ROTATION_270 = 270,  /**< 270 度。*/
    }

    /** 用户离开原因。*/
    public enum SessionLeaveReason : Int32
    {
        NERTC_SESSION_LEAVE_NORMAL = 0,       /**< 正常离开。*/
        NERTC_SESSION_LEAVE_FOR_FAIL_OVER = 1,  /**< 用户断线导致离开。*/
        NERTC_SESSION_LEAVE_UPDATE = 2,       /**< 用户 FAILOVER 过程中产生的 LEAVE。*/
        NERTC_SESSION_LEAVE_FOR_KICK = 3,      /**< 用户被踢导致离开。*/
        NERTC_SESSION_LEAVE_TIME_OUT = 4,      /**< 用户超时导致离开。*/
    }

    /** 音乐文件播放状态。
*/
    public enum AudioMixingState : Int32
    {
        NERTC_AUDIO_MIXING_STATE_FINISHED = 0,       /**< 音乐文件播放结束。*/
        NERTC_AUDIO_MIXING_STATE_FAILED = 1,       /**< 音乐文件报错。详见: #NERTC_AUDIO_MIXING_ERROR_CODE*/
    }

    /** 日志级别。 */
    public enum LogLevel : Int32
    {
        NERTC_LOG_LEVEL_FATAL = 0,        /**< FATAL 级别日志信息。 */
        NERTC_LOG_LEVEL_ERROR = 1,        /**< ERROR 级别日志信息。 */
        NERTC_LOG_LEVEL_WARNING = 2,        /**< WARNING 级别日志信息。 */
        NERTC_LOG_LEVEL_INFO = 3,        /**< INFO 级别日志信息。默认级别 */
        NERTC_LOG_LEVEL_DETAIL_INFO = 4,        /**< DETAIL INFO 级别日志信息。 */
        NERTC_LOG_LEVEL_VERBOS = 5,        /**< VERBOS 级别日志信息。 */
        NERTC_LOG_LEVEL_DEBUG = 6,        /**< DEBUG 级别日志信息。如果你想获取最完整的日志，可以将日志级别设为该等级。*/
        NERTC_LOG_LEVEL_OFF = 7,        /**< 不输出日志信息。*/
    }

    /** 视频推流后发送策略。 */
    public enum SendOnPubType : Int32
    {
        NERTC_SEND_ON_PUB_NONE = 0, /**< 不主动发送数据流，被订阅后发送。 */
        NERTC_SEND_ON_PUB_HIGH = 1, /**< 主动发送大流。 */
        NERTC_SEND_ON_PUB_LOW = 1 << 1, /**< 主动发送小流。 */
        NERTC_SEND_ON_PUB_ALL = NERTC_SEND_ON_PUB_LOW | NERTC_SEND_ON_PUB_HIGH, /**< 主动发送大小流。 */
    }

    /** 设备详细信息。*/
    public struct DeviceInfo
    {
        public string deviceId;        /**< 设备id */
        public string deviceName;    /**< 设备名称 */
        int transportType;                             /**< 设备链接类型，分别指向nertcAudioDeviceTransportType及nertcVideoDeviceTransportType */
        bool suspectedUnavailable;                     /**< 是否是不推荐设备 */
        bool systemDefaultDevice;                     /**< 是否是系统默认设备 */
    }

    /** 声音音量信息。一个数组，包含每个说话者的用户 ID 和音量信息。*/
    public struct AudioVolumeInfo
    {
        public UInt64 uid { get; set; }              /**< 说话者的用户 id。如果返回的 uid 为 0，则默认为本地用户。*/
        public uint volume { get; set; }    /**< 说话者的音量，范围为 0（最低）- 100（最高）。*/
    }

    /** 通话相关的统计信息。*/
    public struct Stats
    {
		public Int32 cpuAppUsage { get; set; }      /**< 当前 app 的 cpu 使用率 (%)。*/
		public Int32 cpuIdleUsage { get; set; }    /**< 当前系统的 cpu 空闲率 (%)。*/
		public Int32 cpuTotalUsage { get; set; }   /**< 当前系统的 cpu 使用率 (%)。*/
		public Int32 memoryAppUsage { get; set; }  /**< 当前app的内存使用率 (%)。*/
		public Int32 memoryTotalUsage;/**< 当前系统的内存使用率 (%)。*/
		public Int32 memoryAppKbytes { get; set; } /**< 当前app的内存使用量 (kb)。*/
		public int totalDuration { get; set; }         /**< 通话时长（秒）。*/
		public Int64 txBytes { get; set; }          /**< 发送字节数，累计值。(bytes)*/
		public Int64 rxBytes { get; set; }          /**< 接收字节数，累计值。(bytes)*/
		public Int64 txAudioBytes { get; set; }    /**< 音频发送字节数，累计值。(bytes)*/
		public Int64 txVideoBytes { get; set; }    /**< 视频发送字节数，累计值。(bytes)*/
		public Int64 rxAudioBytes { get; set; }    /**< 音频接收字节数，累计值。(bytes)*/
		public Int64 rxVideoBytes { get; set; }    /**< 视频接收字节数，累计值。(bytes)*/
		public int txAudioKbitrate { get; set; }      /**< 音频发送码率。(kbps)*/
		public int rxAudioKbitrate { get; set; }      /**< 音频接收码率。(kbps)*/
		public int txVideoKbitrate { get; set; }      /**< 视频发送码率。(kbps)*/
		public int rxVideoKbitrate { get; set; }      /**< 视频接收码率。(kbps)*/
		public int upRtt { get; set; }                 /**< 上行平均往返时延rtt(ms) */
		public int downRtt { get; set; }               /**< 下行平均往返时延rtt(ms) */
		public int txAudioPacketLossRate { get; set; }  /**< 本地上行音频实际丢包率。(%) */
		public int txVideoPacketLossRate { get; set; }  /**< 本地上行视频实际丢包率。(%) */
		public int txAudioPacketLossSum { get; set; }   /**< 本地上行音频实际丢包数。 */
		public int txVideoPacketLossSum { get; set; }   /**< 本地上行视频实际丢包数。 */
		public int txAudioJitter { get; set; }            /**< 本地上行音频抖动计算。(ms) */
		public int txVideoJitter { get; set; }            /**< 本地上行视频抖动计算。(ms) */
		public int rxAudioPacketLossRate { get; set; }  /**< 本地下行音频实际丢包率。(%) */
		public int rxVideoPacketLossRate { get; set; }  /**< 本地下行视频实际丢包率。(%) */
		public int rxAudioPacketLossSum { get; set; }   /**< 本地下行音频实际丢包数。 */
		public int rxVideoPacketLossSum { get; set; }   /**< 本地下行视频实际丢包数。 */
		public int rxAudioJitter { get; set; }            /**< 本地下行音频抖动计算。(ms) */
		public int rxVideoJitter { get; set; }            /**< 本地下行视频抖动计算。(ms) */
    }

    /** 本地视频单条流上传统计信息。*/
    public struct VideoLayerSendStats
    {
        int layerType;         /**< 流类型： 1、主流，2、辅流。*/
        int width;              /**< 视频流宽（像素）。*/
        int height;             /**< 视频流高（像素）。*/
        int captureFrameRate; /**< 视频采集帧率。*/
        int renderFrameRate;  /**< 视频渲染帧率。*/
        int encoderFrameRate; /**< 编码帧率。*/
        int sentFrameRate;    /**< 发送帧率。*/
        int sentBitrate;       /**< 发送码率(kbps)。*/
        int targetBitrate;     /**< 编码器目标码率(kbps)。*/
        int encoderBitrate;    /**< 编码器实际编码码率(kbps)。*/
        public string codecName; /**< 视频编码器名字。*/
    }

    /** 本地视频流上传统计信息。*/
    public struct VideoSendStats
    {
        VideoLayerSendStats videoLayersList;    /**< 视频流信息数组。*/
        int videoLayersCount;                         /**< 视频流条数。*/
    }

    /** 远端视频单条流的统计信息。*/
    public struct VideoLayerRecvStats
    {
        int layerType;         /**< 流类型： 1、主流，2、辅流。*/
        int width;              /**< 视频流宽（像素）。*/
        int height;             /**< 视频流高（像素）。*/
        int receivedBitrate;   /**< 接收到的码率(kbps)。*/
        int receivedFrameRate;    /**< 接收到的帧率 (fps)。*/
        int decoderFrameRate; /**< 解码帧率 (fps)。*/
        int renderFrameRate;  /**< 渲染帧率 (fps)。*/
        int packetLossRate;   /**< 下行丢包率(%)。*/
        int totalFrozenTime;  /**< 用户的下行视频卡顿累计时长(ms)。*/
        int frozenRate;        /**< 用户的下行视频平均卡顿率(%)。*/
        public string codecName; /**< 视频解码器名字。*/
    }

    /** 远端视频流的统计信息。*/
    public struct VideoRecvStats
    {
        UInt64 uid;      /**< 用户 id，指定是哪个用户的视频流。*/
        VideoLayerRecvStats videoLayersList;    /**< 视频流信息数组。*/
        int videoLayersCount;                         /**< 视频流条数。*/
    }

	/** 本地音频流上传统计信息。*/
	[StructLayout(LayoutKind.Sequential)]
	public struct AudioSendStats
    {
		public int numChannels { get; set; }       /**< 当前采集声道数。*/
		public int sentSampleRate { get; set; }    /**< 本地上行音频采样率。*/
		public int sentBitrate { get; set; }        /**< （上次统计后）发送码率(kbps)。*/
		public int audioLossRate { get; set; }     /**< 特定时间内的音频丢包率 (%)。*/
		public Int64 rtt { get; set; }            /**< rtt。*/
		public uint volume { get; set; }     /**< 音量，范围为 0（最低）- 100（最高）。*/
	}

	/** 远端用户的音频统计。*/
	[StructLayout(LayoutKind.Sequential)]
	public struct AudioRecvStats
    {
        public UInt64 uid { get; set; }              /**< 用户 id，指定是哪个用户的音频流。*/
        public int receivedBitrate { get; set; }   /**< （上次统计后）接收到的码率(kbps)。*/
		public int totalFrozenTime { get; set; }  /**< 用户的下行音频卡顿累计时长(ms)。*/
		public int frozenRate { get; set; }        /**< 用户的下行音频平均卡顿率(%)。*/
		public int audioLossRate { get; set; }  /**< 特定时间内的音频丢包率 (%)。*/
		public uint volume { get; set; }    /**< 音量，范围为 0（最低）- 100（最高）。*/
	}

    /** 网络质量统计信息。*/
    public struct NetworkQualityInfo
    {
		public UInt64 uid { get; set; }                         /**< 用户 id，指定是哪个用户的网络质量统计。*/
		public int txQuality { get; set; } /**< 该用户的上行网络质量，详见 #nertcNetworkQualityType.*/
		public int rxQuality { get; set; } /**< 该用户的下行网络质量，详见 #nertcNetworkQualityType.*/
	}

    /** 视频配置的属性。*/
    public struct VideoConfig
    {
        Int32 maxProfile;  /**< 视频编码的分辨率，用于衡量编码质量。*/
        Int32 width;                     /**< 视频编码自定义分辨率之宽度。width为0表示使用maxProfile*/
        Int32 height;                    /**< 视频编码自定义分辨率之高度。height为0表示使用maxProfile*/
        Int32 cropMode;      /**< 视频画面裁剪模式，默认nertcVideoCropModeDefault。*/
        Int32 framerate;  /**< 视频帧率 */
        Int32 minFramerate;  /**< 视频最小帧率 */
        Int32 bitrate;                   /**< 视频编码码率kbps，取0时使用默认值 */
        Int32 minBitrate;               /**< 视频编码码率下限kbps，取0时使用默认值 */
        Int32 degradationPreference;   /**< 编码策略 */
    }

    /** 待共享区域相对于整个屏幕或窗口的位置，如不填，则表示共享整个屏幕或窗口。*/
    public struct Rectangle
    {
        int x;      /**< 左上角的横向偏移。*/
        int y;      /**< 左上角的纵向偏移。*/
        int width;  /**< 待共享区域的宽。*/
        int height; /**< 待共享区域的高。*/
    }

    /** 视频尺寸。*/
    public struct VideoDimensions
    {
        int width;     /**< 宽度 */
        int height;    /**< 高度 */
    }

    /** 屏幕共享编码参数配置。用于衡量编码质量。一期只支持profile设置。*/
    public struct ScreenCaptureParameters
    {
        Int32 profile;     /**< 屏幕共享编码参数配置。*/
        VideoDimensions dimensions;    /**< 屏幕共享视频发送的最大像素值，nertcScreenProfileCustom下生效。*/
        int frameRate;                     /**< 共享视频的帧率，nertcScreenProfileCustom下生效，单位为 fps；默认值为 5，建议不要超过 15。*/
        int bitrate;                        /**< 共享视频的码率，单位为 bps；默认值为 0，表示 sdk 根据当前共享屏幕的分辨率计算出一个合理的值。*/
        bool captureMouseCursor;          /**< 是否采集鼠标用于屏幕共享。*/
        bool windowFocus;                  /**< 调用 startScreenCaptureByWindowId 方法共享窗口时，是否将该窗口前置。*/
        IntPtr excludedWindowList;         /**< 待屏蔽窗口的 id 列表。 */
        int excludedWindowCount;          /**< 待屏蔽窗口的数量。*/
        Int32 prefer; /**< 编码策略倾向。*/
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OnFrameDataCallback(
        UInt64 uid,          /**< uid */
        IntPtr data,         /**< 数据指针 */
        UInt32 type,      /**< nertc_video_type */
        UInt32 width,     /**< 宽度 */
        UInt32 height,    /**< 高度 */
        UInt32 count,     /**< 数据类型个数，即offset及stride的数目 */
        [MarshalAs(UnmanagedType.U4, SizeConst = 4)]
        UInt32[] offset, /**< 每类数据偏移 */
        [MarshalAs(UnmanagedType.U4, SizeConst = 4)]
        UInt32[] stride, /**< 每类数据步进 */
        UInt32 rotation,  /**< nertc_video_rotation */
        IntPtr user_data     /**< 用户透传数据 */);
    
    /** 视频显示设置 */
    public struct VideoCanvas
    {
        OnFrameDataCallback cb; /**< 数据回调  如果是macosx，需要设置nertcEngineContext的videoUseExnternalRender为true才有效*/
        IntPtr userData;        /**< 数据回调的用户透传数据 如果是macosx，需要设置nertcEngineContext的videoUseExnternalRender为true才有效 */
        IntPtr window;           /**< 渲染窗口句柄 如果是macosx，需要设置nertcEngineContext的videoUseExnternalRender为false才有效*/
        Int32 scalingMode; /**< 视频缩放模式 */
    }

    /** 音频帧请求格式。*/
    public struct AudioFrameRequestFormat
    {
        Int32 channels;      /**< 音频频道数量(如果是立体声，数据是交叉的)。单声道: 1；双声道 : 2。*/
        Int32 sampleRate;   /**< 采样率。*/
        int mode; /**<读写模式 */
    }

    /** 音频格式。*/
    public struct AudioFormat
    {
        int type;        /**< 音频类型。*/
        Int32 channels;      /**< 音频频道数量(如果是立体声，数据是交叉的)。单声道: 1；双声道 : 2。*/
        Int32 sampleRate;    /**< 采样率。*/
        Int32 bytesPerSample;    /**< 每个采样点的字节数 : 对于 pcm 来说，一般使用 16 bit，即两个字节。*/
        Int32 samplesPerChannel;    /**< 每个频道的样本数量。*/
    }

    /** 音频帧。*/
    public struct AudioFrame
    {
        AudioFormat format;    /**< 音频格式。*/
        IntPtr data;     /**< 数据缓冲区。有效数据长度为：samplesPerChannel * channels * bytesPerSample。*/
    }

    /** 外部输入的视频桢。*/
    public struct VideoFrame
    {
        int format;      /**< 视频类型  详见: #nertcVideoType*/
        Int64 timestamp;         /**< 视频桢时间戳 */
        Int32 width;             /**< 视频桢宽度 */
        Int32 height;            /**< 视频桢宽高 */
        int rotation;/**<  视频旋转角度 详见: #nertcVideoRotation */
        IntPtr buffer;               /**<  视频桢数据 */
    }

    /** 创建混音的配置项 */
    public struct CreateAudioMixingOption
    {
        public string path;  /**< 本地文件全路径或url */
        int loopCount;                  /**< 循环次数， <= 0, 表示无限循环，默认 1 */
        bool sendEnabled;               /**< 是否可发送，默认为 true */
        Int32 sendVolume;           /**< 发送音量。最大为 100（默认）含义（0%-100%）*/
        bool playbackEnabled;           /**< 是否可回放，默认为 true */
        Int32 playbackVolume;       /**< 回放音量。最大为 100（默认）*/
    }

    /** 创建音效的配置项 */
    public struct CreateAudioEffectOption
    {
        public string path;  /**< 本地文件全路径或url */
        int loopCount;                  /**< 循环次数， <= 0, 表示无限循环，默认 1 */
        bool sendEnabled;               /**< 是否可发送，默认为 true */
        Int32 sendVolume;           /**< 发送音量。最大为 100（默认）含义（0%-100%）*/
        bool playbackEnabled;           /**< 是否可回放，默认为 true */
        Int32 playbackVolume;       /**< 回放音量。最大为 100（默认）*/
    }

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct EngineAudioPositionInfo
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public float[] position;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public float[] quaternion;
	}
}