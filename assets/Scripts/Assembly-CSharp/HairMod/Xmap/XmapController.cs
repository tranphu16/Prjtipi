using System;
using System.Collections.Generic;
using System.IO;

    // Token: 0x020000E9 RID: 233
    internal class XmapController : IActionListener
    {
        // Token: 0x06000B80 RID: 2944 RVA: 0x000BB444 File Offset: 0x000B9644
        public static void Update()
        {
            try
            {
                bool flag = XmapController.IsWaiting();
                if (!flag)
                {
                    bool isLoading = XmapData.Instance().IsLoading;
                    if (!isLoading)
                    {
                        bool isWaitNextMap = XmapController.IsWaitNextMap;
                        if (isWaitNextMap)
                        {
                            bool flag2 = TileMap.mapID == 21 + global::Char.myCharz().cgender;
                            if (flag2)
                            {
                                bool flag3 = GameScr.vItemMap.size() > 0;
                                if (flag3)
                                {
                                    Service.gI().pickItem(-1);
                                }
                            }
                            XmapController.Wait(XmapController.TIME_DELAY_NEXTMAP);
                            XmapController.IsWaitNextMap = false;
                        }
                        else
                        {
                            bool isNextMapFailed = XmapController.IsNextMapFailed;
                            if (isNextMapFailed)
                            {
                                XmapData.Instance().MyLinkMaps = null;
                                XmapController.WayXmap = null;
                                XmapController.IsNextMapFailed = false;
                            }
                            else
                            {
                                bool flag4 = XmapController.WayXmap == null;
                                if (flag4)
                                {
                                    bool flag5 = XmapData.Instance().MyLinkMaps == null;
                                    if (flag5)
                                    {
                                        XmapData.Instance().LoadLinkMaps();
                                        return;
                                    }
                                    XmapController.WayXmap = XmapAlgorithm.FindWay(TileMap.mapID, XmapController.IdMapEnd);
                                    XmapController.IndexWay = 0;
                                    bool flag6 = XmapController.WayXmap == null;
                                    if (flag6)
                                    {
                                        GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
                                        XmapController.FinishXmap();
                                        return;
                                    }
                                }
                                bool flag7 = (TileMap.mapID == XmapController.WayXmap[XmapController.WayXmap.Count - 1] || TileMap.mapID == XmapController.IdMapEnd) && !XmapData.IsMyCharDie();
                                if (flag7)
                                {
                                    bool flag8 = XmapController.ZoneID != -1 && TileMap.zoneID != XmapController.ZoneID;
                                    if (flag8)
                                    {
                                        XmapController.Wait(1000);
                                        
                                    }
                                    else
                                    {
                                        XmapController.ZoneID = ((TileMap.zoneID == XmapController.ZoneID && XmapController.ZoneID != -1) ? (-1) : XmapController.ZoneID);
                                        XmapController.FinishXmap();
                                    }
                                }
                                else
                                {
                                    bool flag10 = TileMap.mapID == XmapController.WayXmap[XmapController.IndexWay];
                                    if (flag10)
                                    {
                                        bool flag11 = XmapData.IsMyCharDie();
                                        if (flag11)
                                        {
                                            Service.gI().returnTownFromDead();
                                            XmapController.IsWaitNextMap = (XmapController.IsNextMapFailed = true);
                                            XmapController.WayXmap = null;
                                        }
                                        else
                                        {
                                            bool flag12 = XmapData.CanNextMap();
                                            if (flag12)
                                            {
                                                XmapController.NextMap(XmapController.WayXmap[XmapController.IndexWay + 1]);
                                                XmapController.IsWaitNextMap = true;
                                            }
                                        }
                                        XmapController.Wait(350);
                                    }
                                    else
                                    {
                                        bool flag13 = TileMap.mapID == XmapController.WayXmap[XmapController.IndexWay + 1];
                                        if (flag13)
                                        {
                                            XmapController.IndexWay++;
                                        }
                                        else
                                        {
                                            XmapController.IsNextMapFailed = true;
                                            XmapController.WayXmap = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("Data/Error/XmapErrorUpdate.txt", ex.Message);
            }
        }

        // Token: 0x06000B81 RID: 2945 RVA: 0x000BB754 File Offset: 0x000B9954
        public void perform(int idAction, object p)
        {
            if (idAction == 1)
            {
                List<int> list = (List<int>)p;
                XmapController.ShowPanelXmap(list);
            }
        }

        // Token: 0x06000B82 RID: 2946 RVA: 0x00006981 File Offset: 0x00004B81
        private static void Wait(int time)
        {
            XmapController.IsWait = true;
            XmapController.TimeStartWait = mSystem.currentTimeMillis();
            XmapController.TimeWait = (long)time;
        }

        // Token: 0x06000B83 RID: 2947 RVA: 0x000BB77C File Offset: 0x000B997C
        private static bool IsWaiting()
        {
            bool flag = XmapController.IsWait && mSystem.currentTimeMillis() - XmapController.TimeStartWait >= XmapController.TimeWait;
            if (flag)
            {
                XmapController.IsWait = false;
            }
            return XmapController.IsWait;
        }

        // Token: 0x06000B84 RID: 2948 RVA: 0x000BB7BC File Offset: 0x000B99BC
        public static void ShowXmapMenu()
        {
            XmapData.Instance().LoadGroupMapsFromFile("GroupMapsXmap");
            MyVector myVector = new MyVector();
            foreach (GroupMap groupMap in XmapData.Instance().GroupMaps)
            {
                myVector.addElement(new Command(groupMap.NameGroup, XmapController._Instance, 1, groupMap.IdMaps));
            }
            GameCanvas.menu.startAt(myVector, 3);
        }

        // Token: 0x06000B85 RID: 2949 RVA: 0x000BB828 File Offset: 0x000B9A28
        public static string get_map_names(int id)
        {
            string text;
            switch (id)
            {
                case 105:
                    text = "Cánh đồng tuyết";
                    break;
                case 106:
                    text = "Rừng tuyết";
                    break;
                case 107:
                    text = "Núi tuyết";
                    break;
                case 108:
                    text = "Dòng sông băng";
                    break;
                case 109:
                    text = "Rừng băng";
                    break;
                case 110:
                    text = "Hang băng";
                    break;
                default:
                    text = TileMap.mapNames[id];
                    break;
            }
            return text;
        }

        // Token: 0x06000B86 RID: 2950 RVA: 0x000BB8A4 File Offset: 0x000B9AA4
        public static void ShowPanelXmap(List<int> idMaps)
        {
            Pk9rXmap.IsMapTransAsXmap = true;
            int count = idMaps.Count;
            GameCanvas.panel.mapNames = new string[count];
            GameCanvas.panel.planetNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                string text = XmapController.get_map_names(idMaps[i]);
                GameCanvas.panel.mapNames[i] = idMaps[i].ToString() + ": " + text;
                GameCanvas.panel.planetNames[i] = "";
            }
            GameCanvas.panel.setTypeMapTrans();
            GameCanvas.panel.show();
        }

        // Token: 0x06000B87 RID: 2951 RVA: 0x0000699B File Offset: 0x00004B9B
        public static void StartRunToMapId(int idMap)
        {
            XmapController.IdMapEnd = idMap;
            Pk9rXmap.IsXmapRunning = true;
        }

        // Token: 0x06000B88 RID: 2952 RVA: 0x000069AA File Offset: 0x00004BAA
        public static void FinishXmap()
        {
            Pk9rXmap.IsXmapRunning = false;
            XmapController.IsNextMapFailed = false;
            XmapData.Instance().MyLinkMaps = null;
            XmapController.WayXmap = null;
        }

        // Token: 0x06000B89 RID: 2953 RVA: 0x000069CA File Offset: 0x00004BCA
        public static void SaveIdMapCapsuleReturn()
        {
            Pk9rXmap.IdMapCapsuleReturn = TileMap.mapID;
        }

        // Token: 0x06000B8A RID: 2954 RVA: 0x000BB950 File Offset: 0x000B9B50
        private static void NextMap(int idMapNext)
        {
            List<MapNext> mapNexts = XmapData.Instance().GetMapNexts(TileMap.mapID);
            bool flag = mapNexts != null;
            if (flag)
            {
                foreach (MapNext mapNext in mapNexts)
                {
                    bool flag2 = mapNext.MapID == idMapNext;
                    if (flag2)
                    {
                        XmapController.NextMap(mapNext);
                        return;
                    }
                }
            }
            GameScr.info1.addInfo("Lỗi tại dữ liệu", 0);
        }

        // Token: 0x06000B8B RID: 2955 RVA: 0x000BB9E0 File Offset: 0x000B9BE0
        private static void NextMap(MapNext mapNext)
        {
            switch (mapNext.Type)
            {
                case TypeMapNext.AutoWaypoint:
                    XmapController.NextMapAutoWaypoint(mapNext);
                    break;
                case TypeMapNext.NpcMenu:
                    XmapController.NextMapNpcMenu(mapNext);
                    break;
                case TypeMapNext.NpcPanel:
                    XmapController.NextMapNpcPanel(mapNext);
                    break;
                case TypeMapNext.Position:
                    XmapController.NextMapPosition(mapNext);
                    break;
                case TypeMapNext.Capsule:
                    XmapController.NextMapCapsule(mapNext);
                    break;
            }
        }

        // Token: 0x06000B8C RID: 2956 RVA: 0x000BBA40 File Offset: 0x000B9C40
        private static void NextMapAutoWaypoint(MapNext mapNext)
        {
            Waypoint waypoint = XmapData.FindWaypoint(mapNext.MapID);
            bool flag = waypoint != null;
            if (flag)
            {
                int posWaypointX = XmapData.GetPosWaypointX(waypoint);
                int posWaypointY = XmapData.GetPosWaypointY(waypoint);
                XmapController.MoveMyChar(posWaypointX, posWaypointY);
                XmapController.RequestChangeMap(waypoint);
            }
        }

        // Token: 0x06000B8D RID: 2957 RVA: 0x000BBA84 File Offset: 0x000B9C84
        public static void findNpc()
        {
            bool flag = TileMap.mapID == 27;
            if (flag)
            {
                XmapController.NextMap(28);
                XmapController.IsWaitNextMap = true;
                XmapController.step = 0;
            }
            else
            {
                bool flag2 = TileMap.mapID == 29;
                if (flag2)
                {
                    XmapController.NextMap(28);
                    XmapController.IsWaitNextMap = true;
                    XmapController.step = 1;
                }
                else
                {
                    bool flag3 = XmapController.step == 0;
                    if (flag3)
                    {
                        XmapController.NextMap(29);
                        XmapController.IsWaitNextMap = true;
                    }
                    else
                    {
                        bool flag4 = XmapController.step == 1;
                        if (flag4)
                        {
                            XmapController.NextMap(27);
                            XmapController.IsWaitNextMap = true;
                        }
                    }
                }
            }
        }

        // Token: 0x06000B8E RID: 2958 RVA: 0x000069D7 File Offset: 0x00004BD7
        public static void CloseMenu()
        {
            ChatPopup.currChatPopup = null;
            Effect2.vEffect2.removeAllElements();
            Effect2.vEffect2Outside.removeAllElements();
            InfoDlg.hide();
            GameCanvas.menu.doCloseMenu();
            GameCanvas.panel.cp = null;
        }

        // Token: 0x06000B8F RID: 2959 RVA: 0x000BBB14 File Offset: 0x000B9D14
        private static void NextMapNpcMenu(MapNext mapNext)
        {
            int num = mapNext.Info[0];
            bool flag = GameScr.findNPCInMap((short)num) == null;
            if (flag)
            {
                XmapController.findNpc();
            }
            else
            {
                bool flag2 = num == 13;
                if (flag2)
                {
                    bool flag3 = !GameCanvas.menu.showMenu;
                    if (flag3)
                    {
                        Service.gI().openMenu(num);
                        XmapController.CloseMenu();
                    }
                    else
                    {
                        for (int i = 0; i < GameCanvas.menu.menuItems.size(); i++)
                        {
                            Command command = (Command)GameCanvas.menu.menuItems.elementAt(i);
                            bool flag4 = command.caption.ToLower().StartsWith("nói") || command.caption.ToLower().StartsWith("talk");
                            if (flag4)
                            {
                                Service.gI().confirmMenu((short)num, (sbyte)i);
                                XmapController.CloseMenu();
                                break;
                            }
                            bool flag5 = mapNext.MapID == 135;
                            if (flag5)
                            {
                                bool flag6 = command.caption.ToLower().StartsWith("kho báu") || command.caption.ToLower().StartsWith("treasure");
                                if (flag6)
                                {
                                    Service.gI().confirmMenu((short)num, (sbyte)i);
                                    XmapController.CloseMenu();
                                    break;
                                }
                                bool flag7 = command.caption.ToLower().StartsWith("đồng ý") || command.caption.ToLower().StartsWith("Accept");
                                if (flag7)
                                {
                                    Service.gI().confirmMenu((short)num, (sbyte)i);
                                    XmapController.CloseMenu();
                                    break;
                                }
                            }
                            bool flag8 = mapNext.MapID == 153;
                            if (flag8)
                            {
                                bool flag9 = command.caption.ToLower().StartsWith("về khu") || command.caption.ToLower().StartsWith("return to");
                                if (flag9)
                                {
                                    Service.gI().confirmMenu((short)num, (sbyte)i);
                                    XmapController.CloseMenu();
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Service.gI().openMenu(num);
                    for (int j = 1; j < mapNext.Info.Length; j++)
                    {
                        int num2 = mapNext.Info[j];
                        Service.gI().confirmMenu((short)num, (sbyte)num2);
                    }
                }
            }
        }

        // Token: 0x06000B90 RID: 2960 RVA: 0x000BBD80 File Offset: 0x000B9F80
        private static void NextMapNpcPanel(MapNext mapNext)
        {
            int num = mapNext.Info[0];
            int num2 = mapNext.Info[1];
            int num3 = mapNext.Info[2];
            bool flag = num == 29;
            if (flag)
            {
                Service.gI().openMenu(num);
                for (int i = 0; i < GameCanvas.menu.menuItems.size(); i++)
                {
                    Command command = (Command)GameCanvas.menu.menuItems.elementAt(i);
                    bool flag2 = command.caption.ToLower().StartsWith("tham gia") || command.caption.ToLower().StartsWith("join");
                    if (flag2)
                    {
                        Service.gI().confirmMenu((short)num, (sbyte)i);
                        XmapController.CloseMenu();
                    }
                }
                bool isShow = GameCanvas.panel.isShow;
                if (isShow)
                {
                    XmapController.CloseMenu();
                    Service.gI().requestMapSelect(num3);
                }
                XmapController.CloseMenu();
            }
            else
            {
                Service.gI().openMenu(num);
                Service.gI().confirmMenu((short)num, (sbyte)num2);
                Service.gI().requestMapSelect(num3);
            }
        }

        // Token: 0x06000B91 RID: 2961 RVA: 0x000BBEA8 File Offset: 0x000BA0A8
        private static void NextMapPosition(MapNext mapNext)
        {
            int num = mapNext.Info[0];
            int num2 = mapNext.Info[1];
            XmapController.MoveMyChar(num, num2);
            Service.gI().requestChangeMap();
            Service.gI().getMapOffline();
        }

        // Token: 0x06000B92 RID: 2962 RVA: 0x000BBEE8 File Offset: 0x000BA0E8
        private static void NextMapCapsule(MapNext mapNext)
        {
            XmapController.SaveIdMapCapsuleReturn();
            int num = mapNext.Info[0];
            Service.gI().requestMapSelect(num);
        }

        // Token: 0x06000B93 RID: 2963 RVA: 0x00006A12 File Offset: 0x00004C12
        public static void UseCapsuleNormal()
        {
            Pk9rXmap.IsShowPanelMapTrans = false;
            Service.gI().useItem(0, 1, -1, 193);
        }

        // Token: 0x06000B94 RID: 2964 RVA: 0x00006A2E File Offset: 0x00004C2E
        public static void UseCapsuleVip()
        {
            Pk9rXmap.IsShowPanelMapTrans = false;
            Service.gI().useItem(0, 1, -1, 194);
        }

        // Token: 0x06000B95 RID: 2965 RVA: 0x00006A4A File Offset: 0x00004C4A
        public static void HideInfoDlg()
        {
            InfoDlg.hide();
        }

        // Token: 0x06000B96 RID: 2966 RVA: 0x000BBF14 File Offset: 0x000BA114
        public static void MoveMyChar(int x, int y)
        {
            global::Char.myCharz().cx = x;
            global::Char.myCharz().cy = y;
            Service.gI().charMove();
            bool flag = ItemTime.isExistItem(4387);
            if (!flag)
            {
                global::Char.myCharz().cx = x;
                global::Char.myCharz().cy = y + 1;
                Service.gI().charMove();
                global::Char.myCharz().cx = x;
                global::Char.myCharz().cy = y;
                Service.gI().charMove();
            }
        }

        // Token: 0x06000B97 RID: 2967 RVA: 0x000BBF98 File Offset: 0x000BA198
        private static void RequestChangeMap(Waypoint waypoint)
        {
            bool isOffline = waypoint.isOffline;
            if (isOffline)
            {
                Service.gI().getMapOffline();
            }
            else
            {
                Service.gI().requestChangeMap();
            }
        }

        // Token: 0x0400145C RID: 5212
        public static int TIME_DELAY_NEXTMAP = 350;

        // Token: 0x0400145D RID: 5213
        private const int TIME_DELAY_RENEXTMAP = 350;

        // Token: 0x0400145E RID: 5214
        private const int ID_ITEM_CAPSULE_VIP = 194;

        // Token: 0x0400145F RID: 5215
        private const int ID_ITEM_CAPSULE = 193;

        // Token: 0x04001460 RID: 5216
        private const int ID_ICON_ITEM_TDLT = 4387;

        // Token: 0x04001461 RID: 5217
        private static readonly XmapController _Instance = new XmapController();

        // Token: 0x04001462 RID: 5218
        public static int IdMapEnd;

        // Token: 0x04001463 RID: 5219
        private static List<int> WayXmap;

        // Token: 0x04001464 RID: 5220
        private static int IndexWay;

        // Token: 0x04001465 RID: 5221
        private static bool IsNextMapFailed;

        // Token: 0x04001466 RID: 5222
        private static bool IsWait;

        // Token: 0x04001467 RID: 5223
        private static long TimeStartWait;

        // Token: 0x04001468 RID: 5224
        private static long TimeWait;

        // Token: 0x04001469 RID: 5225
        private static bool IsWaitNextMap;

        // Token: 0x0400146A RID: 5226
        public static int ZoneID = -1;

        // Token: 0x0400146B RID: 5227
        public static int step;

        // Token: 0x020000EA RID: 234
        public struct MenuMap
        {
            // Token: 0x06000B9A RID: 2970 RVA: 0x00006A6F File Offset: 0x00004C6F
            public MenuMap(string groupName, List<int> idMap)
            {
                this.groupName = groupName;
                this.IDMaps = idMap;
            }

            // Token: 0x0400146C RID: 5228
            public string groupName;

            // Token: 0x0400146D RID: 5229
            public List<int> IDMaps;
        }
    }

