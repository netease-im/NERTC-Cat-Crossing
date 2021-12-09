/** @file nertc_engine_ex.h
 * @brief NERTC SDK扩展接口头文件。
 * NERTC SDK所有接口参数说明: 所有与字符串相关的参数(char *)全部为UTF-8编码。
 * @copyright (c) 2015-2019, NetEase Inc. All rights reserved
 * @author Harrison
 * @date 2018/11/20
 */

#ifndef NERTC_ENGINE_EX_H_
#define NERTC_ENGINE_EX_H_

#include "nertc_base.h"
#include "nertc_base_types.h"
#include "nertc_engine.h"
#include "nertc_engine_defines.h"
#include "nertc_engine_event_handler.h"
#include "nertc_engine_media_stats_observer.h"

/**
 * @namespace nertc
 * @brief namespace nertc
 */
namespace nertc {
/** RtcEngine 类提供了供 App 调用的主要接口方法。

 IRtcEngineEx 是 NERTC SDK 的扩展接口类。创建一个 IRtcEngine 对象并调用这个对象的方法可以激活 NERTC SDK 的通信功能。
 */
class IRtcEngineEx : public IRtcEngine {
   public:
    virtual ~IRtcEngineEx() {}

    /** 获取当前网络状态。

     @return 当前网络状态。#NERtcConnectionStateType.
     */
    virtual NERtcConnectionStateType getConnectionState() = 0;

    /** 开关本地音频发送。该方法用于允许或禁止向网络发送本地音频流。

    @note
    - 该方法不影响录音状态，因为并没有禁用录音设备。
    - 静音状态会在通话结束后被重置为非静音

    @param[in] mute 静音/取消静音:
    - true: 静音本地音频
    - false: 取消静音本地音频（默认）

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int muteLocalAudioStream(bool mute) = 0;

    /** 设置音频编码属性。

    @note
    - 该方法需要在 \ref IRtcEngine::joinChannel "joinChannel" 之前设置好， \ref IRtcEngine::joinChannel "joinChannel"
    之后设置不生效。
    - 音乐场景下，建议将 profile 设置为 kNERtcAudioProfileHighQuality。

    @param[in]  profile 设置采样率，码率，编码模式和声道数: #NERtcAudioProfileType.
    @param[in]  scenario 设置音频应用场景: #NERtcAudioScenarioType.

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setAudioProfile(NERtcAudioProfileType profile, NERtcAudioScenarioType scenario) = 0;

    /** 取消或恢复订阅指定远端用户音频流。
     加入房间时，默认订阅所有远端用户的音频流，您可以通过此方法取消或恢复订阅指定远端用户的音频流。

    @param[in] uid 指定用户的 ID
    @param[in] subscribe
    - true: 订阅指定音频流（默认）；
    - false: 取消订阅指定音频流。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。

    */
    virtual int subscribeRemoteAudioStream(uid_t uid, bool subscribe) = 0;

    /** 设置 耳机补偿效果。

    调用该方法可以为本地发流用户设置 SDK 调教过的耳机补偿效果。

   @note 通话结束后重置为默认关闭

   @param[in] type 预设的耳机类型。默认关闭不选择任何耳机。详细信息请参考 NERtcHpComp。

   @return
   - 0: 方法调用成功；
   - 其他: 方法调用失败。
   */
    virtual int setHpCompType(NERtcHpCompType type) = 0;
    /**
     订阅或取消订阅所有的远端音频
     该操作属于全局操作

     @param subscribe 是否订阅
     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int subscribeAllRemoteAudioStream(bool subscribe) = 0;

    /** 设置主流的视频配置。

      可以在加入房间前或加入房间后调用。设置成功后，下一次开启本端视频时生效。每个属性对应一套视频参数，如、帧率、码率等。会在摄像头重启后生效。
     所有设置的参数均为理想情况下的最大值。当视频引擎因网络环境等原因无法达到设置的分辨率的最大值时，会取最接近最大值的那个值。

     @param[in] config 视频配置: NERtcVideoConfig
     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int setVideoConfig(const NERtcVideoConfig& config) = 0;

    /** 设置是否开启视频大小流模式。
     该方法设置单流或者双流模式。发送端开启双流模式后，接收端可以选择接收大流还是小流。其中，大流指高分辨率、高码率的视频流，小流指低分辨率、低码率的视频流。

     @note
     - 该方法只对摄像头数据生效，自定义输入、屏幕共享等视频流无效。
     - 该方法在加入房间前后都能调用。设置后，会在摄像头重启后生效。

    @param[in] enable
    - true: 双流发送（默认）
    - false: 只发送一个流

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int enableDualStreamMode(bool enable) = 0;

    /** 设置本地辅流视频画布。

     该方法设置本地辅流视频显示信息。App 通过调用此接口绑定本地辅流的显示视窗（view）。 在 App
     开发中，通常在初始化后调用该方法进行本地视频设置，然后再加入房间。

     @param[in] canvas 视频画布信息
     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int setupLocalSubStreamVideoCanvas(NERtcVideoCanvas* canvas) = 0;

    /** 设置本地视图显示模式。

    该方法设置本地视图显示模式。 App 可以多次调用此方法更改显示模式。

    @note 在打开屏幕共享前必须设置本地辅流画布。

    @param[in] scaling_mode  视频显示模式: #NERtcVideoScalingMode
    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setLocalRenderMode(NERtcVideoScalingMode scaling_mode) = 0;

    /** 设置本端的屏幕共享辅流视频显示模式

    该方法设置本地视图显示模式。 App 可以多次调用此方法更改显示模式。

    @note 必须先通过 setupLocalSubStreamVideoCanvas 设置本地辅流画布。

    @param[in] scaling_mode  视频显示模式: #NERtcVideoScalingMode
    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setLocalSubStreamRenderMode(NERtcVideoScalingMode scaling_mode) = 0;

    /** 设置本地视频镜像模式。

    该方法设置本地视频镜像模式。 App 可以多次调用此方法更改镜像模式。

    @param[in] mirror_mode  视频镜像模式: #NERtcVideoMirrorMode
    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setLocalVideoMirrorMode(NERtcVideoMirrorMode mirror_mode) = 0;

    /** 设置远端视图显示模式。

    该方法设置远端视图显示模式。App 可以多次调用此方法更改显示模式。

    @param[in] uid 远端用户 ID。
    @param[in] scaling_mode  视频显示模式: #NERtcVideoScalingMode
    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setRemoteRenderMode(uid_t uid, NERtcVideoScalingMode scaling_mode) = 0;

    /** 设置远端的辅流视频画布。

     该方法绑定远端用户和辅流显示视图，即指定某个 uid 使用对应的画布显示。

     @note
     - 如果 App 不能事先知道对方的 uid，可以在 APP 收到 \ref IRtcEngineEventHandler::onUserJoined "onUserJoined" 事件时设置。
     - 退出频道后，SDK 会把远端用户的绑定关系清除掉。
     - 退出房间后，SDK 清除远端用户和画布的的绑定关系，该设置自动失效。

     @param[in] uid 远端用户 ID。
     @param[in] canvas 视频画布设置
     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int setupRemoteSubStreamVideoCanvas(uid_t uid, NERtcVideoCanvas* canvas) = 0;

    /** 订阅或取消订阅远端的屏幕共享辅流视频，订阅之后才能接收远端的辅流视频数据。

    @note
    - 必须在远端加入房间后调用。
    - 必须先设置远端辅流画布。

    @param[in] uid 指定用户的用户 ID。
    @param[in] subscribe
    - true: 订阅指定远端用户的视频流；
    - false: 取消订阅指定远端用户的视频流。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int subscribeRemoteVideoSubStream(uid_t uid, bool subscribe) = 0;

    /** 设置远端的屏幕共享辅流视频显示模式。

     在远端开启辅流形式的屏幕共享时使用。App 可以多次调用此方法更改显示模式。

    @param[in] uid 远端用户 ID。
    @param[in] scaling_mode  视频显示模式: #NERtcVideoScalingMode
    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setRemoteSubSteamRenderMode(uid_t uid, NERtcVideoScalingMode scaling_mode) = 0;

    /** 开启视频预览。

     该方法用于在进入频道前启动本地视频预览。调用该 API 前，必须:

     - 调用 \ref IRtcEngine::setupLocalVideoCanvas "setupLocalVideoCanvas" 设置预览窗口；

     @note 启用了本地视频预览后，在进入频道前，本地预览必须先关闭，需要先调用 \ref IRtcEngineEx::stopVideoPreview
     "stopVideoPreview" 。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int startVideoPreview() = 0;

    /** 停止视频预览。

    @note 该方法需要在加入房间前调用。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int stopVideoPreview() = 0;

    /** 取消或恢复发布本地视频流。

     成功调用该方法后，远端会触发 onUserVideoMute 回调。

    @note
    - 调用该方法禁视频流时，SDK 不再发送本地视频流，但摄像头仍然处于工作状态。
    - 该方法在加入房间前后均可调用。
    - 若调用该方法取消发布本地视频流，通话结束后会被重置为默认状态，即默认发布本地视频流。
    - 该方法与 \ref nertc::IRtcEngine::enableLocalVideo "enableLocalVideo" (false) 的区别在于， \ref
    nertc::IRtcEngine::enableLocalVideo "enableLocalVideo" (false)
    会关闭本地摄像头设备，muteLocalVideoStream不影响本地视频流采集，不禁用摄像头，且响应速度更快。

    @param[in] mute
    - true: 不发送本地视频流
    - false: 发送本地视频流（默认）

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int muteLocalVideoStream(bool mute) = 0;

    /** 设置音视频通话的相关参数。通过 JSON 配置 SDK 提供技术预览或特别定制功能。以标准化方式公开 JSON 选项。

    @param[in] parameters 音视频通话的相关参数。  JSON 字符串形式。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setParameters(const char* parameters) = 0;

    /** 设置录制的声音格式。
     该方法设置 \ref nertc::INERtcAudioFrameObserver::onAudioFrameDidRecord "onAudioFrameDidRecord" 回调的录制声音格式。

     @note
     - 该方法在加入房间前后均可设置或修改。
     - 取消监听，重置为空。

     @param format 指定 *onAudioFrameDidRecord* 中返回数据的采样率和数据的通道数。允许传入 NULL，默认为 NULL。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int setRecordingAudioFrameParameters(NERtcAudioFrameRequestFormat* format) = 0;

    /** 设置播放的声音格式。
     该方法设置 \ref nertc::INERtcAudioFrameObserver::onAudioFrameWillPlayback "onAudioFrameWillPlayback" 回调的播放声音格式。

     @note
     - 该方法在加入房间前后均可设置或修改。
     - 取消监听，重置为空。

     @param format 指定 *onAudioFrameWillPlayback* 中返回数据的采样率和数据的通道数。允许传入 NULL，默认为 NULL。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int setPlaybackAudioFrameParameters(NERtcAudioFrameRequestFormat* format) = 0;

    /** 设置录制和播放声音混音后的采样率。
     该方法设置 \ref nertc::INERtcAudioFrameObserver::onMixedAudioFrame "onMixedAudioFrame" 回调的声音格式。

     @note
     - 该方法在加入房间前后均可设置或修改。
     - 目前只支持设置采样率。
     - 未调用该接口设置数据格式时，回调中的采样率返回 SDK 默认值。

     @param format 目前只支持设置采样率，其他设置无效。允许传入 null，默认为 null（代表使用音频的原始格式）仅支持 8000， 16000，
     32000， 44100或48000。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int setMixedAudioFrameParameters(NERtcAudioFrameRequestFormat* format) = 0;

    /** 注册语音观测器对象。
     该方法用于设置音频采集和播放PCM回调，可用于声音处理等操作。当需要引擎给出 \ref
     nertc::INERtcAudioFrameObserver::onAudioFrameDidRecord "onAudioFrameDidRecord" 或 \ref
     nertc::INERtcAudioFrameObserver::onAudioFrameWillPlayback "onAudioFrameWillPlayback" 回调时，需要使用该方法注册回调。

     @param observer 接口对象实例。如果传入 NULL，则取消注册，同时会清理NERtcAudioFrameRequestFormat相关设置。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int setAudioFrameObserver(INERtcAudioFrameObserver* observer) = 0;

    /** 开始记录音频 dump。 音频 dump 可用于分析音频问题。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int startAudioDump() = 0;
    /** 结束音频dump。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int stopAudioDump() = 0;

    /**
     设置音频播放扬声器模式（仅iOS/Android有效）

     @param enable 是否使用扬声器模式
     @return 操作返回值，成功则返回 0
     */
    virtual int setLoudspeakerMode(bool enable) = 0;

    /**
     获取目前是否使用扬声器模式（仅iOS/Android有效）

     @param enabled 是否正在使用扬声器模式
     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int getLoudspeakerMode(bool& enabled) = 0;

    /**
    设置 SDK 对 Audio Session 的控制权限

    该方法仅适用于 iOS 平台。
    该方法限制  SDK 对 Audio Session 的操作权限。在默认情况下，SDK 和 App 对 Audio Session 都有控制权，但某些场景下，App
    会希望限制  SDK 对 Audio Session 的控制权限，而使用其他应用或第三方组件对 Audio Session 进行操控。调用该方法可以实现该功能。
    该接口只能在入会之前调用。

    Note:一旦调用该方法限制了 SDK 对 Audio Session 的控制权限， SDK 将无法对 Audio Session
    进行相关设置，而需要用户自己或第三方组件进行维护。

    @return 操作返回值，成功则返回 0
    */
    virtual int setAudioSessionOperationRestriction(NERtcAudioSessionOperationRestriction restriction) = 0;

    /**
     设置音频播放设备的状态(仅iOS/Android有效)
     默认状态为unmute

     @param muted 是否静音
     @return 操作返回值，成功则返回 0
     */
    virtual int setPlayoutDeviceMute(bool mute) = 0;

    /**
     获取音频播放设备的状态(仅iOS/Android有效)

     @param muted 是否静音
     @return 操作返回值，成功则返回 0
     */
    virtual int getPlayoutDeviceMute(bool* muted) = 0;

    /**
     设置录音设备的状态(仅iOS/Android有效)
     默认状态为unmute

     @param mute 是否静音
     @return 操作返回值，成功则返回 0
     */
    virtual int setRecordDeviceMute(bool mute) = 0;

    /**
     获取录音设备的状态(仅iOS/Android有效)

     @param muted 是否静音
     @return 操作返回值，成功则返回 0
     */
    virtual int getRecordDeviceMute(bool* muted) = 0;

    /**
     当前开启的摄像头是否支持手动曝光(仅iOS/Android有效)

     @return YES/NO，支持/不支持
     @note 纯音频SDK禁用该接口，如需使用请前往云信官网下载并替换成视频SDK
     */
    virtual bool isCameraExposurePositionSupported() = 0;

    /**
     设置当前摄像头手动曝光位置(仅iOS/Android有效)

     @param positionX 曝光位置点x坐标
     @param positionY 曝光位置点y坐标
     @return 操作返回值，成功则返回 0
     @note 纯音频SDK禁用该接口，如需使用请前往云信官网下载并替换成视频SDK
     */
    virtual int setCameraExposurePosition(float positionX, float positionY) = 0;

    /**
     当前开启的摄像头是否支持打开手电筒
     @note 仅iOS/Android可用

     @return YES/NO，支持/不支持
     */
    virtual bool isCameraTorchSupported() = 0;

    /**
     设置当前摄像头打开/关闭手电筒

     @note 仅iOS/Android可用

     @param on 开启/关闭
     @return 操作返回值，成功则返回 0
     */
    virtual int setCameraTorchOn(bool on) = 0;

    /**
     查询当前摄像头是否打开手电筒

     @note 仅iOS可用
     @return YES/NO,打开/关闭
     */
    virtual bool isCameraTorchOn() = 0;

    /**
     当前开启的摄像头是否支持缩放

     @return YES/NO，支持/不支持 ( 仅iOS/Android可用)
     @note 纯音频SDK禁用该接口，如需使用请前往云信官网下载并替换成视频SDK
     */
    virtual bool isCameraZoomSupported() = 0;

    /**
     * 获取当前缩放比例
     *
     * @return 获取当前缩放比例
     */
    virtual float getCameraCurrentZoom() = 0;

    /**
     设置当前摄像头缩放比例

     @param factor 比例
     @return 操作返回值，成功则返回 0
     */
    virtual int setCameraZoomFactor(float factor) = 0;

    /**
     当前摄像头支持的最大缩放比例

     @note 仅iOS可用

     @return 最大缩放比例
     */
    virtual float maxCameraZoomScale() = 0;

    /**
     当前开启的摄像头是否支持手动对焦仅iOS/Android可用

     @return true/false，支持/不支持
     @note 纯音频SDK禁用该接口，如需使用请前往云信官网下载并替换成视频SDK
     */
    virtual bool isCameraFocusSupported() = 0;

    /**
     设置当前摄像头聚焦点位置

     @note 仅iOS/Android可用
     @param focusX (0-1) 之间
     @param focusY (0-1) 之间
     @return 操作返回值，成功则返回 0
     */
    virtual int setCameraFocusPosition(float focusX, float focusY) = 0;

    /** 开始播放音乐文件及混音。

    指定本地或在线音频文件来和录音设备采集的音频流进行混音。成功调用该方法后，如果播放状态改变，本地会触发 \ref
    nertc::IRtcEngineEventHandlerEx::onAudioMixingStateChanged "onAudioMixingStateChanged" 回调。

    @note 请在频道内调用该方法，如果在频道外调用该方法可能会出现问题。
     - 支持的音频格式包括：AAC、MP3 和 WAVE。
     - 成功调用该方法后，如果播放状态改变，本地会触发onAudioMixingStateChanged 回调。

    @param[in] option 创建混音任务配置的选项，包括混音任务类型、混音文件全路径或URL等。支持的音频格式包括: AAC、MP3 和 WAVE。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int startAudioMixing(NERtcCreateAudioMixingOption* option) = 0;

    /** 停止播放音乐文件及混音。

    该方法停止播放伴奏。请在房间内调用该方法。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int stopAudioMixing() = 0;

    /** 暂停播放音乐文件及混音。

    该方法暂停播放伴奏。请在房间内调用该方法。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int pauseAudioMixing() = 0;

    /** 恢复播放伴奏。

    该方法恢复混音，继续播放伴奏。请在房间内调用该方法。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int resumeAudioMixing() = 0;

    /** 调节伴奏发送音量。

    该方法调节混音里伴奏的发送音量大小。请在房间内调用该方法。

    @param[in] volume 伴奏发送音量。取值范围为 0~100。默认 100 为原始文件音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setAudioMixingSendVolume(uint32_t volume) = 0;

    /** 获取伴奏发送音量。

    该方法获取混音里伴奏的发送音量大小。请在房间内调用该方法。

    @param[out] volume 伴奏发送音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int getAudioMixingSendVolume(uint32_t* volume) = 0;

    /** 调节伴奏播放音量。

    该方法调节混音里伴奏的播放音量大小。请在房间内调用该方法。

    @param[in] volume 伴奏音量范围为 0~100。默认 100 为原始文件音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setAudioMixingPlaybackVolume(uint32_t volume) = 0;

    /** 获取伴奏播放音量。

     该方法获取混音里伴奏的播放音量大小。请在房间内调用该方法。

    @param[out] volume 伴奏播放音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int getAudioMixingPlaybackVolume(uint32_t* volume) = 0;

    /** 获取伴奏时长。

     该方法获取伴奏时长，单位为毫秒。请在房间内调用该方法。

    @param[out] duration 伴奏时长，单位为毫秒。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int getAudioMixingDuration(uint64_t* duration) = 0;

    /** 获取音乐文件的播放进度。

    该方法获取当前伴奏播放进度，单位为毫秒。请在房间内调用该方法。

    @param[out] position 伴奏播放进度，单位为毫秒。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int getAudioMixingCurrentPosition(uint64_t* position) = 0;

    /** 设置音乐文件的播放位置。

    该方法可以设置音频文件的播放位置，这样你可以根据实际情况播放文件，而非从头到尾播放整个文件。

    @param[in] seek_position 进度条位置，单位为毫秒。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setAudioMixingPosition(uint64_t seek_position) = 0;

    /** 播放指定音效文件。

    @note
     - 该方法播放指定的本地或在线音效文件。请在加入房间后调用该方法。
     - 您可以多次调用该方法，通过传入不同的音效文件的effect_id 和option
    ，同时播放多个音效文件，实现音效叠加。为获得最佳用户体验，建议同时播放的音效文件不超过 3 个。
     - 成功调用该方法后，如果播放结束，本地会触发onAudioEffectFinished回调。
     - 支持的音频格式包括: AAC、MP3 和 WAVE。
     - 若通过本接口成功播放音效文件后，反复停止或重新播放该 effectId 对应的音效文件，以最近一次的 option 设置为准。

    @param[in] effect_id 指定音效的 ID。每个音效均有唯一的 ID。
    @param[in] option 创建音效任务配置的选项，包括混音任务类型、混音文件全路径或URL等。支持的音频格式包括: AAC、MP3 和 WAVE。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int playEffect(uint32_t effect_id, NERtcCreateAudioEffectOption* option) = 0;

    /** 停止播放指定音效文件。

    请在频道内调用该方法。

    @param[in] effect_id 指定音效的 ID。每个音效均有唯一的 ID。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int stopEffect(uint32_t effect_id) = 0;

    /** 停止播放所有音效文件。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int stopAllEffects() = 0;

    /** 暂停音效文件播放。

    请在频道内调用该方法。

    @param[in] effect_id 指定音效的 ID。每个音效均有唯一的 ID。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int pauseEffect(uint32_t effect_id) = 0;

    /** 恢复播放指定音效文件。

    请在频道内调用该方法。

    @param[in] effect_id 指定音效的 ID。每个音效均有唯一的 ID。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int resumeEffect(uint32_t effect_id) = 0;

    /** 暂停所有音效文件播放。

    请在频道内调用该方法。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int pauseAllEffects() = 0;

    /** 恢复播放所有音效文件。

    请在频道内调用该方法。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int resumeAllEffects() = 0;

    /** 调节音效发送音量。

    该方法调节音效的发送音量大小。请在频道内调用该方法。

    @param[in] effect_id 指定音效的 ID。每个音效均有唯一的 ID。
    @param[in] volume 音效音量范围为 0~100。默认 100 为原始文件音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setEffectSendVolume(uint32_t effect_id, uint32_t volume) = 0;
    /** 获取音效发送音量。

    该方法获取音效的发送音量大小。请在频道内调用该方法。

    @param[in] effect_id 指定音效的 ID。每个音效均有唯一的 ID。
    @param[out] volume 音效发送音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int getEffectSendVolume(uint32_t effect_id, uint32_t* volume) = 0;

    /** 设置音效文件播放音量。

    请在加入房间后调用该方法。

    @param[in] effect_id 指定音效的 ID。每个音效均有唯一的 ID。
    @param[in] volume 音效播放音量。范围为0~100，默认为100。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setEffectPlaybackVolume(uint32_t effect_id, uint32_t volume) = 0;

    /** 获取音效文件播放音量。

     请在加入房间后调用该方法。

    @param[in] effect_id 指定音效的 ID。每个音效均有唯一的 ID。
    @param[out] volume 音效播放音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int getEffectPlaybackVolume(uint32_t effect_id, uint32_t* volume) = 0;

    /** 开启或关闭耳返。

    @note
     - 请在房间内调用该方法。
     - 开启耳返功能后，必须连接上耳机或耳麦，才能正常使用耳返功能。建议通过 \ref
    IRtcEngineEventHandlerEx::onAudioDeviceStateChanged  "onAudioDeviceStateChanged" 及 \ref
    IRtcEngineEventHandlerEx::onAudioDefaultDeviceChanged  "onAudioDefaultDeviceChanged"
    监听播放设备的变化，当监听到播放设备切换为耳机时才开启耳返。

    @param[in] enabled 开启或关闭。
    @param[in] volume 耳返音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int enableEarback(bool enabled, uint32_t volume) = 0;

    /** 设置耳返音量。

    请在频道内调用该方法。

    @param[in] volume 耳返音量。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setEarbackVolume(uint32_t volume) = 0;

    /** 注册统计信息观测器。

    @param[in] observer 统计信息观测器

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setStatsObserver(IRtcMediaStatsObserver* observer) = 0;

    /** 启用说话者音量提示。
     该方法允许 SDK 定期向 App 反馈本地发流用户和瞬时音量最高的远端用户（最多 3
     位）的音量相关信息，即当前谁在说话以及说话者的音量。 启用该方法后，只要房间内有发流用户，无论是否有人说话，SDK
     都会在加入房间后根据预设的时间间隔触发\ref IRtcEngineEventHandlerEx::onRemoteAudioVolumeIndication
     "onRemoteAudioVolumeIndication"回调。

     @param enable 是否启用说话者音量提示。
     @param interval 指定音量提示的时间间隔，单位为毫秒。必须设置为 100 毫秒的整数倍值。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int enableAudioVolumeIndication(bool enable, uint64_t interval) = 0;

    /** 通过指定区域共享屏幕。共享一个屏幕或该屏幕的部分区域。用户需要在该方法中指定想要共享的屏幕区域。
     调用该方法时需要指定待共享的屏幕区域，共享该屏幕的整体画面或指定区域。
     如果您在加入房间后调用该方法开启辅流，调用成功后，远端触发 onUserSubStreamVideoStart 回调

     @note
     - 该方法仅适用于 Windows。
     - 该方法打开视频辅流。

     @param  screen_rect 指定待共享的屏幕相对于虚拟屏的位置。
     @param  region_rect (可选)
     指定待共享区域相对于整个屏幕屏幕的位置。如果设置的共享区域超出了屏幕的边界，则只共享屏幕内的内容；如果将 width 或 height
     设为 0, 则共享整个屏幕。
     @param  capture_params 屏幕共享的编码参数配置。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int startScreenCaptureByScreenRect(const NERtcRectangle& screen_rect, const NERtcRectangle& region_rect,
                                               const NERtcScreenCaptureParameters& capture_params) = 0;

    /**通过指定屏幕 ID 开启屏幕共享，屏幕共享内容以辅流形式发送。
     屏幕共享内容以辅流形式发送。如果您在加入房间后调用该方法开启辅流，调用成功后，远端触发 onUserSubStreamVideoStart 回调。

     @note
     - 该方法仅适用于 macOS。
     - 该方法打开视频辅流。

     @param  display_id 指定待共享的屏幕 ID。开发者需要通过该参数指定你要共享的那个屏幕。
     @param  region_rect
     （可选）指定待共享的区域相对于整个窗口的位置。如果设置的共享区域超出了窗口的边界，则只共享窗口内的内容；如果宽或高为
     0，则共享整个窗口。
     @param  capture_params 屏幕共享的编码参数配置。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int startScreenCaptureByDisplayId(unsigned int display_id, const NERtcRectangle& region_rect,
                                              const NERtcScreenCaptureParameters& capture_params) = 0;

    /** 通过指定屏幕 ID 开启屏幕共享，屏幕共享内容以辅流形式发送。

     调用该方法时需要指定待共享的屏幕 ID ，共享该屏幕的整体画面或指定区域。
     如果您在加入房间后调用该方法开启辅流，调用成功后，远端触发 onUserSubStreamVideoStart 回调。

     @note
     - 该方法仅适用于 Windows 和 macOS。
     - 该方法打开视频辅流。

     @param  window_id 指定待共享的窗口 ID。
     @param  region_rect
     （可选）指定待共享的区域相对于整个窗口的位置。如果设置的共享区域超出了窗口的边界，则只共享窗口内的内容；如果宽或高为
     0，则共享整个窗口。
     @param  capture_params 屏幕共享的编码参数配置。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int startScreenCaptureByWindowId(void* window_id, const NERtcRectangle& region_rect,
                                             const NERtcScreenCaptureParameters& capture_params) = 0;

    /** 通过外部输入视频源的方式开启辅流功能。


     @note
     - 该方法仅适用于 iOS。
     - 该方法打开视频辅流。

     @param  capture_params 屏幕共享的编码参数配置。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int startScreenCapture(const NERtcScreenCaptureParameters& capture_params) = 0;
    /** 通过外部输入视频源的方式开启辅流功能。


     @note
     - 该方法仅适用于 Android。
     - 该方法打开视频辅流。

     @param  capture_params 屏幕共享的编码参数配置。
     @param  intent 录屏权限请求返回的intent, 类型为android.content.Intent
     @param  callback 录屏停止回调监听，类型为android.media.projection.MediaProjection&Callback

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int startScreenCapture(const NERtcScreenCaptureParameters& capture_params, void* intent, void* callback) = 0;

    /** 在共享屏幕或窗口时，更新共享的区域。
    @param  region_rect
    （可选）指定待共享的区域相对于整个窗口的位置。如果设置的共享区域超出了窗口的边界，则只共享窗口内的内容；如果宽或高为
    0，则共享整个窗口。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int updateScreenCaptureRegion(const NERtcRectangle& region_rect) = 0;

    /** 停止屏幕共享。

     如果您在加入房间后调用该方法关闭辅流，调用成功后，远端触发 onUserSubStreamVideoStop 回调。

     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
    */
    virtual int stopScreenCapture() = 0;

    /** 暂停屏幕共享。

     @return
     - 0: 方法调用成功
     - 其他: 方法调用失败
    */
    virtual int pauseScreenCapture() = 0;

    /** 恢复屏幕共享。

     @return
     - 0: 方法调用成功
     - 其他: 方法调用失败
    */
    virtual int resumeScreenCapture() = 0;

    /** 开启或关闭外部视频源数据输入

     通过该方法启用外部视频数据输入功能时，需要通过 IVideoDeviceManager::setDevice 设置 kNERtcExternalVideoDeviceID
    为外部视频输入源 ID。

    @note 该方法设置内部引擎为启用状态，在 \ref IRtcEngine::leaveChannel "leaveChannel" 后仍然有效。

    @param[in] enabled 是否外部视频源数据输入:
    - true: 开启外部视频源数据输入；
    - false: 关闭外部视频源数据输入 (默认)。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setExternalVideoSource(bool enabled) = 0;

    /** 开启或关闭辅流外部视频源数据输入

    @note
    - 仅iOS有效
    - 该方法设置内部引擎为启用状态，在 \ref IRtcEngine::leaveChannel "leaveChannel" 后仍然有效。

    @param[in] enabled 是否外部视频源数据输入:
    - true: 开启外部视频源数据输入；
    - false: 关闭外部视频源数据输入 (默认)。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setSubStreamExternalVideoSource(bool enabled) = 0;

    /** 推送外部视频帧。
     该方法主动将视频帧数据用 NERtcVideoFrame 类封装后传递给 SDK。 请确保在你调用本方法前已调用
    setExternalVideoSource，并将参数设为 true，否则调用本方法后会一直报错。

    @note 该方法设置内部引擎为启用状态，在 \ref IRtcEngine::leaveChannel "leaveChannel" 后不再有效。

    @param[in] frame 视频桢数据:

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int pushExternalVideoFrame(NERtcVideoFrame* frame) = 0;

    /** 推送外部视频帧。
     该方法主动将视频帧数据用 NERtcVideoFrame 类封装后传递给 SDK。 请确保在你调用本方法前已调用
    setSubStreamExternalVideoSource，并将参数设为 true，否则调用本方法后会一直报错。

    @note
    - 仅iOS有效
    - 该方法设置内部引擎为启用状态，在 \ref IRtcEngine::leaveChannel "leaveChannel" 后不再有效。

    @param[in] frame 视频桢数据:

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int pushSubStreamExternalVideoFrame(NERtcVideoFrame* frame) = 0;

    /** 开启或关闭外部音频源数据输入。
     当该方法调用成功后，音频输入设备选择和异常重启会失效。调用成功后可以使用 pushExternalAudioFrame 接口发送音频 PCM 数据。

    @note
     - 请在加入房间前调用该方法。
     -
    该方法设置内部引擎为启用状态，启动时将用虚拟设备代替麦克风工作，在leaveChannel后仍然有效。如果需要关闭该功能，需要在下次通话前调用接口关闭外部音频数据输入功能。
     - 启用外部音频数据输入功能后，SDK 内部实现部分麦克风由外部输入数据代替，麦克风相关的设置会失败或不在通话中生效。例如进行
    loopback 检测时，会听到输入的外部数据。

    @param[in] enabled 是否外部数据输入:
    - true: 开启外部数据输入；
    - false: 关闭外部数据输入 (默认)。
    @param[in] sample_rate 数据采样率，后续数据传入需要按该格式传入。
    注意：调用接口关闭功能时可传入任意合法值，此时设置不会生效。
    @param[in] channels 数据声道数，后续数据传入需要按该格式传入。
    注意：调用接口关闭功能时可传入任意合法值，此时设置不会生效。
    可设置为：
    - 1：单声道。
    - 2：双声道。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int setExternalAudioSource(bool enabled, int sample_rate, int channels) = 0;

    /** 推送外部音频数据输入

    将外部音频数据帧推送给内部引擎。
    通过 setExternalAudioSource 启用外部音频数据输入功能成功后，可以使用 pushExternalAudioFrame 接口发送音频 PCM 数据。

    @note
     - 该方法需要在加入房间后调用。
     - 数据帧时长建议匹配 10ms 周期。
     - 外部输入数据帧，数据时长和调用周期时长一致。
    - 该方法在音频输入设备关闭后不再生效。例如关闭本地音频、通话结束、通话前麦克风设备测试关闭等情况下，该设置不再生效。

    @param[in] frame 桢数据，数据长度不能超过7680:
    - 外部输入数据帧，数据时长和调用周期时长一致。

    @return
    - 0: 方法调用成功；
    - 其他: 方法调用失败。
    */
    virtual int pushExternalAudioFrame(NERtcAudioFrame* frame) = 0;

    /** 查询 SDK 版本号。

     该方法在加入房间前后都能调用。

     @param[out] build 编译号。
     @return 当前的 SDK 版本号，格式为字符串，如1.0.0.
     */
    virtual const char* getVersion(int* build) = 0;

    /** 查看指定错误码的错误描述。

     @note
     目前该方法无效，只返回空值。请在 \ref IRtcEngineEventHandler::onError "onError" 中查看返回的错误码及具体的错误描述。

     @param[in] error_code #NERtcErrorCode 。
     @return 详细错误码描述
     */
    virtual const char* getErrorDescription(int error_code) = 0;

    /** 上传 SDK 信息。
     只能在加入频道后调用。
     上传的信息包括 log 和 Audio dump 等文件。

     @return void
     */
    virtual void uploadSdkInfo() = 0;

    /** 添加房间推流任务，成功添加后当前用户可以收到该直播流的状态通知。
     @note
     - 该方法仅适用直播场景。
     - 请在房间内调用该方法，该方法在通话中有效。
     - 该方法每次只能增加一路旁路推流地址。如需推送多路流，则需多次调用该方法。同一个音视频房间（即同一个 channelid）可以创建 3
     个不同的推流任务。
     - 成功添加推流任务后，当前用户会收到该直播流的相关状态通知。
     @param[in] info 直播任务信息。详细信息请参考 \ref NERtcLiveStreamTaskInfo "NERtcLiveStreamTaskInfo"
     @param[in] callback 当前任务的回调函数.如果为null,则由\ref nertc::IRtcEngineEventHandlerEx::onAddLiveStreamTask
     "onAddLiveStreamTask" 回调
     @param[in] opaque 户透传数据，在callback中返回。
     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int addLiveStreamTask(const NERtcLiveStreamTaskInfo& info, onNERtcLiveStreamTaskCallback callback,
                                  void* opaque) = 0;

    /** 更新修改房间推流任务。
     @note
     - 该方法仅适用直播场景。
     - 请在房间内调用该方法，该方法在通话中有效。
     @param[in] info 直播任务信息。详细信息请参考 \ref NERtcLiveStreamTaskInfo "NERtcLiveStreamTaskInfo"
     @param[in] callback 当前任务的回调函数.如果为null,则由\ref nertc::IRtcEngineEventHandlerEx::onUpdateLiveStreamTask
     "onUpdateLiveStreamTask" 回调
     @param[in] opaque 户透传数据，在callback中返回。
     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int updateLiveStreamTask(const NERtcLiveStreamTaskInfo& info, onNERtcLiveStreamTaskCallback callback,
                                     void* opaque) = 0;

    /** 删除房间推流任务。
     @note
     - 该方法仅适用直播场景。
     - 通话结束，房间成员全部离开房间后，推流任务会自动删除。如果房间内还有用户存在，则需要创建推流任务的用户删除推流任务。
     @param[in] task_id  直播任务 ID。
     @param[in] callback 当前任务的回调函数.如果为null,则由\ref nertc::IRtcEngineEventHandlerEx::onRemoveLiveStreamTask
     "onRemoveLiveStreamTask" 回调
     @param[in] opaque 户透传数据，在callback中返回。
     @return
     - 0: 方法调用成功；
     - 其他: 方法调用失败。
     */
    virtual int removeLiveStreamTask(const char* task_id, onNERtcLiveStreamTaskCallback callback, void* opaque) = 0;

    // range && 3d audio test api
    virtual int updateAudioRecvRange(int range, int roll_off) = 0;
    virtual int updateSelfPosition(float position[3], float queternion[4]) = 0;
    virtual int enableSpatializer(bool enable, bool apply_to_team) = 0;
    virtual int enableRoomEffects(bool enable) = 0;
    virtual int SetRoomProperty(int size, int material, float reflection_scalar, float reverb_gain, float reverb_time,
        float reverb_brightness) = 0;
    virtual int SetRenderMode(int mode) = 0;


};

////////////////////////////////////////////////////////
/** \addtogroup createNERtcEngine
 @{
 */
////////////////////////////////////////////////////////

/** 创建 RTC 引擎对象并返回指针。

 @note 同时只支持一个IRtcEngine对象，新创建前必须先释放前一个IRtcEngine。

 @return RTC 引擎对象的指针。
 */
NERTC_API nertc::IRtcEngineEx* NERTC_CALL createNERtcEngine();

/** 销毁 RTC 引擎对象。
 @note 释放前需要先调用\ref nertc::IRtcEngine::release "release"
 */
NERTC_API void NERTC_CALL destroyNERtcEngine(void*& nertc_engine_inst);

////////////////////////////////////////////////////////
/** @} */
////////////////////////////////////////////////////////
}  // namespace nertc

#endif
