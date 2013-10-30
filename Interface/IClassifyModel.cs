﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IClassifyModel : IBaseModel<Classify>
    {
        IQueryable<Classify> GetLastClass(int ParentID, int AccountMainID);

        IQueryable<Classify> GetLastClass(int ParentID, int AccountMainID,int NoID);

        int AddClass(int PID, int Level, int AccountMainID, string Name);

        IQueryable<Classify> GetClassByPID(int PID);

        int UpdClass(int PID, int ID, string Name, int AccountMainID);

        bool GetIsMainNode(int ID);

        List<Classify> Get1levelClass(int accountMainID);
    }
}
