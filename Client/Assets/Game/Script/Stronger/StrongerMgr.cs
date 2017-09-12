﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StrongerMgr : Singleton<StrongerMgr>
{
    public bool IsStrongerOpen(string strongerType)
    {
        enStrongerType type = (enStrongerType)Enum.Parse(typeof(enStrongerType), strongerType);
        string errMsg;
        switch (type)
        {
            case enStrongerType.equipAdvLv:
            case enStrongerType.equipStar:
                return SystemMgr.instance.IsEnabled(enSystem.hero, out errMsg);                
            case enStrongerType.equipSkillLv:
            case enStrongerType.weaponTtalentLv:
                return SystemMgr.instance.IsEnabled(enSystem.weapon, out errMsg);     
            case enStrongerType.treasure:
                return SystemMgr.instance.IsEnabled(enSystem.treasure, out errMsg);
            case enStrongerType.flame:
                return SystemMgr.instance.IsEnabled(enSystem.flame, out errMsg);
            case enStrongerType.petEquipAdvLv:
            case enStrongerType.petEquipStar:            
            case enStrongerType.petStar:
            case enStrongerType.petSkill:
                return SystemMgr.instance.IsEnabled(enSystem.pet, out errMsg);
            case enStrongerType.petAdvLv:
                return SystemMgr.instance.IsEnabled(enSystem.petAdvance, out errMsg);
            case enStrongerType.petTalent:
                return SystemMgr.instance.IsEnabled(enSystem.petTalent, out errMsg);            
            case enStrongerType.dailyTask:
            case enStrongerType.growthTask:
                return SystemMgr.instance.IsEnabled(enSystem.dailyTask, out errMsg);
            case enStrongerType.warriorTried:
                return SystemMgr.instance.IsEnabled(enSystem.warriorTried, out errMsg);
            case enStrongerType.normalLevel:
                return SystemMgr.instance.IsEnabled(enSystem.scene, out errMsg);            
            case enStrongerType.arena:
                return SystemMgr.instance.IsEnabled(enSystem.arena, out errMsg);           
            case enStrongerType.levelReward:
                return SystemMgr.instance.IsEnabled(enSystem.opActivity, out errMsg);
            case enStrongerType.goldLevel:
                return SystemMgr.instance.IsEnabled(enSystem.goldLevel, out errMsg);
            case enStrongerType.recharge:
                return true;
        }
        return true;
    }

    public int GetStrongerProgress(string strongerType)
    {
        enStrongerType type = (enStrongerType)Enum.Parse(typeof(enStrongerType), strongerType);
        int progress=0;
        float target = 0;
        float current = 0;
        float baseNum = 0;
        Role hero = RoleMgr.instance.Hero;
        int currentLv = hero.GetInt(enProp.level);
        StrongerHeroCfg heroCfg = StrongerHeroCfg.m_cfgs[currentLv];
        StrongerPetCfg petCfg = StrongerPetCfg.m_cfgs[currentLv];
        StrongerHeroCfg heroCfgBase = StrongerHeroCfg.m_cfgs[1];
        StrongerPetCfg petCfgBase = StrongerPetCfg.m_cfgs[1];
        switch (type)
        {
            case enStrongerType.equipAdvLv:
                {
                    EquipsPart equipsPart = hero.EquipsPart;                    
                    for (int i = 0; i < (int)enEquipPos.maxNormal - (int)enEquipPos.minNormal + 1; ++i)
                    {
                        Equip equip = equipsPart.GetEquip((enEquipPos)i);
                        current += equip.AdvLv;
                        target += heroCfg.equipAdvLv;
                        baseNum += heroCfgBase.equipAdvLv;
                    }
                    Weapon curWeapon = hero.WeaponPart.CurWeapon;
                    current += curWeapon.Equip.AdvLv;
                    target += heroCfg.equipAdvLv;
                    baseNum += heroCfgBase.equipAdvLv;
                    break;
                }
            case enStrongerType.equipStar:
                {
                    EquipsPart equipsPart = hero.EquipsPart;                   
                    for (int i = 0; i < (int)enEquipPos.maxNormal - (int)enEquipPos.minNormal + 1; ++i)
                    {
                        Equip equip = equipsPart.GetEquip((enEquipPos)i);
                        int star = EquipCfg.m_cfgs[equip.EquipId].star;
                        current += star;
                        target += heroCfg.equipStar;
                        baseNum += heroCfgBase.equipStar;
                    }
                    Weapon curWeapon = hero.WeaponPart.CurWeapon;
                    int weaponStar = EquipCfg.m_cfgs[curWeapon.Equip.EquipId].star;
                    current += weaponStar;
                    target += heroCfg.equipStar;
                    baseNum += heroCfgBase.equipStar;
                    break;
                }
            case enStrongerType.equipSkillLv:
                {
                    WeaponPart weaponPart = hero.WeaponPart;
                    /*for (int i = 0; i < (int)enEquipPos.maxWeapon - (int)enEquipPos.minWeapon + 1; ++i)
                    {
                        Weapon weapon = weaponPart.GetWeapon(i);
                        for (int j = 0; j < (int)enSkillPos.max; ++j)
                        {
                            WeaponSkill skill = weapon.GetSkill(j);
                            current += skill.lv;
                            target += heroCfg.equipSkillLv;
                        }
                    }*/
                    Weapon weapon = weaponPart.CurWeapon;
                    for (int j = 0; j < (int)enSkillPos.max; ++j)
                    {
                        WeaponSkill skill = weapon.GetSkill(j);
                        current += skill.lv;
                        target += heroCfg.equipSkillLv;
                        baseNum +=heroCfgBase.equipSkillLv;
                    }
                    break;
                }
            case enStrongerType.weaponTtalentLv:
                {
                    WeaponPart weaponPart = hero.WeaponPart;
                    /*for (int i = 0; i < (int)enEquipPos.maxWeapon - (int)enEquipPos.minWeapon + 1; ++i)
                    {
                        Weapon weapon = weaponPart.GetWeapon(i);
                        for (int j = 0; j < (int)enSkillPos.max; ++j)
                        {                        
                            WeaponSkill skill = weapon.GetSkill(j);
                            for (int k = 0; k < skill.TalentCount; ++k)
                            {
                                WeaponSkillTalent talent = skill.GetTalent(k);
                                current += talent.lv;
                                target += heroCfg.weaponTalentLv;
                            }
                        }
                    }*/
                    Weapon weapon = weaponPart.CurWeapon;
                    for (int j = 0; j < (int)enSkillPos.max; ++j)
                    { 
                        WeaponSkill skill = weapon.GetSkill(j);                 
                        for (int k = 0; k < skill.TalentCount; ++k)
                        {                            
                            WeaponSkillTalent talent = skill.GetTalent(k);
                            current += talent.lv;
                            target += heroCfg.weaponTalentLv;
                            baseNum += heroCfgBase.weaponTalentLv;
                        }
                    }
                    break;
                }
            case enStrongerType.treasure:
                TreasurePart treasurePart = hero.TreasurePart;
                List<int> treasures = heroCfg.GetTreasure();
                List<int> baseTreasures = heroCfgBase.GetTreasure();
                foreach(Treasure treasure in treasurePart.Treasures.Values)
                {
                    current += treasure.level;
                }
                target = treasures[0] * treasures[1];
                baseNum = baseTreasures[0] * baseTreasures[1];
                break;
            case enStrongerType.flame:
                {
                    FlamesPart flamesPart = hero.FlamesPart;
                    List<Flame> flames = heroCfg.GetFlames();
                    List<Flame> baseFlames = heroCfgBase.GetFlames();
                    for(int i=0;i<flames.Count;++i)
                    {
                        Flame flame = flamesPart.GetFlame(flames[i].FlameId);
                        if(flame!=null)
                        {
                            current += flame.Level;                            
                        }
                        target += flames[i].Level;
                        baseNum += baseFlames[i].Level;
                    }
                    break;
                }
            case enStrongerType.petEquipAdvLv:
                {
                    List<Role> pets = GetPets();
                    for (int i = 0; i < petCfg.num; ++i)
                    {
                        if (i<= pets.Count-1)
                        {
                            EquipsPart equipsPart = pets[i].EquipsPart;
                            for (int j = 0; j < (int)enEquipPos.minWeapon - (int)enEquipPos.minNormal + 1; ++j)
                            {
                                Equip equip = equipsPart.GetEquip((enEquipPos)j);
                                current += equip.AdvLv;                                
                            }
                        }
                        target += petCfg.equipAdvLv*((int)enEquipPos.minWeapon - (int)enEquipPos.minNormal + 1);                        
                    }
                    baseNum = petCfgBase.num*petCfgBase.equipAdvLv * ((int)enEquipPos.minWeapon - (int)enEquipPos.minNormal + 1);
                    break;
                }
            case enStrongerType.petEquipStar:
                {
                    List<Role> pets = GetPets();
                    for (int i = 0; i < petCfg.num; ++i)
                    {
                        if (i <= pets.Count - 1)
                        {
                            EquipsPart equipsPart = pets[i].EquipsPart;
                            for (int j = 0; j < (int)enEquipPos.minWeapon - (int)enEquipPos.minNormal + 1; ++j)
                            {
                                Equip equip = equipsPart.GetEquip((enEquipPos)j);
                                int star = EquipCfg.m_cfgs[equip.EquipId].star;
                                current += star;                                
                            }
                        }
                        target += petCfg.equipStar*((int)enEquipPos.minWeapon - (int)enEquipPos.minNormal + 1);                        
                    }
                    baseNum = petCfgBase.num*petCfgBase.equipStar * ((int)enEquipPos.minWeapon - (int)enEquipPos.minNormal + 1);
                    break;
                }
            case enStrongerType.petAdvLv:
                {
                    List<Role> pets = GetPets();               
                    for (int i = 0; i < petCfg.num; ++i)
                    {
                        if (i <= pets.Count - 1)
                        {
                            current += pets[i].GetInt(enProp.advLv);
                        }
                        target += petCfg.petAdvLv;                       
                    }
                    baseNum = petCfgBase.num * petCfgBase.petAdvLv;
                    break;
                }
            case enStrongerType.petStar:
                {
                    List<Role> pets = GetPets();
                    for (int i = 0; i < petCfg.num; ++i)
                    {
                        if (i <= pets.Count - 1)
                        {
                            current += pets[i].GetInt(enProp.star);                            
                        }
                        target += petCfg.petStar;                        
                    }
                    baseNum = petCfgBase.num*petCfgBase.petStar;
                    break;
                }
            case enStrongerType.petSkill:
                {
                    List<Role> pets = GetPets();
                    List<int> petSkills = petCfg.GetPetSkill();
                    List<int> petSkillsBase = petCfgBase.GetPetSkill();              
                    for (int i = 0; i < petCfg.num; ++i)
                    {
                        if (i <= pets.Count - 1)
                        {
                            PetSkillsPart petSkillsPart = pets[i].PetSkillsPart;
                            foreach(PetSkill petSkill in petSkillsPart.PetSkills.Values)
                            {
                                current += petSkill.level;
                            }                            
                        }
                        target += (petSkills[0] * petSkills[1]);                       
                    }
                    baseNum = petCfgBase.num * (petSkillsBase[0] * petSkillsBase[1]);
                    break;
                }
            case enStrongerType.petTalent:
                {
                    List<Role> pets = GetPets();
                    List<int> petTalents = petCfg.GetTalent();
                    List<int> petTalentsBase = petCfgBase.GetTalent();
                    for (int i = 0; i < petCfg.num; ++i)
                    {
                        if (i <= pets.Count - 1)
                        {
                            TalentsPart petTelentsPart= pets[i].TalentsPart;
                            int advLv = pets[i].GetInt(enProp.advLv);
                            foreach (Talent talent in petTelentsPart.Talents.Values)
                            {
                                TalentPosCfg talentPosCfg = TalentPosCfg.m_cfgs[talent.pos];
                                if (advLv >= talentPosCfg.needAdvLv)
                                    current += talent.level;
                            }
                        }
                        target += (petTalents[0] * petTalents[1]);                        
                    }
                    baseNum =petCfgBase.num *  (petTalentsBase[0] * petTalentsBase[1]);
                    break;
                }          
        }
        progress = target - baseNum == 0 ? 100 : Mathf.RoundToInt((current - baseNum) / (target - baseNum) * 100);
        if (progress > 100)
            progress = 100;
        return progress;
    }   

    public void GoStronger(string strongerType)
    {
        enStrongerType type = (enStrongerType)Enum.Parse(typeof(enStrongerType), strongerType);
        
        switch(type)
        {
            case enStrongerType.equipAdvLv:
            case enStrongerType.equipStar:
                UIMgr.instance.Open<UIEquip>().SelectEquipUpGrade();
                break;
            case enStrongerType.equipSkillLv:
                UIMgr.instance.Open<UIWeapon>();
                break;
            case enStrongerType.weaponTtalentLv:
                UIMgr.instance.Open<UIWeapon>().m_tab.SetSel(1);
                break;
            case enStrongerType.treasure:
                UIMgr.instance.Open<UITreasure>();
                break;
            case enStrongerType.flame:
                UIMgr.instance.Open<UIFlame>();
                break;
            case enStrongerType.petEquipAdvLv:
            case enStrongerType.petEquipStar:
            case enStrongerType.petAdvLv:
            case enStrongerType.petStar:
            case enStrongerType.petSkill:
            case enStrongerType.petTalent:
                UIMgr.instance.Open<UIChoosePet>();
                break;
            case enStrongerType.dailyTask:
                UIMgr.instance.Open<UITask>();
                break;
            case enStrongerType.warriorTried:
                UIMgr.instance.Open<UIWarriorsTried>();
                break;
            case enStrongerType.normalLevel:
                UIMgr.instance.Open<UILevelSelect>();
                break;
            case enStrongerType.growthTask:
                UIMgr.instance.Open<UITask>().btnsGroup.SetSel(1);
                break;
            case enStrongerType.arena:
                UIMgr.instance.Open<UIArena>();
                break;
            case enStrongerType.recharge:
                break;
            case enStrongerType.levelReward:
                UIMgr.instance.Open<UIOpActivity>().SelectOpActivity(1);
                break;
            case enStrongerType.goldLevel:
                UIMgr.instance.Open<UIGoldLevel>();
                break;
        }
    }

    List<Role> GetPets()
    {
        List<Role> pets = RoleMgr.instance.Hero.PetsPart.GetFightPets();

        Role temp = new Role();
        for (int i = pets.Count; i > 0; i--)
        {
            for (int j = 0; j < i - 1; j++)
            {
                if (pets[j].GetInt(enProp.power) < pets[j + 1].GetInt(enProp.power))
                {
                    temp = pets[j];
                    pets[j] = pets[j + 1];
                    pets[j + 1] = temp;
                }
            }           
        }
        return pets;
    }
}

public enum enStrongerType
{
    equipAdvLv,//英雄身上装备等阶
    equipStar,//英雄身上装备觉醒星级
    equipSkillLv,//英雄技能等级
    weaponTtalentLv,//铭文等级
    treasure,//神器个数、等级
    flame,//圣火相关
    petEquipAdvLv,//神侍装备等阶
    petEquipStar,//神侍装备觉醒星级
    petAdvLv,//神侍突破等阶
    petStar,//神侍星级
    petSkill,//神侍技能
    petTalent,//神侍天赋
    dailyTask,//每日任务
    warriorTried,//勇士试炼
    normalLevel,//主线副本
    growthTask,//成长任务
    arena,//竞技场
    recharge,//充值
    levelReward,//冲级豪礼
    goldLevel,//金币副本
    eliteLv, //众神传
    prophetTower,//预言者之塔
    treasureRob,//神奇争夺

}
