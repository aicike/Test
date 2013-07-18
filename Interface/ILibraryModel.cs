﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ILibraryModel<T>
    {
        IQueryable<T> GetLibraryList(int accountMainID);
    }
}
