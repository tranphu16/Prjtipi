using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Assembly_CSharp.DiemDanh
{
    public class DiemDanhTanThu : mScreen
    {
        private static DiemDanhTanThu instance;
        public static List<ItemDiemDanh> listItem = new List<ItemDiemDanh>();

        // UI Elements
        public static Image background, btnExit, btnNhan;
        public static Image bg;
        public static Image quay;
        public static Image[] effr = new Image[4];

        // Animation counters
        public static long count = 0;
        public static long last = mSystem.currentTimeMillis();

        public static DiemDanhTanThu gI()
        {
            return instance ??= new DiemDanhTanThu();
        }

        public static void loadImage()
        {
            try
            {
                quay = Image.createImage("res/x2/vongQuayMayMan/quay");
                background = Image.createImage("res/x2/diemDanh/background");
                btnExit = Image.createImage("res/x2/diemDanh/btnExit");
                btnNhan = Image.createImage("res/x2/diemDanh/btnNhan");
                bg = Image.createImage("res/x2/diemDanh/bg0");

                for (int i = 0; i < 4; i++)
                {
                    effr[i] = Image.createImage($"res/x2/diemDanh/effr{i}");
                }

                // Sau khi load xong resources, gửi request lấy data từ server
                sendCheckDiemDanh();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in loadImage: {e.Message}");
            }
        }
        public override void update()
        {
            GameScr.gI().update();
        }

        private static void ProcessInitialData(Message msg)
        {
            try
            {
                listItem.Clear();
                int size = msg.reader().readInt();
                Debug.Log("Received item size: " + size);

                for (int i = 0; i < size; i++)
                {
                    int itemId = msg.reader().readInt();
                    int color = msg.reader().readInt();
                    bool claimed = msg.reader().readBoolean();
                    bool canClaim = msg.reader().readBoolean();

                    ItemDiemDanh item = new ItemDiemDanh(itemId, color);
                    item.claimed = claimed;
                    item.canClaim = canClaim;
                    listItem.Add(item);

                    Debug.Log($"Item {i}: ID={itemId}, Claimed={claimed}, CanClaim={canClaim}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in ProcessInitialData: {e.Message}\nStackTrace: {e.StackTrace}");
            }
        }
        private static bool checkCanClaimDay(int day)
        {
            if (listItem == null || listItem.Count < (day + 1) * 6)
                return false;

            // Chỉ cần check canClaim của ngày đó
            int startIndex = day * 6;
            return listItem[startIndex].canClaim;
        }

        public override void switchToMe()
        {
            base.switchToMe();
            loadImage(); // Sẽ tự động gọi sendCheckDiemDanh() sau khi load resources
        }
        public override void paint(mGraphics g)
        {
            try
            {
                // Paint main game screen first
                if (GameScr.gI() != null)
                {
                    GameScr.gI().paint(g);
                }
                int newWidth = GameCanvas.w; // Tăng width thêm 100px so với trước
                int newHeight = GameCanvas.h;

                // Vẽ background với kích thước lớn hơn
                g.drawImageScale(background,
                    (GameCanvas.w - newWidth) / 2,
                    (GameCanvas.h - newHeight) / 2,
                    newWidth + 40, newHeight + 50, 0);

                g.drawImageScale(btnExit, GameCanvas.w - 105, 55, 20, 20, 0);
                paintListItem(g, listItem);
                base.paint(g);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in paint: {e.Message}\nStackTrace: {e.StackTrace}");
            }
        }

        public static void paintListItem(mGraphics g, List<ItemDiemDanh> listItemz)
        {
            try
            {
                if (g == null || listItemz == null || listItemz.Count == 0) return;

                // Vẽ tiêu đề
                mFont.tahoma_7b_dark.drawString(g, "Điểm Danh Hàng Ngày", GameCanvas.w / 2, 60, mFont.CENTER);

                for (int day = 0; day < 7; day++)
                {
                    int startY = 80 + day * 30;

                    // Draw day label
                    if (mFont.tahoma_7b_dark != null)
                    {
                        string dayLabel = GetDayLabel(day);
                        mFont.tahoma_7b_dark.drawString(g, dayLabel, 110, startY + 7, mFont.LEFT);
                    }

                    // Draw items for each day
                    for (int i = 0; i < 6; i++)
                    {
                        int index = day * 6 + i;
                        if (index < listItemz.Count)
                        {
                            int x = GameCanvas.w / 2 - 100 + i * 35;
                            drawItem(g, x, startY, listItemz[index]);
                        }
                    }

                    if (btnNhan != null)
                    {
                        int btnX = GameCanvas.w - 140;
                        int btnY = startY + 3;

                        if (listItemz.Count > day * 6 && listItemz[day * 6] != null)
                        {
                            if (listItemz[day * 6].claimed)
                            {
                                // Chỉ vẽ text "Đã nhận" mà không vẽ button
                                mFont.tahoma_7b_red.drawString(g, "Đã Nhận", btnX + 20, startY + 8, mFont.CENTER);
                            }
                            else if (listItemz[day * 6].canClaim)
                            {
                                // Vẽ cả button và text "Nhận"
                                g.drawImageScale(btnNhan, btnX, btnY, 40, 20, 0);
                                mFont.tahoma_7b_green.drawString(g, "Nhận", btnX + 20, startY + 8, mFont.CENTER);
                            }
                            else
                            {
                                // Vẽ button disable và text "Chờ"
                                g.drawImageScale(btnNhan, btnX, btnY, 40, 20, 0);
                                mFont.tahoma_7b_focus.drawString(g, "Chờ", btnX + 20, startY + 8, mFont.CENTER);
                            }
                        }
                    }
                }

                // Update effect counter
                if (mSystem.currentTimeMillis() - last > 50)
                {
                    count++;
                    last = mSystem.currentTimeMillis();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in paintListItem: {e.Message}");
            }
        }
        private static string GetDayLabel(int day)
        {
            switch (day)
            {
                case 0: return "Thứ 2";
                case 1: return "Thứ 3";
                case 2: return "Thứ 4";
                case 3: return "Thứ 5";
                case 4: return "Thứ 6";
                case 5: return "Thứ 7";
                case 6: return "Chủ Nhật";
                default: return "";
            }
        }
        private static void drawItem(mGraphics g, int x, int y, ItemDiemDanh item)
        {
            try
            {
                if (item == null || g == null) return;

                // Vẽ nền item
                g.drawImageScale(bg, x, y, 25, 25, 0);

                // Vẽ hiệu ứng
                g.drawImageScale(effr[count % 4], x - 3, y - 3, 30, 30, 0);

                // Vẽ icon item với kiểm tra null
                ItemTemplate template = ItemTemplates.get((short)item.id);
                if (template != null)
                {
                    SmallImage.drawSmallImage(g, template.iconID, x + 12, y + 12, 0, 3);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error drawing item: {e.Message}");
            }
        }

        public override void updateKey()
        {
            if (GameCanvas.isPointerHoldIn(GameCanvas.w - 105, 55, 20, 20) && GameCanvas.isPointerJustRelease)
            {
                GameScr.gI().switchToMe();
                return;
            }

            for (int day = 0; day < 7; day++)
            {
                int btnY = 80 + day * 30;
                if (GameCanvas.isPointerHoldIn(GameCanvas.w - 140, btnY + 3, 40, 20) && GameCanvas.isPointerJustRelease)
                {
                    if (checkCanClaimDay(day))
                    {
                        sendClaimReward(day);
                    }
                }
            }

            GameCanvas.clearAllPointerEvent();
            base.updateKey();
        }

        public static void receiveMsg(Message msg)
        {
            try
            {
                byte type = (byte)msg.reader().readByte();
                Debug.Log($"Received message type: {type}");

                switch (type)
                {
                    case 0:
                        ProcessInitialData(msg);
                        break;
                    case 1:
                        ProcessRewardClaim(msg);
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in receiveMsg: {e.Message}");
            }
        }

        public static void ProcessRewardClaim(Message msg)
        {
            try
            {
                int day = msg.reader().readInt();
                bool success = msg.reader().readBoolean();

                if (success)
                {
                    // Update UI immediately
                    for (int i = day * 6; i < (day + 1) * 6; i++)
                    {
                        if (i < listItem.Count)
                        {
                            listItem[i].claimed = true;
                            listItem[i].canClaim = false;
                        }
                    }

                    // Enable next day if exists
                    if (day < 6)
                    {
                        for (int i = (day + 1) * 6; i < (day + 2) * 6; i++)
                        {
                            if (i < listItem.Count)
                            {
                                listItem[i].canClaim = true;
                            }
                        }
                    }

                    GameScr.info1.addInfo("Điểm danh ngày " + GetDayLabel(day) + " thành công!", 0);
                }
                else
                {
                    GameScr.info1.addInfo("Không thể nhận thưởng ngày này!", 0);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in ProcessRewardClaim: {e.Message}");
            }
        }

        public static void sendClaimReward(int day)
        {
            try
            {
                if (day < 0 || day >= 7) // Kiểm tra day nằm trong phạm vi 0-6
                    return;

                Message msg = new Message(70);
                msg.writer().writeByte(1); // type claim reward
                msg.writer().writeInt(day);
                Session_ME.gI().sendMessage(msg);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public static void sendCheckDiemDanh()
        {
            try
            {
                Message msg = new Message(70);
                msg.writer().writeByte(0); // type check data
                Session_ME.gI().sendMessage(msg);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
    }

    public class ItemDiemDanh
    {
        public int id;
        public int color;
        public bool claimed;
        public bool canClaim; // Thêm biến canClaim

        public ItemDiemDanh(int id, int color)
        {
            this.id = id;
            this.color = color;
            this.claimed = false;
            this.canClaim = false; // Khởi tạo canClaim là false
        }
    }
}