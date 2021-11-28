using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

[assembly: Preserve(typeof(Queryable), AllMembers = true)]
[assembly: Preserve(typeof(DateTime), AllMembers = true)]
[assembly: Preserve(typeof(Enumerable), AllMembers = true)]
[assembly: Preserve(typeof(IQueryable), AllMembers = true)]
namespace DoctorDiary.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
