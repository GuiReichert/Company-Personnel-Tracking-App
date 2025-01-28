using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Personnel_Tracking_App
{
    public static class Definitions
    {
        public static class TaskStates
        {
            public static int OnEmployee = 1;
            public static int Submitted = 2;
            public static int Approved = 3;
            public static int Reviewed = 4;
            public static int Done = 5;
        }
        public static class PermissionStates
        {
            public static int OnEmployee = 1;
            public static int Approved = 2;
            public static int Disapproved = 3;
        }

    }
}
