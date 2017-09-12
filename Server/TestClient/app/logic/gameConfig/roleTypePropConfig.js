"use strict";

var gameConfig = require("./gameConfig");
var propertyTable = require("./propertyTable");

class RoleTypePropConfig
{
    constructor() {
        this.id = "";
        this.props = {};
    }

    static fieldsDesc() {
        return propertyTable.addFightPropParamList({
            id: {type: String},
        });
    }

    /**
     * 使用默认读取方式读完一行数据后可以执行对行对象的再次处理
     * 如果使用自定义读取方式，直接在那个自定义读取方式里处理就行了，不用这个函数了
     * 这个函数可选，没有就不执行
     * @param {object} row - 行数据对象
     */
    static afterDefReadRow(row)
    {
        row.props = propertyTable.getPropertyTableFromRow(row);
    }
}

/**
 *
 * @param {(string|number)?} key - 主键或有效数据行号，不填的话，就返回全部行
 * @returns {RoleTypePropConfig}
 */
function getRoleTypePropConfig(key)
{
    return gameConfig.getCsvConfig("roleTypeProp", key);
}

/**
 * @returns {RoleTypePropConfig}
 */
function getRoleTypeProp()
{
    return getRoleTypePropConfig("role").props;
}

/**
 * @returns {RoleTypePropConfig}
 */
function getPowerProp()
{
    return getRoleTypePropConfig("power").props;
}

/**
 * @returns {RoleTypePropConfig}
 */
function getPowerRateProp()
{
    return getRoleTypePropConfig("powerRate").props;
}


exports.RoleTypePropConfig = RoleTypePropConfig;
exports.getRoleTypePropConfig = getRoleTypePropConfig;
exports.getRoleTypeProp = getRoleTypeProp;
exports.getPowerProp = getPowerProp;
exports.getPowerRateProp = getPowerRateProp;