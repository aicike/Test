namespace System.Web
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PagedList<T> : List<T>
    {
        public PagedList(IList<T> items, int pageIndex, int pageSize)
        {
            this.PageSize = pageSize;
            this.TotalItemCount = items.Count;
            this.TotalPageCount = (int)Math.Ceiling((double)(((double)this.TotalItemCount) / ((double)this.PageSize)));
            this.CurrentPageIndex = pageIndex;
            this.StartRecordIndex = ((this.CurrentPageIndex - 1) * this.PageSize) + 1;
            this.EndRecordIndex = (this.TotalItemCount > (pageIndex * pageSize)) ? (pageIndex * pageSize) : this.TotalItemCount;
            for (int i = this.StartRecordIndex - 1; i < this.EndRecordIndex; i++)
            {
                base.Add(items[i]);
            }
        }

        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            base.AddRange(items);
            this.TotalItemCount = totalItemCount;
            this.TotalPageCount = (int)Math.Ceiling((double)(((double)totalItemCount) / ((double)pageSize)));
            this.PageSize = pageSize;
            this.CurrentPageIndex = pageIndex;
            this.StartRecordIndex = ((pageIndex - 1) * pageSize) + 1;
            this.EndRecordIndex = (this.TotalItemCount > (pageIndex * pageSize)) ? (pageIndex * pageSize) : totalItemCount;
        }

        private int currentPageIndex;
        public int CurrentPageIndex
        {
            get
            {
                return currentPageIndex;
            }
            set
            {
                if (TotalItemCount==0||Math.Ceiling(TotalItemCount * 1.0 / PageSize) >= value)
                {
                    currentPageIndex = value;
                }
                else
                {
                    currentPageIndex = value - 1;
                }
            }
        }

        public int EndRecordIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int StartRecordIndex
        {
            get;
            set;
        }

        public int TotalItemCount
        {
            get;
            set;
        }

        public int TotalPageCount
        {
            get;
            set;
        }
    }
}

