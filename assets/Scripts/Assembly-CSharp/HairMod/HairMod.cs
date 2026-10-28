using Assets.src.e;
using MODMANHHDC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class HairMod : IActionListener
{

    public static string acc;
    public static string pass;
    public static int server = -1;
    public static bool ttsp,
        ttdt,
        phimtat = true,
        showchars = true,
        isSanBoss = true,
        xoamap;
    public static List<string> vMenuMod;
    public static long currLogin;
    public static int xJ, yJ;
    private static HairMod instance;
    public static List<Char> vListChar;
    public static List<SanBoss> vBoss;
    public static int xChar, yChar, wChar, hChar;
    public static long timeUse;
    public static Image logo, settings;
    public static int xC, yC;
    public HairMod()
    {
        vBoss = new List<SanBoss>();
        xC = 0;
        yC = 30;
    }
    public static HairMod gI()
    {
        if (instance == null)
        {
            instance = new HairMod();
        }
        return instance;
    }
    public static void initMenu()
    {
        HunrModMenu.ShowMain();
    }

    public static void ShowLegacyMenu()
    {
        if (vMenuMod == null || vMenuMod.Count == 0)
        {
            vMenuMod = new List<string>()
            {
                "Thông Tin",
                "Auto Item",
                "Xóa Map",
                "Auto Skill",
                "Thêm"
            };
        }
        MyVector myVector = new MyVector();
        for (int i = 0; i < vMenuMod.Count; i++)
        {

            myVector.addElement(new Command(vMenuMod[i], gI(), 1, i));

        }
        GameCanvas.menu.startAt(myVector, 0);
    }
    public static void Debug(string s)
    {

    }
    public static void addInfo(string s, bool b)
    {
        GameScr.info1.addInfo(s + (b ? "Bật" : "Tắt"), 0);
    }
    public static void addInfo(string s)
    {
        GameScr.info1.addInfo(s, 0);
    }
    public static bool isHaveItem(int id)
    {
        for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
        {
            Item item = Char.myCharz().arrItemBag[i];
            if (item != null && item.template.id == id)
            {
                return true;
            }
        }
        return false;
    }
    public static void useItemWithTime(int id, long time)
    {
        foreach (var item in Char.myCharz().arrItemBag)
        {
            if (item != null && item.template.id == id)
            {
                if (mSystem.currentTimeMillis() - timeUse >= time)
                {
                    timeUse = mSystem.currentTimeMillis();
                    time = timeUse;
                    Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
                    break;
                }
            }
        }
    }
    public static void UseItem(int id)
    {
        foreach (var item in Char.myCharz().arrItemBag)
        {
            if (item != null && item.template.id == id)
            {
                Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
                break;
            }
        }
    }
    public static Item FindItemName(string name)
    {
        foreach (var item in Char.myCharz().arrItemBag)
        {
            if (item != null && item.template.name.ToLower().StartsWith(name))
            {
                return item;
            }
        }
        return null;
    }
    public static Item FindItemNameExactly(string name)
    {
        foreach (var item in Char.myCharz().arrItemBag)
        {
            if (item != null && item.template.name.ToLower().Contains(name))
            {
                return item;
            }
        }
        return null;
    }
    public static bool IsPressed(int x, int y, int w, int h)
    {
        return GameCanvas.isPointerHoldIn(x, y, w, h) /*&& GameCanvas.isPointerClick*/;
    }
    public static void resizeImage(Image img)
    {
        int wx1 = img.w / 2;//vi la hinh anh x2 nen chuyen size ve x1 xong tinh len
        int hx1 = img.h / 2;
        int w = wx1 * mGraphics.zoomLevel;
        int h = hx1 * mGraphics.zoomLevel;
        img.texture = myCommand.Resize(img.texture, w, h);
        img.w = img.texture.width;
        img.h = img.texture.height;
        Image.setTextureQuality(img.texture);
    }
   public static Image imgGIF;
    public static void PaintGIFLogo(mGraphics g, int x, int y, int z)
    {
        if (imgGIF == null)
        {
            imgGIF = Image.createImage("res/x2/mainImage/logo1");
            if (mGraphics.zoomLevel != 2)
            {
                InventoryBag.gI().resizeImage(imgGIF);
            }
            return;
        }
        else { 
            g.drawImage(imgGIF, x, y, z);
        }
    }
    public static void HotKey()
    {
        switch (GameCanvas.keyAsciiPress)
        {
            case 'q':
                HunrModMenu.ShowProjectModPanel();
                break;
            case 'x':
                initMenu();
                break;
            case 'j':
                Pk9rXmap.LoadMap(0);
                break;
            case 'k':
                Pk9rXmap.LoadMap(2);
                break;
            case 'l':
                Pk9rXmap.LoadMap(1);
                break;
            case 'a':
                GameCanvas.panel.indexMod[12] = !GameCanvas.panel.indexMod[12];
                HairMod.addInfo("Tự Đánh\n", GameCanvas.panel.indexMod[12]);
                break;
            case 'b':
                Service.gI().friend(0, -1);
                break;
            case 'd':
                if (Char.myCharz().myskill != null)
                {
                    AutoSkil.gI.method_18();
                }
                break;
            case 'f':
                if (isHaveItem(921))
                {
                    UseItem(921);
                }
                else if (isHaveItem(454))
                {
                    UseItem(454);
                }
                else if (isHaveItem(2379))
                {
                    UseItem(2379);
                }
                else
                {
                    Item item = FindItemName("bông tai");
                    if (item != null)
                    {
                        UseItem(item.template.id);
                    }
                }
                break;
            case 'e':
                GameCanvas.panel.indexMod[13] = !GameCanvas.panel.indexMod[13];
                HairMod.addInfo("Tự Động Hồi Sinh\n", GameCanvas.panel.indexMod[13]);
                break;
            case 'c':
                if (isHaveItem(194))
                {
                    UseItem(194);
                }
                else if (isHaveItem(193))
                {
                    UseItem(193);
                }
                else if (isHaveItem(2309))
                {
                    UseItem(2309);
                }
                break;
            case 'm':
                Service.gI().openUIZone();
                GameCanvas.panel.setTypeZone();
                GameCanvas.panel.show();
                break;
            case 'n':
                AutoPick.gclass11_0.method_1();
                break;
            case 's':
                GameCanvas.panel.indexMod[2] = !GameCanvas.panel.indexMod[2];
                HairMod.addInfo("Auto Focus Boss\n", GameCanvas.panel.indexMod[2]);
                break;
            case 't':
                HunrModMenu.ShowAutoTrain();
                break;
            case 'g':
                if (Char.myCharz().charFocus == null)
                {
                    GameScr.info1.addInfo("Vui lòng chọn mục tiêu", 0);
                }
                else
                {
                    Service.gI().giaodich(0, Char.myCharz().charFocus.charID, -1, -1);
                }
                break;
        }
    }
    public static void ResetTF()
    {
        ChatTextField.gI().isShow = false;
        ChatTextField.gI().parentScreen = GameScr.gI();
    }
    public static void InputTF(IChatable chatable, string name, string text, int typeInput)
    {
        ChatTextField.gI().strChat = name;
        ChatTextField.gI().tfChat.name = text;
        ChatTextField.gI().tfChat.setIputType(typeInput);
        ChatTextField.gI().left.caption = "OK";
        ChatTextField.gI().right.caption = "Cancel";
        ChatTextField.gI().startChat2(chatable, string.Empty);
    }

    public static IEnumerator loadAccount()
    {
        while (GameCanvas.currentScreen != GameCanvas.serverScreen)
        {
            yield return new WaitForSecondsRealtime(2.5f);
        }

        if (GameCanvas.currentDialog != null)
            GameCanvas.currentDialog.center.performAction();


        if (!ServerListScreen.loadScreen)
        {
            GameCanvas.serverScreen.perform(2, null);

            yield return new WaitForSecondsRealtime(1f);
        }

        while (!ServerListScreen.loadScreen || GameCanvas.currentScreen != GameCanvas.serverScreen)
        {
            yield return null;
        }

        if (GameCanvas.loginScr == null)
        {
            GameCanvas.loginScr = new LoginScr();
        }

        acc = acc != null && acc != string.Empty ? acc : GameCanvas.loginScr.tfUser.getText();
        pass = pass != null && pass != string.Empty ? pass : GameCanvas.loginScr.tfPass.getText();
        server = server != -1 ? server : ServerListScreen.ipSelect;
        if (acc != string.Empty && pass != string.Empty && server != -1)
        {
            yield return new WaitForSecondsRealtime(2);

            if (GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen)
            {

                while (server != ServerListScreen.ipSelect || !Session_ME.gI().isConnected())
                {
                    GameCanvas.serverScr.perform(100 + server, null);

                    yield return new WaitForSecondsRealtime(0.5f);

                }

                if (GameCanvas.loginScr == null)
                {
                    GameCanvas.loginScr = new LoginScr();
                }
                GameCanvas.loginScr.switchToMe();

                Service.gI().login(acc, pass, GameMidlet.VERSION, (sbyte)0);

                if (Session_ME.connected)
                {
                    GameCanvas.startWaitDlg();
                }
                else
                {
                    GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
                }
            }
        }
        else
        {
            if ((GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen) &&
                Rms.loadRMSString("acc") != string.Empty)
            {

                GameCanvas.serverScreen.perform(3, null);
            }
        }
    }
    public static void Update()
    {
        HunrModMenu.LoadDisplaySettings();
        TuanBipMods.Update();
        HunrAutoChat.Update();
        Vector.Updates();
        AutoPick.gclass11_0.method_0();
        AutoPean.gclass10_0.method_0();
        AutoItem.gclass5_0.method_0();
        Boss.Update();
        ListCharsInMap.update();
        Pk9rXmap.Update();
        TanSatHandler.Update();
        AutoSkil.gI.method_0();
        if (GameCanvas.panel.indexMod[13])
        {
            Revive();
        }
        if (GameCanvas.panel.indexMod[14])
        {
            Main.main.StartCoroutine(loadAccount());
        }
        if (showchars)
        {
            vListChar = new List<Char>();
            vListChar.Clear();
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                if (@char.cName != null && @char.cName != "" && !@char.isPet && !@char.isMiniPet && !@char.cName.StartsWith("#") && !@char.cName.StartsWith("$") && @char.cName != "Trọng tài")
                    vListChar.Add(@char);
            }
        }
        if (GameCanvas.panel.indexMod[2])
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)(GameScr.vCharInMap.elementAt(i));
                if (isBoss(@char) && @char.cTypePk == 5)
                {
                    Char.myCharz().focusManualTo(@char);
                }
            }
        }
        if (GameCanvas.panel.indexMod[3])
        {
            for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
            {
                Item item = Char.myCharz().arrItemBag[i];
                if (item != null)
                {
                    for (int j = 0; j < item.itemOption.Length; j++)
                    {
                        if (item.itemOption[j] != null && (item.itemOption[j].optionTemplate.id == 93 || item.template.name.ToLower().Contains("sao pha lê")))
                        {
                            if (mSystem.currentTimeMillis() - currVD >= 2000L)
                            {
                                currVD = mSystem.currentTimeMillis();
                                Service.gI().useItem(2, 1, (sbyte)i, -1);
                            }
                            
                        }
                    }
                }
            }
        }
    }
    static long currVD;
    public static void Revive()
    {
        if ((global::Char.myCharz().luong + Char.myCharz().luongKhoa > 0) && global::Char.myCharz().meDead && global::Char.myCharz().cHP <= 0 && GameCanvas.gameTick % 20 == 0)
        {
            Service.gI().wakeUpFromDead();
            global::Char.myCharz().meDead = false;
            global::Char.myCharz().statusMe = 1;
            global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
            global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
            global::Char @char = global::Char.myCharz();
            global::Char char2 = global::Char.myCharz();
            global::Char.myCharz().cp3 = 0;
            char2.cp2 = 0;
            @char.cp1 = 0;
            ServerEffect.addServerEffect(109, global::Char.myCharz(), 2);
            GameScr.gI().center = null;
            GameScr.isHaveSelectSkill = true;
        }
    }

    public static Texture2D Convert(Texture texture)
    {
        RenderTexture renderTexture = new RenderTexture(texture.width, texture.height, 0);
        Graphics.Blit(texture, renderTexture);
        RenderTexture.active = renderTexture;
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;
        renderTexture.Release();
        return texture2D;
    }
    static Texture2D bg;
    //public static void paintBG()
    //{
    //    if(bg == null)
    //    {
    //        bg = Resources.Load("bg") as Texture2D;
    //        return;
    //    }
    //    GUI.DrawTexture(new Rect(0, 0, Screen.width - Screen.safeArea.x,Screen.height),bg);
    //}
    public static void updateKeyTouch()
    {
        if (GameCanvas.panel.indexMod[0])
        {
            Boss.UpdateTouch();
        }
        if (showchars)
        {
            ListCharsInMap.updateTouch();
        }
        if (!Main.isPC && phimtat)
        {
            if (IsPressed(GameScr.xTG, GameScr.yTG - 33, GameScr.imgNut.getWidth(), GameScr.imgNut.getHeight()))
            {
                //mScreen.keyTouch = 1000;
                if (GameCanvas.isPointerJustRelease)
                {

                    if (isHaveItem(921))
                    {
                        UseItem(921);
                    }
                    else if (isHaveItem(454))
                    {
                        UseItem(454);
                    }
                    else
                    {
                        Item item = FindItemName("bông tai");
                        if (item != null)
                        {
                            UseItem(item.template.id);
                        }
                    }
                    GameCanvas.clearAllPointerEvent();
                    return;
                }
            }
            if (IsPressed(GameScr.xHP - 40, GameScr.yHP, GameScr.imgNut.getWidth(), GameScr.imgNut.getHeight()))
            {
                mScreen.keyTouch = 1001;
                if (GameCanvas.isPointerJustRelease)
                {
                    Service.gI().openUIZone();
                    GameCanvas.clearAllPointerEvent();
                    return;
                }
            }
            UpdateKeyScreen();
        }
        if (settings == null)
        {
            settings = GameCanvas.loadImage("/mainImage/setting.png");
        }
        Image modButton = (settings != null) ? settings : GameScr.imgMenu;
        int modButtonWidth = (modButton != null) ? modButton.getWidth() : 32;
        int modButtonHeight = (modButton != null) ? modButton.getHeight() : 32;
        if(IsPressed(160, 0, modButtonWidth, modButtonHeight) && GameCanvas.isPointerClick)
        {
            if (GameCanvas.isPointerJustRelease)
            {
                initMenu();
                SoundMn.gI().panelOpen();
                GameCanvas.clearAllPointerEvent();
                return;
            }
        }
        if (showchars)
        {
            for (int i = 0; i < vListChar.Count; i++)
            {
                if (IsPressed(xChar, (GameCanvas.panel.indexMod[0]) ? 95 + (hChar + 1) * i : 35 + (hChar + 1) * i, wChar, hChar))
                {
                    GameCanvas.isPointerJustDown = false;
                    GameScr.gI().isPointerDowning = false;
                    if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
                    {
                        global::Char @char = vListChar[i];
                        if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.cName == @char.cName)
                        {
                            TeleportTo(global::Char.myCharz().charFocus.cx, global::Char.myCharz().charFocus.cy);
                        }
                        else
                        {
                            FocusTo(@char.charID);
                        }
                        global::Char.myCharz().currentMovePoint = null;
                        GameCanvas.clearAllPointerEvent();
                        return;
                    }
                }
            }
        }
    }
    static bool pp = true;
    static bool pd = true;
    public static void paintGIF(int x, int y, int w, int h, Texture2D texture)
    {
        GUI.DrawTexture(new Rect(x, y, w, h), texture);
    }
    public static void paint(mGraphics g)
    {
        Debug("PA1");
        if (settings == null)
        {
            settings = GameCanvas.loadImage("/mainImage/setting.png");
        }
        Image modButton = (settings != null) ? settings : GameScr.imgMenu;
        if (modButton != null)
        {
            g.drawImage(modButton, 160, 0, mGraphics.TOP | mGraphics.LEFT);
        }
        Debug("PA2");
        PaintThongTin(g);
        Debug("PA3");
        if ((Application.platform is RuntimePlatform.Android || Application.platform is RuntimePlatform.IPhonePlayer) && phimtat && GameCanvas.isTouch && !GameCanvas.panel.isShow && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
        {
            g.drawImage((mScreen.keyTouch != 1000) ? GameScr.imgNut : GameScr.imgNutF, GameScr.xTG + 20, GameScr.yTG - 20, mGraphics.HCENTER | mGraphics.VCENTER);
            if (isHaveItem(921))
            {
                Small img = SmallImage.imgNew[7993];
                if (img == null)
                {
                    SmallImage.createImage(7993);
                    return;
                }
                g.drawImage(img.img, GameScr.xTG + 21, GameScr.yTG - 20, mGraphics.HCENTER | mGraphics.VCENTER);
            }
            else if (isHaveItem(454))
            {
                Small img = SmallImage.imgNew[3896];
                if (img == null)
                {
                    SmallImage.createImage(3896);
                    return;
                }
                g.drawImage(img.img, GameScr.xTG + 21, GameScr.yTG - 20, mGraphics.HCENTER | mGraphics.VCENTER);
            }
            else
            {
                Item item = FindItemName("bông tai");
                if (item != null)
                {
                    Small img = SmallImage.imgNew[item.template.iconID];
                    if (img == null)
                    {
                        SmallImage.createImage(item.template.iconID);
                        return;
                    }
                    g.drawImage(img.img, GameScr.xTG + 21, GameScr.yTG - 20, mGraphics.HCENTER | mGraphics.VCENTER);
                }
            }
            g.drawImage((mScreen.keyTouch != 1001) ? GameScr.imgNut : GameScr.imgNutF, GameScr.xHP - 20, GameScr.yHP + 20, mGraphics.HCENTER | mGraphics.VCENTER);
            g.drawImage(GameScr.doikhu, GameScr.xHP - 20, GameScr.yHP + 20, mGraphics.HCENTER | mGraphics.VCENTER);
            mFont.tahoma_7b_white.drawString(g, TileMap.zoneID.ToString(), GameScr.xHP - 20, GameScr.yHP + 15, mGraphics.HCENTER | mGraphics.VCENTER, mFont.tahoma_7b_green2);
            xJ = GameCanvas.w - GameScr.xHP - 20;
            yJ = GameScr.yHP - 60;
            g.drawImage((mScreen.keyTouch != 1002) ? GameScr.imgNut : GameScr.imgNutF, xJ, yJ, mGraphics.HCENTER | mGraphics.VCENTER);
            g.drawImage(GameScr.arrow5, xJ, yJ, mGraphics.HCENTER | mGraphics.VCENTER);
            xJ += GameScr.imgNut.getWidth() + 20;
            g.drawImage((mScreen.keyTouch != 1003) ? GameScr.imgNut : GameScr.imgNutF, xJ, yJ, mGraphics.HCENTER | mGraphics.VCENTER);
            g.drawImage(GameScr.arrow4, xJ, yJ, mGraphics.HCENTER | mGraphics.VCENTER);
            xJ += GameScr.imgNut.getWidth() + 20;
            g.drawImage((mScreen.keyTouch != 1004) ? GameScr.imgNut : GameScr.imgNutF, xJ, yJ, mGraphics.HCENTER | mGraphics.VCENTER);
            g.drawImage(GameScr.arrow, xJ, yJ, mGraphics.HCENTER | mGraphics.VCENTER);
        }
        Debug("PA4");
        if (GameCanvas.panel.indexMod[0])
        {
            Boss.Paint(g);
        }
        ListCharsInMap.paint(g);
        if (GameCanvas.panel.indexMod[1])
        {
            int x = GameCanvas.w / 2;
            int y = 90;
            mFont.tahoma_7b_green.drawStringBd(g, "[BOSS Trong Khu]", x, y, mFont.CENTER, mFont.tahoma_7b_green2);
            y += 11;
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                var c = (Char)GameScr.vCharInMap.elementAt(i);
                if (c != null && (c.cTypePk == 5 || (c.charID < 0 && c.charID > -1000 && c.charID != -114)) && !c.isPet && !c.isMiniPet)
                {
                    mFont.tahoma_7b_blue.drawStringBd(g, string.Concat(new object[] {
                        c.cName, "[",NinjaUtil.getMoneys(c.cHP),"/",NinjaUtil.getMoneys(c.cHPFull),"]"," - ",(global::Char.myCharz().charFocus == c) ? "Đang Focus" : "Chưa Focus"
                        }), x, y, mFont.CENTER, mFont.tahoma_7_blue1);
                    x += 11;
                }
            }
        }
    }
    public static void FocusTo(int charId)
    {
        int num = 0;
        Char @char;
        while (true)
        {
            if (num < GameScr.vCharInMap.size())
            {
                @char = (Char)GameScr.vCharInMap.elementAt(num);
                if (!@char.isMiniPet && !@char.isPet && @char.charID == charId)
                    break;
                num++;
                continue;
            }
            return;
        }
        Char.myCharz().mobFocus = null;
        Char.myCharz().npcFocus = null;
        Char.myCharz().itemFocus = null;
        Char.myCharz().charFocus = @char;
    }
    public static void TeleportTo(int x, int y)
    {
        Char.myCharz().cx = x;
        Char.myCharz().cy = y;
        Service.gI().charMove();
        Char.myCharz().cx = x;
        Char.myCharz().cy = y + 1;
        Service.gI().charMove();
        Char.myCharz().cx = x;
        Char.myCharz().cy = y;
        Service.gI().charMove();
    }
    public static void paintListBosses(mGraphics g)
    {
        int num = 35;
        foreach (var boss in vBoss)
        {
            boss.paint(g, GameCanvas.w - 2, num, mFont.RIGHT);
            num += 10;
        }
    }
    public static void PaintListChars(mGraphics g)
    {
        if (showchars)
        {

            wChar = 142;
            hChar = 9;
            xChar = GameCanvas.w - wChar;
            yChar = (GameCanvas.panel.indexMod[0]) ? 95 : 35;
            for (int i = 0; i < vListChar.Count; i++)
            {
                Char @char = vListChar[i];
                g.setColor(2721889, 0.5f);
                g.fillRect(xChar, yChar + 2, wChar - 2, hChar);
                if (@char.cName != null && @char.cName != "" && !@char.isPet && !@char.isMiniPet && !@char.cName.StartsWith("#") && !@char.cName.StartsWith("$") && @char.cName != "Trọng tài")
                {
                    string text = string.Concat(new object[4]
                    {
                        @char.cName,
                        " [",
                        NinjaUtil.getMoneys(@char.cHP),
                        "]"
                    });
                    bool flag;
                    if (!(flag = isBoss(@char)))
                        text = string.Concat(new object[6]
                        {
                            @char.cName,
                            " [",
                            NinjaUtil.getMoneys(@char.cHP),
                            " - ",
                            @char.getGender(),
                            "]"
                        });
                    if (Char.myCharz().charFocus != null && Char.myCharz().charFocus.cName == @char.cName)
                    {
                        g.setColor(14155776);
                        g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy + 1, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
                        mFont.tahoma_7b_red.drawString(g, i + 1 + ". " + text, xChar + 2, yChar, 0);
                    }
                    else if (flag)
                    {
                        g.setColor(16383818);
                        g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy + 1, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
                        mFont.tahoma_7_red.drawString(g, i + 1 + ". " + text, xChar + 2, yChar, 0);
                    }
                    else
                    {
                        mFont.tahoma_7.drawString(g, i + 1 + ". " + text, xChar + 2, yChar, 0);
                    }
                    yChar += hChar + 1;
                }
            }
        }
    }
    public static bool isBoss(Char ch)
    {
        if (ch.cName != null && ch.cName != "" && !ch.isPet && !ch.isMiniPet && char.IsUpper(char.Parse(ch.cName.Substring(0, 1))) && ch.cName != "Trọng tài" && !ch.cName.StartsWith("#"))
            return !ch.cName.StartsWith("$");
        return false;
    }
    private static void UpdateKeyScreen()
    {
        xJ = GameCanvas.w - GameScr.xHP - 40;
        yJ = GameScr.yHP - 80;
        if (IsPressed(xJ, yJ, GameScr.imgNut.getWidth(), GameScr.imgNut.getHeight()))
        {
            mScreen.keyTouch = 1002;
            if (GameCanvas.isPointerJustRelease)
            {
                GameCanvas.keyAsciiPress = 'j';
                GameCanvas.clearAllPointerEvent();
            }
            return;
        }
        xJ += GameScr.imgNut.getWidth() + 20;
        if (IsPressed(xJ, yJ, GameScr.imgNut.getWidth(), GameScr.imgNut.getHeight()))
        {
            mScreen.keyTouch = 1003;
            if (GameCanvas.isPointerJustRelease)
            {
                GameCanvas.keyAsciiPress = 'k';
                GameCanvas.clearAllPointerEvent();
            }
            return;
        }
        xJ += GameScr.imgNut.getWidth() + 20;
        if (IsPressed(xJ, yJ, GameScr.imgNut.getWidth(), GameScr.imgNut.getHeight()))
        {
            mScreen.keyTouch = 1004;
            if (GameCanvas.isPointerJustRelease)
            {
                GameCanvas.keyAsciiPress = 'l';
                GameCanvas.clearAllPointerEvent();
            }
            return;
        }
    }
    private static void PaintThongTin(mGraphics g)
    {
        if (ttsp)
        {
            int x = 5;
            int y = GameCanvas.h / 2 - GameCanvas.hh / 2;
            int z = mFont.LEFT;
            string hp = "HP: " + NinjaUtil.getMoneys(Char.myCharz().cHP) + "/" + NinjaUtil.getMoneys(Char.myCharz().cHPFull);
            string ki = "KI: " + NinjaUtil.getMoneys(Char.myCharz().cMP) + "/" + NinjaUtil.getMoneys(Char.myCharz().cMPFull);
            string sd = "SĐ: " + NinjaUtil.getMoneys(Char.myCharz().cDamFull);
            string def = "Giáp: " + NinjaUtil.getMoneys(Char.myCharz().cDefull);
            string sm = "SM: " + NinjaUtil.getMoneys(Char.myCharz().cPower);
            string tn = "TN: " + NinjaUtil.getMoneys(Char.myCharz().cTiemNang);
            mFont mfont = mFont.tahoma_7b_red;
            mfont.drawString(g, "Sư Phụ: " + Char.myCharz().cName, x, y, mFont.LEFT, mFont.tahoma_7b_green2);
            y += 10;
            mfont = mFont.tahoma_7b_yellowL;
            mfont.drawString(g, sm, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, tn, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, hp, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, ki, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, sd, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, def, x, y, z, mFont.tahoma_7b_green2);
        }
        if (ttdt && Char.myCharz().havePet)
        {
            int x = 5;
            int y = GameCanvas.h / 2 - GameCanvas.hh / 2;
            int z = mFont.LEFT;
            string hp;
            string ki;
            string sd;
            string def;
            string sm;
            string tn;
            mFont mfont = mFont.tahoma_7b_red;
            if (GameCanvas.gameTick % 15 == 0)
            {
                Service.gI().petInfo();
            }
            y += 70;
            hp = "HP: " + NinjaUtil.getMoneys(Char.myPetz().cHP) + "/" + NinjaUtil.getMoneys(Char.myPetz().cHPFull);
            ki = "KI: " + NinjaUtil.getMoneys(Char.myPetz().cMP) + "/" + NinjaUtil.getMoneys(Char.myPetz().cMPFull);
            sd = "SĐ: " + NinjaUtil.getMoneys(Char.myPetz().cDamFull);
            def = "Giáp: " + NinjaUtil.getMoneys(Char.myPetz().cDefull);
            sm = "SM: " + NinjaUtil.getMoneys(Char.myPetz().cPower);
            tn = "TN: " + NinjaUtil.getMoneys(Char.myPetz().cTiemNang);
            mfont.drawString(g, "Đệ Tử: " + Char.myPetz().cName, x, y, mFont.LEFT, mFont.tahoma_7b_green2);
            y += 10;
            mfont = mFont.tahoma_7b_yellowL;
            mfont.drawString(g, sm, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, tn, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, hp, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, ki, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, sd, x, y, z, mFont.tahoma_7b_green2);
            y += 10;
            mfont.drawString(g, def, x, y, z, mFont.tahoma_7b_green2);
        }
    }
    public static bool Chat(string str)
    {
        if (str.StartsWith("cheat "))
        {
            int cheat = int.Parse(str.Split(" ")[1]);
            Time.timeScale = cheat;
            addInfo("|0|Cheat " + cheat);
            str = string.Empty;
            return true;
        }
        else if (str.StartsWith("k "))
        {
            int zoneID = int.Parse(str.Split(" ")[1]);
            Service.gI().requestChangeZone(zoneID, -1);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("ttsp"))
        {
            ttsp = !ttsp;
            addInfo("|0|Thông tin sư phụ: ", ttsp);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("ttdt"))
        {
            ttdt = !ttdt;
            addInfo("|0|Thông tin đệ tử: ", ttdt);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("phimtat"))
        {
            phimtat = !phimtat;
            addInfo("|0|Phím Tắt: ", phimtat);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("ttnv"))
        {
            showchars = !showchars;
            addInfo("|0|Thông tin nguời trong map: ", showchars);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("sb"))
        {
            GameCanvas.panel.indexMod[0] = !GameCanvas.panel.indexMod[0];
            HairMod.addInfo("Săn BOSS\n", GameCanvas.panel.indexMod[0]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("btk"))
        {
            GameCanvas.panel.indexMod[1] = !GameCanvas.panel.indexMod[1];
            HairMod.addInfo("BOSS Trong Khu\n", GameCanvas.panel.indexMod[1]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("fcb"))
        {
            GameCanvas.panel.indexMod[2] = !GameCanvas.panel.indexMod[2];
            HairMod.addInfo("Focus BOSS\n", GameCanvas.panel.indexMod[2]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("vutdo"))
        {
            GameCanvas.panel.indexMod[3] = !GameCanvas.panel.indexMod[3];
            HairMod.addInfo("Vứt Đồ\n", GameCanvas.panel.indexMod[3]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("ts"))
        {
            GameCanvas.panel.indexMod[4] = !GameCanvas.panel.indexMod[4];
            HairMod.addInfo("Tàn Sát\n", GameCanvas.panel.indexMod[4]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("nsq"))
        {
            GameCanvas.panel.indexMod[5] = !GameCanvas.panel.indexMod[5];
            HairMod.addInfo("Né Siêu Quái\n", GameCanvas.panel.indexMod[5]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("vdh"))
        {
            GameCanvas.panel.indexMod[6] = !GameCanvas.panel.indexMod[6];
            HairMod.addInfo("Vuợt Địa Hình\n", GameCanvas.panel.indexMod[6]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("adt"))
        {
            GameCanvas.panel.indexMod[7] = !GameCanvas.panel.indexMod[7];
            HairMod.addInfo("Auto Bơm Đậu Cho Đệ\n", GameCanvas.panel.indexMod[7]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("anhat"))
        {
            GameCanvas.panel.indexMod[8] = !GameCanvas.panel.indexMod[8];
            HairMod.addInfo("Auto Nhặt\n", GameCanvas.panel.indexMod[8]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("xindau"))
        {
            GameCanvas.panel.indexMod[9] = !GameCanvas.panel.indexMod[9];
            HairMod.addInfo("Xin Đậu\n", GameCanvas.panel.indexMod[9]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("chodau"))
        {
            GameCanvas.panel.indexMod[10] = !GameCanvas.panel.indexMod[10];
            HairMod.addInfo("Cho Đậu\n", GameCanvas.panel.indexMod[10]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("thudau"))
        {
            GameCanvas.panel.indexMod[11] = !GameCanvas.panel.indexMod[11];
            HairMod.addInfo("Thu Đậu\n", GameCanvas.panel.indexMod[11]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("ak"))
        {
            GameCanvas.panel.indexMod[12] = !GameCanvas.panel.indexMod[12];
            HairMod.addInfo("Tự Đánh\n", GameCanvas.panel.indexMod[12]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("ahs"))
        {
            GameCanvas.panel.indexMod[13] = !GameCanvas.panel.indexMod[13];
            HairMod.addInfo("Tự Động Hồi Sinh\n", GameCanvas.panel.indexMod[13]);
            str = string.Empty;
            return true;
        }
        else if (str.Equals("alogin"))
        {
            GameCanvas.panel.indexMod[14] = !GameCanvas.panel.indexMod[14];
            HairMod.addInfo("Auto Login\n", GameCanvas.panel.indexMod[14]);
            str = string.Empty;
            return true;
        }
        return false;
    }

    public void perform(int idAction, object p)
    {
        switch ((int)p)
        {
            case 0:
                Vector.MenuThongTin();
                break;
            case 1:
                Vector.MenuAutoItem();
                break;
            case 2:
                xoamap = !xoamap;
                break;
            case 3:
                AutoSkil.gI.method_1();
                break;
            case 4:
                GameCanvas.panel.setType(0);
                GameCanvas.panel.show();
                GameCanvas.panel.setTypeMod();
                break;
        }

    }
}
