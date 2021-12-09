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
using NERTC;

public struct RtcPosition {
    public UInt64 userId;
    public float x;
    public float y;
    public float z;
    public float rx;
    public float ry;
    public float rz;
    public float rw;
}

public class RtcService {
    public class Status {
        public const int NONE = -1;
        public const int READY = 0;
        public const int START = 1;
        public const int JOINED = 2;
    }

    private IRtcEngine engine;
    private const string RTCKey = ""; // Please put the key here
    private const string RTCToken = ""; // Please put the token here
    private static RtcService instance;
    private NERtcEngineContext param;
    private RtcEngineEventHandler handler;

    public static string ChannelID { get; set; }
    public static string UserID { get; set; }
    public static string Token { get; set; }

    public static IRtcEngine Engine {
        get {
            return instance.engine;
        }
    }

    private Atomic<int> state;
    public static int State {
        get { return instance.state.Value; }
    }

    private AtomicQueue<UInt64> joinQueue;
    public static AtomicQueue<UInt64> JoinQueue {
        get { return instance.joinQueue; }
    }

    private AtomicQueue<UInt64> leaveQueue;
    public static AtomicQueue<UInt64> LeaveQueue {
        get { return instance.leaveQueue; }
    }

    private AtomicQueue<RtcPosition> posRefreshQueue;
    public static AtomicQueue<RtcPosition> PosRefreshQueue {
        get { return instance.posRefreshQueue; }
    }

    void OnConnectionStateChange(Int32 state, Int32 reason) {
        Cons.Log("OnConnectionStateChange state[{0}], reason[{1}]", state, reason);

        ConnectionStateType connectionState = (ConnectionStateType) state;

        // State edge switch change
        switch (connectionState) {
            case ConnectionStateType.NERTC_CONNECTION_STATE_CONNECTED:
                Cons.Log("NERtgEngine state change {0} -> {1}", this.state.Value, Status.JOINED);
                this.state.Value = Status.JOINED;
                break;

            case ConnectionStateType.NERTC_CONNECTION_STATE_CONNECTING:
                Cons.Log("NERtgEngine status change {0} -> {1}", this.state.Value, Status.JOINED);
                this.state.Value = Status.JOINED;
                break;

            case ConnectionStateType.NERTC_CONNECTION_STATE_RECONNECTING:
                Cons.Log("NERtgEngine status change {0} -> {1}", this.state.Value, Status.JOINED);
                this.state.Value = Status.JOINED;
                break;

            default:
                Cons.Log("NERtgEngine status change {0} -> {1}", this.state.Value, Status.START);
                this.state.Value = Status.START;
                break;
        }
    }

    void OnUserJoined(UInt64 uid, string userName) {
        Cons.Log("OnUserJoined uid[{0}]", uid);

        joinQueue.Enqueue(uid);

        Cons.Log("Someone comes");
    }

    void OnUserLeft(UInt64 uid, Int32 reason) {
        Cons.Log("OnUserLeft uid[{0}]", uid);

        leaveQueue.Enqueue(uid);

        Cons.Log("Someone left");
    }

    void OnUserPositionUpdated(UInt64 userId, float x, float y, float z, float rx, float ry, float rz, float rw) {
        Cons.Log("User {0} move to x[{1}], y[{2}], z[{3}]", userId, x, y, z);

        RtcPosition position = new RtcPosition();
        position.userId = userId;
        position.x = x;
        position.y = y;
        position.z = z;
        position.rx = rx;
        position.ry = ry;
        position.rz = rz;
        position.rw = rw;

        posRefreshQueue.Enqueue(position);
    }

    private RtcService() {
        state = new Atomic<int>(Status.NONE);
        engine = RtcEngine.Create();
        state.Value = Status.READY;

        ChannelID = UserID = "";
        Token = RTCToken;
        joinQueue = new AtomicQueue<ulong>();
        leaveQueue = new AtomicQueue<ulong>();
        posRefreshQueue = new AtomicQueue<RtcPosition>();

        handler = new RtcEngineEventHandler();
        handler.OnConnectionStateChange = OnConnectionStateChange;
        handler.OnUserJoined = OnUserJoined;
        handler.OnUserLeft = OnUserLeft;
        handler.OnUserPositionUpdated = OnUserPositionUpdated;

        param = new NERtcEngineContext();
        param.AppKey = RTCKey;
        param.LogDirPath = Helper.AppDataPath;
        param.LogFileMaxSizeKbytes = 2048;
        param.LogLevel = 5;
        param.EventHandler = handler;
        Cons.Log("Init with path {0}", param.LogDirPath);

        int ret = engine.Initialize(param);

        if (ret != 0) {
            Cons.Log("Rtc service is not ready, please check and debug");
            return;
        }

        state.Value = Status.START;

        engine.EnableSpatializer(true, false);
        engine.UpdateAudioRecvRange(50, 1);
        engine.SetRenderMode(3);
        engine.SetAudioProfile(6, 2);
    }

    static RtcService() {
        Cons.Log("Rtc service static begin...");

        if (instance == null) {
            Cons.Log("Rtc service static init...");
            instance = new RtcService();
        }
    }

    ~RtcService() {
        Release();
    }

    public static void Release() {
        Cons.Log("### Rtc service start release engine");

        if (instance == null) {
            Cons.Log("### Rtc service should not release null");
            return;
        }

        if (instance.state.Value == Status.JOINED) {
            Cons.Log("### Rtc service stop");
            Stop();
        }

        if (instance.state.Value >= Status.START) {
            Cons.Log("### Rtc service release engine");
            instance.engine.Release(true);
        }

        if (instance.state.Value >= Status.READY) {
            Cons.Log("### Rtc service destroy engine");
            RtcEngine.Destroy(instance.engine);
        }

        instance = null;
    }

    private void DoStart() {
        //engine.EnableSpatializer(true, false);
        //engine.UpdateAudioRecvRange(50, 1);
        //engine.SetRenderMode(3);
        //engine.EnableLocalAudio(false);

        System.Random r = new System.Random();

        //ret = engine.JoinChannel("", "123456", (ulong)r.Next(1000000, 3000000));

        int ret = engine.JoinChannel(Token, ChannelID, ulong.Parse(UserID));

        if (ret == 0) {
            state.Value = Status.JOINED;
        }
    }

    public static void Start() {
        instance.DoStart();
    }

    private void DoStop() {
        if (state.Value > Status.START) {
            engine.LeaveChannel();
        }
    }

    public static void Stop() {
        instance.DoStop();
    }

    private void DoReportPosition(RtcPosition position) {
        EngineAudioPositionInfo info = new EngineAudioPositionInfo();
        info.position = new float[3];
        info.quaternion = new float[4];

        info.position[0] = position.x;
        info.position[1] = position.y;
        info.position[2] = position.z;

        info.quaternion[0] = position.rx;
        info.quaternion[1] = position.ry;
        info.quaternion[2] = position.rz;
        info.quaternion[3] = position.rw;

        Cons.Log("Report audio position info "
            + "x(" + info.position[0].ToString() + "), "
            + "y(" + info.position[1].ToString() + "), "
            + "z(" + info.position[2].ToString() + "), "
            + "quaternion info x(" + info.quaternion[0].ToString() + "), "
            + "y(" + info.quaternion[1].ToString() + "), "
            + "z(" + info.quaternion[2].ToString() + "), "
            + "w(" + info.quaternion[3].ToString() + ")");
        engine.UpdateSelfPosition(info);
    }

    public static void ReportPosition(RtcPosition position) {
        instance.DoReportPosition(position);
    }
}
