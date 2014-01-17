using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class PanoramaModel : BaseModel<Panorama>, IPanoramaModel
    {
        public IQueryable<Panorama> GetByAccountMainID(int accountMainID)
        {
            return base.List().Where(a => a.AccountMainID == accountMainID);
        }

        public new Result Add(Panorama panorama)
        {
            //Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);

            return null;
        }
    }
}
