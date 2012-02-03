
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;

namespace BP.Sys
{
    /// <summary>
    /// 通用实体
    /// </summary>
    public class GEEntity : Entity
    {
        #region 构造函数
        public override string ToString()
        {
            return this.FK_MapData;
        }
        public override string ClassID
        {
            get
            {
                return this.FK_MapData;
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string FK_MapData = null;
        /// <summary>
        /// 通用实体
        /// </summary>
        public GEEntity()
        {
        }
        /// <summary>
        /// 通用实体
        /// </summary>
        /// <param name="nodeid">节点ID</param>
        public GEEntity(string fk_mapdata)
        {
            this.FK_MapData = fk_mapdata;
        }
        /// <summary>
        /// 通用实体
        /// </summary>
        /// <param name="nodeid">节点ID</param>
        /// <param name="_oid">OID</param>
        public GEEntity(string fk_mapdata, object pk)
        {
            this.FK_MapData = fk_mapdata;
            this.PKVal = pk;
            this.Retrieve();
        }
        #endregion

        #region Map
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                if (this.FK_MapData == null)
                    throw new Exception("没有给" + this.FK_MapData + "值，您不能获取它的Map。");

                this._enMap = BP.Sys.MapData.GenerHisMap(this.FK_MapData);
                return this._enMap;
            }
        }
        /// <summary>
        /// GEEntitys
        /// </summary>
        public override Entities GetNewEntities
        {
            get
            {
                if (this.FK_MapData == null)
                    return new GEEntitys();
                return new GEEntitys(this.FK_MapData);
            }
        }
        #endregion
 
    }
    /// <summary>
    /// 通用实体s
    /// </summary>
    public class GEEntitys : EntitiesOID
    {
        #region 重载基类方法
        public override string ToString()
        {
            //if (this.FK_MapData == null)
            //    throw new Exception("@没有能 FK_MapData 给值。");
            return this.FK_MapData;
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string FK_MapData = null;
        #endregion

        #region 方法
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                //if (this.FK_MapData == null)
                //    throw new Exception("@没有能 FK_MapData 给值。");

                if (this.FK_MapData == null)
                    return new GEEntity();
                return new GEEntity(this.FK_MapData);
            }
        }
        /// <summary>
        /// 通用实体ID
        /// </summary>
        public GEEntitys()
        {
        }
        /// <summary>
        /// 通用实体ID
        /// </summary>
        /// <param name="fk_mapdtl"></param>
        public GEEntitys(string fk_mapdata)
        {
            this.FK_MapData = fk_mapdata;
        }
        #endregion
    }
}
