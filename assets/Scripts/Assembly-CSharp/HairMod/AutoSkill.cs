using System;
using System.Collections.Generic;
using System.Threading;

// Token: 0x02000015 RID: 21
public class AutoSkil : IChatable, IActionListener
{
	// Token: 0x06000110 RID: 272 RVA: 0x0001A3F0 File Offset: 0x000185F0
	public AutoSkil()
	{
		this.bool_0 = true;
		this.long_0 = new long[10];
		this.bool_4 = new bool[10];
		this.long_1 = new long[10];
		this.long_2 = new long[10];
		this.listMucTieu = new List<global::Char>();
		this.string_0 = new string[]
		{
			"Nhập delay",
			"mili giây"
		};
		this.inputHpBomb = new string[]
		{
			"Nhập HP BomB",
			"HP"
		};
		this.method_3();
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0001A488 File Offset: 0x00018688
	public void method_0()
	{
		if (GameCanvas.panel.indexMod[12])
		{
			this.method_6();
		}
		if (this.bool_6)
		{
			this.method_17();
		}
		if (this.HP_LIMIT > 0)
		{
			this.method_19();
		}
		if (this.bool_2)
		{
			this.method_7();
		}
		if (!global::Char.myCharz().meDead)
		{
			for (int i = 0; i < GameScr.keySkill.Length; i++)
			{
				if (this.bool_4[i])
				{
					this.method_8(i);
				}
			}
		}
		if (this.bool_0 && GameCanvas.gameTick % 20 == 0)
		{
			this.bool_0 = false;
			this.method_5();
		}
		if (this.DoiMucTieu)
		{
			this.method_15();
		}
	}

	// Token: 0x06000112 RID: 274
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
				long num = long.Parse(chatTextField.tfChat.getText());
				this.long_2[this.int_0] = num;
				this.bool_4[this.int_0] = true;
				GameScr.info1.addInfo(string.Concat(new string[]
				{
					"Auto ",
					GameScr.keySkill[this.int_0].template.name,
					": ",
					NinjaUtil.getMoneys(num),
					" mili giây"
				}), 0);
			}
			catch
			{
				GameScr.info1.addInfo("Vui Lòng Nhập Lại Delay!", 0);
			}
			HairMod.ResetTF();
			return;
		}
		if (chatTextField.strChat.Equals(this.inputHpBomb[0]))
		{
			try
			{
				int num2 = this.HP_LIMIT = int.Parse(chatTextField.tfChat.getText());
				GameScr.info1.addInfo(string.Concat(new string[]
				{
					"Auto Bomb Boss dưới [",
					NinjaUtil.getMoneys((long)num2),
					" HP]"
				}), 0);
			}
			catch
			{
				GameScr.info1.addInfo("Vui Lòng Nhập Lại HP!", 0);
			}
			HairMod.ResetTF();
		}
	}

	// Token: 0x06000113 RID: 275
	public void onCancelChat()
	{
	}

	// Token: 0x06000114 RID: 276
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
			case 1:
				GameCanvas.panel.indexMod[12] = !GameCanvas.panel.indexMod[12];

                GameScr.info1.addInfo("Tự Đánh\n" + (GameCanvas.panel.indexMod[12] ? "[ON]" : "[OFF]"), 0);
				if (GameCanvas.panel.indexMod[12])
				{
					this.DoiMucTieu = false;
					return;
				}
				break;
			case 2:
				this.bool_2 = !this.bool_2;
				GameScr.info1.addInfo("Đánh Khi Đệ Cần\n" + (this.bool_2 ? "[ON]" : "[OFF]"), 0);
				return;
			case 3:
				{
					MyVector myVector = new MyVector();
					for (int i = 0; i < GameScr.keySkill.Length; i++)
					{
						myVector.addElement(new Command(((GameScr.keySkill[i] != null) ? GameScr.keySkill[i].template.name : "null") + "\n[" + (i + 1).ToString() + "]\n", this, 10, i));
					}
					GameCanvas.menu.startAt(myVector, 3);
					return;
				}
			case 4:
				this.bool_6 = !this.bool_6;
				GameScr.info1.addInfo("Auto Khiên Pro\n" + (this.bool_6 ? "[ON]" : "[OFF]"), 0);
				return;
			case 5:
				GameScr.info1.addInfo("Không Hỗ Trợ Lưu Cài Đặt Ở Mục Này!", 0);
				return;
			case 6:
				this.DoiMucTieu = !this.DoiMucTieu;
				GameScr.info1.addInfo("Đánh Chuyển Mục Tiêu\n" + (this.DoiMucTieu ? "[ON]" : "[OFF]"), 0);
				if (this.DoiMucTieu)
				{
					this.bool_1 = false;
					return;
				}
				break;
			case 7:
				this.listMucTieu.Clear();
				GameScr.info1.addInfo("Đã Xóa Clear!", 0);
				return;
			case 8:
				if (global::Char.myCharz().charFocus != null)
				{
					this.listMucTieu.Remove(global::Char.myCharz().charFocus);
					GameScr.info1.addInfo(string.Concat(new string[]
					{
					"Đã Xóa ",
					global::Char.myCharz().charFocus.cName,
					" [",
					global::Char.myCharz().charFocus.charID.ToString(),
					"]"
					}), 0);
					return;
				}
				break;
			case 9:
				if (global::Char.myCharz().charFocus != null)
				{
					this.listMucTieu.Add(global::Char.myCharz().charFocus);
					GameScr.info1.addInfo(string.Concat(new string[]
					{
					"Đã Thêm ",
					global::Char.myCharz().charFocus.cName,
					" [",
					global::Char.myCharz().charFocus.charID.ToString(),
					"]"
					}), 0);
					return;
				}
				break;
			case 10:
				this.method_2((int)p);
				return;
			case 11:
				{
					int num = (int)p;
					this.bool_4[num] = !this.bool_4[num];
					if (this.bool_4[num])
					{
						this.long_2[num] = -1L;
					}
					GameScr.info1.addInfo("Auto " + GameScr.keySkill[num].template.name + (this.bool_4[num] ? (": " + NinjaUtil.getMoneys(this.long_2[num]) + " mili giây") : "\n[OFF]"), 0);
					return;
				}
			case 12:
                HairMod.InputTF(this, this.string_0[0], this.string_0[1], TField.INPUT_TYPE_NUMERIC);
				this.int_0 = (int)p;
				return;
			case 13:
				{
					int num2 = (int)p;
					GameScr.keySkill[num2].coolDown = 0;
					GameScr.keySkill[num2].manaUse = 0;
					GameScr.info1.addInfo("Đóng Băng " + GameScr.keySkill[num2].template.name, 0);
					return;
				}
			case 14:
				if (this.HP_LIMIT <= 0)
				{
                    HairMod.InputTF(this, this.inputHpBomb[0], this.inputHpBomb[1], TField.INPUT_TYPE_NUMERIC);
				}
				if (this.HP_LIMIT > 0)
				{
					this.HP_LIMIT = 0;
					GameScr.info1.addInfo("Auto Bomb Boss\n[OFF]", 0);
				}
				break;
			default:
				return;
		}
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0001ABBC File Offset: 0x00018DBC
	public void method_1()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Tự Đánh\n" + (GameCanvas.panel.indexMod[12] ? "[ON]" : "[OFF]"), this, 1, null));
		myVector.addElement(new Command("Đánh Khi Đệ Cần\n" + (this.bool_2 ? "[ON]" : "[OFF]"), this, 2, null));
		myVector.addElement(new Command(GameScr.keySkill.Length.ToString() + " Ô Kỹ Năng", this, 3, null));
		myVector.addElement(new Command("Auto Khiên Pro\n" + (this.bool_6 ? "[ON]" : "[OFF]"), this, 4, null));
		if (global::Char.myCharz().cgender == 2)
		{
			myVector.addElement(new Command("Auto Bomb Boss\n[" + NinjaUtil.getMoneys((long)this.HP_LIMIT) + " HP]", this, 14, null));
		}
		myVector.addElement(new Command("Lưu Cài Đặt\n" + (this.saveSettings ? "[ON]" : "[OFF]"), this, 5, null));
		myVector.addElement(new Command("Đánh Chuyển Mục Tiêu\n" + (this.DoiMucTieu ? "[ON]" : "[OFF]"), this, 6, null));
		if (this.listMucTieu.Count > 0)
		{
			myVector.addElement(new Command("Clear Danh Sách Chuyển Mục Tiêu", this, 7, null));
		}
		if (global::Char.myCharz().charFocus != null)
		{
			if (this.listMucTieu.Contains(global::Char.myCharz().charFocus))
			{
				myVector.addElement(new Command("Xóa Khỏi Danh Sách Chuyển Mục Tiêu", this, 8, null));
			}
			else
			{
				myVector.addElement(new Command("Thêm Vào Danh Sách Chuyển Mục Tiêu", this, 9, null));
			}
		}
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0001ADB4 File Offset: 0x00018FB4
	private void method_2(int int_3)
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Auto Sử Dụng\n" + (this.bool_4[int_3] ? ("[" + NinjaUtil.getMoneys(this.long_2[int_3]) + " mili giây]") : "[OFF]"), this, 11, int_3));
		myVector.addElement(new Command("Nhập Delay\n[mili giây]", this, 12, int_3));
		myVector.addElement(new Command("Đóng Băng\n" + GameScr.keySkill[int_3].template.name, this, 13, int_3));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00007FFA File Offset: 0x000061FA
	private void method_3()
	{
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00007FFA File Offset: 0x000061FA
	private void method_4()
	{
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0001AE6C File Offset: 0x0001906C
	private void method_5()
	{
		for (int i = 0; i < global::Char.myCharz().nClass.skillTemplates.Length; i++)
		{
			SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
			Skill skill = global::Char.myCharz().getSkill(skillTemplate);
			GameScr.keySkill[i] = skill;
			GameScr.gI().saveKeySkillToRMS();
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0001AEC8 File Offset: 0x000190C8
	public void method_6()
	{
		if (!global::Char.myCharz().meDead && global::Char.myCharz().cHP > 0 && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && (!global::Char.myCharz().myskill.paintCanNotUseSkill || GameCanvas.panel.isShow))
		{
			int num = this.method_14();
			if (mSystem.currentTimeMillis() - this.long_0[num] > this.method_13(global::Char.myCharz().myskill))
			{
				if (GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus) && (double)global::Res.abs(global::Char.myCharz().mobFocus.xFirst - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.7)
				{
					global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
					this.method_12();
					this.long_0[num] = mSystem.currentTimeMillis();
					return;
				}
				if (global::Char.myCharz().charFocus != null && this.useSkill(global::Char.myCharz().charFocus) && (double)global::Res.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.7)
				{
					global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
					this.method_11();
					this.long_0[num] = mSystem.currentTimeMillis();
				}
			}
			return;
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0001B0D0 File Offset: 0x000192D0
	private void method_7()
	{
		if (this.bool_3 && GameScr.vMob.size() != 0)
		{
			if (global::Char.myCharz().myskill.template.type != 3)
			{
				Mob mobFocus = (Mob)GameScr.vMob.elementAt(0);
				int num = 0;
				for (int i = 0; i < GameScr.vMob.size(); i++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(i);
					int num2 = global::Res.abs(global::Char.myCharz().cx - mob.x);
					int num3 = global::Res.abs(global::Char.myCharz().cy - mob.y);
					int num4 = num2 * num2 + num3 * num3;
					if (num < num4)
					{
						num = num4;
						mobFocus = mob;
					}
				}
				global::Char.myCharz().mobFocus = mobFocus;
				int num5 = this.method_14();
				if (mSystem.currentTimeMillis() - this.long_0[num5] > (long)(global::Char.myCharz().myskill.coolDown + 100) && GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus))
				{
					this.method_12();
					this.long_0[num5] = mSystem.currentTimeMillis();
					this.bool_3 = false;
				}
				return;
			}
		}
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0001B20C File Offset: 0x0001940C
	private void method_8(int int_3)
	{
		if (TileMap.mapID != 21 && TileMap.mapID != 22)
		{
			if (TileMap.mapID != 23)
			{
				if (int_3 >= GameScr.keySkill.Length)
				{
					int_3 = GameScr.keySkill.Length - 1;
				}
				if (int_3 < 0)
				{
					int_3 = 0;
				}
				if (GameScr.keySkill[int_3] == null || GameScr.keySkill[int_3].paintCanNotUseSkill)
				{
					return;
				}
				if (GameScr.keySkill[int_3].coolDown == 0)
				{
					this.long_2[int_3] = 500L;
					return;
				}
				if (this.method_10(GameScr.keySkill[int_3]) && !GameScr.gI().isCharging() && mSystem.currentTimeMillis() - this.long_5 > 150L)
				{
					if (this.long_2[int_3] == -1L && GameCanvas.gameTick % 20 == 0)
					{
						this.long_1[int_3] = mSystem.currentTimeMillis();
						this.long_5 = mSystem.currentTimeMillis();
						GameScr.gI().doSelectSkill(GameScr.keySkill[int_3], true);
					}
					if (mSystem.currentTimeMillis() - this.long_1[int_3] > this.long_2[int_3])
					{
						this.long_1[int_3] = mSystem.currentTimeMillis();
						this.long_5 = mSystem.currentTimeMillis();
						GameScr.gI().doSelectSkill(GameScr.keySkill[int_3], true);
					}
				}
				return;
			}
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0001B384 File Offset: 0x00019584
	public bool useSkill(global::Char @char)
	{
		if (TileMap.mapID == 113)
		{
			return @char != null && global::Char.myCharz().myskill != null && (@char.cTypePk == 5 || @char.cTypePk == 3);
		}
		if (@char != null && global::Char.myCharz().myskill != null)
		{
			if (@char.statusMe != 14 && @char.statusMe != 5 && global::Char.myCharz().myskill.template.type != 2)
			{
				if ((global::Char.myCharz().cFlag != 8 || @char.cFlag == 0) && (global::Char.myCharz().cFlag == 0 || @char.cFlag != 8) && (global::Char.myCharz().cFlag == @char.cFlag || global::Char.myCharz().cFlag == 0 || @char.cFlag == 0) && (@char.cTypePk != 3 || global::Char.myCharz().cTypePk != 3) && global::Char.myCharz().cTypePk != 5 && @char.cTypePk != 5 && (global::Char.myCharz().cTypePk != 1 || @char.cTypePk != 1))
				{
					if (global::Char.myCharz().cTypePk != 4)
					{
						goto IL_159;
					}
					if (@char.cTypePk != 4)
					{
						goto IL_159;
					}
				}
				return true;
			}
			IL_159:
			return global::Char.myCharz().myskill.template.type == 2 && @char.cTypePk != 5;
		}
		return false;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0001B514 File Offset: 0x00019714
	private bool method_10(Skill gclass164_0)
	{
		if (gclass164_0.template.manaUseType == 2)
		{
			return true;
		}
		if (gclass164_0.template.manaUseType != 1)
		{
			return global::Char.myCharz().cMP >= gclass164_0.manaUse;
		}
		return global::Char.myCharz().cMP >= gclass164_0.manaUse * global::Char.myCharz().cMPFull / 100;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0001B578 File Offset: 0x00019778
	private void method_11()
	{
		try
		{
			MyVector myVector = new MyVector();
			myVector.addElement(global::Char.myCharz().charFocus);
			Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
		}
		catch
		{
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0001B5C4 File Offset: 0x000197C4
	private void method_12()
	{
		try
		{
			MyVector myVector = new MyVector();
			myVector.addElement(global::Char.myCharz().mobFocus);
			Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
		}
		catch
		{
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0001B610 File Offset: 0x00019810
	private long method_13(Skill gclass164_0)
	{
		if (gclass164_0.template.id != 20 && gclass164_0.template.id != 22 && gclass164_0.template.id != 7 && gclass164_0.template.id != 18)
		{
			if (gclass164_0.template.id != 23)
			{
				long num = (long)((double)gclass164_0.coolDown * 1.2);
				if (num < 415L)
				{
					return 415L;
				}
				return num;
			}
		}
		return (long)gclass164_0.coolDown + 500L;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0001B6A8 File Offset: 0x000198A8
	private int method_14()
	{
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			if (GameScr.keySkill[i] == global::Char.myCharz().myskill)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0001B6E0 File Offset: 0x000198E0
	private void method_15()
	{
		if (this.listMucTieu.Count == 0)
		{
			GameScr.info1.addInfo("Danh sách chuyển mục tiêu trống!", 0);
			this.DoiMucTieu = false;
			return;
		}
		if (!global::Char.myCharz().meDead && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && !global::Char.myCharz().myskill.paintCanNotUseSkill)
		{
			this.long_3 = this.method_16(global::Char.myCharz().myskill);
			if (this.int_1 >= this.listMucTieu.Count)
			{
				this.int_1 = 0;
			}
			if (mSystem.currentTimeMillis() - this.long_4 >= this.long_3)
			{
				this.long_4 = mSystem.currentTimeMillis();
				global::Char.myCharz().charFocus = GameScr.findCharInMap(this.listMucTieu[this.int_1].charID);
				this.int_1++;
				if (this.int_1 >= this.listMucTieu.Count)
				{
					this.int_1 = 0;
				}
				if (global::Char.myCharz().charFocus != null && this.useSkill(global::Char.myCharz().charFocus) && (double)global::Res.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
				{
					global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
					this.method_11();
				}
			}
			return;
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000084BE File Offset: 0x000066BE
	private long method_16(Skill gclass164_0)
	{
		if (gclass164_0.coolDown <= 500)
		{
			return 1000L;
		}
		return (long)((double)gclass164_0.coolDown * 1.2 + 200.0);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0001B8D8 File Offset: 0x00019AD8
	private void method_17()
	{
		if (TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23 && GameScr.keySkill[7] != null && !GameScr.keySkill[7].paintCanNotUseSkill && this.method_10(GameScr.keySkill[7]) && !GameScr.gI().isCharging() && !global::Char.myCharz().protectEff  && !global::Char.myCharz().sleepEff && global::Char.myCharz().holdEffID == 0 && mSystem.currentTimeMillis() - this.long_5 >= 150L)
		{
			this.long_5 = mSystem.currentTimeMillis();
			GameScr.gI().doSelectSkill(GameScr.keySkill[7], true);
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0001B9D8 File Offset: 0x00019BD8
	public void method_18()
	{
		int num = this.method_14();
		GameScr.keySkill[num].coolDown = 0;
		GameScr.keySkill[num].manaUse = 0;
		GameScr.info1.addInfo("Đóng Băng\n" + GameScr.keySkill[num].template.name, 0);
	}

	// Token: 0x06000127 RID: 295
	private void method_19()
	{
		if (this.HP_LIMIT > 0 && global::Char.myCharz().charFocus != null && (this.method_20(global::Char.myCharz().charFocus) || global::Char.myCharz().charFocus.cHP > 0) && global::Char.myCharz().charFocus.cHP <= this.HP_LIMIT && !GameScr.keySkill[4].paintCanNotUseSkill && mSystem.currentTimeMillis() - this.long_6 > 100L)
		{
			this.long_6 = mSystem.currentTimeMillis();
			new Thread(new ThreadStart(this.method_0)).Start();
			return;
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00016588 File Offset: 0x00014788
	private bool method_20(global::Char gclass22_0)
	{
		return gclass22_0.cName != null && gclass22_0.cName != "" && !gclass22_0.isPet && !gclass22_0.isMiniPet && char.IsUpper(char.Parse(gclass22_0.cName.Substring(0, 1))) && gclass22_0.cName != "Trọng tài" && !gclass22_0.cName.StartsWith("#") && !gclass22_0.cName.StartsWith("$");
	}

	// Token: 0x06000129 RID: 297 RVA: 0x000084F3 File Offset: 0x000066F3
	public bool[] method_21()
	{
		return this.bool_4;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x000084FB File Offset: 0x000066FB
	public bool method_22()
	{
		return this.bool_3;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00008503 File Offset: 0x00006703
	public void method_23(bool isPetRequestAttack)
	{
		this.bool_3 = isPetRequestAttack;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000850C File Offset: 0x0000670C
	public bool method_24()
	{
		return this.bool_2;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00008514 File Offset: 0x00006714
	public void method_25(bool isAutoAttack)
	{
        GameCanvas.panel.indexMod[12] = isAutoAttack;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000851D File Offset: 0x0000671D
	public bool method_26()
	{
		return GameCanvas.panel.indexMod[12];

    }

	// Token: 0x0600012F RID: 303 RVA: 0x00008525 File Offset: 0x00006725
	public bool method_27()
	{
		return this.bool_0;
	}

	// Token: 0x0400010D RID: 269
	public static AutoSkil gI = new AutoSkil();

	// Token: 0x0400010E RID: 270
	private bool bool_0;

	// Token: 0x0400010F RID: 271
	public bool bool_1;

	// Token: 0x04000110 RID: 272
	private long[] long_0;

	// Token: 0x04000111 RID: 273
	private bool bool_2;

	// Token: 0x04000112 RID: 274
	private bool bool_3;

	// Token: 0x04000113 RID: 275
	private bool[] bool_4;

	// Token: 0x04000114 RID: 276
	private long[] long_1;

	// Token: 0x04000115 RID: 277
	private long[] long_2;

	// Token: 0x04000116 RID: 278
	private int int_0;

	// Token: 0x04000117 RID: 279
	private bool DoiMucTieu;

	// Token: 0x04000118 RID: 280
	private long long_3;

	// Token: 0x04000119 RID: 281
	private long long_4;

	// Token: 0x0400011A RID: 282
	private List<global::Char> listMucTieu;

	// Token: 0x0400011B RID: 283
	private int int_1;

	// Token: 0x0400011C RID: 284
	private bool bool_6;

	// Token: 0x0400011D RID: 285
	private string[] string_0;

	// Token: 0x0400011E RID: 286
	private bool saveSettings;

	// Token: 0x0400011F RID: 287
	private long long_5;

	// Token: 0x04000120 RID: 288
	private long long_6;

	// Token: 0x04000121 RID: 289
	private int HP_LIMIT;

	// Token: 0x04000122 RID: 290
	private string[] inputHpBomb;
}
