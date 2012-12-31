using System;
using System.Data;
using BP.DA;
using BP.WF;
using BP.Port ; 
using BP.En;

namespace BP.WF
{
	/// <summary>
    /// 流程实例
	/// </summary>
    public class GenerWorkFlowAttr
    {
        #region 基本属性
        /// <summary>
        /// 地税编号
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 工作流
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 流程状态
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// 标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 记录人
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// 产生时间
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 完成时间
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// 得分
        /// </summary>
        public const string Cent = "Cent";
        /// <summary>
        /// note
        /// </summary>
        public const string FlowNote = "FlowNote";
        /// <summary>
        /// 当前工作到的节点.
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 当前工作岗位
        /// </summary>
        public const string FK_Station = "FK_Station";
        /// <summary>
        /// 部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 流程ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// 是否启用
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// 流程名称
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// 发起人名称
        /// </summary>
        public const string RecName = "RecName";
        /// <summary>
        /// 节点名称
        /// </summary>
        public const string NodeName = "NodeName";
        /// <summary>
        /// 部门名称
        /// </summary>
        public const string DeptName = "DeptName";
        /// <summary>
        /// 流程类别
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// 优先级
        /// </summary>
        public const string PRI = "PRI";
        /// <summary>
        /// 流程应完成时间
        /// </summary>
        public const string SDTOfFlow = "SDTOfFlow";
        /// <summary>
        /// 节点应完成时间
        /// </summary>
        public const string SDTOfNode = "SDTOfNode";
        #endregion
    }
	/// <summary>
    /// 流程实例
	/// </summary>
	public class GenerWorkFlow : Entity
	{	
		#region 基本属性
        public override string PK
        {
            get
            {
                return GenerWorkFlowAttr.WorkID;
            }
        }
		/// <summary>
		/// 工作流程编号
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Flow);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FlowName);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FlowName, value);
            }
        }
        /// <summary>
        /// 优先级
        /// </summary>
        public int PRI
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.PRI);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.PRI, value);
            }
        }
        /// <summary>
        /// 类别编号
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FK_FlowSort, value);
            }
        }
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Dept);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.Title);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.Title,value);
			}
		}
		/// <summary>
		/// 产生时间
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.RDT);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.RDT,value);
			}
		}
        /// <summary>
        /// 节点应完成时间
        /// </summary>
        public string SDTOfNode
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.SDTOfNode);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.SDTOfNode, value);
            }
        }
        /// <summary>
        /// 流程应完成时间
        /// </summary>
        public string SDTOfFlow
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.SDTOfFlow);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.SDTOfFlow, value);
            }
        }
		/// <summary>
		/// 流程ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(GenerWorkFlowAttr.WorkID);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.WorkID,value);
			}
		}
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowAttr.FID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FID, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.Rec);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.Rec, value);
            }
        }
        public string RecName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.RecName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.RecName, value);
            }
        }
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.DeptName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.DeptName, value);
            }
        }
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.NodeName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.NodeName, value);
            }
        }
		/// <summary>
		/// 当前工作到的节点
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// 工作流程状态( 0, 未完成,1 完成, 2 强制终止 3, 删除状态,) 
		/// </summary>
        public WFState WFState
        {
            get
            {
                return (WFState)this.GetValIntByKey(GenerWorkFlowAttr.WFState);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.WFState, (int)value);
            }
        }
       
        public string WFStateText
        {
            get
            {
                BP.WF.WFState ws = (WFState)this.WFState;
                switch(ws)
                {
                    case WF.WFState.Complete:
                        return "已完成";
                    case WF.WFState.Runing:
                        return "在运行";
                    case WF.WFState.HungUp:
                        return "挂起";
                    default:
                        return "未判断";
                }
            }
        }
		#endregion

		#region 构造函数
		/// <summary>
		/// 产生的工作流程
		/// </summary>
		public GenerWorkFlow()
		{
		}
        public GenerWorkFlow(Int64 workId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(GenerWorkFlowAttr.WorkID, workId);
            if (qo.DoQuery() == 0)
                throw new Exception("工作 GenerWorkFlow [" + workId + "]不存在，可能是已经完成。");
        }
        /// <summary>
        /// 执行修复
        /// </summary>
        public void DoRepair()
        { 
        }
		/// <summary>
		/// 重写基类方法
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_GenerWorkFlow");
                map.EnDesc = "流程实例";

                map.AddTBIntPK(GenerWorkFlowAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(GenerWorkFlowAttr.FID, 0, "流程ID", true, true);

                map.AddTBString(GenerWorkFlowAttr.FK_FlowSort, null, "流程类别", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.FK_Flow, null, "流程", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.FlowName, null, "流程名称", true, false, 0, 500, 10);

                map.AddTBString(GenerWorkFlowAttr.Title, null, "标题", true, false, 0, 500, 10);
                map.AddTBInt(GenerWorkFlowAttr.WFState, 0, "流程状态", true, false);

                map.AddTBString(GenerWorkFlowAttr.Rec, null, "发起人", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.RecName, null, "发起人名称", true, false, 0, 500, 10);

                map.AddTBDateTime(GenerWorkFlowAttr.RDT, "记录日期", true, true);
                map.AddTBInt(GenerWorkFlowAttr.FK_Node, 0, "节点", true, false);
                map.AddTBString(GenerWorkFlowAttr.NodeName, null, "节点名称", true, false, 0, 500, 10);

                map.AddTBString(GenerWorkFlowAttr.FK_Dept, null, "部门", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.DeptName, null, "部门名称", true, false, 0, 500, 10);
                map.AddTBInt(GenerWorkFlowAttr.PRI, 1, "优先级", true, true);

                map.AddTBDateTime(GenerWorkFlowAttr.SDTOfNode, "节点应完成时间", true, true);
                map.AddTBDateTime(GenerWorkFlowAttr.SDTOfFlow, "流程应完成时间", true, true);

                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "工作报告");  // "工作报告";
                rm.ClassMethodName = this.ToString() + ".DoRpt";
                rm.Icon = "/WF/Img/FileType/doc.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowSelfTest", "流程自检"); // "流程自检";
                rm.ClassMethodName = this.ToString() + ".DoSelfTestInfo";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "流程自检并修复";
                rm.ClassMethodName = this.ToString() + ".DoRepare";
                rm.Warning = "您确定要执行此功能吗？ \t\n 1)如果是断流程，并且停留在第一个节点上，系统为执行删除它。\t\n 2)如果是非地第一个节点，系统会返回到上次发起的位置。";
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		#region 重载基类方法
		/// <summary>
		/// 删除后,需要把工作者列表也要删除.
		/// </summary>
        protected override void afterDelete()
        {
            // . clear bad worker .  
            DBAccess.RunSQLReturnTable("DELETE FROM WF_GenerWorkerlist WHERE WorkID in  ( select WorkID from WF_GenerWorkerlist WHERE WorkID not in (select WorkID from WF_GenerWorkFlow) )");

            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID,this.FID);
            wf.DoDeleteWorkFlowByReal(); /* 删除下面的工作。*/
            base.afterDelete();
        }
		#endregion 

		#region 执行诊断
        public string DoRpt()
        {
            PubClass.WinOpen("WFRpt.aspx?WorkID=" + this.WorkID + "&FID="+this.FID+"&FK_Flow="+this.FK_Flow);
            return null;
        }
		/// <summary>
		/// 执行修复
		/// </summary>
		/// <returns></returns>
        public string DoRepare()
        {
            if (this.DoSelfTestInfo() == "没有发现异常。")
                return "没有发现异常。";

            string sql = "SELECT FK_Node FROM WF_GenerWorkerlist WHERE WORKID='" + this.WorkID + "' ORDER BY FK_Node desc";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                /*如果是开始工作节点，就删除它。*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID );
                wf.DoDeleteWorkFlowByReal();
                return "此流程是因为发起工作失败被系统删除。";
            }

            int FK_Node = int.Parse(dt.Rows[0][0].ToString());

            Node nd = new Node(FK_Node);
            if (nd.IsStartNode)
            {
                /*如果是开始工作节点，就删除它。*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
                wf.DoDeleteWorkFlowByReal();
                return "此流程是因为发起工作失败被系统删除。";
            }

            this.FK_Node = nd.NodeID;
            this.NodeName = nd.Name;
            this.Update();

            string str = "";
            GenerWorkerLists wls = new GenerWorkerLists();
            wls.Retrieve(GenerWorkerListAttr.FK_Node, FK_Node, GenerWorkerListAttr.WorkID, this.WorkID);
            foreach (GenerWorkerList wl in wls)
            {
                str += wl.FK_Emp + wl.FK_EmpText + ",";
            }

            return "此流程是因为[" + nd.Name + "]工作发送失败被回滚到当前位置，请转告[" + str + "]流程修复成功。";
        }
		public string DoSelfTestInfo()
		{
            GenerWorkerLists wls = new GenerWorkerLists(this.WorkID, this.FK_Flow);

			#region  查看一下当前的节点是否开始工作节点。
			Node nd = new Node(this.FK_Node);
			if (nd.IsStartNode)
			{
				/* 判断是否是退回的节点 */
				Work wk = nd.HisWork;
				wk.OID = this.WorkID;
				wk.Retrieve();

				if (wk.NodeState!=NodeState.Back)
				{
					return "当前的工作节点 存在于开始工作节点上 还不是退回流程 不应该出现的情况。建议删除当前的工作节点。 ";
				}
			}
			#endregion


			#region  查看一下是否有当前的工作节点信息。
			bool isHave=false;
            foreach (GenerWorkerList wl in wls)
			{
				if (wl.FK_Node==this.FK_Node)
					isHave=true;
			}

			if (isHave==false)
			{
				/*  */
				return "已经不存在当前的工作节点信息，造成此流程的原因可能是没有捕获的系统异常，建议删除此流程或者交给系统自动修复它。";
			}
			#endregion

			return "没有发现异常。";
		}
		#endregion
	}
	/// <summary>
    /// 流程实例s
	/// </summary>
	public class GenerWorkFlows : Entities
	{
		/// <summary>
		/// 根据工作流程,工作人员 ID 查询出来他当前的能做的工作.
		/// </summary>
		/// <param name="flowNo">流程编号</param>
		/// <param name="empId">工作人员ID</param>
		/// <returns></returns>
		public static DataTable QuByFlowAndEmp(string flowNo, int empId)
		{
			string sql="SELECT a.WorkID FROM WF_GenerWorkFlow a, WF_GenerWorkerlist b WHERE a.WorkID=b.WorkID   AND b.FK_Node=a.FK_Node  AND b.FK_Emp='"+empId.ToString()+"' AND a.FK_Flow='"+flowNo+"'";
			return DBAccess.RunSQLReturnTable(sql);
		}
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new GenerWorkFlow();
			}
		}
		/// <summary>
		/// 流程实例集合
		/// </summary>
		public GenerWorkFlows(){}
		#endregion
	}
	
}
