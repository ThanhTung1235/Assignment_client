﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Service
{
    class APIHandle
    {
        public static string LOGIN_API = "https://fptstudentmanager.azurewebsites.net/api/StudentsAPI/authentication";
        public static string GET_INFO_USER = "https://fptstudentmanager.azurewebsites.net/api/StudentsAPI/";
        public static string GET_INFO_CLASS = "";
        public static string CHANGE_INFO_USER = "https://fptstudentmanager.azurewebsites.net/api/StudentsAPI/change-information";
        public static string GET_INFO_COURSE = "";
    }
}
