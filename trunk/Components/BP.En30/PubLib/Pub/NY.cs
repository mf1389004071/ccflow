using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// 会计期间
	/// </summary>
	public class NY :SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_NY";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
                return this.ToE("NY", "年月");// "年月";
			}
		}
		#endregion 

		#region 构造方法
		 
		public NY(){}
		 
		public NY(string _No ): base(_No){}
		
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class NYs :SimpleNoNameFixs
	{
      
		/// <summary>
		/// 会计期间集合
		/// </summary>
		public NYs()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NY();
			}
		}
		 
		 

	}
}
