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

namespace NERTC {
    public enum RtcErrorCode : Int32 {
        NERTC_NO_ERROR = 0,                                 /**< 没有错误 */
        //资源分配错误 （正常请求时不会有此类错误返回）
        NERTC_ERR_CHANNEL_RESERVE_PERMISSION_DENIED = 403,  /**< 没有权限，包括没有开通音视频功能、没有开通非安全但是请求了非安全等 */
        NERTC_ERR_CHANNEL_RESERVE_TIME_OUT = 408,           /**< 请求超时 */
        NERTC_ERR_CHANNEL_RESERVE_ERROR_PARAM = 414,        /**< 服务器请求参数错误 */
        NERTC_ERR_CHANNEL_RESERVE_SERVER_FAIL = 500,        /**< 分配频道服务器未知错误 */
        NERTC_ERR_CHANNEL_RESERVE_MORE_THAN_TWO_USER = 600, /**< 只支持两个用户, 有第三个人试图使用相同的频道名分配频道 */

        //LIVESTREAM TASK
        NERTC_ERR_LS_TASK_REQUEST_INVALID = 1301,           /**< TASK请求无效，被后续操作覆盖 */
        NERTC_ERR_LS_TASK_IS_INVAILD = 1400,                /**< TASK参数格式错误 */
        NERTC_ERR_LS_TASK_ROOM_EXITED = 1401,               /**< 房间已经退出 */
        NERTC_ERR_LS_TASK_NUM_LIMIT = 1402,                 /**< 推流任务超出上限 */
        NERTC_ERR_LS_TASK_DUPLICATE_ID = 1403,              /**< 推流ID重复 */
        NERTC_ERR_LS_TASK_NOT_FOUND = 1404,                 /**< TASK_ID任务不存在，或频道不存在 */
        NERTC_ERR_LS_TASK_REQUEST_ERR = 1417,               /**< 请求失败 */
        NERTC_ERR_LS_TASK_INTERNAL_SERVER_ERR = 1500,       /**< 服务器内部错误 */
        NERTC_ERR_LS_TASK_INVALID_LAYOUT = 1501,            /**< 布局参数错误 */
        NERTC_ERR_LS_TASK_USER_PIC_ERR = 1512,              /**< 用户图片错误 */

        //其他错误
        NERTC_ERR_CHANNEL_START_FAIL = 11000,               /**< 通道发起失败 */
        NERTC_ERR_CHANNEL_DISCONNECTED = 11001,             /**< 断开连接 */
        NERTC_ERR_VERSION_SELF_LOW = 11002,                 /**< 本人SDK版本太低不兼容 */
        NERTC_ERR_VERSION_REMOTE_LOW = 11003,               /**< 对方SDK版本太低不兼容 */
        NERTC_ERR_CHANNEL_CLOSED = 11004,                   /**< 通道被关闭 */
        NERTC_ERR_CHANNEL_KICKED = 11005,                   /**< 账号被踢 */
        NERTC_ERR_DATA_ERROR = 11400,                       /**< 数据错误 */
        NERTC_ERR_INVALID = 11403,                          /**< 无效的操作 */

        //连接服务器错误
        NERTC_ERR_CHANNEL_JOIN_TIME_OUT = 20101,            /**< 请求超时 */
        NERTC_ERR_CHANNEL_JOIN_MEETING_MODE_ERROR = 20102,  /**< 会议模式错误 */
        NERTC_ERR_CHANNEL_JOIN_RTMP_MODE_ERROR = 20103,     /**< RTMP用户加入非RTMP频道 */
        NERTC_ERR_CHANNEL_JOIN_RTMP_NODES_ERROR = 20104,        /**< 超过频道最多RTMP人数限制 */
        NERTC_ERR_CHANNEL_JOIN_RTMP_HOST_ERROR = 20105,     /**< 已经存在一个主播 */
        NERTC_ERR_CHANNEL_JOIN_RTMP_CREATE_ERROR = 20106,   /**< 需要旁路直播, 但频道创建者非主播 */
        NERTC_ERR_CHANNEL_JOIN_LAYOUT_ERROR = 20208,        /**< 主播自定义布局错误 */
        NERTC_ERR_CHANNEL_JOIN_INVALID_PARAM = 20400,       /**< 错误参数 */
        NERTC_ERR_CHANNEL_JOIN_DES_KEY = 20401,             /**< 密码加密错误 */
        NERTC_ERR_CHANNEL_JOIN_INVALID_REQUST = 20417,      /**< 错误请求 */
        NERTC_ERR_CHANNEL_SERVER_UNKNOWN = 20500,           /**< 服务器内部错误 */

        //ENGINE ERROR CODE
        NERTC_ERR_FATAL = 30001,               /**< 通用错误 */
        NERTC_ERR_OUT_OF_MEMORY = 30002,         /**< 内存耗尽 */
        NERTC_ERR_INVALID_PARAM = 30003,        /**< 错误的参数 */
        NERTC_ERR_NOT_SUPPORTED = 30004,        /**< 不支持的操作 */
        NERTC_ERR_INVALID_STATE = 30005,        /**< 当前状态不支持的操作 */
        NERTC_ERR_LACK_OF_RESOURCE = 30006,      /**< 资源耗尽 */
        NERTC_ERR_INVALID_INDEX = 30007,        /**< 非法 INDEX */
        NERTC_ERR_DEVICE_NOT_FOUND = 30008,      /**< 设备未找到 */
        NERTC_ERR_INVALID_DEVICE_SOURCE_ID = 30009,/**< 非法设备 ID */
        NERTC_ERR_INVALID_VIDEO_PROFILE = 30010, /**< 非法的视频 PROFILE TYPE */
        NERTC_ERR_CREATE_DEVICE_SOURCE_FAIL = 30011,  /**< 设备创建错误 */
        NERTC_ERR_INVALID_RENDER = 30012,       /**< 非法的渲染容器 */
        NERTC_ERR_DEVICE_PREVIEW_ALREADY_STARTED = 30013,/**< 设备已经打开 */
        NERTC_ERR_TRANSMIT_PENDDING = 30014,    /**< 传输错误 */
        NERTC_ERR_CONNECT_FAIL = 30015,         /**< 连接服务器错误 */
        NERTC_ERR_CREATE_DUMP_FILE_FAIL = 30016,    /**< 创建AUDIO DUMP文件失败 */
        NERTC_ERR_START_DUMP_FAIL = 30017,         /**< 开启AUDIO DUMP失败 */
        NERTC_ERR_DESKTOP_CAPTURE_INVALID_STATE = 30020,    /**< 启动桌面录屏失败，不能与CAMERA同时启动 */
        NERTC_ERR_DESKTOP_CAPTURE_INVALID_PARAM = 30021,    /**< 桌面录屏传入参数无效 */
        NERTC_ERR_DESKTOP_CAPTURE_NOT_READY     = 30022,    /**< 桌面录屏未就绪 */

        NERTC_ERR_CHANNEL_ALREADY_JOINED = 30100,    /**< 重复加入频道 */
        NERTC_ERR_CHANNEL_NOT_JOINED = 30101,        /**< 尚未加入频道 */
        NERTC_ERR_CHANNEL_REPLEATEDLY_LEAVE = 30102, /**< 重复离开频道 */
        NERTC_ERR_REQUEST_JOIN_CHANNEL_FAIL = 30103,  /**< 加入频道操作失败 */
        NERTC_ERR_SESSION_NOT_FOUND = 30104,         /**< 会话未找到 */
        NERTC_ERR_USER_NOT_FOUND = 30105,            /**< 用户未找到 */
        NERTC_ERR_INVALID_USER_ID = 30106,           /**< 非法的用户 ID */
        NERTC_ERR_MEDIA_NOT_STARTED = 30107,         /**< 用户多媒体数据未连接 */
        NERTC_ERR_SOURCE_NOT_FOUND = 30108,          /**< SOURCE 未找到 */

        NERTC_ERR_CONNECTION_NOT_FOUND = 30200,      /**< 连接未找到 */
        NERTC_ERR_STREAM_NOT_FOUND = 30201,          /**< 媒体流未找到 */
        NERTC_ERR_ADD_TRACK_FAIL = 30202,            /**< 加入 TRACK 失败 */
        NERTC_ERR_TRACK_NOT_FOUND = 30203,           /**< TRACK 未找到 */
        NERTC_ERR_MEDIA_CONNECTION_DISCONNECTED = 30204, /**< 媒体连接断开 */
        NERTC_ERR_SIGNAL_DISCONNECTED = 30205,      /**< 信令连接断开 */
        NERTC_ERR_SERVER_KICKED = 30206,            /**< 被踢出频道 */
        NERTC_ERR_KICKED_FOR_ROOM_CLOSED = 30207,     /**< 因频道已关闭而被踢出 */
        
        NERTC_RUNTIME_ERR_ADM_NO_AUTHORIZE = 40000,    /**< 没有音频设备权限 */
        
        NERTC_RUNTIME_ERR_VDM_NO_AUTHORIZE = 50000,    /**< 没有视频设备权限 */
        
        NERTC_RUNTIME_ERR_SCREEN_CAPTURE_NO_AUTHORIZE = 60000,    /**< 没有录制视频权限 */
    }
}