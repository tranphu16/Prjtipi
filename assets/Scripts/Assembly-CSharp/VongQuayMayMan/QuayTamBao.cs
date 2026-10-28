using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;

public class VongQuayMayMan : mScreen
{
    public static List<ItemVongQuay> listItem = new List<ItemVongQuay>();
    public static List<ItemVongQuay> listNhan = new List<ItemVongQuay>();
    public static int phiQuayx1 = 1;
    public static int phiQuayx10 = 10;
    public static int icon1 = 10;
    public static int icon2 = 10;
    public static bool isTamBao = false;
    public static bool isTamBaoVip = false;
    public static bool isNhan = false;
    public static Image khung, background, btnExit, btnRollx1, btnRollx10, thuong, vip, txtThuong, txtVip, chucMung, btnDong, btnReRoll, imgSelect, homQua;
    public static Image bg;
    public static Image[] effr = new Image[4];
    public static Image[] effy = new Image[4];
    public static Image quay;
    public static Item item = new Item();
    public static int vanMay = 0;
    private static VongQuayMayMan vqmm;

    public static bool isQuay = false;
    public static long lastQuay;
    public static int soLuot;
    public static long count = 0;
    public static long select = 0;
    public static long lastSelect = mSystem.currentTimeMillis();
    public static long last = mSystem.currentTimeMillis();
    public static int speed = 5;

    public static string slKeyText = "0";
    public static VongQuayMayMan gI()
    {
        return vqmm ??= new VongQuayMayMan();
    }

    public static void loadImage()
    {
        for (int i = 13; i <= 23; i++)
        {
            listItem.Add(new ItemVongQuay(i, 6));
        }
        background = Image.createImage("res/x2/vongQuayMayMan/background");
        btnExit = Image.createImage("res/x2/vongQuayMayMan/btnExit");
        btnRollx1 = Image.createImage("res/x2/vongQuayMayMan/btnRollx1");
        btnRollx10 = Image.createImage("res/x2/vongQuayMayMan/btnRollx10");
        imgSelect = Image.createImage("res/x2/vongQuayMayMan/select");
        btnDong = Image.createImage("res/x2/vongQuayMayMan/btnDong");
        btnReRoll = Image.createImage("res/x2/vongQuayMayMan/btnReRoll");
        quay = Image.createImage("res/x2/vongQuayMayMan/quay");
        chucMung = Image.createImage("res/x2/vongQuayMayMan/chucMung");
        homQua = Image.createImage("res/x2/vongQuayMayMan/gift70");
        bg = Image.createImage($"res/x2/vongQuayMayMan/bg0");
        for (int i = 0; i < 4; i++)
        {
            effr[i] = Image.createImage($"res/x2/vongQuayMayMan/effr{i}");
            effy[i] = Image.createImage($"res/x2/vongQuayMayMan/effy{i}");
        }
    }

    public override void update()
    {
        GameScr.gI().update();
    }

    public override void paint(mGraphics g)
    {
        try
        {
            GameScr.gI().paint(g);
            int newWidth = GameCanvas.w - 100;
            int newHeight = GameCanvas.h - 70;

            g.drawImageScale(background, (GameCanvas.w - newWidth) / 2, (GameCanvas.h - newHeight) / 2, newWidth, newHeight, 0); // Thay đổi kích thước nền

            g.drawImageScale(btnExit, GameCanvas.w - 120, 55, 20, 20, 0);

            if (isNhan)
            {
                paintNhanThuong(g, listNhan);
            }
            else
            {
                paintListItem(g, listItem);
            }

            g.drawImageScale(isNhan ? btnDong : btnRollx1, GameCanvas.w / 2 - 90, GameCanvas.h - 95, 66, 25, 0);
            mFont.tahoma_7b_green.drawString(g, isNhan ? "" : $"Tốn {phiQuayx1}", GameCanvas.w / 2 - 80, GameCanvas.h - 105, mFont.LEFT);

            g.drawImageScale(isNhan ? btnReRoll : btnRollx10, GameCanvas.w / 2 + 30, GameCanvas.h - 95, 66, 25, 0);
            mFont.tahoma_7b_green.drawString(g, isNhan ? "" : $"Tốn {phiQuayx10}", GameCanvas.w / 2 + 40, GameCanvas.h - 105, mFont.LEFT);

            if (isQuay)
            {
                if (mSystem.currentTimeMillis() - lastSelect > speed)
                {
                    select = (select + 1) % listNhan.Count; // Dùng listNhan thay vì listItem
                    lastSelect = mSystem.currentTimeMillis();
                    if (speed > 5) speed -= 1;
                }

                if (mSystem.currentTimeMillis() - lastQuay > 3000)
                {
                    isQuay = false;
                    speed = 5;
                    isNhan = true;
                    // Không cần clear và add lại listNhan vì đã có data từ server
                }
            }
            base.paint(g);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e.ToString());
        }
    }

    public static void paintNhanThuong(mGraphics g, List<ItemVongQuay> listItemz)
    {
        int baseY = GameCanvas.hh;
        int padding = 10;
        int itemSize = 5;
        int rowWidth = 5 * (itemSize + padding);

        g.drawImageScale(chucMung, GameCanvas.hw - 70, 40, 140, 100, 0);

        for (int i = 0; i < listItemz.Count; i++)
        {
            int row = i / 5;
            int col = i % 5;

            int x = GameCanvas.w / 2 - rowWidth + col * (itemSize + padding + 23);
            int y = baseY + row * (itemSize - padding - 25);

            var item = listItemz[i];
            g.drawImageScale(bg, x - 13, y - 13, itemSize + 26, itemSize + 26, 0);

            if (item.color % 5 == 3)
            {
                g.drawImageScale(effy[count % 4], x - 18, y - 18, itemSize + 36, itemSize + 36, 0);
            }
            else if (item.color % 5 == 4)
            {
                g.drawImageScale(effr[count % 4], x - 18, y - 18, itemSize + 36, itemSize + 36, 0);
            }

            if (i == select && isQuay)
            {
                g.drawImageScale(imgSelect, x - 20, y - 20, itemSize + 40, itemSize + 40, 0);
            }

            SmallImage.drawSmallImage(g, ItemTemplates.get((short)item.id).iconID, x + itemSize / 2, y + itemSize / 2, 0, 3);
        }

        if (mSystem.currentTimeMillis() - last > 50)
        {
            count++;
            last = mSystem.currentTimeMillis();
        }
    }


    public static void paintListItem(mGraphics g, List<ItemVongQuay> listItemz)
    {
        int itemSize = 5;
        int padding = 10;

        int frameWidth = 150;
        int frameHeight = 50;

        int startX = GameCanvas.w / 2 - frameWidth / 2;
        int startY = GameCanvas.h / 2 - frameHeight;

        int cols = 5; // Số cột
        int rows = 3; // Số hàng

        int cellWidth = (frameWidth - (cols - 1) * padding) / cols;
        int cellHeight = (frameHeight - (rows - 1) * padding) / rows;

        int index = 0;

        // Vẽ Dòng 1
        for (int j = 0; j < cols; j++)
        {
            if (index < listItemz.Count)
            {
                int x = startX + j * (cellWidth + padding + 5);
                int y = startY;

                drawItem(g, x, y, listItemz[index], itemSize, index);

                index++;
            }
        }
        SmallImage.drawSmallImage(g, isTamBaoVip ? icon2 : icon1, GameCanvas.w / 2 - 45, GameCanvas.h - 100, 0, 3);
        SmallImage.drawSmallImage(g, isTamBaoVip ? icon2 : icon1, GameCanvas.w / 2 + 80, GameCanvas.h - 100, 0, 3);

        g.drawImageScale(homQua, GameCanvas.w / 2 - 40, GameCanvas.h / 2 - 38, 50, 50, 0);
        mFont.tahoma_7b_green.drawString(g, slKeyText, GameCanvas.w / 2 + 10, GameCanvas.h / 2 - 15, mFont.LEFT);
        SmallImage.drawSmallImage(g, 4028, GameCanvas.w / 2 + 25, GameCanvas.h / 2 - 14, 0, 0);

        // Vẽ Dòng 2 (Với hai ô ở hai bên)
        if (index < listItemz.Count)
        {
            int xLeft = startX;
            int y = startY + cellHeight + padding + 15;

            drawItem(g, xLeft, y, listItemz[index], itemSize, index);

            index++;

            int xRight = startX + (cols - 1) * (cellWidth + padding + 5);

            if (index < listItemz.Count)
            {
                drawItem(g, xRight, y, listItemz[index], itemSize, index);
                index++;
            }
        }

        // Vẽ Dòng 3
        for (int j = 0; j < cols; j++)
        {
            if (index < listItemz.Count)
            {
                int x = startX + j * (cellWidth + padding + 5);
                int y = startY + 2 * (cellHeight + padding + 15);

                drawItem(g, x, y, listItemz[index], itemSize, index);

                index++;
            }
        }

        if (mSystem.currentTimeMillis() - last > 50)
        {
            count++;
            last = mSystem.currentTimeMillis();
        }
    }

    private static void drawItem(mGraphics g, int x, int y, ItemVongQuay item, int itemSize, int index)
    {
        // Vẽ khung nền cho item
        g.drawImageScale(bg, x - 13, y - 13, itemSize + 26, itemSize + 26, 0);

        // Vẽ hiệu ứng nếu index là 1, 4, 6, hoặc 9
        if (index == 1 || index == 4 || index == 7 || index == 9)
        {
            g.drawImageScale(effy[count % 4], x - 18, y - 18, itemSize + 36, itemSize + 36, 0);
        }

        SmallImage.drawSmallImage(g, ItemTemplates.get((short)item.id).iconID, x + itemSize / 2, y + itemSize / 2, 0, 3);

        if (index == select && isQuay)
        {
            g.drawImageScale(imgSelect, x - 20, y - 20, itemSize + 40, itemSize + 40, 0);
        }
    }


    public override void updateKey()
    {
        // Nút Exit
        if (GameCanvas.isPointerHoldIn(GameCanvas.w - 120, 55, 20, 20) && GameCanvas.isPointerJustRelease)
        {
            GameScr.gI().switchToMe();
        }

        // Nút Quay X1
        if (!isQuay)
        {
            int rollBtnX = GameCanvas.w / 2 - 100;
            int rollBtnY = GameCanvas.h - 95;
            if (GameCanvas.isPointerHoldIn(rollBtnX, rollBtnY, 66, 25) && GameCanvas.isPointerJustRelease)
            {
                speed = Res.random(10, 50);
                lastQuay = mSystem.currentTimeMillis();
                if (isNhan) isNhan = false;
                else
                {
                    soLuot = 1;
                    sendTamBao(soLuot);
                }
            }

            // Nút Quay X10
            int rollBtnX10 = GameCanvas.w / 2 + 40;
            int rollBtnY10 = GameCanvas.h - 95;
            if (GameCanvas.isPointerHoldIn(rollBtnX10, rollBtnY10, 66, 25) && GameCanvas.isPointerJustRelease)
            {
                speed = Res.random(10, 50);
                lastQuay = mSystem.currentTimeMillis();
                if (isNhan) isNhan = false;
                else
                {
                    soLuot = 10;
                    sendTamBao(soLuot);
                }
            }
        }

        Char.myCharz().currentMovePoint = null;
        GameCanvas.clearAllPointerEvent();
        base.updateKey();
    }

    public static void receiveMsg(Message msg)
    {
        try
        {
            int type = msg.reader().readByte();
            switch (type)
            {
                case 0:
                case 3:
                    ProcessInitialData(msg, type);
                    break;

                case 1:
                    isQuay = true;
                    lastQuay = mSystem.currentTimeMillis();
                    listNhan.Clear();

                    int size = msg.reader().readInt();
                    if (size < 0 || size > msg.reader().available() / 8) return;

                    for (int i = 0; i < size; i++)
                    {
                        int itemId = msg.reader().readInt();
                        int color = msg.reader().readInt();
                        listNhan.Add(new ItemVongQuay(itemId, color));
                    }

                    // Set a timer to show results after animation
                    speed = 50;
                    lastSelect = mSystem.currentTimeMillis();

                    break;
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Error in receiveMsg: {e.Message}");
        }
    }

    private static void ProcessInitialData(Message msg, int type)
    {
        isTamBaoVip = type == 3;
        listItem.Clear();

        int size = msg.reader().readInt();
        if (size < 0 || size > msg.reader().available() / 8) return;

        for (int i = 0; i < size; i++)
        {
            int itemId = msg.reader().readInt();
            int color = msg.reader().readInt();
            listItem.Add(new ItemVongQuay(itemId, color));
        }

        slKeyText = msg.reader().readInt().ToString();
        icon1 = msg.reader().readInt();
        icon2 = msg.reader().readInt();
    }

    public static void sendTamBao(int luotQuay)
    {
        try
        {
            Message msg = new Message(69);
            msg.writer().writeByte(isTamBaoVip ? 4 : 1);
            msg.writer().writeInt(luotQuay);
            Session_ME.gI().sendMessage(msg);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e.ToString());
        }
    }


    public static void sendDataTamBao()
    {
        try
        {
            Message msg = new Message(69);
            msg.writer().writeByte(isTamBaoVip ? 3 : 0);
            Session_ME.gI().sendMessage(msg);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e.ToString());
        }
    }

    public override void switchToMe()
    {
        SoundMn.gI().stopAll();
        base.switchToMe();
    }
}