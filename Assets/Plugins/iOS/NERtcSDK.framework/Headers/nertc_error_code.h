/** @file nertc_error_code.h
 * @brief NERtc SDK的错误码定义
 * @copyright (c) 2015-2019, NetEase Inc. All rights reserved
 * @author Harrison
 * @date 2018/11/23
 */

#ifndef NERTC_ERROR_CODE_H_
#define NERTC_ERROR_CODE_H_

/**
 * @namespace nertc
 * @brief namespace nertc
 */
namespace nertc {
/** 错误代码。

错误代码意味着 SDK 遇到不可恢复的错误，需要应用程序干预。
*/
enum NERtcErrorCode {
    kNERtcNoError = 0, /**< 没有错误 */
    //资源分配错误 （正常请求时不会有此类错误返回）
    kNERtcErrChannelReservePermissionDenied = 403, /**< 没有权限，包括没有开通音视频功能、没有开通非安全但是请求了非安全等 */
    kNERtcErrChannelReserveTimeOut = 408,    /**< 请求超时 */
    kNERtcErrChannelReserveErrorParam = 414, /**< 服务器请求参数错误 */
    kNERtcErrChannelReserveServerFail = 500, /**< 分配频道服务器未知错误 */
    kNERtcErrChannelReserveMoreThanTwoUser = 600, /**< 只支持两个用户, 有第三个人试图使用相同的频道名分配频道 */

    // livestream task
    kNERtcErrLsTaskRequestInvalid = 1301,    /**< task请求无效，被后续操作覆盖 */
    kNERtcErrLsTaskIsInvaild = 1400,         /**< task参数格式错误 */
    kNERtcErrLsTaskRoomExited = 1401,        /**< 房间已经退出 */
    kNERtcErrLsTaskNumLimit = 1402,          /**< 推流任务超出上限 */
    kNERtcErrLsTaskDuplicateId = 1403,       /**< 推流ID重复 */
    kNERtcErrLsTaskNotFound = 1404,          /**< taskId任务不存在，或频道不存在 */
    kNERtcErrLsTaskRequestErr = 1417,        /**< 请求失败 */
    kNERtcErrLsTaskInternalServerErr = 1500, /**< 服务器内部错误 */
    kNERtcErrLsTaskInvalidLayout = 1501,     /**< 布局参数错误 */
    kNERtcErrLsTaskUserPicErr = 1512,        /**< 用户图片错误 */

    //其他错误
    kNERtcErrChannelStartFail = 11000,    /**< 通道发起失败 */
    kNERtcErrChannelDisconnected = 11001, /**< 断开连接 */
    kNERtcErrVersionSelfLow = 11002,      /**< 本人SDK版本太低不兼容 */
    kNERtcErrVersionRemoteLow = 11003,    /**< 对方SDK版本太低不兼容 */
    kNERtcErrChannelClosed = 11004,       /**< 通道被关闭 */
    kNERtcErrChannelKicked = 11005,       /**< 账号被踢 */
    kNERtcErrDataError = 11400,           /**< 数据错误 */
    kNERtcErrInvalid = 11403,             /**< 无效的操作 */

    //连接服务器错误
    kNERtcErrChannelJoinTimeOut = 20101,          /**< 请求超时 */
    kNERtcErrChannelJoinMeetingModeError = 20102, /**< 会议模式错误 */
    kNERtcErrChannelJoinRtmpModeError = 20103,    /**< rtmp用户加入非rtmp频道 */
    kNERtcErrChannelJoinRtmpNodesError = 20104,   /**< 超过频道最多rtmp人数限制 */
    kNERtcErrChannelJoinRtmpHostError = 20105,    /**< 已经存在一个主播 */
    kNERtcErrChannelJoinRtmpCreateError = 20106,  /**< 需要旁路直播, 但频道创建者非主播 */
    kNERtcErrChannelJoinLayoutError = 20208,      /**< 主播自定义布局错误 */
    kNERtcErrChannelJoinInvalidParam = 20400,     /**< 错误参数 */
    kNERtcErrChannelJoinDesKey = 20401,           /**< 密码加密错误 */
    kNERtcErrChannelJoinInvalidRequst = 20417,    /**< 错误请求 */
    kNERtcErrChannelServerUnknown = 20500,        /**< 服务器内部错误 */
    // Engine error code
    kNERtcErrFatal = 30001,                       /**< 通用错误 */
    kNERtcErrOutOfMemory = 30002,                 /**< 内存耗尽 */
    kNERtcErrInvalidParam = 30003,                /**< 错误的参数 */
    kNERtcErrNotSupported = 30004,                /**< 不支持的操作 */
    kNERtcErrInvalidState = 30005,                /**< 当前状态不支持的操作 */
    kNERtcErrLackOfResource = 30006,              /**< 资源耗尽 */
    kNERtcErrInvalidIndex = 30007,                /**< 非法 index */
    kNERtcErrDeviceNotFound = 30008,              /**< 设备未找到 */
    kNERtcErrInvalidDeviceSourceID = 30009,       /**< 非法设备 ID */
    kNERtcErrInvalidVideoProfile = 30010,         /**< 非法的视频 profile type */
    kNERtcErrCreateDeviceSourceFail = 30011,      /**< 设备创建错误 */
    kNERtcErrInvalidRender = 30012,               /**< 非法的渲染容器 */
    kNERtcErrDevicePreviewAlreadyStarted = 30013, /**< 设备已经打开 */
    kNERtcErrTransmitPendding = 30014,            /**< 传输错误 */
    kNERtcErrConnectFail = 30015,                 /**< 连接服务器错误 */
    kNERtcErrCreateDumpFileFail = 30016,          /**< 创建Audio dump文件失败 */
    kNERtcErrStartDumpFail = 30017,               /**< 开启Audio dump失败 */
    kNERtcErrDesktopCaptureInvalidState = 30020,  /**< 启动桌面录屏失败，不能与camera同时启动 */
    kNERtcErrDesktopCaptureInvalidParam = 30021,  /**< 桌面录屏传入参数无效 */
    kNERtcErrDesktopCaptureNotReady = 30022,      /**< 桌面录屏未就绪 */
    kNERtcErrUninitialized = 30023,               /**< 引擎未初始化 */

    kNERtcErrChannelAlreadyJoined = 30100,    /**< 重复加入频道 */
    kNERtcErrChannelNotJoined = 30101,        /**< 尚未加入频道 */
    kNERtcErrChannelRepleatedlyLeave = 30102, /**< 重复离开频道 */
    kNERtcErrRequestJoinChannelFail = 30103,  /**< 加入频道操作失败 */
    kNERtcErrSessionNotFound = 30104,         /**< 会话未找到 */
    kNERtcErrUserNotFound = 30105,            /**< 用户未找到 */
    kNERtcErrInvalidUserID = 30106,           /**< 非法的用户 ID */
    kNERtcErrMediaNotStarted = 30107,         /**< 用户多媒体数据未连接 */
    kNERtcErrSourceNotFound = 30108,          /**< source 未找到 */

    kNERtcErrConnectionNotFound = 30200,          /**< 连接未找到 */
    kNERtcErrStreamNotFound = 30201,              /**< 媒体流未找到 */
    kNERtcErrAddTrackFail = 30202,                /**< 加入 track 失败 */
    kNERtcErrTrackNotFound = 30203,               /**< track 未找到 */
    kNERtcErrMediaConnectionDisconnected = 30204, /**< 媒体连接断开 */
    kNERtcErrSignalDisconnected = 30205,          /**< 信令连接断开 */
    kNERtcErrServerKicked = 30206,                /**< 被踢出频道 */
    kNERtcErrKickedForRoomClosed = 30207,         /**< 因频道已关闭而被踢出 */

    kNERtcErrOSAuthorize = 30300, /**< 操作系统权限问题 */

    kNERtcRuntimeErrADMNoAuthorize = 40000,    /**< 没有音频设备权限 */
    kNERtcRuntimeErrADMInitRecording = 40001,  /**< 音频采集设备初始化失败 */
    kNERtcRuntimeErrADMStartRecording = 40002, /**< 音频采集设备开始失败 */
    kNERtcRuntimeErrADMStopRecording = 40003,  /**< 音频采集设备运行时错误 */
    kNERtcRuntimeErrADMInitPlayout = 40004,    /**< 音频播放设备初始化失败 */
    kNERtcRuntimeErrADMStartPlayout = 40005,   /**< 音频播放设备开始失败 */
    kNERtcRuntimeErrADMStopPlayout = 40006,    /**< 音频播放设备运行时错误 */

    kNERtcRuntimeErrVDMCameraCreate = 40503,         /**< 摄像头启动失败 */
    kNERtcRuntimeErrVDMScreenCapturerCreate = 40504, /**< 取屏启动失败 */

    kNERtcErrVideoCaptureUnknownError = 41000,  // 相机发生未知错误
    kNERtcErrVideoCaptureDisconnected = 41001,  // 相机断开，可能是被其他相机抢占了
    kNERtcErrVideoCaptureFreezed = 41002,

    kNERtcRuntimeErrVDMNoAuthorize = 50000,           /**< 没有视频设备权限 */
    kNERtcRuntimeErrVDMNotScreenUseSubStream = 50001, /**< 非屏幕共享使用辅流 */

    kNERtcRuntimeErrScreenCaptureNoAuthorize = 60000, /**< 没有录制视频权限 */

};

/** @enum NERtcRoomServerErrorCode room server相关错误码。TODO：NERtcErrorCode */
enum NERtcRoomServerErrorCode {
    kNERtcRoomServerErrOK = 200,              /**< 操作成功 */
    kNERtcRoomServerErrAuthError = 401,       /**< 认证错误*/
    kNERtcRoomServerErrChannelNotExist = 404, /**< 房间不存在*/
    kNERtcRoomServerErrUidNotExist = 405,     /**< 房间下的uid不存在 */
    kNERtcRoomServerErrDataError = 417,       /**< 请求数据不对 */
    kNERtcRoomServerErrUnknown = 500,         /**< 内部错误（TurnServer请求异常）*/
    kNERtcRoomServerErrServerError = 600,     /**< 服务器内部错误 */
    kNERtcRoomServerErrInvilid = 11403,       /**< 无效的操作 */
};

/** @enum NERtcAudioMixingErrorCode 混音音乐文件错误码。
 */
enum NERtcAudioMixingErrorCode {
    kNERtcAudioMixingErrorOK = 0,           /**< 没有错误。*/
    kNERtcAudioMixingErrorFatal = 1,        /**< 通用错误。*/
    kNERtcAudioMixingErrorCanNotOpen = 100, /**< 音乐文件打开出错。*/
    // kNERtcAudioMixingErrorTooFrequentCall = 101,      	/**< 音乐文件打开太频繁。*/
    // kNERtcAudioMixingErrorInterruptedEOF= 102,      		/**< 音乐文件播放中断。*/
};

}  // namespace nertc

#endif
