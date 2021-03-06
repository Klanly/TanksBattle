"use strict";

var gameConfig = require("./gameConfig");
var propertyTable = require("./propertyTable");
var propDistributeConfig = require("./propDistributeConfig");
var roleTypePropConfig = require("./roleTypePropConfig");
var logUtil = require("../../libs/logUtil");
var enProp = require("../enumType/propDefine").enProp;
var enPropFight = require("../enumType/propDefine").enPropFight;
var roleLvPropConfig = require("./roleLvPropConfig");
var propValueConfig = require("./propValueConfig");
var propBasicConfig = require("./propBasicConfig");

class RoleConfig
{
    constructor() {
        this.id = 0;
        this.name = "";
        this.mod = "";
        this.prop = "";
        this.propType = "";
        this.propValue = "";
        this.propDistribute = "";
        this.behitRate = 0;
        this.behitFxs = "";
        this.deadFx = "";
        this.uniqueSkill = "";
        this.atkUpSkill = "";
        this.skills = [];
        this.skillFile = "";
        this.maxStar = 0;
        this.maxAdvanceLevel = 0;
        this.initEquips = "";
        this.soulNum = 0;
        this.upgradeCostId = "";
        this.advanceCostId = "";
        this.upstarCostId = "";
        this.positioning = "";
        this.icon = "";
        this.uiModScale = 0;
        this.talents = [];
        this.bornBuffs = "";
        this.roleType = 0;
        this.initStar = 0;
        this.pieceItemId = 0;
        this.pieceNum = 0;
        this.pieceNumReturn = 0;
        this.power = 0;
    }

    static fieldsDesc() {
        return {
            id: {type: String},
            name: {type: String},
            mod: {type: String},
            prop: {type: String},
            propType: {type: String},
            propValue: {type: String},
            propDistribute: {type: String},
            behitRate: {type: Number},
            behitFxs: {type: String},
            deadFx: {type: String},
            uniqueSkill: {type: String},
            atkUpSkill: {type: String},
            skills: {type: Array, elemType: String},
            skillFile: {type: String},
            maxStar: {type: Number},
            maxAdvanceLevel: {type: Number},
            initEquips: {type: String},
            soulNum: {type: Number},
            upgradeCostId: {type: String},
            advanceCostId: {type: String},
            upstarCostId: {type: String},
            positioning: {type: String},
            icon: {type: String},
            uiModScale: {type: Number},
            talents: {type: Array, elemType: String},
            bornBuffs: {type: String},
            roleType: {type: Number},
            initStar: {type: Number},
            pieceItemId: {type: Number},
            pieceNum: {type: Number},
            pieceNumReturn: {type: Number},
            power: {type: Number},
        };
    }

    /**
     *
     * @param {object} target
     * @param {number} lv
     * @param {number} advLv
     * @param {number} star
     */
    getBaseProp(target, lv, advLv, star)
    {
        if(this.propType=="角色属性")
        {
            //属性等级系数（角色）*角色属性分配比例*属性值系数（role）
            var propsDistribute = this.propDistribute ? propDistributeConfig.getPropDistributeConfig(this.propDistribute).props : {};
            propertyTable.mul(roleTypePropConfig.getRoleTypeProp(), propsDistribute, target);
            //logUtil.info("属性等级系数（角色）*角色属性分配比例*属性值系数（role）="+target[enPropFight.hpMax]);
            propertyTable.mulValue(roleLvPropConfig.getRoleLvPropConfig(lv).rate, target, target);
            //logUtil.info("*属性等级系数（角色）="+target[enPropFight.hpMax]);

            //加初始值
            var propsValue = this.propValue ? propValueConfig.getPropValueConfig(this.propValue).props : {};
            propertyTable.add(target, propsValue, target);
            //logUtil.info("+初始值="+target[enPropFight.hpMax]);

            //战斗力：（角色战斗力初值+角色属性等级系数）*战斗力系数
            target[enProp.power] = (this.power + roleLvPropConfig.getRoleLvPropConfig(lv).rate) * propBasicConfig.getPropBasicConfig().powerRate;
            //logUtil.info("战斗力="+target[enProp.power]);
        }
        else
        {
            // 怪物不做计算
            propertyTable.set(0,target);
        }
    }
}

/**
 *
 * @param {(string|number)?} key - 主键或有效数据行号，不填的话，就返回全部行
 * @returns {RoleConfig}
 */
function getRoleConfig(key)
{
    return gameConfig.getCsvConfig("role", key);
}

exports.RoleConfig = RoleConfig;
exports.getRoleConfig = getRoleConfig;