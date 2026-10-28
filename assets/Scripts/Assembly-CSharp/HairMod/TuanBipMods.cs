using System;

/// <summary>
/// Các tiện ích không trùng với lõi HairMod, chọn lọc từ project TuanBip.
/// </summary>
public sealed class TuanBipMods : IActionListener
{
    private const string AutoTtnlKey = "TuanBip.AutoTTNL";
    private const string AutoGrapeKey = "TuanBip.AutoGrape";
    private const string AutoFlagKey = "TuanBip.AutoFlag";
    private const string AvoidBossKey = "TuanBip.AvoidBoss";

    private static TuanBipMods instance;
    private static bool settingsLoaded;
    private static bool autoTtnl;
    private static bool autoGrape;
    private static bool autoFlag;
    private static bool avoidBoss;
    private static bool lockPosition;
    private static int lockedX;
    private static int lockedY;
    private static int lockedMap = -1;
    private static long lastTtnl;
    private static long lastGrape;
    private static long lastFlag;
    private static long lastAvoidBoss;
    private static long lastLock;

    public static TuanBipMods gI()
    {
        if (instance == null)
        {
            instance = new TuanBipMods();
        }
        return instance;
    }

    public static void ShowMenu()
    {
        LoadSettings();
        MyVector menu = new MyVector();
        menu.addElement(new Command("Auto TTNL Xayda\n" + Status(autoTtnl), gI(), 1, null));
        menu.addElement(new Command("Tự ăn nho\n" + Status(autoGrape), gI(), 2, null));
        menu.addElement(new Command("Khóa vị trí\n" + Status(lockPosition), gI(), 3, null));
        menu.addElement(new Command("Auto cờ đen\n" + Status(autoFlag), gI(), 4, null));
        menu.addElement(new Command("Né boss\n" + Status(avoidBoss), gI(), 5, null));
        menu.addElement(new Command("Dịch chuyển tới mục tiêu", gI(), 6, null));
        menu.addElement(new Command("Dùng Capsule", gI(), 7, null));
        menu.addElement(new Command("Xóa map\n" + Status(HairMod.xoamap), gI(), 8, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    public void perform(int idAction, object p)
    {
        switch (idAction)
        {
            case 1:
                autoTtnl = !autoTtnl;
                Save(AutoTtnlKey, autoTtnl);
                Notify("Auto TTNL Xayda", autoTtnl);
                break;
            case 2:
                autoGrape = !autoGrape;
                Save(AutoGrapeKey, autoGrape);
                Notify("Tự ăn nho", autoGrape);
                break;
            case 3:
                TogglePositionLock();
                break;
            case 4:
                autoFlag = !autoFlag;
                Save(AutoFlagKey, autoFlag);
                Notify("Auto cờ đen", autoFlag);
                break;
            case 5:
                avoidBoss = !avoidBoss;
                Save(AvoidBossKey, avoidBoss);
                Notify("Né boss", avoidBoss);
                break;
            case 6:
                TeleportToTarget();
                break;
            case 7:
                UseCapsule();
                break;
            case 8:
                HairMod.xoamap = !HairMod.xoamap;
                Notify("Xóa map", HairMod.xoamap);
                break;
        }
    }

    public static void Update()
    {
        LoadSettings();
        if (GameCanvas.currentScreen != GameScr.gI() || Char.myCharz() == null)
        {
            return;
        }
        long now = mSystem.currentTimeMillis();
        UpdatePositionLock(now);
        UpdateAutoTtnl(now);
        UpdateAutoGrape(now);
        UpdateAutoFlag(now);
        UpdateAvoidBoss(now);
    }

    private static void UpdateAutoTtnl(long now)
    {
        if (!autoTtnl || now - lastTtnl < 1000L)
        {
            return;
        }
        lastTtnl = now;
        Char me = Char.myCharz();
        if (me.cgender != 2 || me.cHP <= 0 || me.statusMe == 14 || me.stone || me.holdEffID != 0 || me.blindEff || me.sleepEff)
        {
            return;
        }
        Skill recovery = FindSkillByIcon(720);
        Skill transform = FindSkillByIcon(539);
        if ((me.cHP < me.cHPFull || me.cMP < me.cMPFull) && IsReady(recovery))
        {
            GameScr.gI().doSelectSkill(recovery, true);
        }
        else if (transform != null && me.myskill != transform)
        {
            GameScr.gI().doSelectSkill(transform, true);
        }
    }

    private static void UpdateAutoGrape(long now)
    {
        if (!autoGrape || Char.myCharz().cStamina > 5 || now - lastGrape < 1000L)
        {
            return;
        }
        lastGrape = now;
        if (!UseItemByTemplate(212))
        {
            UseItemByTemplate(211);
        }
    }

    private static void UpdatePositionLock(long now)
    {
        if (!lockPosition || now - lastLock < 600L)
        {
            return;
        }
        lastLock = now;
        Char me = Char.myCharz();
        if (TileMap.mapID != lockedMap)
        {
            lockPosition = false;
            Notify("Khóa vị trí", false);
            return;
        }
        if (!Char.isLoadingMap && me.cHP > 0 && me.statusMe != 14)
        {
            me.cx = lockedX;
            me.cy = lockedY;
            Service.gI().charMove();
        }
    }

    private static void UpdateAutoFlag(long now)
    {
        if (!autoFlag || now - lastFlag < 500L)
        {
            return;
        }
        lastFlag = now;
        bool enemyNearby = HasFlaggedEnemyNearby();
        if (!enemyNearby && Char.myCharz().cFlag == 0)
        {
            Service.gI().getFlag(1, 8);
        }
        else if (enemyNearby && Char.myCharz().cFlag == 8)
        {
            Service.gI().getFlag(1, 0);
        }
    }

    private static void UpdateAvoidBoss(long now)
    {
        if (!avoidBoss || now - lastAvoidBoss < 17000L || !HasHostileBoss())
        {
            return;
        }
        lastAvoidBoss = now;
        int[] zones = GameScr.gI().zones;
        if (zones != null && zones.Length > 1)
        {
            int nextIndex = Res.random(0, zones.Length);
            if (nextIndex == TileMap.zoneID)
            {
                nextIndex = (nextIndex + 1) % zones.Length;
            }
            Service.gI().requestChangeZone(nextIndex, -1);
        }
    }

    private static bool HasFlaggedEnemyNearby()
    {
        Char me = Char.myCharz();
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char target = (Char)GameScr.vCharInMap.elementAt(i);
            if (target != null && target.charID > 0 && target.cFlag != 0 && Math.abs(target.cx - me.cx) <= 500 && Math.abs(target.cy - me.cy) <= 500)
            {
                return true;
            }
        }
        return false;
    }

    private static bool HasHostileBoss()
    {
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            Char target = (Char)GameScr.vCharInMap.elementAt(i);
            if (target != null && target.cHP > 0 && target.cTypePk == 5 && !target.isMiniPet && !target.isPet)
            {
                return true;
            }
        }
        return false;
    }

    private static void TogglePositionLock()
    {
        lockPosition = !lockPosition;
        if (lockPosition)
        {
            lockedX = Char.myCharz().cx;
            lockedY = Char.myCharz().cy;
            lockedMap = TileMap.mapID;
        }
        Notify("Khóa vị trí", lockPosition);
    }

    private static void TeleportToTarget()
    {
        Char target = Char.myCharz().charFocus;
        if (target == null)
        {
            GameScr.info1.addInfo("Hãy chọn nhân vật hoặc boss trước", 0);
            return;
        }
        HairMod.TeleportTo(target.cx, target.cy);
    }

    private static void UseCapsule()
    {
        if (!UseItemByTemplate(193) && !UseItemByTemplate(194) && !UseItemByTemplate(2309))
        {
            GameScr.info1.addInfo("Không có Capsule trong hành trang", 0);
        }
    }

    private static bool UseItemByTemplate(int templateId)
    {
        Item[] bag = Char.myCharz().arrItemBag;
        if (bag == null)
        {
            return false;
        }
        for (int i = 0; i < bag.Length; i++)
        {
            Item item = bag[i];
            if (item != null && item.template != null && item.template.id == templateId)
            {
                Service.gI().useItem(0, 1, (sbyte)i, -1);
                return true;
            }
        }
        return false;
    }

    private static Skill FindSkillByIcon(int iconId)
    {
        if (GameScr.keySkill == null)
        {
            return null;
        }
        for (int i = 0; i < GameScr.keySkill.Length; i++)
        {
            Skill skill = GameScr.keySkill[i];
            if (skill != null && skill.template != null && skill.template.iconId == iconId)
            {
                return skill;
            }
        }
        return null;
    }

    private static bool IsReady(Skill skill)
    {
        return skill != null && mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill >= skill.coolDown;
    }

    private static void LoadSettings()
    {
        if (settingsLoaded)
        {
            return;
        }
        settingsLoaded = true;
        autoTtnl = Load(AutoTtnlKey);
        autoGrape = Load(AutoGrapeKey);
        autoFlag = Load(AutoFlagKey);
        avoidBoss = Load(AvoidBossKey);
    }

    private static bool Load(string key)
    {
        return Rms.loadRMSInt(key) == 1;
    }

    private static void Save(string key, bool value)
    {
        Rms.saveRMSInt(key, value ? 1 : 0);
    }

    private static void Notify(string name, bool enabled)
    {
        HairMod.addInfo(name + "\n", enabled);
    }

    private static string Status(bool value)
    {
        return value ? "[ON]" : "[OFF]";
    }
}
