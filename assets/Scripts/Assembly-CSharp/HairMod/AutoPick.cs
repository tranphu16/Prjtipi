using System;
using System.Collections.Generic;

// Token: 0x02000011 RID: 17
public class AutoPick : IActionListener, IChatable
{
	// Token: 0x06000081 RID: 129 RVA: 0x000122A8 File Offset: 0x000104A8
	public AutoPick()
	{
		this.int_0 = 50;
		this.list_0 = new List<int>();
		this.string_0 = new string[]
		{
			"Nhập khoảng cách nhặt",
			"khoảng cách (>50)"
		};
		this.string_1 = new string[]
		{
			"Nhập ID của item",
			"ID"
		};
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00012308 File Offset: 0x00010508
	public void method_0()
	{
		if ((GameScr.isAutoPlay && (GameScr.canAutoPlay )) || !this.bool_0)
		{
			return;
		}
		if (this.method_8(TileMap.mapID))
		{
			this.method_10();
		}
		if (!this.method_8(TileMap.mapID))
		{
			this.method_4();
			if (global::Char.myCharz().itemFocus != null)
			{
				this.method_5();
			}
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0001239C File Offset: 0x0001059C
	public void onChatFromMe(string text, string to)
	{
		ChatTextField chatTextField = ChatTextField.gI();
		if (chatTextField.tfChat.getText() == null || chatTextField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
		{
			chatTextField.isShow = false;
			return;
		}
		if (chatTextField.strChat.Equals(this.string_0[0]))
		{
			try
			{
				int num = this.int_0 = int.Parse(chatTextField.tfChat.getText());
				GameScr.info1.addInfo("Khoảng Cách Nhặt: " + num.ToString(), 0);
			}
			catch
			{
				GameScr.info1.addInfo("Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
			}
			HairMod.ResetTF();
			return;
		}
		if (chatTextField.strChat.Equals(this.string_1[0]))
		{
			try
			{
				int item = int.Parse(chatTextField.tfChat.getText());
				this.list_0.Add(item);
				GameScr.info1.addInfo("Đã Thêm Item " + item.ToString(), 0);
			}
			catch
			{
				GameScr.info1.addInfo("Số Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
			}
            HairMod.ResetTF();
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00007FFA File Offset: 0x000061FA
	public void onCancelChat()
	{
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00012514 File Offset: 0x00010714
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
			case 1:
				this.bool_0 = !this.bool_0;
				this.int_1 = 0;
				GameScr.info1.addInfo("Auto Nhặt\n" + (this.bool_0 ? "[ON]" : "[OFF]"), 0);
				return;
			case 2:
				GameCanvas.panel.indexMod[8] = !GameCanvas.panel.indexMod[8];
				GameScr.info1.addInfo("Nhặt Tất Cả\n" + (GameCanvas.panel.indexMod[8] ? "[ON]" : "[OFF]"), 0);
				return;
			case 3:
				this.bool_0 = !this.bool_0;
				this.int_1 = 1;
				GameScr.info1.addInfo("Nhặt Theo Danh Sách\n" + (this.bool_0 ? "[ON]" : "[OFF]"), 0);
				return;
			case 4:
				this.bool_1 = !this.bool_1;
				GameScr.info1.addInfo("Dịch Đến Item\n" + (this.bool_1 ? "[ON]" : "[OFF]"), 0);
				return;
			case 5:
				HairMod.InputTF(this, this.string_0[0], this.string_0[1], TField.INPUT_TYPE_NUMERIC);
				return;
			case 6:
				if (this.list_0.Count == 0)
				{
					GameScr.info1.addInfo("Danh Sách Trống!", 0);
				}
				if (this.list_0.Count > 0)
				{
					string text = "";
					for (int i = 0; i < this.list_0.Count; i++)
					{
						text = text + this.list_0[i].ToString() + " ";
					}
					GameScr.info1.addInfo(text, 0);
					return;
				}
				break;
			case 7:
				this.list_0.Clear();
				GameScr.info1.addInfo("Đã Clear Danh Sách Nhặt!", 0);
				return;
			case 8:
				HairMod.InputTF(this, this.string_1[0], this.string_1[1], TField.INPUT_TYPE_NUMERIC);
				return;
			case 9:
				this.list_0.Add((int)global::Char.myCharz().itemFocus.template.id);
				GameScr.info1.addInfo(string.Concat(new string[]
				{
				"Đã thêm ",
				global::Char.myCharz().itemFocus.template.name,
				" [",
				global::Char.myCharz().itemFocus.template.id.ToString(),
				"]"
				}), 0);
				return;
			case 10:
				this.method_12();
				break;
			default:
				return;
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x000127C0 File Offset: 0x000109C0
	public void method_1()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Auto Nhặt\n" + ((!this.bool_0 || this.int_1 != 0) ? "[OFF]" : "[ON]"), this, 1, null));
		myVector.addElement(new Command("Nhặt Tất Cả\n" + (GameCanvas.panel.indexMod[8] ? "[ON]" : "[OFF]"), this, 2, null));
		MyVector myVector2 = myVector;
		string str = "Nhặt Theo Danh Sách\n";
		string str2;
		if (this.bool_0)
		{
			if (this.int_1 == 1)
			{
				str2 = "[ON]";
				goto IL_A3;
			}
		}
		str2 = "[OFF]";
		IL_A3:
		myVector2.addElement(new Command(str + str2, this, 3, null));
		myVector.addElement(new Command("Dịch Đến Item\n" + (this.bool_1 ? "[ON]" : "[OFF]"), this, 4, null));
		myVector.addElement(new Command("Khoảng Cách Nhặt\n[" + this.int_0.ToString() + "]", this, 5, null));
		myVector.addElement(new Command("Xem Danh Sách Nhặt", this, 6, null));
		myVector.addElement(new Command("Clear Danh Sách Nhặt", this, 7, null));
		myVector.addElement(new Command("Xem danh sách ItemID", this, 10, null));
		myVector.addElement(new Command("Thêm ItemID", this, 8, null));
		if (global::Char.myCharz().itemFocus != null)
		{
			myVector.addElement(new Command(string.Concat(new string[]
			{
				"Thêm: ",
				global::Char.myCharz().itemFocus.template.name,
				" [",
				global::Char.myCharz().itemFocus.template.id.ToString(),
				"] "
			}), this, 9, null));
		}
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00007FFA File Offset: 0x000061FA
	private void method_2()
	{
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00007FFA File Offset: 0x000061FA
	private void method_3()
	{
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000129AC File Offset: 0x00010BAC
	public void method_4()
	{
		if (global::Char.myCharz().itemFocus != null)
		{
			return;
		}
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
			int num = global::Res.abs(global::Char.myCharz().cx - itemMap.x);
			int num2 = global::Res.abs(global::Char.myCharz().cy - itemMap.y);
			if (num <= this.int_0 && num2 <= this.int_0 && this.method_7(itemMap) && itemMap.template.id != 673)
			{
				global::Char.myCharz().itemFocus = itemMap;
				return;
			}
		}
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00012A64 File Offset: 0x00010C64
	public void method_5()
	{
		if (mSystem.currentTimeMillis() - this.long_0 < 550L || global::Char.myCharz().itemFocus == null)
		{
			return;
		}
		if (this.bool_1 && !global::Char.isLockKey)
		{
			this.method_6(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			if (global::Char.myCharz().itemFocus.template.id != 673)
			{
				Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
				this.long_0 = mSystem.currentTimeMillis();
			}
			return;
		}
		if (global::Char.myCharz().cx < global::Char.myCharz().itemFocus.x)
		{
			global::Char.myCharz().cdir = 1;
		}
		else
		{
			global::Char.myCharz().cdir = -1;
		}
		int num = global::Res.abs(global::Char.myCharz().cx - global::Char.myCharz().itemFocus.x);
		int num2 = global::Res.abs(global::Char.myCharz().cy - global::Char.myCharz().itemFocus.y);
		if (num <= 40 && num2 < 40)
		{
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			if (global::Char.myCharz().itemFocus.template.id != 673)
			{
				Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
				this.long_0 = mSystem.currentTimeMillis();
				return;
			}
		}
		else
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
			global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00012C40 File Offset: 0x00010E40
	private void method_6(int int_2, int int_3)
	{
		global::Char.myCharz().cx = int_2;
		global::Char.myCharz().cy = int_3;
		Service.gI().charMove();
		global::Char.myCharz().cx = int_2;
		global::Char.myCharz().cy = int_3 + 1;
		Service.gI().charMove();
		global::Char.myCharz().cx = int_2;
		global::Char.myCharz().cy = int_3;
		Service.gI().charMove();
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00012CB0 File Offset: 0x00010EB0
	private bool method_7(ItemMap gclass82_0)
	{
		if (GameCanvas.panel.indexMod[8])
		{
			return true;
		}
		if (this.int_1 == 0)
		{
			return gclass82_0.playerId == global::Char.myCharz().charID || gclass82_0.playerId == -1;
		}
		return this.int_1 == 1 && this.list_0.Contains((int)gclass82_0.template.id) && (gclass82_0.playerId == global::Char.myCharz().charID || gclass82_0.playerId == -1);
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00008123 File Offset: 0x00006323
	private bool method_8(int int_2)
	{
		return int_2 >= 85 && int_2 <= 91;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00008134 File Offset: 0x00006334
	private bool method_9(ItemMap gclass82_0)
	{
		return gclass82_0.template.id >= 372 && gclass82_0.template.id <= 378;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00012D40 File Offset: 0x00010F40
	private void method_10()
	{
		try
		{
			if (global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().cHP > 0)
			{
				for (int i = 0; i < GameScr.vItemMap.size(); i++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
					if (itemMap != null && this.method_9(itemMap))
					{
						if (global::Res.abs(global::Char.myCharz().cx - itemMap.x) <= 60 && global::Res.abs(global::Char.myCharz().cy - itemMap.y) <= 60)
						{
							if (mSystem.currentTimeMillis() - this.long_0 > 550L)
							{
								Service.gI().pickItem(itemMap.itemMapID);
								this.long_0 = mSystem.currentTimeMillis();
							}
						}
						else if (!global::Char.isLockKey && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && mSystem.currentTimeMillis() - this.long_1 > 100L)
						{
							this.long_1 = mSystem.currentTimeMillis();
							this.method_6(itemMap.x, itemMap.y);
						}
						break;
					}
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00012EA8 File Offset: 0x000110A8
	public void method_11(ItemMap itemMap)
	{
		if (!this.bool_0)
		{
			return;
		}
		if (!this.method_8(TileMap.mapID))
		{
			return;
		}
		if (!this.method_9(itemMap))
		{
			return;
		}
		if (global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().cHP > 0)
		{
			if ((global::Res.abs(global::Char.myCharz().cx - itemMap.x) > 60 || global::Res.abs(global::Char.myCharz().cy - itemMap.y) > 60) && !global::Char.isLockKey && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5)
			{
				this.long_1 = mSystem.currentTimeMillis();
				this.method_6(itemMap.x, itemMap.y);
			}
			Service.gI().pickItem(itemMap.itemMapID);
			return;
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00012FA4 File Offset: 0x000111A4
	private void method_12()
	{
		try
		{
			string text = "";
			short num = 0;
			while ((int)num < ItemTemplates.itemTemplates.size())
			{
				try
				{
					text = string.Concat(new string[]
					{
						text,
						num.ToString(),
						"._",
						ItemTemplates.get(num).name.Replace(" ", "_"),
						" "
					});
				}
				catch
				{
				}
				num += 1;
			}
			text = text.Trim().Replace(" ", "\n").Replace("_", " ");
			if (text != "")
			{
				ChatPopup.addBigMessage(text, 100000, new Npc(-1, 0, 0, 0, 0, 0));
				ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
				ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
				ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00008279 File Offset: 0x00006479
	public bool method_13()
	{
		return this.bool_0;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00008281 File Offset: 0x00006481
	public void method_14(bool isAutoPick)
	{
		this.bool_0 = isAutoPick;
	}

	// Token: 0x04000096 RID: 150
	public static AutoPick gclass11_0 = new AutoPick();

	// Token: 0x04000097 RID: 151
	private bool bool_0;

	// Token: 0x04000098 RID: 152
	private long long_0;

	// Token: 0x04000099 RID: 153
	private int int_0;

	// Token: 0x0400009A RID: 154
	private bool bool_1;

	// Token: 0x0400009B RID: 155
	private bool bool_2;

	// Token: 0x0400009C RID: 156
	private int int_1;

	// Token: 0x0400009D RID: 157
	private List<int> list_0;

	// Token: 0x0400009E RID: 158
	private string[] string_0;

	// Token: 0x0400009F RID: 159
	private string[] string_1;

	// Token: 0x040000A0 RID: 160
	private long long_1;
}
