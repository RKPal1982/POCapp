using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Events.Month;
using DayPilot.Web.Mvc.Enums;

namespace Solutions.OnlineSelling.Web.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookEvent(string start="", string end = "", string column = "")
        {
            return View();
        }

        public ActionResult Backend()
        {
            return new Dpm().CallBack(this);
        }
         
        class Dpm : DayPilotMonth
        {
            protected override void OnInit(InitArgs e)
            {
                Events = new BusinessLogic.CalendarEntity().GetCalendar(true);

                DataIdField = "EventId";
                DataTextField = "Title";
                DataStartField = "EventStart";
                DataEndField = "EventEnd";

                Update();
            }

            protected override void OnEventMove(EventMoveArgs e)
            {
                var toBeResized = new BusinessLogic.CalendarEntity().GetCalendar(Convert.ToInt32(e.Id));
                toBeResized.EventStart = e.NewStart;
                toBeResized.EventEnd = e.NewEnd;

                new BusinessLogic.CalendarEntity().ManageCalendar(toBeResized);

                Update();
            }

            protected override void OnEventResize(EventResizeArgs e)
            {
                var toBeResized = new BusinessLogic.CalendarEntity().GetCalendar(Convert.ToInt32(e.Id));
                toBeResized.EventStart = e.NewStart;
                toBeResized.EventEnd = e.NewEnd;

                new BusinessLogic.CalendarEntity().ManageCalendar(toBeResized);

                Update();
            }

            private int i = 0;
            protected override void OnBeforeEventRender(BeforeEventRenderArgs e)
            {
                if (Id == "dp_customization")
                {
                    // alternating color
                    int colorIndex = i % 4;
                    string[] backColors = { "#FFE599", "#9FC5E8", "#B6D7A8", "#EA9999" };
                    string[] borderColors = { "#F1C232", "#3D85C6", "#6AA84F", "#CC0000" };
                    e.BackgroundColor = backColors[colorIndex];
                    e.BorderColor = borderColors[colorIndex];
                    e.FontColor = "#000";
                    i++;
                }
            }

            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime)e.Data["start"];
                        Update(CallBackUpdateType.Full);
                        break;

                    case "previous":
                        StartDate = StartDate.AddMonths(-1);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "next":
                        StartDate = StartDate.AddMonths(1);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "today":
                        StartDate = DateTime.Today;
                        Update(CallBackUpdateType.Full);
                        break;

                    case "refresh":
                        Update();
                        break;
                }
            }


            protected override void OnFinish()
            {
                // only load the data if an update was requested by an Update() call
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                // this select is a really bad example, no where clause
                Events = new BusinessLogic.CalendarEntity().GetCalendar(true);

                DataIdField = "EventId";
                DataTextField = "Title";
                DataStartField = "EventStart";
                DataEndField = "EventEnd";

                Update();
            }
            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                if (e.Data == null) return;
                var toBeCreated = new Model.TblCalendar { EventStart = e.Start, EventEnd = e.End, Title = (string)e.Data["name"] };
                new BusinessLogic.CalendarEntity().ManageCalendar(toBeCreated);

                Update();
            }


           
        }

       
    }


}