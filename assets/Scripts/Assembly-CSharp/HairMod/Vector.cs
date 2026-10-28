using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public class Vector : HairMod, IActionListener
{
    private static Vector instance;
    public static List<string> listItems;
    public static bool[] useItem = new bool[10];
    public const long TIME_USE_ITEM = 600000L;
    public const long TIME_USE_CSKB = 1000L;
    public static Vector gI()
    {
        if (instance == null)
        {
            instance = new Vector();
        }
        return instance;
    }
    public Vector()
    {

    }
    public static void addStatus(string s)
    {
        GameScr.info1.addInfo(s, 0);
    }
    public static void Updates()
    {
        try
        {
            string itemName;
            if (useItem[0])
            {
                itemName = "bổ huyết";
                Item item = FindItemName(itemName);
                if (item != null && !object.Equals(item, FindItemNameExactly("bổ huyết 2")))
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có bổ huyết");
                    useItem[0] = false;
                }
            }
            else if (useItem[1])
            {
                itemName = "cuồng nộ";
                Item item = FindItemName(itemName);
                if (item != null && !object.Equals(item, FindItemNameExactly("cuồng nộ 2")))
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có cuồng nộ");
                    useItem[1] = false;
                }
            }
            else if (useItem[2])
            {
                itemName = "giáp xên";
                Item item = FindItemName(itemName);
                if (item != null && !object.Equals(item, FindItemNameExactly("giáp xên 2")))
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có giáp xên");
                    useItem[2] = false;
                }
            }
            else if (useItem[3])
            {
                itemName = "bổ khí";
                Item item = FindItemName(itemName);
                if (item != null && !object.Equals(item, FindItemNameExactly("bổ khí 2")))
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có bổ khí");
                    useItem[3] = false;
                }
            }
            else if (useItem[4])
            {
                if (GameScr.gI().isBagFull())
                {
                    useItem[4] = false;
                    return;
                }
                itemName = "viên capsule kì bí";
                Item item = FindItemName(itemName);
                if (item != null)
                {
                    useItemWithTime(item.template.id, TIME_USE_CSKB);
                }
                else
                {
                    addStatus("Không có capsule kì bí");
                    useItem[4] = false;
                }
            }
            else if (useItem[5])
            {
                itemName = "khẩu trang";
                Item item = FindItemName(itemName);
                if (item != null)
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có khẩu trang");
                    useItem[5] = false;
                }
            }
            else if (useItem[6])
            {
                itemName = "bổ huyết 2";
                Item item = FindItemNameExactly(itemName);
                if (item != null)
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có bổ huyết 2");
                    useItem[6] = false;
                }
            }
            else if (useItem[7])
            {
                itemName = "cuồng nộ 2";
                Item item = FindItemNameExactly(itemName);
                if (item != null)
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có cuồng nộ 2");
                    useItem[7] = false;
                }
            }
            else if (useItem[8])
            {
                itemName = "giáp xên 2";
                Item item = FindItemNameExactly(itemName);
                if (item != null)
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có giáp xên 2");
                    useItem[8] = false;
                }
            }
            else if (useItem[9])
            {
                itemName = "bổ khí 2";
                Item item = FindItemNameExactly(itemName);
                if (item != null)
                {
                    useItemWithTime(item.template.id, TIME_USE_ITEM);
                }
                else
                {
                    addStatus("Không có bổ khí 2");
                    useItem[9] = false;
                }
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
    }
    public static string getStatusMenuInfo(bool a)
    {
        return a ? "\n[ON]" : "\n[OFF]";
    }
    public static void MenuThongTin()
    {
        MyVector myVector = new MyVector();
        myVector.addElement(new Command("Sư Phụ" + getStatusMenuInfo(ttsp), gI(), 1, 0));
        myVector.addElement(new Command("Đệ Tử" + getStatusMenuInfo(ttdt), gI(), 1, 1));
        myVector.addElement(new Command("Người Khác" + getStatusMenuInfo(showchars), gI(), 1, 2));
        GameCanvas.menu.startAt(myVector, 0);
    }
    public static void MenuAutoItem()
    {
        if (listItems == null || listItems.Count == 0)
        {
            listItems = new List<string>()
        {
            "Bổ Huyết",
            "Cuồng Nộ",
            "Giáp Xên",
            "Bổ Khí",
            "Capsule Kì Bí",
            "Khẩu Trang",
            "Bổ Huyết C2",
            "Cuồng Nộ C2",
            "Giáp Xên C2",
            "Bổ Khí C2",
        };
            return;
        }
        MyVector myVector = new MyVector();
        for (int i = 0; i < listItems.Count; i++)
        {
            myVector.addElement(new Command(listItems[i] + getStatusMenuInfo(useItem[i]), gI(), 2, i));
        }
        GameCanvas.menu.startAt(myVector, 1);
    }
    public void perform(int idAction, object p)
    {
        try
        {
            switch (idAction)
            {
                case 1:
                    switch ((int)p)
                    {
                        case 0:
                            ttsp = !ttsp;
                            addInfo("|0|Thông tin sư phụ: ", ttsp);
                            break;
                        case 1:
                            ttdt = !ttdt;
                            addInfo("|0|Thông tin đệ tử: ", ttdt);
                            break;
                        case 2:
                            showchars = !showchars;
                            addInfo("|0|Thông tin nguời trong map: ", showchars);
                            break;
                    }
                    break;
                case 2:
                    switch ((int)p)
                    {
                        case 0:
                            useItem[0] = !useItem[0];
                            if (useItem[0])
                            {
                                addStatus("Đã auto bổ huyết: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt auto bổ huyết");
                            }
                            break;
                        case 1:
                            useItem[1] = !useItem[1];
                            if (useItem[1])
                            {
                                addStatus("Đã auto cuồng nộ: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt auto cuồng nộ");
                            }
                            break;
                        case 2:
                            useItem[2] = !useItem[2];
                            if (useItem[2])
                            {
                                addStatus("Đã auto giáp xên: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt auto giáp xên");
                            }
                            break;
                        case 3:
                            useItem[3] = !useItem[3];
                            if (useItem[3])
                            {
                                addStatus("Đã auto bổ khí: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt auto bổ khí");
                            }
                            break;
                        case 4:
                            useItem[4] = !useItem[4];
                            if (useItem[4])
                            {
                                addStatus("Đã auto cskb: " + (TIME_USE_CSKB / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt auto cskb");
                            }
                            break;
                        case 5:
                            useItem[5] = !useItem[5];
                            if (useItem[5])
                            {
                                addStatus("Đã auto khẩu trang: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt khẩu trang");
                            }
                            break;
                        case 6:
                            useItem[6] = !useItem[6];
                            if (useItem[6])
                            {
                                addStatus("Đã auto bổ huyết 2: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt bổ huyết 2");
                            }
                            break;
                        case 7:
                            useItem[7] = !useItem[7];
                            if (useItem[7])
                            {
                                addStatus("Đã auto cuồng nộ 2: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt cuồng nộ 2");
                            }
                            break;
                        case 8:
                            useItem[8] = !useItem[8];
                            if (useItem[8])
                            {
                                addStatus("Đã auto giáp xên 2: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt giáp xên 2");
                            }
                            break;
                        case 9:
                            useItem[9] = !useItem[9];
                            if (useItem[9])
                            {
                                addStatus("Đã auto bổ khí 2: " + (TIME_USE_ITEM / 1000) + "s");
                            }
                            else
                            {
                                addStatus("Đã tắt bổ khí 2");
                            }
                            break;
                    }
                    break;
            }

        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
    }
}