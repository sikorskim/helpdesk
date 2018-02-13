using helpdesk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helpdesk.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            runSqlProcedure();

            return View();
        }

        void runSqlProcedure()
        {
            helpdeskContext db = new helpdeskContext();
            var param1 = new SqlParameter();
            param1.ParameterName = "@start";
            param1.SqlDbType = SqlDbType.DateTime;
            param1.SqlValue = "2017-01-01";

            var param2 = new SqlParameter();
            param2.ParameterName = "@end";
            param2.SqlDbType = SqlDbType.DateTime;
            param2.SqlValue = "2018-03-01";

            //var result = db.Database.SqlQuery("CountOrdersByCategory @start,@end").ToList();
        }
    }
}