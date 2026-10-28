public sealed class HunrModMenu : IActionListener
{
    private static HunrModMenu instance;
    private static bool displaySettingsLoaded;
    private const string BossDisplayKey = "HunrBossDisplay";
    private const string CharacterDisplayKey = "HunrCharacterDisplay";

    public static HunrModMenu gI()
    {
        if (instance == null)
        {
            instance = new HunrModMenu();
        }
        return instance;
    }

    public static void ShowMain()
    {
        LoadDisplaySettings();
        MyVector menu = new MyVector();
        menu.addElement(new Command("Auto Map", gI(), 1, null));
        menu.addElement(new Command("Auto Skill", gI(), 2, null));
        menu.addElement(new Command("Auto Pean", gI(), 3, null));
        menu.addElement(new Command("Auto Pick", gI(), 4, null));
        menu.addElement(new Command("Auto Train", gI(), 5, null));
        menu.addElement(new Command("More", gI(), 6, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    private static void ShowMore()
    {
        MyVector menu = new MyVector();
        menu.addElement(new Command("Đồ Họa", gI(), 7, null));
        menu.addElement(new Command("Auto Chat", gI(), 8, null));
        menu.addElement(new Command("Tính năng TiPi", gI(), 9, null));
        menu.addElement(new Command("TuanBip Mods", gI(), 21, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    private static void ShowGraphics()
    {
        LoadDisplaySettings();
        MyVector menu = new MyVector();
        menu.addElement(new Command("Thông Báo\nBoss\n" + Status(GameCanvas.panel.indexMod[0]), gI(), 13, null));
        menu.addElement(new Command("Danh Sách\nNhân Vật\nTrong Khu\n" + Status(HairMod.showchars), gI(), 14, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    private static void ShowAutoChat()
    {
        MyVector menu = new MyVector();
        menu.addElement(new Command("Auto Chat Map\n" + Status(HunrAutoChat.MapEnabled), gI(), 16, null));
        menu.addElement(new Command("Auto Chat Thế Giới\n" + Status(HunrAutoChat.GlobalEnabled), gI(), 17, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    private static void ShowTipiFeatures()
    {
        MyVector menu = new MyVector();
        menu.addElement(new Command("Auto Item", gI(), 10, null));
        menu.addElement(new Command("Auto Hồi Sinh\n" + Status(GameCanvas.panel.indexMod[13]), gI(), 11, null));
        menu.addElement(new Command("Auto Buff Đậu\n" + Status(GameCanvas.panel.indexMod[7]), gI(), 12, null));
        menu.addElement(new Command("Auto Focus Boss\n" + Status(GameCanvas.panel.indexMod[2]), gI(), 15, null));
        menu.addElement(new Command("Tự Đánh VIP\n[Không tương thích]", gI(), 18, null));
        menu.addElement(new Command("Auto Nhặt NRSD\n[Không có logic]", gI(), 19, null));
        menu.addElement(new Command("MOD Project-int", gI(), 20, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    private static void ShowAutoMap()
    {
        MyVector menu = new MyVector();
        menu.addElement(new Command("Qua trái [J]", gI(), 30, null));
        menu.addElement(new Command("Qua giữa [L]", gI(), 31, null));
        menu.addElement(new Command("Qua phải [K]", gI(), 32, null));
        menu.addElement(new Command("Danh sách Map", gI(), 33, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    public static void ShowAutoTrain()
    {
        MyVector menu = new MyVector();
        menu.addElement(new Command("Tàn Sát\n" + Status(GameCanvas.panel.indexMod[4]), gI(), 40, null));
        menu.addElement(new Command("Né Siêu Quái\n" + Status(GameCanvas.panel.indexMod[5]), gI(), 41, null));
        menu.addElement(new Command("Vượt Địa Hình\n" + Status(GameCanvas.panel.indexMod[6]), gI(), 42, null));
        menu.addElement(new Command("Auto Nhặt Khi Train\n" + Status(TanSatHandler.IsAutoPickItems), gI(), 43, null));
        menu.addElement(new Command("Cấu hình MOD Project-int", gI(), 44, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    public void perform(int idAction, object p)
    {
        switch (idAction)
        {
            case 1:
                ShowAutoMap();
                break;
            case 2:
                AutoSkil.gI.method_1();
                break;
            case 3:
                AutoPean.gclass10_0.method_1();
                break;
            case 4:
                AutoPick.gclass11_0.method_1();
                break;
            case 5:
                ShowAutoTrain();
                break;
            case 6:
                ShowMore();
                break;
            case 7:
                ShowGraphics();
                break;
            case 8:
                ShowAutoChat();
                break;
            case 9:
                ShowTipiFeatures();
                break;
            case 10:
                Vector.MenuAutoItem();
                break;
            case 11:
                TogglePanelMod(13, "Auto Hồi Sinh");
                break;
            case 12:
                TogglePanelMod(7, "Auto Buff Đậu");
                break;
            case 13:
                TogglePanelMod(0, "Thông báo BOSS");
                Rms.saveRMSInt(BossDisplayKey, GameCanvas.panel.indexMod[0] ? 1 : 0);
                break;
            case 14:
                HairMod.showchars = !HairMod.showchars;
                Rms.saveRMSInt(CharacterDisplayKey, HairMod.showchars ? 1 : 0);
                HairMod.addInfo("Nhân vật trong Map\n", HairMod.showchars);
                break;
            case 15:
                TogglePanelMod(2, "Auto Focus Boss");
                break;
            case 16:
                HunrAutoChat.ShowMapMenu();
                break;
            case 17:
                HunrAutoChat.ShowGlobalMenu();
                break;
            case 18:
                GameScr.info1.addInfo("Tự Đánh VIP HUNR không tương thích với bộ Auto Skill hiện tại", 0);
                break;
            case 19:
                GameScr.info1.addInfo("HUNR chỉ có cờ Auto Nhặt NRSD, không có đường thực thi", 0);
                break;
            case 20:
                HairMod.ShowLegacyMenu();
                break;
            case 21:
                TuanBipMods.ShowMenu();
                break;
            case 30:
                Pk9rXmap.LoadMap(0);
                break;
            case 31:
                Pk9rXmap.LoadMap(1);
                break;
            case 32:
                Pk9rXmap.LoadMap(2);
                break;
            case 33:
                Pk9rXmap.ShowPanelMapTrans();
                break;
            case 40:
                TogglePanelMod(4, "Tàn Sát");
                break;
            case 41:
                TogglePanelMod(5, "Né Siêu Quái");
                break;
            case 42:
                TogglePanelMod(6, "Vượt Địa Hình");
                break;
            case 43:
                TanSatHandler.IsAutoPickItems = !TanSatHandler.IsAutoPickItems;
                HairMod.addInfo("Auto Nhặt Khi Train\n", TanSatHandler.IsAutoPickItems);
                break;
            case 44:
                ShowProjectModPanel();
                break;
        }
    }

    public static void ShowProjectModPanel()
    {
        GameCanvas.panel.setType(0);
        GameCanvas.panel.show();
        GameCanvas.panel.setTypeMod();
    }

    private static string Status(bool value)
    {
        return value ? "[ON]" : "[OFF]";
    }

    private static void TogglePanelMod(int index, string name)
    {
        GameCanvas.panel.indexMod[index] = !GameCanvas.panel.indexMod[index];
        HairMod.addInfo(name + "\n", GameCanvas.panel.indexMod[index]);
    }

    public static void LoadDisplaySettings()
    {
        if (displaySettingsLoaded)
        {
            return;
        }
        displaySettingsLoaded = true;
        int bossDisplay = Rms.loadRMSInt(BossDisplayKey);
        if (bossDisplay != -1)
        {
            GameCanvas.panel.indexMod[0] = bossDisplay == 1;
        }
        int characterDisplay = Rms.loadRMSInt(CharacterDisplayKey);
        if (characterDisplay != -1)
        {
            HairMod.showchars = characterDisplay == 1;
        }
    }
}
