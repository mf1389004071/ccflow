using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{
    /// <summary>
    /// M2M
    /// </summary>
    public class M2MAttr : EntityMyPKAttr
    {
        public const string FK_MapData = "FK_MapData";
        public const string M2MNo = "M2MNo";
        public const string EnOID = "EnOID";
        public const string Doc = "Doc";
        public const string ValsSQL = "ValsSQL";
        public const string ValsName = "ValsName";
        public const string DtlObj = "DtlObj";
        /// <summary>
        /// 选择数
        /// </summary>
        public const string NumSelected = "NumSelected";
    }
	/// <summary>
    ///  M2M 数据存储
	/// </summary>
    public class M2M : EntityMyPK
    {
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(M2MAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// 选择数
        /// </summary>
        public int NumSelected
        {
            get
            {
                return this.GetValIntByKey(M2MAttr.NumSelected);
            }
            set
            {
                this.SetValByKey(M2MAttr.NumSelected, value);
            }
        }
        public Int64 EnOID
        {
            get
            {
                return this.GetValInt64ByKey(M2MAttr.EnOID);
            }
            set
            {
                this.SetValByKey(M2MAttr.EnOID, value);
            }
        }
        /// <summary>
        /// 操作对象对于m2mm有效
        /// </summary>
        public string DtlObj
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.DtlObj);
            }
            set
            {
                this.SetValByKey(M2MAttr.DtlObj, value);
            }
        }
        public string ValsSQL
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.ValsSQL);
            }
            set
            {
                this.SetValByKey(M2MAttr.ValsSQL, value);
            }
        }
        public string ValsName
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.ValsName);
            }
            set
            {
                this.SetValByKey(M2MAttr.ValsName, value);
            }
        }
        /// <summary>
        /// 数据
        /// </summary>
        public string Vals
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.Doc);
            }
            set
            {
                this.SetValByKey(M2MAttr.Doc, value);
            }
        }
        public string M2MNo
        {
            get
            {
                return this.GetValStrByKey(M2MAttr.M2MNo);
            }
            set
            {
                this.SetValByKey(M2MAttr.M2MNo, value);
            }
        }
        #region 构造方法
        /// <summary>
        /// M2M数据存储
        /// </summary>
        public M2M()
        {
        }
        #endregion

        /// <summary>
        /// M2M数据存储
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_M2M");
                map.EnDesc = "M2M数据存储";
                map.DepositaryOfMap = Depositary.Application;

                map.AddMyPK();
                map.AddTBString(M2MAttr.FK_MapData, null, "FK_MapData", true, true, 0, 20, 20);
                map.AddTBString(M2MAttr.M2MNo, null, "M2MNo", true, true, 0, 20, 20);

                map.AddTBInt(M2MAttr.EnOID, 0, "实体OID", true, false);
                map.AddTBString(M2MAttr.DtlObj, null, "DtlObj(对于m2mm有效)", true, true, 0, 20, 20);

                map.AddTBStringDoc();
                map.AddTBStringDoc(M2MAttr.ValsName, null, "ValsName", true, true);
                map.AddTBStringDoc(M2MAttr.ValsSQL, null, "ValsSQL", true, true);

                map.AddTBInt(M2MAttr.NumSelected, 0, "选择数", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeUpdateInsertAction()
        {
            this.InitMyPK();
            return base.beforeUpdateInsertAction();
        }
        public void InitMyPK()
        {
            this.MyPK = this.FK_MapData + "_" + this.M2MNo + "_" + this.EnOID + "_" + this.DtlObj;
        }
    }
	/// <summary>
    /// M2M数据存储
	/// </summary>
    public class M2Ms : SimpleNoNames
    {
        /// <summary>
        /// M2M数据存储s
        /// </summary>
        public M2Ms() 
        {
        }
        /// <summary>
        /// M2M数据存储s
        /// </summary>
        /// <param name="FK_MapData"></param>
        /// <param name="EnOID"></param>
        public M2Ms(int FK_MapData, Int64 EnOID)
        {
            this.Retrieve(M2MAttr.FK_MapData, FK_MapData, M2MAttr.EnOID, EnOID);
        }
        /// <summary>
        /// M2M数据存储 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new M2M();
            }
        }
    }
}
