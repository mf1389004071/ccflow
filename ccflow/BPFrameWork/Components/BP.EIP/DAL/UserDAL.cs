﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.EIP.Interface;

namespace BP.EIP.DAL
{
    internal class UserDAL 
    {
        public bool Exists(string No)
        {
            throw new NotImplementedException();
        }

        public void Add(BaseEntity entity, out string statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public BaseEntity GetEntity(string No)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDT()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDT(string[] Ids)
        {
            throw new NotImplementedException();
        }

        public int Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public int BatchDelete(string[] ids)
        {
            throw new NotImplementedException();
        }

        public int SetDeleted(string[] ids)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable Search(string searchValue)
        {
            throw new NotImplementedException();
        }

        public int Update(BaseEntity entity, out string statusCode, out string statusMessage)
        {
            throw new NotImplementedException();
        }

        public int BatchSave(List<BaseEntity> entityList)
        {
            throw new NotImplementedException();
        }

        public bool ExistsName(string userName)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDTByDept(string departmentNo, bool containChildren)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDTByRole(string roleId)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDTByStatus(Enum.AuditStatus status)
        {
            throw new NotImplementedException();
        }

        public int SetDefaultRole(string userId, string roleId)
        {
            throw new NotImplementedException();
        }

        public int BatchSetDefaultRole(string[] userIds, string roleId)
        {
            throw new NotImplementedException();
        }

        public string[] GetUserRoleIds(string userId)
        {
            throw new NotImplementedException();
        }

        public int AddUserToRole(string userId, string[] addToRoleIds)
        {
            throw new NotImplementedException();
        }

        public int RemoveUserFromRole(string userId, string[] removeRoleIds)
        {
            throw new NotImplementedException();
        }
    }
}
