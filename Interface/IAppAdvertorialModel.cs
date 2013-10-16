using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface IAppAdvertorialModel : IBaseModel<AppAdvertorial>
    {
        IQueryable<AppAdvertorial> GetList(int AccountMainID);

        Result AddAppAdvertorial(AppAdvertorial appadvertorial, HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th);

        Result DelAppAdvertorial(int ID);

        Result EditAppAdvertorial(AppAdvertorial appadvertorial, HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th);

        int EditAppAdvertorialStick(int ID, int isok, int accoutMainID, int Sort);

        int EditAppAdvertorialSort(int ID, int AccountMainID, int Sort, int type);
    }
}
