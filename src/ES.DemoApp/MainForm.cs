using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ES.DemoApp
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cancelSource;
        private Queue<string> window = new Queue<string>();
        private readonly IEventStoreConnection connection;
        private EventStoreSubscription eventStoreSubscription;
        private UserCredentials userCredentials = new UserCredentials("admin", "changeit");
        private EventStoreSubscription eventStoreProjectionSubscription;
        private Queue<string> everythingWindow=new Queue<string>();
        private readonly ProjectionsManager pm;
        private EventStoreSubscription eventPostersProjectionSubscription;
        private EventStoreSubscription alarmClockSubscription;
        private AlarmClock ac;
        private Queue<string> heavyPostersWindow= new Queue<string>();

        public MainForm()
        {
            InitializeComponent();
            var settings = ConnectionSettings.Create();
            settings.SetDefaultUserCredentials(userCredentials);

            connection = EventStoreConnection.Create(settings, new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 1113));

            connection.Connect();

            Thread.Sleep(3000);


            pm = new ProjectionsManager(
                new ConsoleLogger(),
                new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 2113));


            
                pm.Enable("$by_category", userCredentials);
                pm.Enable("$by_event_type", userCredentials);
                pm.Enable("$stream_by_category", userCredentials);
                pm.Enable("$streams", userCredentials);
            
            ac = new AlarmClock(connection);
            ac.Start();
        }

        private void postActivitiesButton_Click(object sender, EventArgs e)
        {
            cancelSource = new CancellationTokenSource();
            var token = cancelSource.Token;

            Task.Factory.StartNew(() => PostActivities(token)); 
        }

        private void PostActivities(CancellationToken token)
        {
            var users = postAs.Text.Split(',');
            var acts = this.activities.Text.Split(',');

            var random = new Random();

            while (!token.IsCancellationRequested)
            {

                var user = users[random.Next(0, users.Length - 1)];
                var activity = acts[random.Next(0, acts.Length - 1)];

                var j = new JObject();

                j["type"] = activity;
                j["user"] = user;
                j["description"] = "some user generated text";

                var mentions = new HashSet<string>();

                var mentionCount = random.Next(0, users.Length - 2);

                while (mentions.Count< mentionCount)
                {
                    mentions.Add(users[random.Next(0, users.Length - 1)]);
                }
                
                j["mentions"] = new JArray(mentions.ToArray());

                var s = j.ToString(Formatting.None);
                var b = Encoding.UTF8.GetBytes(s);
                

                connection.AppendToStream("activities-" + user, ExpectedVersion.Any,
                    new EventData(Guid.NewGuid(), "ActivityCompleted", true, b, null));

                Thread.Sleep(50); //calm stuff down so we can see
            }

            connection.Close();
        }

        private void stopPostingActivitiesButton_Click(object sender, EventArgs e)
        {
            cancelSource.Cancel();
        }

        private void startReading_Click(object sender, EventArgs e)
        {
            if (eventStoreSubscription == null)
            {
                eventStoreSubscription = connection.SubscribeToStream("activities-" + readAs.Text, true,
                    (x, y) => AppendToReadLog(x, y));
            }
        }

        private void AppendToReadLog(EventStoreSubscription eventStoreSubscription, ResolvedEvent resolvedEvent)
        {
            while (window.Count > 20)
            {
                window.Dequeue();
            }

            var j = JObject.Parse(Encoding.UTF8.GetString(resolvedEvent.Event.Data));

            window.Enqueue(string.Format("Activity: {0}:{1} Mentionned by {2}",j.Value<string>("type"), j.Value<string>("decription"), j.Value<JArray>("mentions").Select(x=> x.ToString()).Aggregate((x,y) => x + " " + y)));

            var text = window.Aggregate((x, y) => x + "\r\n" + y);

       
            BeginInvoke(new Action( () => events.Text = text));
            
        }

        private void stopreading_Click(object sender, EventArgs e)
        {

            eventStoreSubscription.Close();
            eventStoreSubscription = null;
        }

        private void repartitionByUser_Click(object sender, EventArgs e)
        {
           
           //pm.Enable("$users", userCredentials);
           pm.CreateContinuous("partition-by-user", 

    @"
        fromCategory('activities')    
        .when({
      
            ActivityCompleted: function(s, e) {
                linkTo('everything-' + e.data.user, e);
                var i;
                for(i = 0; i < e.data.mentions.length; ++i){
                    linkTo('everything-' + e.data.mentions[i], e);
                }
                return s;
            },
        })
                    ", userCredentials );

           if (eventStoreProjectionSubscription == null)
           {
               eventStoreProjectionSubscription = connection.SubscribeToStream("everything-" + readAs.Text, true,
                   (x, y) => AppendToEverythingReadLog(x, y));
           }

        }

        private void AppendToEverythingReadLog(EventStoreSubscription eventStoreSubscription, ResolvedEvent resolvedEvent)
        {
            while (everythingWindow.Count > 20)
            {
                everythingWindow.Dequeue();
            }

            var j = JObject.Parse(Encoding.UTF8.GetString(resolvedEvent.Event.Data));

            everythingWindow.Enqueue(string.Format("Activity: {0}:{1}:{2} Mentionned by {3}", j.Value<string>("user"), j.Value<string>("type"), j.Value<string>("decription"), j.Value<JArray>("mentions").Select(x => x.ToString()).Aggregate((x, y) => x + " " + y)));

            var text = everythingWindow.Aggregate((x, y) => x + "\r\n" + y);

            BeginInvoke(new Action(() => everythingEvents.Text = text));

        }

        private void emitWhenConditionDetected_Click(object sender, EventArgs e)
        {

            //pm.Enable("$users", userCredentials);
            pm.CreateContinuous("emit-time-out",

     @"
        fromCategory('activities').foreachStream()    
        .when({
            $init: function () {
            return { count: 0 }; // initial state
            },

            ActivityCompleted: function(s, e) {
                emit('alarmclock', 'FutureMessage', {stream: e.streamId, type: 'IntervalOccured' ,user: e.data.user, count: s.count, ttl: 10});
                s.count += 1
                return s;
            },
            
            IntervalOccured: function(s,e){
                if(e.data.count + 50 < s.count){
                    emit('heavy-posters', 'HeavyPosterDetected', {user: e.data.user, events: s.count - e.data.count});
                
                }
                else{
                        emit('light-posters', 'LightPosterDetected', {user: e.data.user, events: s.count - e.data.count});
                
                }
            }

        })
                    ", userCredentials);

            if (alarmClockSubscription == null)
            {
                alarmClockSubscription = connection.SubscribeToStream("alarmclock", true,
                    (x, y) => AppendToFutureMessages(x, y));
            }

            if (eventPostersProjectionSubscription == null)
            {
                eventPostersProjectionSubscription = connection.SubscribeToStream("heavy-posters", true,
                    (x, y) => AppendToHeavyPosters(x, y));
            }


        }

        private void AppendToHeavyPosters(IDisposable eventStoreSubscription1, ResolvedEvent resolvedEvent)
        {
            while (heavyPostersWindow.Count > 20)
            {
                heavyPostersWindow.Dequeue();
            }

            var j = JObject.Parse(Encoding.UTF8.GetString(resolvedEvent.Event.Data));

            heavyPostersWindow.Enqueue(string.Format("User: {0}, Posts in interva: {1}", j.Value<string>("user"), j.Value<string>("events")));

            var text = everythingWindow.Aggregate((x, y) => x + "\r\n" + y);

            BeginInvoke(new Action(()=>  heavyPosters.Text += Encoding.UTF8.GetString(resolvedEvent.Event.Data) + "\r\n" ));
        }

        private void AppendToFutureMessages(EventStoreSubscription eventStoreSubscription1, ResolvedEvent resolvedEvent)
        {
            
        }
    }

    public class AlarmClock
	{
        private readonly IEventStoreConnection connection;
        private readonly SortedList list = new SortedList();
		private readonly object locker = new object();
		private Stopwatch clock;

        public AlarmClock(IEventStoreConnection connection)
        {
            this.connection = connection;
        }

        private void Handle(JToken message)
		{
            
			lock (locker)
			{
				long timeOut = clock.ElapsedTicks + message.Value<int>("ttl") * 1000000 ;
				list.Add(timeOut, new Tuple<long, JToken>(timeOut, message));
			}
		}

		public void Start()
		{
		    var subscription = connection.SubscribeToStream("alarmclock", false, EventAppeared);
			clock = new Stopwatch();
			clock.Start();
		    var thread = new Thread(Run);
		    thread.IsBackground=true;
		    thread.Start();
		}

        private void EventAppeared(EventStoreSubscription arg1, ResolvedEvent arg2)
        {
            JToken token = JToken.Parse(Encoding.UTF8.GetString(arg2.Event.Data));
            Handle(token);

        }

        private void Run()
		{
			while (true)
			{
				lock (locker)
				{
					while (true)
					{
						if( list.Count == 0)
 							break;

						object o = list.GetByIndex(0);
						var item = (Tuple<long, JToken>) o;
						if (item.Item1 < clock.ElapsedTicks)
						{
							list.Remove(item.Item1);
						    connection.AppendToStream(item.Item2.Value<string>("stream"), -2,
						        new EventData(Guid.NewGuid(), item.Item2.Value<string>("type"),true,
						            Encoding.UTF8.GetBytes(item.Item2.ToString(Formatting.None)), null));
						}
						else
						{
							break;
						}
					}					
				}

				Thread.Sleep(1); //Give chance to add time outs
			}
		}
	}

}
