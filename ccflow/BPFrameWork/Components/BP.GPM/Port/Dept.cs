using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.GPM
{
	/// <summary>
	/// 部门属性
	/// </summary>
    public class DeptAttr : EntityNoNameAttr
	{
		/// <summary>
		/// 部门性质
		/// </summary>
		public const string WorkCharacter="WorkCharacter";
		/// <summary>
		/// 部门
		/// </summary>
		public const string FK_Dept="FK_Dept";
		/// <summary>
		/// 工作基次
		/// </summary>
		public const string WorkFloor="WorkFloor";
		/// <summary>
		/// 部门性质
		/// </summary>
		public const string DeptType="DeptType";
	}
	/// <summary>
	/// 部门
	/// </summary>
	public class Dept:EntityNoName
	{
		#region 属性
        public new string Name
        {
            get
            {
                if (BP.Web.WebUser.SysLang == "B5")
                    return Sys.Language.Turn2Traditional(this.GetValStrByKey("Name"));

                return this.GetValStrByKey("Name");
            }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int Grade
        {
            get
            {
                return this.No.Length / 2;
            }
        }
		#endregion

		#region 构造函数
		/// <summary>
		/// 部门
		/// </summary>
		public Dept(){}
		/// <summary>
		/// 部门
		/// </summary>
		/// <param name="no">编号</param>
        public Dept(string no) : base(no){}
		#endregion

		#region 重写方法
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //连接到的那个数据库上. (默认的是: AppCenterDSN )
                map.PhysicsTable = "Port_Dept";
                map.EnType = EnType.Admin;

                map.EnDesc = this.ToE("Dept", "部门"); // "部门";// 实体的描述.
                map.DepositaryOfEntity = Depositary.Application; //实体map的存放位置.
                map.DepositaryOfMap = Depositary.Application;    // Map 的存放位置.
                map.CodeStruct = "22222222";
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.IsAutoGenerNo = false;

                map.AdjunctType = AdjunctType.None;
                map.AddTBStringPK(DeptAttr.No, null, null, true, false, 2, 20, 40);
                map.AddTBString(DeptAttr.Name, null,null, true, false, 0, 60, 400);
                //   map.AddTBInt(DeptAttr.Grade, 0, "级次", true, false);
                //  map.AddBoolean(DeptAttr.IsDtl, false, "是否明细", true, true);
                this._enMap = map;
                return this._enMap;
            }
		}
        protected override bool beforeUpdateInsertAction()
        {
           // this.Grade = this.No.Length / 2;
            return base.beforeUpdateInsertAction();
        }
		#endregion
	}
	/// <summary>
	///得到集合
	/// </summary>
	public class Depts: EntitiesNoName
	{
		/// <summary>
		/// 查询全部。
		/// </summary>
		/// <returns></returns>
        public override int RetrieveAll()
        {

            if (Web.WebUser.No == "admin")
                return base.RetrieveAll();

            QueryObject qo11 = new QueryObject(this);
            qo11.AddWhere(DeptAttr.No, " like ", Web.WebUser.FK_Dept + "%");
            return qo11.DoQuery();
        }
		/// <summary>
		/// 得到一个新实体
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Dept();
			}
		}
		/// <summary>
		/// create ens
		/// </summary>
		public Depts(){}
		
	}
}
