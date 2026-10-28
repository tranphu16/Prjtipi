using System;

    // Token: 0x020000E7 RID: 231
    public class Pk9rXmap
    {
    // Token: 0x06000B6D RID: 2925 RVA: 0x000BACEC File Offset: 0x000B8EEC

    // Token: 0x06000B6E RID: 2926 RVA: 0x000BAE94 File Offset: 0x000B9094


    // Token: 0x06000B6F RID: 2927 RVA: 0x000BB038 File Offset: 0x000B9238
    private static int[] wayPointMapLeft;

    // Token: 0x0400144B RID: 5195
    private static int[] wayPointMapCenter;

    // Token: 0x0400144C RID: 5196
    private static int[] wayPointMapRight;
    public static void Update()
        {
            bool isLoading = XmapData.Instance().IsLoading;
            if (isLoading)
            {
                XmapData.Instance().Update();
            }
            bool isXmapRunning = Pk9rXmap.IsXmapRunning;
            if (isXmapRunning)
            {
                XmapController.Update();
            }
        }

        // Token: 0x06000B70 RID: 2928 RVA: 0x000BB070 File Offset: 0x000B9270
        public static void Info(string text)
        {
            bool flag = text.Equals("Bạn chưa thể đến khu vực này") || text.Equals("You can not travel to this area");
            if (flag)
            {
                XmapController.FinishXmap();
            }
        }

        // Token: 0x06000B71 RID: 2929 RVA: 0x000BB0A4 File Offset: 0x000B92A4
        public static bool XoaTauBay(object obj)
        {
            Teleport teleport = (Teleport)obj;
            bool isMe = teleport.isMe;
            bool flag2;
            if (isMe)
            {
                global::Char.myCharz().isTeleport = false;
                bool flag = teleport.type == 0;
                if (flag)
                {
                    Controller.isStopReadMessage = false;
                    global::Char.ischangingMap = true;
                }
                Teleport.vTeleport.removeElement(teleport);
                flag2 = true;
            }
            else
            {
                flag2 = false;
            }
            return flag2;
        }

        // Token: 0x06000B72 RID: 2930 RVA: 0x000BB100 File Offset: 0x000B9300
        public static void SelectMapTrans(int selected)
        {
            bool isMapTransAsXmap = Pk9rXmap.IsMapTransAsXmap;
            if (isMapTransAsXmap)
            {
                XmapController.HideInfoDlg();
                string text = GameCanvas.panel.mapNames[selected];
                int idMapFromPanelXmap = XmapData.GetIdMapFromPanelXmap(text);
                XmapController.StartRunToMapId(idMapFromPanelXmap);
            }
            else
            {
                XmapController.SaveIdMapCapsuleReturn();
                Service.gI().requestMapSelect(selected);
            }
        }

        // Token: 0x06000B73 RID: 2931 RVA: 0x000BB150 File Offset: 0x000B9350
        public static void ShowPanelMapTrans()
        {
            Pk9rXmap.IsMapTransAsXmap = false;
            bool isShowPanelMapTrans = Pk9rXmap.IsShowPanelMapTrans;
            if (isShowPanelMapTrans)
            {
                GameCanvas.panel.setTypeMapTrans();
                GameCanvas.panel.show();
            }
            else
            {
                Pk9rXmap.IsShowPanelMapTrans = true;
            }
        }
    private static void LoadWaypointsInMap()
    {
       ResetSavedWaypoints();
        int num = TileMap.vGo.size();
        bool flag = num != 2;
        if (flag)
        {
            for (int i = 0; i < num; i++)
            {
                Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
                bool flag2 = waypoint.maxX < 60;
                if (flag2)
                {
                   wayPointMapLeft[0] = (int)(waypoint.minX + 15);
                   wayPointMapLeft[1] = (int)waypoint.maxY;
                }
                else
                {
                    bool flag3 = (int)waypoint.maxX > TileMap.pxw - 60;
                    if (flag3)
                    {
                       wayPointMapRight[0] = (int)(waypoint.maxX - 15);
                       wayPointMapRight[1] = (int)waypoint.maxY;
                    }
                    else
                    {
                       wayPointMapCenter[0] = (int)(waypoint.minX + 15);
                       wayPointMapCenter[1] = (int)waypoint.maxY;
                    }
                }
            }
        }
        else
        {
            Waypoint waypoint2 = (Waypoint)TileMap.vGo.elementAt(0);
            Waypoint waypoint3 = (Waypoint)TileMap.vGo.elementAt(1);
            bool flag4 = (waypoint2.maxX < 60 && waypoint3.maxX < 60) || ((int)waypoint2.minX > TileMap.pxw - 60 && (int)waypoint3.minX > TileMap.pxw - 60);
            if (flag4)
            {
               wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
               wayPointMapLeft[1] = (int)waypoint2.maxY;
               wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
               wayPointMapRight[1] = (int)waypoint3.maxY;
            }
            else
            {
                bool flag5 = waypoint2.maxX < waypoint3.maxX;
                if (flag5)
                {
                   wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
                   wayPointMapLeft[1] = (int)waypoint2.maxY;
                   wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
                   wayPointMapRight[1] = (int)waypoint3.maxY;
                }
                else
                {
                   wayPointMapLeft[0] = (int)(waypoint3.minX + 15);
                   wayPointMapLeft[1] = (int)waypoint3.maxY;
                   wayPointMapRight[0] = (int)(waypoint2.maxX - 15);
                   wayPointMapRight[1] = (int)waypoint2.maxY;
                }
            }
        }
    }
    private static void ResetSavedWaypoints()
    {
        wayPointMapLeft = new int[2];
        wayPointMapCenter = new int[2];
        wayPointMapRight = new int[2];
    }
    public static void TeleportTo(int x, int y)
    {
        bool canAutoPlay = GameScr.canAutoPlay;
        if (canAutoPlay)
        {
            global::Char.myCharz().cx = x;
            global::Char.myCharz().cy = y;
            Service.gI().charMove();
        }
        else
        {
            global::Char.myCharz().cx = x;
            global::Char.myCharz().cy = y;
            Service.gI().charMove();
            global::Char.myCharz().cx = x;
            global::Char.myCharz().cy = y + 1;
            Service.gI().charMove();
            global::Char.myCharz().cx = x;
            global::Char.myCharz().cy = y;
            Service.gI().charMove();
        }
    }
    public static int GetYGround(int x)
    {
        int num = 50;
        int i = 0;
        while (i < 30)
        {
            i++;
            num += 24;
            bool flag = TileMap.tileTypeAt(x, num, 2);
            if (flag)
            {
                bool flag2 = num % 24 != 0;
                if (flag2)
                {
                    num -= num % 24;
                }
                break;
            }
        }
        return num;
    }
    public static void LoadMap(int position)
    {
           LoadWaypointsInMap();
            switch (position)
            {
                case 0:
                    {
                        bool flag2 = wayPointMapLeft[0] != 0 &&wayPointMapLeft[1] != 0;
                        if (flag2)
                        {
                           TeleportTo(wayPointMapLeft[0],wayPointMapLeft[1]);
                        }
                        else
                        {
                           TeleportTo(60,GetYGround(60));
                        }
                        break;
                    }
                case 1:
                    {
                        bool flag3 =wayPointMapRight[0] != 0 &&wayPointMapRight[1] != 0;
                        if (flag3)
                        {
                           TeleportTo(wayPointMapRight[0],wayPointMapRight[1]);
                        }
                        else
                        {
                           TeleportTo(TileMap.pxw - 60,GetYGround(TileMap.pxw - 60));
                        }
                        break;
                    }
                case 2:
                    {
                        bool flag4 =wayPointMapCenter[0] != 0 &&wayPointMapCenter[1] != 0;
                        if (flag4)
                        {
                           TeleportTo(wayPointMapCenter[0],wayPointMapCenter[1]);
                        }
                        else
                        {
                           TeleportTo(TileMap.pxw / 2,GetYGround(TileMap.pxw / 2));
                        }
                        break;
                    }
            }
            bool flag5 = TileMap.mapID != 7 && TileMap.mapID != 14 && TileMap.mapID != 0;
            if (flag5)
            {
                Service.gI().requestChangeMap();
            }
            else
            {
                Service.gI().getMapOffline();
            }
        
    }
    // Token: 0x06000B74 RID: 2932 RVA: 0x0000693B File Offset: 0x00004B3B
    public static void FixBlackScreen()
        {
            Controller.gI().loadCurrMap(0);
            Service.gI().finishLoadMap();
            global::Char.isLoadingMap = false;
        }

        // Token: 0x04001456 RID: 5206
        public static bool IsXmapRunning = false;

        // Token: 0x04001457 RID: 5207
        public static bool IsMapTransAsXmap = false;

        // Token: 0x04001458 RID: 5208
        public static bool IsShowPanelMapTrans = true;

        // Token: 0x04001459 RID: 5209
        public static bool IsUseCapsuleNormal = true;

        // Token: 0x0400145A RID: 5210
        public static bool IsUseCapsuleVip = true;

        // Token: 0x0400145B RID: 5211
        public static int IdMapCapsuleReturn = -1;
    }

