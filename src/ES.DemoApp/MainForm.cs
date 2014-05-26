using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventStore.ClientAPI;
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

        public MainForm()
        {
            InitializeComponent();
            var settings = ConnectionSettings.Create();
            settings.SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));

            connection = EventStoreConnection.Create(settings, new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 1113));

            connection.Connect();
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
    }
}
