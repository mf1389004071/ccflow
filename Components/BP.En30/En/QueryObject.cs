using System;
using System.Data;
using BP.DA;
using BP.En;
//using BP.En.MBase;


namespace BP.En
{
	
	/// <summary>
	/// QueryObject 的摘要说明。
	/// </summary>
	public class QueryObject
	{
		private Entity _en =null;
		private Entities _ens=null;
		private string _sql = "";
		private Entity En
		{
			get
			{
				if (this._en == null)
					return this.Ens.GetNewEntity;
				else
					return this._en;
			}
			set
			{
				this._en = value;
			}
		}
		private Entities Ens
		{
			get
			{
				return this._ens;
			}
			set
			{
				this._ens=value;
			}
		}
		/// <summary>
		/// 处理Order by , group by . 
		/// </summary>
		private string _groupBy="";
		/// <summary>
		/// 要得到的查询sql 。
		/// </summary>
        public string SQL
        {
            get
            {
                string sql = "";
                string selecSQL = SqlBuilder.SelectSQL(this.En, this.Top);
                if (this._sql == null || this._sql.Length == 0)
                    sql = selecSQL + this._groupBy + this._orderBy;
                else
                    sql = selecSQL + "  AND ( " + this._sql + " ) " + _groupBy + this._orderBy;


                sql = sql.Replace("  ", " ");
                sql = sql.Replace("  ", " ");

                sql = sql.Replace("WHERE AND", "WHERE");
                sql = sql.Replace("WHERE  AND", "WHERE");

                sql = sql.Replace("WHERE ORDER", "ORDER");
                return sql;
            }
            set
            {
                if (value.IndexOf("(") == -1)
                    this.IsEndAndOR = false;
                else
                    this.IsEndAndOR = true;

                this._sql = this._sql + " " + value;
            }
        }
        public string SQLWithOutPara
        {
            get
            {
                string sql = this.SQL;
                foreach (Para en in this.MyParas)
                {
                    sql = sql.Replace("@" + en.ParaName, "'"+en.val.ToString()+"'" );
                }
                return sql;
            }
        }
        public void AddWhere(string str)
        {
            this._sql = this._sql + " " + str;
        }
        /// <summary>
        /// 修改于2009 -05-12 
        /// </summary>
		private int _Top = -1;
		public int Top
		{
			get
			{
				return _Top;
			}
			set
			{
				this._Top=value;
			}		
		}
        private Paras _Paras = null;
        public Paras MyParas
        {
            get
            {
                if (_Paras == null)
                    _Paras = new Paras();
                return _Paras;
            }
            set
            {
                _Paras = value;
            }
        }
        private Paras _ParasR = null;
        public Paras MyParasR
        {
            get
            {
                if (_ParasR == null)
                    _ParasR = new Paras();
                return _ParasR;
            }
        }
        public void AddPara(string key, object v)
        {
            key = "P" + key;
            this.MyParas.Add(key, v);
        }
        public QueryObject()
        {
        }
		/// DictBase
        public QueryObject(Entity en)
        {
            this.MyParas.Clear();
            this._en = en;
            this.HisDBType = this._en.EnMap.EnDBUrl.DBType;
        }
		public QueryObject(Entities ens)
		{
            this.MyParas.Clear();
			ens.Clear();
			this._ens =ens;
            this.HisDBType = this._ens.GetNewEntity.EnMap.EnDBUrl.DBType;
		}
        public BP.DA.DBType HisDBType = DBType.SQL2000;
        public string HisVarStr
        {
            get
            {
                switch (this.HisDBType)
                {
                    case DBType.SQL2000:
                    case DBType.Access:
                        return "@";
                    default:
                        return ":";
                }
            }
        }
		/// <summary>
		/// 增加函数查寻．
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="exp">表达格式 大于，等于，小于</param>
		/// <param name="len">长度</param>
        public void AddWhereLen(string attr, string exp, int len, BP.DA.DBType dbtype)
        {
            switch (dbtype)
            {
                case DBType.Oracle9i:
                    this.SQL = "( LENGTH( " + attr2Field(attr) + " ) " + exp + " '" + len.ToString() + "')";
                    break;
                default:
                    this.SQL = "( LEN( " + attr2Field(attr) + " ) " + exp + " '" + len.ToString() + "')";
                    break;
            }
        }
		/// <summary>
		/// 增加查询条件，条件用 IN 表示．sql必须是一个列的集合．
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="sql">此sql,必须是有一个列的集合．</param>
        public void AddWhereInSQL(string attr, string sql)
        {
            this.AddWhere(attr, " IN ", "( " + sql + " )");
        }
		/// <summary>
		/// 增加查询条件，条件用 IN 表示．sql必须是一个列的集合．
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="sql">此sql,必须是有一个列的集合．</param>
		public void AddWhereNotInSQL(string attr, string sql)  
		{
			this.AddWhere(attr, " NOT IN ", " ( "+sql+" ) " );
		}
		public void AddWhereNotIn(string attr, string val)  
		{
			this.AddWhere(attr, " NOT IN ", " ( "+val+" ) " );
		}
		/// <summary>
		/// 增加条件, DataTable 第一列的值．
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="dt">第一列是要组合的values</param>
        public void AddWhereIn(string attr, DataTable dt)
        {
            string strs = "";
            foreach (DataRow dr in dt.Rows)
            {
                strs += dr[0].ToString() + ",";
            }
            strs = strs.Substring(strs.Length - 1, 0);
            this.AddWhereIn(attr, strs);
        }
		/// <summary>
		/// 增加条件,vals 必须是sql可以识别的字串．
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="vals">用 , 分开的．</param>
		public void AddWhereIn(string attr, string vals)  
		{
			this.AddWhere(attr, " IN ",vals);
		}
		/// <summary>
		/// 增加条件
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="exp">操作符号（根据不同的数据库）</param>
		/// <param name="val">值</param>
        public void AddWhere(string attr, string exp, string val)
        {
            if (val == null)
                val = "";

            if (val == "all")
            {
                this.SQL = "( 1=1 )";
                return;
            }

            if (exp.ToLower().Contains(" in"))
            {
                this.SQL = "( " + attr2Field(attr) + " " + exp + "  " + val + " )";
                return;
            }

            if (exp.ToLower().Contains("like"))
            {
                if (attr == "FK_Dept")
                {
                    val = val.Replace("'", "");
                    val = val.Replace("%", "");

                    switch (this.HisDBType)
                    {
                        case DBType.Oracle9i:
                            this.SQL = "( " + attr2Field(attr) + " " + exp + " '%'||" + this.HisVarStr + "FK_Dept||'%' )";
                            this.MyParas.Add("FK_Dept", val);
                            break;
                        default:
                            //this.SQL = "( " + attr2Field(attr) + " " + exp + "  '" + this.HisVarStr + "FK_Dept%' )";
                              this.SQL = "( " + attr2Field(attr) + " " + exp + "  '"+val+"%' )";
                            //this.MyParas.Add("FK_Dept", val);
                            break;
                    }
                }
                else
                {
                    if (val.Contains(":") || val.Contains("@"))
                        this.SQL = "( " + attr2Field(attr) + " " + exp + "  " + val + " )";
                    else
                    {
                        if (val.Contains("'") == false)
                            this.SQL = "( " + attr2Field(attr) + " " + exp + "  '" + val + "' )";
                        else
                            this.SQL = "( " + attr2Field(attr) + " " + exp + "  " + val + " )";
                    }
                }
                return;
            }
            this.SQL = "( " + attr2Field(attr) + " " + exp +  this.HisVarStr + attr + ")";
            this.MyParas.Add(attr, val);
        }
        public void AddWhereDept(string val)
        {
            string attr = "FK_Dept";
            string exp = "=";

            if (val.Contains("'") == false)
                this.SQL = "( " + attr2Field(attr) + " " + exp + "  '" + val + "' )";
            else
                this.SQL = "( " + attr2Field(attr) + " " + exp + "  " + val + " )";
        }

        public void AddWhereField(string attr, string exp, string val)
        {
            if (val.ToString() == "all")
            {
                this.SQL = "( 1=1 )";
                return;
            }

            if (exp.ToLower().Contains(" in"))
            {
                this.SQL = "( " + attr + " " + exp + "  " + val + " )";
                return;
            }

            this.SQL = "( " + attr + " " + exp + " :" + attr + " )";
            this.MyParas.Add(attr, val);
        }
        public void AddWhereField(string attr, string exp, int val)
        {
            if (val.ToString() == "all")
            {
                this.SQL = "( 1=1 )";
                return;
            }

            if (exp.ToLower().Contains(" in"))
            {
                this.SQL = "( " + attr + " " + exp + "  " + val + " )";
                return;
            }

            if (attr == "RowNum")
            {
                this.SQL = "( " + attr + " " + exp + "  " + val + " )";
                return;
            }

            this.SQL = "( " + attr + " " + exp + "  " + this.HisVarStr +  attr + " )";
            this.MyParas.Add(attr, val);
        }
		/// <summary>
		/// 增加条件
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="exp">操作符号（根据不同的数据库）</param>
		/// <param name="val">值</param>
		public void AddWhere(string attr, string exp, int val)  
		{
            if (attr == "RowNum")
            {
                this.SQL = "( " + attr2Field(attr) + " " + exp + " " + val + ")";
            }
            else
            {
                this.SQL = "( " + attr2Field(attr) + " " + exp + this.HisVarStr+ attr + ")";
                this.MyParas.Add(attr, val);
            }
		}
		public void AddHD()
		{
			this.SQL="(  1=1 ) ";
		}
        /// <summary>
        /// 非恒等。
        /// </summary>
        public void AddHD_Not()
        {
            this.SQL = "(  1=2 ) ";
        }
		/// <summary>
		/// 增加条件
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="exp">操作符号（根据不同的数据库）</param>
		/// <param name="val">值</param>
        public void AddWhere(string attr, string exp, float val)
        {
            this.MyParas.Add(attr, val);
            this.SQL = "( " + attr2Field(attr) + " " + exp + " "+this.HisVarStr + attr + ")";
        }
		/// <summary>
		/// 增加条件(默认的是= )
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="val">值</param>
        public void AddWhere(string attr, string val)
        {
            this.AddWhere(attr, "=", val);
        }
        public void AddWhere(string attr, int val)
        {
            this.AddWhere(attr, "=", val);
        }
		/// <summary>
		/// 增加条件
		/// </summary>
		/// <param name="attr">属性</param>
		/// <param name="val">值 true/false</param>
		public void AddWhere(string attr, bool val)  
		{
			if (val)
				this.AddWhere(attr, "=", 1);
			else
				this.AddWhere(attr, "=", 0);
		}
        public void AddWhere(string attr, Int64 val)
        {
            this.AddWhere(attr,  val.ToString() );
        }

		public void AddWhere(string attr, float val)  
		{
			this.AddWhere(attr, "=", val);
		}

		public void AddWhere(string attr, object val)  
		{
            if (val == null)
                throw new Exception("Attr="+attr+", val is null");

            if (val.GetType() == typeof(int) || val.GetType() == typeof(Int32) )
			{
				//int i = int.Parse(val.ToString()) ;
				this.AddWhere(attr, "=", (int)val);
				return;
			} 
			this.AddWhere(attr, "=", val.ToString() );
		}

		public void addLeftBracket()
		{
			this.SQL=" ( ";
		}

		public void addRightBracket() 
		{
			this.SQL=" ) ";
			this.IsEndAndOR=true;
		}
		 
		public void addAnd() 
		{
			this.SQL=" AND ";
		}

		public void addOr() 
		{
			this.SQL=" OR ";

		}

		#region 关于endsql
		public void addGroupBy(string attr)  
		{
			this._groupBy=" GROUP BY  " + attr2Field(attr);
		}

		public void addGroupBy(string attr1, string attr2)
		{
			this._groupBy=" GROUP BY  "+attr2Field(attr1) + " , " + attr2Field(attr2);
		}

		private string _orderBy="";
		public void addOrderBy(string attr)
		{
			if (this._orderBy.IndexOf("ORDER BY")!=-1)
			{
				this._orderBy=" , "+attr2Field(attr);
			}
			else
			{
				this._orderBy=" ORDER BY "+attr2Field(attr);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="attr"></param>
		public void addOrderByRandom() 
		{
			if (this._orderBy.IndexOf("ORDER BY")!=-1)
			{
				this._orderBy=" , NEWID()";
			}
			else
			{
				this._orderBy=" ORDER BY NEWID()";
			}
		}
		/// <summary>
		/// addOrderByDesc
		/// </summary>
		/// <param name="attr"></param>
		/// <param name="desc"></param>
		public void addOrderByDesc(string attr) 
		{
			this._orderBy=" ORDER BY "+attr2Field(attr)+" DESC ";
		}
        public void addOrderByDesc(string attr1, string attr2)
        {
            this._orderBy = " ORDER BY  " + this.HisMap.PhysicsTable + "." + attr1 + " DESC ," + this.HisMap.PhysicsTable + "." + attr2 + " DESC";
        }

//		public void addOrderBy(string attr1, string attr2)
//		{
//			this._endSql=" ORDER BY  "+attr2Field(attr1) + ","+attr2Field(attr2);
//		}

		public void addOrderBy(string attr1,  string attr2)
		{
            this._orderBy = " ORDER BY  " + this.HisMap.PhysicsTable + "." + attr1 + "," + this.HisMap.PhysicsTable + "." + attr2;
		}
		#endregion

		public void addHaving(){}
		/// 清除查询条件
		public void clear() 
		{
			this._sql = "";
			this._groupBy="";
			this._orderBy="";
            this.MyParas.Clear();
		}
        private Map _HisMap;
        public Map HisMap
        {
            get
            {
                if (_HisMap == null)
                    _HisMap = this.En.EnMap;
                return _HisMap;
            }
            set
            {
                _HisMap = value;
            }
        }
		private string attr2Field(string attr)
		{
            return this.HisMap.PhysicsTable + "." + this.HisMap.GetFieldByKey(attr);
		}
        public DataTable DoGroupReturnTable(Entity en, Attrs attrsOfGroupKey, Attr attrGroup, GroupWay gw, OrderWay ow)
        {
            switch (en.EnMap.EnDBUrl.DBType)
            {
                case DBType.Oracle9i:
                    return DoGroupReturnTableOracle(en, attrsOfGroupKey, attrGroup, gw, ow);
                default:
                    return DoGroupReturnTableSqlServer(en, attrsOfGroupKey, attrGroup, gw, ow);
            }
        }

        public DataTable DoGroupReturnTableOracle(Entity en, Attrs attrsOfGroupKey, Attr attrGroup, GroupWay gw, OrderWay ow )
        {
            #region  生成要查询的语句
            string fields = "";
            string str = "";
            foreach (Attr attr in attrsOfGroupKey)
            {
                if (attr.Field == null)
                    continue;

                str = "," + attr.Field;
                fields += str;
            }

            if (attrGroup.Key == "MyNum")
            {
                switch (gw)
                {
                    case GroupWay.BySum:
                        fields += ", COUNT(*) AS MyNum";
                        break;
                    case GroupWay.ByAvg:
                        fields += ", AVG(" + attrGroup.Field + ") AS MyNum";
                        break;
                    default:
                        throw new Exception("no such case:");
                }
            }
            else
            {
                switch (gw)
                {
                    case GroupWay.BySum:
                        fields += ",SUM(" + attrGroup.Field + ") AS " + attrGroup.Key;
                        break;
                    case GroupWay.ByAvg:
                        fields += ",AVG(" + attrGroup.Field + ") AS " + attrGroup.Key;
                        break;
                    default:
                        throw new Exception("no such case:");
                }
            }

            string by = "";
            foreach (Attr attr in attrsOfGroupKey)
            {
                if (attr.Field == null)
                    continue;

                str = "," + attr.Field;
                by += str;
            }
            by = by.Substring(1);
            //string sql 
            string sql = "SELECT " + fields.Substring(1) + " FROM " + this.En.EnMap.PhysicsTable + " WHERE " + this._sql + " Group BY " + by;
            #endregion

            #region
            Map map = new Map();
            map.PhysicsTable = "@VT@";
            map.Attrs = attrsOfGroupKey;
            map.Attrs.Add(attrGroup);
            #endregion .

            string sql1 = SqlBuilder.SelectSQLOfOra(en.ToString(), map) + " " + SqlBuilder.GenerFormWhereOfOra(en, map);

            sql1 = sql1.Replace("@TopNum", "");
            sql1 = sql1.Replace("FROM @VT@", "FROM (" + sql + ") VT");
            sql1 = sql1.Replace("@VT@", "VT");
            sql1 = sql1.Replace("TOP", "");

            if (ow == OrderWay.OrderByUp)
                sql1 += " ORDER BY " + attrGroup.Key + " DESC ";
            else
                sql1 += " ORDER BY " + attrGroup.Key;

            return DBAccess.RunSQLReturnTable(sql1, this.MyParas);
        }

		public DataTable DoGroupReturnTableSqlServer(Entity en, Attrs attrsOfGroupKey, Attr attrGroup, GroupWay gw, OrderWay ow)
		{

			#region  生成要查询的语句
			string fields="";
			string str="";
			foreach(Attr attr in attrsOfGroupKey)
			{
				if (attr.Field==null)
					continue;
				str=","+attr.Field  ;
				fields+=str;
			}

			if (attrGroup.Key=="MyNum")
			{
				switch(gw)
				{
					case GroupWay.BySum:
						fields+=", COUNT(*) AS MyNum";
						break;
					case GroupWay.ByAvg:
						fields+=", AVG(*)   AS MyNum";
						break;
					default:
						throw new Exception("no such case:");
				}
			}
			else
			{
				switch(gw)
				{
					case GroupWay.BySum:
						fields+=",SUM("+attrGroup.Field+") AS "+attrGroup.Key;
						break;
					case GroupWay.ByAvg:
						fields+=",AVG("+attrGroup.Field+") AS "+attrGroup.Key;
						break;
					default:
						throw new Exception("no such case:");
				}
			}

			string by="";
			foreach(Attr attr in attrsOfGroupKey)
			{
				if (attr.Field==null)
					continue;

				str=","+attr.Field ;
				by+=str;
			}
			by=by.Substring(1);
			//string sql 
			string sql="SELECT "+fields.Substring(1)+" FROM "+this.En.EnMap.PhysicsTable+" WHERE "+this._sql+" Group BY "+by;
			#endregion

			#region
			Map map = new Map();
			map.PhysicsTable= "@VT@" ;
			map.Attrs=attrsOfGroupKey;
			map.Attrs.Add( attrGroup );
			#endregion .
			//string sql1=SqlBuilder.SelectSQLOfMS( map )+" "+SqlBuilder.GenerFormWhereOfMS( en,map) + "   AND ( " + this._sql+" ) "+_endSql;

			string sql1=SqlBuilder.SelectSQLOfMS( map )+" "+SqlBuilder.GenerFormWhereOfMS( en,map);
 
			sql1=sql1.Replace("@TopNum", "" ) ;

			sql1=sql1.Replace( "FROM @VT@", "FROM ("+sql+") VT");

			sql1=sql1.Replace( "@VT@", "VT");
			sql1=sql1.Replace( "TOP", "");
			if (ow==OrderWay.OrderByUp)
				sql1+=" ORDER BY "+attrGroup.Key +" DESC ";
			else
				sql1+=" ORDER BY "+attrGroup.Key ;
            return DBAccess.RunSQLReturnTable(sql1, this.MyParas);
		}
		/// <summary>
		/// 分组查询，返回datatable.
		/// </summary>
		/// <param name="attrsOfGroupKey"></param>
		/// <param name="groupValField"></param>
		/// <param name="gw"></param>
		/// <returns></returns>
		public DataTable DoGroupReturnTable1(Entity en, Attrs attrsOfGroupKey, Attr attrGroup, GroupWay gw, OrderWay ow)
		{
			#region  生成要查询的语句
			string fields="";
			string str="";
            foreach (Attr attr in attrsOfGroupKey)
            {
                if (attr.Field == null)
                    continue;
                str = "," + attr.Field;
                fields += str;
            }

			if (attrGroup.Key=="MyNum")
			{
				switch(gw)
				{
					case GroupWay.BySum:
						fields+=", COUNT(*) AS MyNum";
						break;
					case GroupWay.ByAvg:
						fields+=", AVG(*)   AS MyNum";
						break;
					default:
						throw new Exception("no such case:");
				}
			}
			else
			{
				switch(gw)
				{
					case GroupWay.BySum:
						fields+=",SUM("+attrGroup.Field+") AS "+attrGroup.Key;
						break;
					case GroupWay.ByAvg:
						fields+=",AVG("+attrGroup.Field+") AS "+attrGroup.Key;
						break;
					default:
						throw new Exception("no such case:");
				}
			}

			string by="";
			foreach(Attr attr in attrsOfGroupKey)
			{
				if (attr.Field==null)
					continue;

				str=","+attr.Field ;
				by+=str;
			}
			by=by.Substring(1);
			//string sql 
			string sql="SELECT "+fields.Substring(1)+" FROM "+this.En.EnMap.PhysicsTable+" WHERE "+this._sql+" Group BY "+by;
			#endregion

			#region
			Map map = new Map();
			map.PhysicsTable= "@VT@" ;
			map.Attrs=attrsOfGroupKey;
			map.Attrs.Add( attrGroup );
			#endregion .

			//string sql1=SqlBuilder.SelectSQLOfMS( map )+" "+SqlBuilder.GenerFormWhereOfMS( en,map) + "   AND ( " + this._sql+" ) "+_endSql;

			string sql1=SqlBuilder.SelectSQLOfMS( map )+" "+SqlBuilder.GenerFormWhereOfMS( en,map);
 
			sql1=sql1.Replace("@TopNum", "" ) ;
			sql1=sql1.Replace( "FROM @VT@", "FROM ("+sql+") VT");
			sql1=sql1.Replace( "@VT@", "VT");
			sql1=sql1.Replace( "TOP", "");
			if (ow==OrderWay.OrderByUp)
				sql1+=" ORDER BY "+attrGroup.Key +" DESC ";
			else
				sql1+=" ORDER BY "+attrGroup.Key ;
			return DBAccess.RunSQLReturnTable( sql1  );
		}
		/// <summary>
		/// 在尾部上是否执行了 AddAnd()方法。
		/// </summary>
		public bool IsEndAndOR=false;
        public string[] FullAttrs = null;
		/// <summary>
		/// 执行查询
		/// </summary>
		/// <returns></returns>
        public int DoQuery()
        {
            try
            {
                if (this._en == null)
                {
                    return this.doEntitiesQuery();
                }
                else
                    return this.doEntityQuery();
            }
            catch (Exception ex)
            {
                try
                {
                    if (this._en == null)
                        this.Ens.GetNewEntity.CheckPhysicsTable();
                    else
                        this._en.CheckPhysicsTable();
                }
                catch
                {
                }
                throw ex;
            }
        }
        public string DealString(string sql)
        {
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            string strs = "";
            foreach (DataRow dr in dt.Rows)
            {
                strs += ",'" + dr[0].ToString() + "'";
            }
            return strs.Substring(1);
        }
        public string GenerPKsByTableWithPara(string pk,string sql, int from, int to)
        {
            //Log.DefaultLogWriteLineWarning(" ***************************** From= " + from + "  T0" + to);
            DataTable dt = DBAccess.RunSQLReturnTable(sql, this.MyParas);
            string pks = "";
            int i = 0;
            int paraI = 0;
            foreach (DataRow dr in dt.Rows)
            {
                i++;
                if (i > from)
                {
                    paraI++;
                    //pks += "'" + dr[0].ToString() + "'";
                    pks += SystemConfig.AppCenterDBVarStr+"R" + paraI + ",";
                    this.MyParasR.Add("R" + paraI, dr[0].ToString());
                    if (i >= to)
                        return pks.Substring(0, pks.Length - 1);
                }
            }
            if (pks == "")
            {
                return null;
                //return " '1'  ";
                return "  ";
            }
            return pks.Substring(0, pks.Length - 1);
        }
        public string GenerPKsByTable(string sql, int from, int to)
        {
            //Log.DefaultLogWriteLineWarning(" ***************************** From= " + from + "  T0" + to);
            DataTable dt = DBAccess.RunSQLReturnTable(sql,this.MyParas);
            string pks = "";
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                i++;
                if (i > from)
                {
                    if (i >= to)
                    {
                        pks += "'" + dr[0].ToString() + "'";
                        return pks;
                    }
                    else
                        pks += "'" + dr[0].ToString() + "',";
                }
            }
            if (pks == "")
                return "  '11111111' ";
            return pks.Substring(0, pks.Length-1);
        }
       
        public int DoQuery(string pk, int pageSize, int pageIdx)
        {
            if (pk == "OID")
                return DoQuery(pk, pageSize, pageIdx, pk,true);
            else
                return DoQuery(pk, pageSize, pageIdx, pk, false);
        }
        
        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="pk">主键</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIdx">第x页</param>
        /// <param name="orderby">排序</param>
        /// <param name="orderway">排序方式: 两种情况 Down UP </param>
        /// <returns>查询结果</returns>
        public int DoQuery(string pk, int pageSize, int pageIdx, string orderBy, string orderWay)
        {
            if (orderWay.ToLower().Trim() == "up")
                return DoQuery(pk, pageSize, pageIdx, orderBy, false);
            else
                return DoQuery(pk, pageSize, pageIdx, orderBy, true);
        }
        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="pk">主键</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIdx">第x页</param>
        /// <param name="orderby">排序</param>
        /// <returns>查询结果</returns>
        public int DoQuery(string pk, int pageSize, int pageIdx, bool isDesc)
        {
            return DoQuery(pk, pageSize, pageIdx, pk, isDesc);
        }
        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="pk">主键</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIdx">第x页</param>
        /// <param name="orderby">排序</param>
        /// <param name="orderway">排序方式: 两种情况 desc 或者 为 null. </param>
        /// <returns>查询结果</returns>
        public int DoQuery(string pk, int pageSize, int pageIdx, string orderBy, bool isDesc)
        {
            this._orderBy = "";

            string isDescStr = "";
            if (isDesc)
                isDescStr = " DESC ";


            if (isDesc)
                this.addOrderByDesc(pk);
            else
                this.addOrderBy(pk);

            int pageNum = 0;
            try
            {
                if (this._en == null)
                {
                    int recordConut = 0;
                    recordConut = this.GetCount(); // 获取 它的数量。

                    decimal pageCountD = decimal.Parse(recordConut.ToString()) / decimal.Parse(pageSize.ToString()); // 页面个数。
                    string[] strs = pageCountD.ToString("0.0000").Split('.');
                    if (int.Parse(strs[1]) > 0)
                        pageNum = int.Parse(strs[0]) + 1;
                    else
                        pageNum = int.Parse(strs[0]);

                    int myleftCount = recordConut - (pageNum * pageSize);

                    pageNum++;
                    int top = pageSize * (pageIdx - 1);

                    //	string wheresql=this.SQL;
                    //					int i =wheresql.IndexOf("WHERE EnumKey='");
                    //					int i =wheresql.IndexOf("WHERE (1=1)");
                    //					int i =wheresql.IndexOf("WHERE (1=1)");

                    string sql = "";
                    Entity en = this._ens.GetNewEntity;
                    Map map = en.EnMap;
                    int toIdx = 0;
                    string pks = "";
                    switch (map.EnDBUrl.DBType)
                    {
                        case DBType.Oracle9i:
                            // sql = "SELECT  " + pk + " FROM " + map.PhysicsTable + " WHERE  order by  " + pk + isDesc;
                            toIdx = top + pageSize;
                            if (this._sql == "" || this._sql == null)
                            {
                                if (top == 0)
                                    sql = "SELECT * FROM ( SELECT  " + pk + " FROM " + map.PhysicsTable + "  order by " + orderBy + isDescStr + "   ) WHERE ROWNUM <=" + pageSize;
                                else
                                    sql = "SELECT * FROM ( SELECT  " + pk + " FROM " + map.PhysicsTable + "  order by " + orderBy + isDescStr + ") ";
                            }
                            else
                            {
                                if (top == 0)
                                    sql = "SELECT * FROM ( SELECT  " + pk + " FROM " + map.PhysicsTable + " WHERE " + this._sql + " order by " + orderBy + isDescStr + "   )  WHERE ROWNUM <=" + pageSize;
                                else
                                    sql = "SELECT * FROM ( SELECT  " + pk + " FROM " + map.PhysicsTable + " WHERE " + this._sql + " order by " + orderBy + isDescStr + "   ) ";
                            }

                            sql = sql.Replace("AND ( ( 1=1 ) )", " ");

                            pks = this.GenerPKsByTableWithPara(pk, sql, top, top + pageSize);
                            this.clear();
                            this.MyParas = this.MyParasR;
                            if (pks != null)
                                this.AddWhereIn(pk, "(" + pks + ")");
                            else
                                this.AddHD();

                            if (isDesc)
                                this.addOrderByDesc(pk);
                            else
                                this.addOrderBy(pk);

                            this.Top = pageSize;
                            return this.doEntitiesQuery();
                        default:
                            // sql = "SELECT  " + pk + " FROM " + map.PhysicsTable + " WHERE  order by  " + pk + isDesc;
                            toIdx = top + pageSize;
                            if (this._sql == "" || this._sql == null)
                            {
                                if (top == 0)
                                    sql = " SELECT top " + pageSize + "  [" + pk + "] FROM " + map.PhysicsTable + "  order by " + orderBy + isDescStr;
                                else
                                    sql = " SELECT  [" + pk + "] FROM " + map.PhysicsTable + "  order by " + orderBy + isDescStr;
                            }
                            else
                            {
                                if (top == 0)
                                    sql = "SELECT TOP " + pageSize + " [" + pk + "] FROM " + map.PhysicsTable + " WHERE " + this._sql + " order by " + orderBy + isDescStr;
                                else
                                    sql = "SELECT  [" + pk + "] FROM " + map.PhysicsTable + " WHERE " + this._sql + " order by " + orderBy + isDescStr;
                            }

                            sql = sql.Replace("AND ( ( 1=1 ) )", " ");

                            pks = this.GenerPKsByTableWithPara(pk, sql, top, top + pageSize);
                            this.clear();
                            this.MyParas = this.MyParasR;

                            if (pks == null)
                                this.AddHD_Not();
                            else
                                this.AddWhereIn(pk, "(" + pks + ")");

                            if (isDesc)
                                this.addOrderByDesc(orderBy);
                            else
                                this.addOrderBy(orderBy);

                            this.Top = pageSize;
                            return this.doEntitiesQuery();
                    }
                }
                else
                    return this.doEntityQuery();
            }
            catch (Exception ex)
            {
                try
                {
                    if (this._en == null)
                        this.Ens.GetNewEntity.CheckPhysicsTable();
                    else
                        this._en.CheckPhysicsTable();
                }
                catch
                {
                }
                throw ex;
            }
        }
        /// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public DataTable DoQueryToTable()
		{
			try
			{
				string sql=this.SQL;
				sql=sql.Replace("WHERE (1=1) AND ( AND ( ( ( 1=1 ) ) AND ( ( 1=1 ) ) ) )","");

				return  DBAccess.RunSQLReturnTable(sql,this.MyParas);
			}
			catch(Exception ex)
			{
				if (this._en == null)
					this.Ens.GetNewEntity.CheckPhysicsTable();
				else
					this._en.CheckPhysicsTable();
				throw ex;
			}
		}
		/// <summary>
		/// 得到返回的数量
		/// </summary>
		/// <returns>得到返回的数量</returns>
        public int GetCount()
		{
			string sql=this.SQL;
			//sql="SELECT COUNT(*) "+sql.Substring(sql.IndexOf("FROM") ) ;
            string ptable = this.En.EnMap.PhysicsTable;
            string pk = this.En.PK;

            switch (this.En.EnMap.EnDBUrl.DBType)
            {
                case DBType.Oracle9i:
                    if (this._sql == "" || this._sql == null)
                        sql = "SELECT COUNT(" + ptable + "." + pk + ") as C FROM " + ptable;
                    else
                        sql = "SELECT COUNT(" + ptable + "." + pk + ") as C " + sql.Substring(sql.IndexOf("FROM "));
                    break;
                default:
                    if (this._sql == "" || this._sql == null)
                        sql = "SELECT COUNT(" + ptable + "." + pk + ") as C FROM " + ptable;
                    else
                        sql = "SELECT COUNT(" + ptable + "." + pk + ") as C FROM " + ptable + " WHERE " + this._sql;

                    //sql="SELECT COUNT(*) as C "+this._endSql  +sql.Substring(  sql.IndexOf("FROM ") ) ;
                    //sql="SELECT COUNT(*) as C FROM "+ this._ens.GetNewEntity.EnMap.PhysicsTable+ "  " +sql.Substring(sql.IndexOf("WHERE") ) ;
                    //int i = sql.IndexOf("ORDER BY") ;
                    //if (i!=-1)
                    //	sql=sql.Substring(0,i);
                    break;
            }
            try
            {
                int i = DBAccess.RunSQLReturnValInt(sql, this.MyParas);
                if (this.Top == -1)
                    return i;

                if ( this.Top >= i )
                    return i;
                else
                    return this.Top;
            }
            catch (Exception ex)
            {
                if (SystemConfig.IsDebug)
                    this.En.CheckPhysicsTable();
                throw ex;
            }
		}
		/// <summary>
		/// 最大的数量
		/// </summary>
		/// <param name="topNum">最大的数量</param>
		/// <returns>要查询的信息</returns>
		public DataTable DoQueryToTable(int topNum)
		{
			this.Top=topNum;
            return DBAccess.RunSQLReturnTable(this.SQL, this.MyParas);
		}

//		/// 分页查询
//		public int DoQuery(int pageNum,int pageCount)  
//		{
//			if (this._ens==null ) throw new Exception("@Entity 不能执行这个方法。");
//			_ens.clear();
//			return this.doEntitiesQuery(pageNum,pageCount);
//		}

		private int doEntityQuery()
		{
            return EnDA.Retrieve(this.En, this.SQL, this.MyParas);
		}
        private int doEntitiesQuery()
        {
            switch (this._ens.GetNewEntity.EnMap.EnDBUrl.DBType)
            {
                case DBType.Oracle9i:
                    if (this.IsEndAndOR == false)
                    {
                        if (this.Top == -1)
                            this.AddHD();
                        else
                            this.AddWhereField("RowNum", "<=", this.Top);
                    }
                    else
                    {
                        if (this.Top == -1)
                        {
                        }
                        else
                        {
                            this.addAnd();
                            this.AddWhereField("RowNum", "<=", this.Top);
                        }
                    }
                    break;
                case DBType.SQL2000:
                default:
                    break;
            }
            return EnDA.Retrieve(this.Ens, this.SQL, this.MyParas, this.FullAttrs);
        }
	}
}
