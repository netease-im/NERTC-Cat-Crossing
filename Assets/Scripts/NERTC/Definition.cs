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

namespace NERTC
{
    public class Parameters
    {
        /** 通过 JSON 配置 SDK 提供技术预览或特别定制功能。以标准化方式公开 JSON 选项。详见API set_parameters*/
        public const string NERTC_KEY_RECORD_HOST_ENABLED = "record_host_enabled";                        /**< bool value. true: 录制主讲人, false: 不是录制主讲人 */
        public const string NERTC_KEY_RECORD_AUDIO_ENABLED = "record_audio_enabled";                       /**< bool value，启用服务器音频录制。默认值 false */
        public const string NERTC_KEY_RECORD_VIDEO_ENABLED = "record_video_enabled";                       /**< bool value，启用服务器视频录制。默认值 false */
        public const string NERTC_KEY_RECORD_TYPE = "record_type";                                /**< int value, nertc_Record_type */
        public const string NERTC_KEY_AUTO_SUBSCRIBE_AUDIO = "auto_subscribe_audio";                       /**< bool value，其他用户打开音频时，自动订阅。 默认值 true */
        public const string NERTC_KEY_PUBLISH_SELF_STREAM_ENABLED = "publish_self_stream_enabled";                /**< bool value，开启旁路直播。默认值 false */
        public const string NERTC_KEY_LOG_LEVEL = "log_level";                                  /**< int value, nertc_Log_level，SDK 输出小于或等于该级别的log，默认为 nertc_Log_level_info */
        public const string NERTC_KEY_AUDIO_PROCESSING_AECENABLE = "audio_processing_aec_enable";                /**< bool value. AEC开关，默认值 true */
        public const string NERTC_KEY_AUDIO_PROCESSING_AGCENABLE = "audio_processing_agc_enable";                /**< bool value. AGC开关，默认值 true */
        public const string NERTC_KEY_AUDIO_PROCESSING_NSENABLE = "audio_processing_ns_enable";                 /**< bool value. NS开关，默认值 true */
        public const string NERTC_KEY_AUDIO_PROCESSING_AINSENABLE = "audio_processing_ai_ns_enable";              /**< bool value.AI  NS开关，建议通话前修改，默认值 false */
        public const string NERTC_KEY_AUDIO_PROCESSING_EXTERNAL_AUDIO_MIX_ENABLE = "audio_processing_external_audiomix_enable"; /**< bool value. 输入混音开关，默认值 false */
        public const string NERTC_KEY_AUDIO_PROCESSING_EARPHONE = "audio_processing_earphone";                  /**< bool value. 通知SDK是否使用耳机， true: 使用耳机, false: 不使用耳机，默认值 false */
        public const string NERTC_KEY_VIDEO_SEND_ON_PUB_TYPE = "video_sendonpub_type";                       /**< int value. nertc_Send_on_pub_type；设置视频发送策略，默认发送大流 nertc_Send_on_pub_high；通话前设置有效 */
    }
}