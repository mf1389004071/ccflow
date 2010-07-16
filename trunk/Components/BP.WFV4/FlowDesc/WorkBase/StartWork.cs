using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;
 

namespace BP.WF
{

	/// <summary>
	/// 开始工作基类属性
	/// </summary>
    public class StartWorkAttr : WorkAttr
    {
        #region 可选的属性
        /// <summary>
        /// 编号（文书）
        /// </summary>
        public const string BillNo = "BillNo";
        /// <summary>
        /// 县局（用于文书编号）
        /// </summary>
        public const string FK_XJ = "FK_XJ";
        /// <summary>
        /// 部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        #endregion

        /// <summary>
        /// 工作内容标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 工作流程状态( 0, 进行中,1 完成任务, 2强制终止) 
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// 日志
        /// </summary>
        public const string WFLog = "WFLog";
    }
	/// <summary>	 
	/// 开始工作基类,所有开始工作都要从这里继承
	/// </summary>
	abstract public class StartWork : Work 
	{
        #region 与_SQLCash 操作有关
        private SQLCash _SQLCash = null;
        public override SQLCash SQLCash
        {
            get
            {
                if (_SQLCash == null)
                {
                    _SQLCash = BP.DA.Cash.GetSQL(this.NodeID.ToString());
                    if (_SQLCash == null)
                    {
                        _SQLCash = new SQLCash(this);
                        BP.DA.Cash.SetSQL(this.NodeID.ToString(), _SQLCash);
                    }
                }
                return _SQLCash;
            }
            set
            {
                _SQLCash = value;
            }
        }
        #endregion

		#region  文书属性
		/// <summary>
		/// FK_Dept
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(StartWorkAttr.FK_Dept);
			}
            set
            {
                this.SetValByKey(StartWorkAttr.FK_Dept, value);
            } 
		}
		public string FK_DeptOf2Code
		{
			get
			{
				return this.FK_Dept.Substring(6);
			} 
			 
		}
		/// <summary>
		/// FK_XJ
		/// </summary>
        public string FK_XJ
        {
            get
            {
                return this.GetValStringByKey(StartWorkAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(StartWorkAttr.FK_Dept, value);
            }
        }
		/// <summary>
		/// 文书编号
		/// </summary>
        public string BillNo
		{
			get
			{
				return this.GetValStringByKey(StartWorkAttr.BillNo);
			} 
		}
		#endregion

		#region 基本属性
		/// <summary>
		/// 日志
		/// </summary>
		public string WFLog
		{
			get
			{
				return this.GetValStringByKey(StartWorkAttr.WFLog);
			}
			set
			{
				this.SetValByKey(StartWorkAttr.WFLog,value);
			} 
		}
		/// <summary>
		/// 工作内容标题
		/// </summary>
		public string Title
		{
			get
			{
				return this.GetValStringByKey(StartWorkAttr.Title);
			}
			set
			{
				this.SetValByKey(StartWorkAttr.Title,value);
			} 
		}
		/// <summary>
		/// 工作流程状态( 0, 未完成,1 完成, 2 强制终止 3, 删除状态,) 
		/// </summary>
		public WFState WFState
		{
			get
			{
				return (WFState)this.GetValIntByKey(StartWorkAttr.WFState);
			}
			set
			{
				this.SetValByKey(StartWorkAttr.WFState,(int)value);
			}
		}
        public string WFStateT
        {
            get
            {
                switch (this.WFState)
                {
                    case WFState.Complete:
                        return "流程完成";
                    case WFState.Delete:
                        return "删除";
                    case WFState.Runing:
                        return "运行中";
                    case WFState.Stop:
                        return "停止";
                    default:
                        throw new Exception("@没有判断的状态。");
                }
            }
        }
		#endregion

		#region 构造函数
		/// <summary>
		/// 工作流程
		/// </summary>
		protected StartWork()
		{
			//this.WFState=0;
			//this.NodeState=NodeState.Init;
			//this.BelongToDept=Web.WebUser.FK_Dept;
		}
        protected StartWork(Int64 oid):base(oid)
        {

        }
		#endregion
		
		#region  重写基类的方法。
		/// <summary>
		/// 删除之前的操作。
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete() 
		{
			if (base.beforeDelete()==false)
				return false;			 
			if (this.OID < 0 )
				throw new Exception("@实体["+this.EnDesc+"]没有被实例化，不能Delete().");
			return true;
		}
		/// <summary>
		/// 插入之前的操作。
		/// </summary>
		/// <returns></returns>
        protected override bool beforeInsert()
        {
            this.InitBookNo(); //初始化文书编号

            if (this.OID > 0)
                throw new Exception("@实体[" + this.EnDesc + "], 已经被实例化，不能Insert.");

            this.SetValByKey("OID", DBAccess.GenerOID(BP.Web.WebUser.FK_Dept.Substring(2)));

            return base.beforeInsert();
        }

        protected override bool beforeUpdateInsertAction()
        {
          //  this.Emps = Web.WebUser.No;
            if (this.Title == "")
                this.Title = this.HisRec.Name + "在" + DateTime.Now.ToString("MM月dd号HH:mm") + "发起";

            return base.beforeUpdateInsertAction();
        }
		/// <summary>
		/// 更新操作
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			this.InitBookNo();
			if (base.beforeUpdate()==false)
				return false;
			if (this.OID < 0 )			
				throw new Exception("@实体["+this.EnDesc+"]没有被实例化，不能Update().");
			return base.beforeUpdate();
		}
		#endregion

		#region 方法
		/// <summary>
		/// 设置文书编号
		/// </summary>
		public void InitBookNo()
		{
			try
			{
                string no = this.GetValStringByKey(StartWorkAttr.BillNo);
				if (no=="" || no=="自动生成" || no.Length==0 || no ==null || no=="&nbsp;" )
				{
					this.EnMap.CodeStruct="4";
                    no = this.GenerNewNoByKey("BillNo", "FK_Dept", BP.Web.WebUser.FK_Dept);
					if (no ==null)
						no= BP.Web.WebUser.FK_Dept+"0001";
					this.SetValByKey("No", no);
				}
			}
			catch(Exception ex)
			{
				throw new Exception("@产生编号错误："+ex.Message);
			}
		}
		#endregion

	}
	/// <summary>
	/// 工作流程采集信息的基类 集合
	/// </summary>
	abstract public class StartWorks : Works
	{
		
		#region 抽象属性		
		/// <summary>
		/// 他的审核节点
		/// </summary>
		protected FlowCheckNodes _flowCheckNodes=null;
		/// <summary>
		/// 子类需要实现（他的审核节点）
		/// </summary>
		public abstract FlowCheckNodes FlowCheckNodes{get;}
		#endregion		

		#region 构造方法
		/// <summary>
		/// 信息采集基类
		/// </summary>
		public StartWorks()
		{
		}
		#endregion 

		#region 公共查询方法
		/// <summary>
		/// sss
		/// </summary>
		/// <param name="DeptNo"></param>
		/// <param name="wfState"></param>
		/// <returns></returns>
		public DataTable RetrieveByDeptWFStatePRI( string wfState )
		{
			//return this.RetrieveAllToTable();
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(StartWorkAttr.WFState,wfState);
			qo.DoQuery();
			//Log.DefaultLogWriteLine(LogType.Info,qo.SQL) ;
			return this.ToDataTableField();
		}

		/// <summary>
		/// 查询到我的任务.
		/// </summary>		 
		/// <returns></returns>
		public DataTable RetrieveMyTask_del(string flow)
		{
			QueryObject qo = new QueryObject(this);
			//qo.Top=50;
			qo.AddWhere(StartWorkAttr.OID," in ", " ( SELECT WorkID FROM V_WF_Msg  WHERE  FK_Flow='"+flow+"' AND FK_Emp='"+Web.WebUser.No+"' )" );
			return qo.DoQueryToTable();
		}
		/// <summary>
		/// 查询到我的任务.
		/// </summary>		 
		/// <returns></returns>
		public DataTable RetrieveMyTask(string flow)
		{
			//string sql="SELECT OID AS WORKID, TITLE, ";
			QueryObject qo = new QueryObject(this);
			//qo.Top=50;
			if (BP.SystemConfig.AppCenterDBType==DBType.Oracle9i)
				qo.AddWhere(StartWorkAttr.OID," in ", " (  SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Node IN ( SELECT FK_NODE FROM WF_GenerWorkerList WHERE FK_Emp='"+Web.WebUser.No+"' AND FK_Flow='"+flow+"' AND WORKID=WF_GenerWorkFlow.WORKID ) )" );
			else
				qo.AddWhere(StartWorkAttr.OID," in ", " (  SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Node IN ( SELECT FK_NODE FROM WF_GenerWorkerList WHERE FK_Emp='"+Web.WebUser.No+"' AND FK_Flow='"+flow+"' AND WORKID=WF_GenerWorkFlow.WORKID ) )" );
			return qo.DoQueryToTable();
		}
		/// <summary>
		/// 查询到我的任务.
		/// </summary>		 
		/// <returns></returns>
		public DataTable RetrieveMyTaskV2(string flow)
		{
			string sql="SELECT OID, TITLE, RDT, Rec FROM  "+this.GetNewEntity.EnMap.PhysicsTable +" WHERE OID IN (   SELECT WorkID FROM WF_GenerWorkFlow WHERE FK_Node IN ( SELECT FK_NODE FROM WF_GenerWorkerList WHERE FK_Emp='"+Web.WebUser.No+"' AND FK_Flow='"+flow+"' AND WORKID=WF_GenerWorkFlow.WORKID )  )";
			return DBAccess.RunSQLReturnTable(sql) ;
			/*
			QueryObject qo = new QueryObject(this);
			//qo.Top=50;
			qo.AddWhere(StartWorkAttr.OID," in ", " ( SELECT WorkID FROM V_WF_Msg  WHERE  FK_Flow='"+flow+"' AND FK_Emp="+Web.WebUser.No+")" );
			return qo.DoQueryToTable();
			*/
		}
		/// <summary>
		/// 查询到我的任务.
		/// </summary>		 
		/// <returns></returns>
		public DataTable RetrieveMyTask(string flow,string flowSort)
		{
			QueryObject qo = new QueryObject(this);
			//qo.Top=50;
			qo.AddWhere(StartWorkAttr.OID," IN ", " ( SELECT WorkID FROM V_WF_Msg  WHERE  (FK_Flow='"+flow+"' AND FK_Emp='"+Web.WebUser.No+"' ) and ( FK_Flow in ( SELECT No from WF_Flow WHERE FK_FlowSort='"+flowSort+"' )) )" );
			return qo.DoQueryToTable();			 
		}
		#endregion 
	}
}
