using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult UploadCategoryImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Content/images/Category"), fileName);

                file.SaveAs(path);

                result.Data = new { Success = true, ImageURL = string.Format("/Content/images/Category{0}", fileName) };

            }
            catch(Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }

        public JsonResult UploadProductImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Content/images/Product"), fileName);

                file.SaveAs(path);

                result.Data = new { Success = true, ImageURL = string.Format("/Content/images/Product{0}", fileName) };

            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }
    }
}