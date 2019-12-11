using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
//用于代替自带的HUD组件
namespace UnityEngine.Networking
{
    [EditorBrowsable(EditorBrowsableState.Never), AddComponentMenu("Network/NetworkManagerHUD"), RequireComponent(typeof(NetworkManager))]
    public class NetworkManagerHUD : MonoBehaviour
    {
        public NetworkManager manager;
        [SerializeField]//是否显示GUI界面
        public bool showGUI = true;
        [SerializeField]//设置按钮界面X轴
        public int offsetX;
        [SerializeField]//设置按钮界面Y轴
        public int offsetY;
        private int x= 0;//按钮判定
        public Text text;//存放IP
        private bool m_ShowServer;
        private void Awake()
        {
            this.manager = base.GetComponent<NetworkManager>();//获取NetworkManager组件
        }
        public void OnStart()//[修改]通过按钮进行操作以服务器客户端启动
        {
           x=1;
        }
        private void Update()
        {
        }
        //按钮方法
        private void OnGUI()
        {
            if (!this.showGUI)//UI是否关闭
            {
                return;
            }
            int num = 10 + this.offsetX;
            int num2 = 40 + this.offsetY;
            bool flag = this.manager.client == null || this.manager.client.connection == null || this.manager.client.connection.connectionId == -1;
            if (!this.manager.IsClientConnected() && !NetworkServer.active && this.manager.matchMaker == null)
            {
                if (flag)//检查NetworkManager组件内的必要属性
                {
                    if (Application.platform != RuntimePlatform.WebGLPlayer)//判断是否是网页平台
                    {
                        if (x==1)//以服务器客户端启动（即是服务器也是客户端）
                        {
                            this.manager.StartHost();
                            //x=0;
                        }
                    }
                    //this.manager.networkAddress=text.text;//获取输入框内的IP[后期开发使用]
                    if (Application.platform == RuntimePlatform.WebGLPlayer)
                    {
                        GUI.Box(new Rect((float)num, (float)num2, 200f, 25f), "(  WebGL cannot be server  )");//警告提示
                        num2 += 24;
                    }
                }
                else
                {
                    GUI.Label(new Rect((float)num, (float)num2, 200f, 20f), string.Concat(new object[]//联机提示
                    {
                        "Connecting to ",
                        this.manager.networkAddress,
                        ":",
                        this.manager.networkPort,
                        ".."
                    }));
                    num2 += 24;
                    if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Cancel Connection Attempt"))//取消连接
                    {
                        this.manager.StopClient();
                    }
                }
            }
            else
            {
                if (NetworkServer.active)
                {
                    string text = "Server: port=" + this.manager.networkPort;
                    if (this.manager.useWebSockets)
                    {
                        text += " (Using WebSockets)";
                    }
                    GUI.Label(new Rect((float)num, (float)num2, 300f, 20f), text);
                    num2 += 24;
                }
                if (this.manager.IsClientConnected())
                {
                    GUI.Label(new Rect((float)num, (float)num2, 300f, 20f), string.Concat(new object[]
                    {
                        "Client: address=",
                        this.manager.networkAddress,
                        " port=",
                        this.manager.networkPort
                    }));
                    num2 += 24;
                }
            }
            if (this.manager.IsClientConnected() && !ClientScene.ready)
            {
                if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Client Ready"))
                {
                    ClientScene.Ready(this.manager.client.connection);
                    if (ClientScene.localPlayers.Count == 0)
                    {
                        ClientScene.AddPlayer(0);
                    }
                }
                num2 += 24;
            }
            if (NetworkServer.active || this.manager.IsClientConnected())
            {
                if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "try again")||Input.GetKey(KeyCode.Escape))
                {
                    this.manager.StopHost();
                }
                num2 += 24;
            }
            if (!NetworkServer.active && !this.manager.IsClientConnected() && flag)
            {
                num2 += 10;
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    GUI.Box(new Rect((float)(num - 5), (float)num2, 220f, 25f), "(WebGL cannot use Match Maker)");
                    return;
                }
                if (this.manager.matchMaker == null)
                {
                    if (x==4)
                    {
                        this.manager.StartMatchMaker();
                    }
                    num2 += 24;
                }
                else
                {
                    if (this.manager.matchInfo == null)
                    {
                        if (this.manager.matches == null)
                        {
                            if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Create Internet Match"))
                            {
                                this.manager.matchMaker.CreateMatch(this.manager.matchName, this.manager.matchSize, true, string.Empty, string.Empty, string.Empty, 0, 0, new NetworkMatch.DataResponseDelegate<MatchInfo>(this.manager.OnMatchCreate));
                            }
                            num2 += 24;
                            GUI.Label(new Rect((float)num, (float)num2, 100f, 20f), "Room Name:");
                            this.manager.matchName = GUI.TextField(new Rect((float)(num + 100), (float)num2, 100f, 20f), this.manager.matchName);
                            num2 += 24;
                            num2 += 10;
                            if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Find Internet Match"))
                            {
                                this.manager.matchMaker.ListMatches(0, 20, string.Empty, false, 0, 0, new NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>>(this.manager.OnMatchList));
                            }
                            num2 += 24;
                        }
                        else
                        {
                            for (int i = 0; i < this.manager.matches.Count; i++)
                            {
                                MatchInfoSnapshot matchInfoSnapshot = this.manager.matches[i];
                                if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Join Match:" + matchInfoSnapshot.name))
                                {
                                    this.manager.matchName = matchInfoSnapshot.name;
                                    this.manager.matchMaker.JoinMatch(matchInfoSnapshot.networkId, string.Empty, string.Empty, string.Empty, 0, 0, new NetworkMatch.DataResponseDelegate<MatchInfo>(this.manager.OnMatchJoined));
                                }
                                num2 += 24;
                            }
                            if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Back to Match Menu"))
                            {
                                this.manager.matches = null;
                            }
                            num2 += 24;
                        }
                    }
                    if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Change MM server"))
                    {
                        this.m_ShowServer = !this.m_ShowServer;
                    }
                    if (this.m_ShowServer)
                    {
                        num2 += 24;
                        if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Local"))
                        {
                            this.manager.SetMatchHost("localhost", 1337, false);
                            this.m_ShowServer = false;
                        }
                        num2 += 24;
                        if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Internet"))
                        {
                            this.manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
                            this.m_ShowServer = false;
                        }
                        num2 += 24;
                        if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Staging"))
                        {
                            this.manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
                            this.m_ShowServer = false;
                        }
                    }
                    num2 += 24;
                    GUI.Label(new Rect((float)num, (float)num2, 300f, 20f), "MM Uri: " + this.manager.matchMaker.baseUri);
                    num2 += 24;
                    if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Disable Match Maker"))
                    {
                        this.manager.StopMatchMaker();
                    }
                    num2 += 24;
                }
            }
        }
    }
}
