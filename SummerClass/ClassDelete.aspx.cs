﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SummerClass
{
    public partial class ClassDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int studentID = Convert.ToInt32(Request.QueryString["ClassID"]);

            //BusinessLogicLayer.ClassBusinessLogic.
        }
    }
}