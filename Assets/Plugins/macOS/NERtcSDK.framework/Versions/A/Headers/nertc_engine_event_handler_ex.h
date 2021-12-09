/** @file nertc_engine_event_handler_ex.h
 * @brief NERTC SDK回调扩展接口头文件。
 * NERTC SDK所有接口参数说明: 所有与字符串相关的参数(char *)全部为UTF-8编码。
 * @copyright (c) 2015-2019, NetEase Inc. All rights reserved
 * @author Harrison
 * @date 2018/11/20
 */

#ifndef NERTC_ENGINE_EVENT_HANDLER_EX_H_
#define NERTC_ENGINE_EVENT_HANDLER_EX_H_

#include "nertc_base_types.h"
#include "nertc_engine_defines.h"
#include "nertc_engine_event_handler.h"

/**
 * @namespace nertc
 * @brief namespace nertc
 */
namespace nertc {
/** IRtcEngineEventHandlerEx 回调扩展接口类用于 SDK 向 App 发送回调事件通知，App 通过继承该接口类的方法获取 SDK 的事件通知。

 接口类的所有方法都有缺省（空）实现，App 可以根据需要只继承关心的事件。在回调方法中，App 不应该做耗时或者调用可能会引起阻塞的
 API（如开启音频或视频等），否则可能影响 SDK 的运行。
 */
class IRtcEngineEventHandlerEx : public IRtcEngineEventHandler {
   public:
    virtual ~IRtcEngineEventHandlerEx() {}

    /** 远端用户开启屏幕共享辅流通道的回调。

     @param uid 远端用户ID。
     @param max_profile 最大分辨率。
     */
    virtual void onUserSubStreamVideoStart(uid_t uid, NERtcVideoProfileType max_profile) {
        (void)uid;
        (void)max_profile;
    }
    /** 远端用户停止屏幕共享辅流通道的回调。

     @param uid 远端用户ID。
     */
    virtual void onUserSubStreamVideoStop(uid_t uid) {
        (void)uid;
    }

    /** 远端用户视频配置更新的回调。

     @param uid 远端用户 ID。
     @param max_profile 视频编码的分辨率，用于衡量编码质量。
     */
    virtual void onUserVideoProfileUpdate(uid_t uid, NERtcVideoProfileType max_profile) {
        (void)uid;
        (void)max_profile;
    }
    /** 远端用户是否静音的回调。

     @param uid 远端用户ID。
     @param mute 是否静音。
     */
    virtual void onUserAudioMute(uid_t uid, bool mute) {
        (void)uid;
        (void)mute;
    }
    /** 远端用户暂停或恢复发送视频流的回调。

     @param uid 远端用户ID。
     @param mute 是否禁视频流。
     */
    virtual void onUserVideoMute(uid_t uid, bool mute) {
        (void)uid;
        (void)mute;
    }
    /** 音频设备状态更改的回调。

     @param device_id 设备ID。
     @param device_type 音频设备类型。详细信息请参考 NERtcAudioDeviceType。
     @param device_state 音频设备状态。
     */
    virtual void onAudioDeviceStateChanged(const char device_id[kNERtcMaxDeviceIDLength], NERtcAudioDeviceType device_type,
                                           NERtcAudioDeviceState device_state) {
        (void)device_id;
        (void)device_type;
        (void)device_state;
    }

    /** 音频默认设备更改的回调。

     @param device_id 设备ID。
     @param device_type 音频设备类型。
     */
    virtual void onAudioDefaultDeviceChanged(const char device_id[kNERtcMaxDeviceIDLength], NERtcAudioDeviceType device_type) {
        (void)device_id;
        (void)device_type;
    }

    /** 音频设备路由更改回调（仅iOS/Android有效）。

    @param routing 音频路类型。
    */
    virtual void onAudioDeviceRoutingDidChange(NERtcAudioOutputRouting routing) {
        (void)routing;
    }

    /** 视频设备状态已改变的回调。

     @param device_id 设备ID。
     @param device_type 视频设备类型。
     @param device_state 视频设备状态。
     */
    virtual void onVideoDeviceStateChanged(const char device_id[kNERtcMaxDeviceIDLength], NERtcVideoDeviceType device_type,
                                           NERtcVideoDeviceState device_state) {
        (void)device_id;
        (void)device_type;
        (void)device_state;
    }

    /**
     当前开启摄像头焦点位置变化回调 ( 仅iOS有效)

     @param focusPointX 新的焦点位置x坐标
     @param focusPointY 新的焦点位置y坐标
     @note 纯音频SDK禁用该接口，如需使用请前往云信官网下载并替换成视频SDK
    */
    virtual void onCameraFocusChanged(float focusPointX, float focusPointY) {
        (void)focusPointX;
        (void)focusPointY;
    }

    /**
     当前开启摄像头曝光位置变化回调 ( 仅iOS有效)

     @param exposurePointX 新的曝光位置x坐标
     @param exposurePointY 新的曝光位置y坐标
     @note 纯音频SDK禁用该接口，如需使用请前往云信官网下载并替换成视频SDK
    */
    virtual void onCameraExposureChanged(float exposurePointX, float exposurePointY) {
        (void)exposurePointX;
        (void)exposurePointY;
    }

    /**
    当前开启摄像头焦点矩形区域变化回调 ( 仅Android有效)

    @param left 焦点矩形区域左边的X坐标
    @param top 焦点矩形区域顶部的Y坐标
    @param right 焦点矩形区域右边的X坐标
    @param bottom 焦点矩形区域底部的Y坐标
    @note 纯音频SDK禁用该接口，如需使用请前往云信官网下载并替换成视频SDK
    */
    virtual void onCameraFocusChanged(int32_t left, int32_t top, int32_t right, int32_t bottom) {
        (void)left;
        (void)top;
        (void)right;
        (void)bottom;
    }

    /**
    当前开启摄像头曝光矩形区域变化回调 ( 仅Android有效)

    @param left 曝光矩形区域左边的X坐标
    @param top 曝光矩形区域顶部的Y坐标
    @param right 曝光矩形区域右边的X坐标
    @param bottom 曝光矩形区域底部的Y坐标
    @note 纯音频SDK禁用该接口，如需使用请前往云信官网下载并替换成视频SDK
    */
    virtual void onCameraExposureChanged(int32_t left, int32_t top, int32_t right, int32_t bottom) {
        (void)left;
        (void)top;
        (void)right;
        (void)bottom;
    }

    /** 已接收到远端音频首帧的回调。

     @param uid 远端用户 ID，指定是哪个用户的音频流。
     */
    virtual void onFirstAudioDataReceived(uid_t uid) {
        (void)uid;
    }

    /** 已显示首帧远端视频的回调。

    第一帧远端视频显示在视图上时，触发此调用。

     @param uid 用户 ID，指定是哪个用户的视频流。
     */
    virtual void onFirstVideoDataReceived(uid_t uid) {
        (void)uid;
    }

    /** 已解码远端音频首帧的回调。

     @param uid 远端用户 ID，指定是哪个用户的音频流。
     */
    virtual void onFirstAudioFrameDecoded(uid_t uid) {
        (void)uid;
    }

    /** 已接收到远端视频并完成解码的回调。

    引擎收到第一帧远端视频流并解码成功时，触发此调用。

     @param uid 用户 ID，指定是哪个用户的视频流。
     @param width 视频流宽（px）。
     @param height 视频流高（px）。

     */
    virtual void onFirstVideoFrameDecoded(uid_t uid, uint32_t width, uint32_t height) {
        (void)uid;
        (void)width;
        (void)height;
    }

    /** 采集视频数据回调。

     @param data 采集视频数据。
     @param type 视频类型。
     @param width 视频宽度。
     @param height 视频高度。
     @param count 视频Plane Count。
     @param offset 视频offset。
     @param stride 视频stride。
     @param rotation 视频旋转角度。
     */
    virtual void onCaptureVideoFrame(void* data, NERtcVideoType& type, uint32_t width, uint32_t height, uint32_t count,
                                     uint32_t offset[kNERtcMaxPlaneCount], uint32_t stride[kNERtcMaxPlaneCount],
                                     NERtcVideoRotation rotation, uint32_t& texture_id) {
        (void)data;
        (void)type;
        (void)width;
        (void)height;
        (void)count;
        (void)offset;
        (void)stride;
        (void)rotation;
        (void)texture_id;
    }

    /** 本地用户的音乐文件播放状态改变回调。

    调用 startAudioMixing 播放混音音乐文件后，当音乐文件的播放状态发生改变时，会触发该回调。

    - 如果播放音乐文件正常结束，state 会返回相应的状态码 kNERtcAudioMixingStateFinished，error_code 返回
    kNERtcAudioMixingErrorOK。
    - 如果播放出错，则返回状态码 kNERtcAudioMixingStateFailed，error_code 返回相应的出错原因。
    - 如果本地音乐文件不存在、文件格式不支持、无法访问在线音乐文件 URL，error_code都会返回 kNERtcAudioMixingErrorCanNotOpen。

    @param state 音乐文件播放状态，详见 #NERtcAudioMixingState.
    @param error_code 错误码，详见 #NERtcAudioMixingErrorCode.
    */
    virtual void onAudioMixingStateChanged(NERtcAudioMixingState state, NERtcAudioMixingErrorCode error_code) {
        (void)state;
        (void)error_code;
    }

    /** 本地用户的音乐文件播放进度回调。

    调用 startAudioMixing 播放混音音乐文件后，当音乐文件的播放进度改变时，会触发该回调。
    @param timestamp_ms 音乐文件播放进度，单位为毫秒
    */
    virtual void onAudioMixingTimestampUpdate(uint64_t timestamp_ms) {
        (void)timestamp_ms;
    }

    /** 本地音效文件播放已结束回调。

    当播放音效结束后，会触发该回调。
    @param effect_id 指定音效的 ID。每个音效均有唯一的 ID。
    */
    virtual void onAudioEffectFinished(uint32_t effect_id) {
        (void)effect_id;
    }

    /** 提示频道内本地用户瞬时音量的回调。

     该回调默认禁用。可以通过 \ref IRtcEngineEx::enableAudioVolumeIndication "enableAudioVolumeIndication" 方法开启。

     开启后，本地用户说话，SDK 会按  \ref IRtcEngineEx::enableAudioVolumeIndication "enableAudioVolumeIndication"
     方法中设置的时间间隔触发该回调。

     如果本地用户将自己静音（调用了 \ref IRtcEngineEx::muteLocalAudioStream "muteLocalAudioStream"），SDK 将音量设置为 0
     后回调给应用层。

     @param volume （混音后的）音量，取值范围为 [0,100]。
     */
    virtual void onLocalAudioVolumeIndication(int volume) {
        (void)volume;
    }

    /** 提示房间内谁正在说话及说话者瞬时音量的回调。

     该回调默认为关闭状态。可以通过 enableAudioVolumeIndication 方法开启。开启后，无论房间内是否有人说话，SDK 都会按
     enableAudioVolumeIndication 方法中设置的时间间隔触发该回调。

     在返回的 speakers 数组中:

     - 如果有 uid 出现在上次返回的数组中，但不在本次返回的数组中，则默认该 uid 对应的远端用户没有说话。
     - 如果volume 为 0，表示该用户没有说话。
     - 如果speakers 数组为空，则表示此时远端没有人说话。

     @param speakers 每个说话者的用户 ID 和音量信息的数组: NERtcAudioVolumeInfo
     @param speaker_number speakers 数组的大小，即说话者的人数。
     @param total_volume （混音后的）总音量，取值范围为 [0,100]。
     */
    virtual void onRemoteAudioVolumeIndication(const NERtcAudioVolumeInfo* speakers, unsigned int speaker_number,
                                               int total_volume) {
        (void)speakers;
        (void)speaker_number;
        (void)total_volume;
    }

    /** 通知添加直播任务结果。

     该回调异步返回 \ref IRtcEngineEx::addLiveStreamTask "addLiveStreamTask" 接口的调用结果；实际推流状态参考 \ref
     IRtcEngineEventHandlerEx::onLiveStreamState "onLiveStreamState"

     @param task_id 任务id
     @param url 推流地址
     @param error_code 结果
     - 0: 调用成功；
     - 其他: 调用失败。
     */
    virtual void onAddLiveStreamTask(const char* task_id, const char* url, int error_code) {
        (void)task_id;
        (void)url;
        (void)error_code;
    }

    /** 通知更新直播任务结果。

     该回调异步返回 \ref IRtcEngineEx::updateLiveStreamTask "updateLiveStreamTask" 接口的调用结果；实际推流状态参考 \ref
     IRtcEngineEventHandlerEx::onLiveStreamState "onLiveStreamState"

     @param task_id 任务id
     @param url 推流地址
     @param error_code 结果
     - 0: 调用成功；
     - 其他: 调用失败。
     */
    virtual void onUpdateLiveStreamTask(const char* task_id, const char* url, int error_code) {
        (void)task_id;
        (void)url;
        (void)error_code;
    }

    /** 通知删除直播任务结果。

     该回调异步返回 \ref IRtcEngineEx::removeLiveStreamTask "removeLiveStreamTask" 接口的调用结果；实际推流状态参考 \ref
     IRtcEngineEventHandlerEx::onLiveStreamState "onLiveStreamState"

     @param task_id 任务id
     @param error_code 结果
     - 0: 调用成功；
     - 其他: 调用失败。
     */
    virtual void onRemoveLiveStreamTask(const char* task_id, int error_code) {
        (void)task_id;
        (void)error_code;
    }

    /** 通知直播推流状态
     @note 该回调在通话中有效。
     @param task_id 任务id
     @param url 推流地址
     @param state #NERtcLiveStreamStateCode, 直播推流状态
     - 505: 推流中；
     - 506: 推流失败；
     - 511: 推流结束；
     */
    virtual void onLiveStreamState(const char* task_id, const char* url, NERtcLiveStreamStateCode state) {
        (void)task_id;
        (void)url;
        (void)state;
    }

    /** 检测到啸叫回调。
     当声源与扩音设备之间因距离过近时，可能会产生啸叫。NERTC SDK
     支持啸叫检测，当检测到有啸叫信号产生的时候，自动触发该回调直至啸叫停止。App
     应用层可以在收到啸叫回调时，提示用户静音麦克风，或直接静音麦克风。

     @note
     啸叫检测功能一般用于语音聊天室或在线会议等纯人声环境，不推荐在包含背景音乐的娱乐场景中使用。

     @param howling 是否出现啸叫
     - true: 啸叫；
     - false: 正常；。
     */
    virtual void onAudioHowling(bool howling) {
        (void)howling;
    }
    /** 通知更新3D音效中与会用户的位置信息
    当用户使用SDK提供的3D音效相关的api(调用了updateSelfPosition方法)，会将己方的位置信息设置给引擎，引擎会将
    此位置信息放在AUdioFrame中发送给对方，对方会收到onUserPositionChanged回调
     
     @note
     出于节省带宽的考虑，引擎不会在每一帧都加上位置信息，只会在用户更新位置后，
     连续发10帧左右的带位置音频帧，故上层在收到此回调时，有可能实际位置并未发生移动。

     @param userId 发生位置变更的远端用户ID
     @param x 位置坐标
     @param y 位置坐标
     @param z 位置坐标
     @param rx 旋转量四元组
     @param ry 旋转量四元组
     @param rz 旋转量四元组
     @param rw 旋转量四元组
     */
    virtual void onUserPositionChanged(uint64_t userId, const float position[3], const float queternion[4]) {
        (void)userId;
        (void)position;
        (void)queternion;
    }

    /** 为unity添加日志重定向
    @param msg 日志信息
    */
    virtual void onLogMessage(const char* msg) {
        (void)msg;
    }
};
}  // namespace nertc

#endif
