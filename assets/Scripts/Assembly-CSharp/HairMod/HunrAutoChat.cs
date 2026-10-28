using System;

public sealed class HunrAutoChat : IActionListener, IChatable
{
    private const long MinimumMapDelay = 1000L;
    private const long MinimumGlobalDelay = 60000L;

    private const string MapTextKey = "HunrAutoChatMapText";
    private const string GlobalTextKey = "HunrAutoChatGlobalText";
    private const string MapDelayKey = "HunrAutoChatMapDelay";
    private const string GlobalDelayKey = "HunrAutoChatGlobalDelay";
    private const string MapEnabledKey = "HunrAutoChatMapEnabled";
    private const string GlobalEnabledKey = "HunrAutoChatGlobalEnabled";

    private const string MapTextPrompt = "Nhập nội dung chat Map";
    private const string GlobalTextPrompt = "Nhập nội dung chat thế giới";
    private const string MapDelayPrompt = "Thời gian auto chat map(ms)";
    private const string GlobalDelayPrompt = "Thời gian auto chat thế giới(ms)";

    private static HunrAutoChat instance;
    private static bool loaded;
    private static string mapText = string.Empty;
    private static string globalText = string.Empty;
    private static long mapDelay = 5000L;
    private static long globalDelay = 60000L;
    private static long lastMapChat;
    private static long lastGlobalChat;
    private static bool mapEnabled;
    private static bool globalEnabled;

    public static HunrAutoChat gI()
    {
        if (instance == null)
        {
            instance = new HunrAutoChat();
        }
        EnsureLoaded();
        return instance;
    }

    public static bool MapEnabled
    {
        get { EnsureLoaded(); return mapEnabled; }
    }

    public static bool GlobalEnabled
    {
        get { EnsureLoaded(); return globalEnabled; }
    }

    public static void Update()
    {
        EnsureLoaded();
        if (!Session_ME.connected)
        {
            return;
        }

        long now = mSystem.currentTimeMillis();
        if (mapEnabled && mapText.Length > 0 && now - lastMapChat >= mapDelay)
        {
            lastMapChat = now;
            Service.gI().chat(mapText);
        }
        if (globalEnabled && globalText.Length > 0 && now - lastGlobalChat >= globalDelay)
        {
            lastGlobalChat = now;
            Service.gI().chatGlobal(globalText);
        }
    }

    public static void ShowMapMenu()
    {
        EnsureLoaded();
        MyVector menu = new MyVector();
        menu.addElement(new Command("Trạng thái\n[" + (mapEnabled ? "ON" : "OFF") + "]", gI(), 1, null));
        menu.addElement(new Command("Nội dung chat", gI(), 2, null));
        menu.addElement(new Command("Thời gian chat\n[" + mapDelay + "ms]", gI(), 3, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    public static void ShowGlobalMenu()
    {
        EnsureLoaded();
        MyVector menu = new MyVector();
        menu.addElement(new Command("Trạng thái\n[" + (globalEnabled ? "ON" : "OFF") + "]", gI(), 4, null));
        menu.addElement(new Command("Nội dung chat", gI(), 5, null));
        menu.addElement(new Command("Thời gian chat\n[" + globalDelay + "ms]", gI(), 6, null));
        GameCanvas.menu.startAt(menu, 0);
    }

    public void perform(int idAction, object p)
    {
        switch (idAction)
        {
            case 1:
                mapEnabled = !mapEnabled;
                Save();
                Info("Auto Chat Map", mapEnabled);
                break;
            case 2:
                OpenInput(MapTextPrompt, "Nội dung", mapText, TField.INPUT_TYPE_ANY);
                break;
            case 3:
                OpenInput(MapDelayPrompt, "Thời gian(ms)", mapDelay.ToString(), TField.INPUT_TYPE_NUMERIC);
                break;
            case 4:
                globalEnabled = !globalEnabled;
                Save();
                Info("Auto Chat Thế Giới", globalEnabled);
                break;
            case 5:
                OpenInput(GlobalTextPrompt, "Nội dung", globalText, TField.INPUT_TYPE_ANY);
                break;
            case 6:
                OpenInput(GlobalDelayPrompt, "Thời gian(ms)", globalDelay.ToString(), TField.INPUT_TYPE_NUMERIC);
                break;
        }
    }

    public void onChatFromMe(string text, string to)
    {
        ChatTextField field = ChatTextField.gI();
        string value = field.tfChat.getText();
        string prompt = field.strChat;
        try
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(prompt))
            {
                return;
            }
            if (prompt.Equals(MapTextPrompt))
            {
                mapText = value;
                GameScr.info1.addInfo("Nội dung Auto Chat Map: " + mapText, 0);
            }
            else if (prompt.Equals(GlobalTextPrompt))
            {
                globalText = value;
                GameScr.info1.addInfo("Nội dung Auto Chat Thế Giới: " + globalText, 0);
            }
            else if (prompt.Equals(MapDelayPrompt))
            {
                mapDelay = ParseDelay(value, MinimumMapDelay, "Auto Chat Map");
            }
            else if (prompt.Equals(GlobalDelayPrompt))
            {
                globalDelay = ParseDelay(value, MinimumGlobalDelay, "Auto Chat Thế Giới");
            }
            Save();
        }
        catch (Exception)
        {
            GameScr.info1.addInfo("Giá trị Auto Chat không hợp lệ", 0);
        }
        finally
        {
            HairMod.ResetTF();
        }
    }

    public void onCancelChat()
    {
        HairMod.ResetTF();
    }

    private static long ParseDelay(string value, long minimum, string label)
    {
        long delay;
        if (!long.TryParse(value, out delay) || delay < minimum)
        {
            GameScr.info1.addInfo(label + " tối thiểu " + minimum + "ms", 0);
            return minimum;
        }
        GameScr.info1.addInfo("Thời gian " + label + ": " + delay + "ms", 0);
        return delay;
    }

    private static void OpenInput(string prompt, string label, string value, int inputType)
    {
        HairMod.InputTF(gI(), prompt, label, inputType);
        ChatTextField.gI().tfChat.setText(value);
    }

    private static void Info(string name, bool enabled)
    {
        GameScr.info1.addInfo(name + "\n[" + (enabled ? "ON" : "OFF") + "]", 0);
    }

    private static void EnsureLoaded()
    {
        if (loaded)
        {
            return;
        }
        loaded = true;
        mapText = Rms.loadRMSString(MapTextKey) ?? string.Empty;
        globalText = Rms.loadRMSString(GlobalTextKey) ?? string.Empty;
        mapDelay = LoadDelay(MapDelayKey, 5000L, MinimumMapDelay);
        globalDelay = LoadDelay(GlobalDelayKey, 60000L, MinimumGlobalDelay);
        mapEnabled = Rms.loadRMSInt(MapEnabledKey) == 1;
        globalEnabled = Rms.loadRMSInt(GlobalEnabledKey) == 1;
    }

    private static long LoadDelay(string key, long defaultValue, long minimum)
    {
        long value;
        string saved = Rms.loadRMSString(key);
        if (!long.TryParse(saved, out value) || value < minimum)
        {
            return defaultValue;
        }
        return value;
    }

    private static void Save()
    {
        Rms.saveRMSString(MapTextKey, mapText);
        Rms.saveRMSString(GlobalTextKey, globalText);
        Rms.saveRMSString(MapDelayKey, mapDelay.ToString());
        Rms.saveRMSString(GlobalDelayKey, globalDelay.ToString());
        Rms.saveRMSInt(MapEnabledKey, mapEnabled ? 1 : 0);
        Rms.saveRMSInt(GlobalEnabledKey, globalEnabled ? 1 : 0);
    }
}
