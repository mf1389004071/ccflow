using System;
using System.Data;
using System.Text.RegularExpressions;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;

namespace Tax666.BusinessRules
{
	/// <summary>
	/// 该类包含HomeLinkType系统的业务逻辑层。
	/// <remarks>
	///  完成插入、删除、更新等操作的业务逻辑校验和逻辑处理。
	/// </remarks>
	/// </summary>
	public class HomeLinkTypeRules
	{
		public HomeLinkTypeRules()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  根据某种规则验证数据实体HomeLinkTypeData中的指定行。
		/// </summary>/// <param name="homeLinkTypeDataRow">要验证的数据实体HomeLinkTypeData的行</param>
		/// <returns>如果某field有错误，返回false</returns>
		private bool Validate(DataRow homeLinkTypeDataRow)
		{
			bool isValid = false;
			homeLinkTypeDataRow.ClearErrors();
			isValid = IsValidField(homeLinkTypeDataRow, HomeLinkTypeData.TypeName_Field,50);

			if ( !isValid )
			{
				homeLinkTypeDataRow.RowError = HomeLinkTypeData.INVALID_FIELDS;
			}
			return isValid;
		}

		/// <summary>
		/// 根据某种规则验证HomeLinkTypeData中的某field。
		/// </summary>
		/// <param name="homeLinkTypeDataRow">要验证的HomeLinkTypeData中一行</param>
		/// <param name="fieldName">要验证的field</param>
		/// <param name="maxLen">该field的最大长度</param>
		/// <returns>如果该field不符合验证条件，返回false</returns>
		private bool IsValidField(DataRow homeLinkTypeDataRow, String fieldName, short maxLen)
		{
			short i = (short)(homeLinkTypeDataRow[fieldName].ToString().Trim().Length);
			if( (i<1) || (i>maxLen))
			{
				// 将该field标记为非法
				homeLinkTypeDataRow.SetColumnError(fieldName, HomeLinkTypeData.INVALID_FIELD);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 添加友情链接的类型
		/// </summary>
		/// <param name="homeLinkTypeData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertHomeLinkType(HomeLinkTypeData homeLinkTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(homeLinkTypeData != null,"HomeLinkTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows.Count == 1,"HomeLinkTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( HomeLinkTypeDataAccess homeLinkTypeDataAccess = new HomeLinkTypeDataAccess() )
				{
					result = homeLinkTypeDataAccess.InsertHomeLinkType(homeLinkTypeData);
				}
			}
			return result;
		}

		/// <summary>
		/// 修改指定记录
		/// </summary>
		/// <param name="homeLinkTypeData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool UpdateHomeLinkType(HomeLinkTypeData homeLinkTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(homeLinkTypeData != null,"HomeLinkTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows.Count == 1,"HomeLinkTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( HomeLinkTypeDataAccess homeLinkTypeDataAccess = new HomeLinkTypeDataAccess() )
				{
					result = homeLinkTypeDataAccess.UpdateHomeLinkType(homeLinkTypeData);
				}
			}
			return result;
		}

		/// <summary>
		/// 删除友情链接类型
		/// </summary>
		/// <param name="homeLinkTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelHomeLinkType(HomeLinkTypeData homeLinkTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(homeLinkTypeData != null,"HomeLinkTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows.Count == 1,"HomeLinkTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows[0];

			bool result;

			using( HomeLinkTypeDataAccess homeLinkTypeDataAccess = new HomeLinkTypeDataAccess() )
			{
				result = homeLinkTypeDataAccess.DelHomeLinkType(homeLinkTypeData);
			}
			return result;
		}

	}
}
