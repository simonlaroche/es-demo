namespace ES.DemoApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.postActivitiesButton = new System.Windows.Forms.Button();
            this.stopPostingActivitiesButton = new System.Windows.Forms.Button();
            this.postAs = new System.Windows.Forms.TextBox();
            this.postAsLabel = new System.Windows.Forms.Label();
            this.activities = new System.Windows.Forms.TextBox();
            this.activitiesLable = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.stopreading = new System.Windows.Forms.Button();
            this.startReading = new System.Windows.Forms.Button();
            this.events = new System.Windows.Forms.TextBox();
            this.readAs = new System.Windows.Forms.TextBox();
            this.readAsLabel = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.everythingEvents = new System.Windows.Forms.TextBox();
            this.repartitionByUser = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.emitWhenConditionDetected = new System.Windows.Forms.Button();
            this.heavyPosters = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // postActivitiesButton
            // 
            this.postActivitiesButton.Location = new System.Drawing.Point(253, 109);
            this.postActivitiesButton.Name = "postActivitiesButton";
            this.postActivitiesButton.Size = new System.Drawing.Size(221, 23);
            this.postActivitiesButton.TabIndex = 5;
            this.postActivitiesButton.Text = "Post Activities";
            this.postActivitiesButton.UseVisualStyleBackColor = true;
            this.postActivitiesButton.Click += new System.EventHandler(this.postActivitiesButton_Click);
            // 
            // stopPostingActivitiesButton
            // 
            this.stopPostingActivitiesButton.Location = new System.Drawing.Point(253, 138);
            this.stopPostingActivitiesButton.Name = "stopPostingActivitiesButton";
            this.stopPostingActivitiesButton.Size = new System.Drawing.Size(221, 23);
            this.stopPostingActivitiesButton.TabIndex = 6;
            this.stopPostingActivitiesButton.Text = "Stop Posting Activities";
            this.stopPostingActivitiesButton.UseVisualStyleBackColor = true;
            this.stopPostingActivitiesButton.Click += new System.EventHandler(this.stopPostingActivitiesButton_Click);
            // 
            // postAs
            // 
            this.postAs.Location = new System.Drawing.Point(92, 19);
            this.postAs.Name = "postAs";
            this.postAs.Size = new System.Drawing.Size(382, 20);
            this.postAs.TabIndex = 1;
            this.postAs.Text = "johndoe,janedoe,jimmybean,johnnywalker,jackdaniel";
            // 
            // postAsLabel
            // 
            this.postAsLabel.AutoSize = true;
            this.postAsLabel.Location = new System.Drawing.Point(12, 24);
            this.postAsLabel.Name = "postAsLabel";
            this.postAsLabel.Size = new System.Drawing.Size(43, 13);
            this.postAsLabel.TabIndex = 0;
            this.postAsLabel.Text = "Post As";
            // 
            // activities
            // 
            this.activities.Location = new System.Drawing.Point(92, 47);
            this.activities.Name = "activities";
            this.activities.Size = new System.Drawing.Size(382, 20);
            this.activities.TabIndex = 3;
            this.activities.Text = "lire,ecrire,poker,dancer,dire-bonjour";
            // 
            // activitiesLable
            // 
            this.activitiesLable.AutoSize = true;
            this.activitiesLable.Location = new System.Drawing.Point(12, 50);
            this.activitiesLable.Name = "activitiesLable";
            this.activitiesLable.Size = new System.Drawing.Size(49, 13);
            this.activitiesLable.TabIndex = 2;
            this.activitiesLable.Text = "Activities";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.activitiesLable);
            this.groupBox1.Controls.Add(this.postAs);
            this.groupBox1.Controls.Add(this.activities);
            this.groupBox1.Controls.Add(this.postActivitiesButton);
            this.groupBox1.Controls.Add(this.stopPostingActivitiesButton);
            this.groupBox1.Controls.Add(this.postAsLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 383);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Post";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.stopreading);
            this.groupBox2.Controls.Add(this.startReading);
            this.groupBox2.Controls.Add(this.events);
            this.groupBox2.Controls.Add(this.readAs);
            this.groupBox2.Controls.Add(this.readAsLabel);
            this.groupBox2.Location = new System.Drawing.Point(525, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(569, 383);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Read Posts";
            // 
            // stopreading
            // 
            this.stopreading.Location = new System.Drawing.Point(413, 45);
            this.stopreading.Name = "stopreading";
            this.stopreading.Size = new System.Drawing.Size(138, 23);
            this.stopreading.TabIndex = 4;
            this.stopreading.Text = "Stop Reading";
            this.stopreading.UseVisualStyleBackColor = true;
            this.stopreading.Click += new System.EventHandler(this.stopreading_Click);
            // 
            // startReading
            // 
            this.startReading.Location = new System.Drawing.Point(271, 44);
            this.startReading.Name = "startReading";
            this.startReading.Size = new System.Drawing.Size(138, 23);
            this.startReading.TabIndex = 3;
            this.startReading.Text = "Start Reading";
            this.startReading.UseVisualStyleBackColor = true;
            this.startReading.Click += new System.EventHandler(this.startReading_Click);
            // 
            // events
            // 
            this.events.Location = new System.Drawing.Point(25, 73);
            this.events.Multiline = true;
            this.events.Name = "events";
            this.events.Size = new System.Drawing.Size(526, 294);
            this.events.TabIndex = 2;
            // 
            // readAs
            // 
            this.readAs.Location = new System.Drawing.Point(76, 19);
            this.readAs.Name = "readAs";
            this.readAs.Size = new System.Drawing.Size(475, 20);
            this.readAs.TabIndex = 1;
            this.readAs.Text = "johndoe";
            // 
            // readAsLabel
            // 
            this.readAsLabel.AutoSize = true;
            this.readAsLabel.Location = new System.Drawing.Point(22, 19);
            this.readAsLabel.Name = "readAsLabel";
            this.readAsLabel.Size = new System.Drawing.Size(48, 13);
            this.readAsLabel.TabIndex = 0;
            this.readAsLabel.Text = "Read As";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.everythingEvents);
            this.groupBox3.Controls.Add(this.repartitionByUser);
            this.groupBox3.Location = new System.Drawing.Point(12, 411);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(487, 352);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Repartition";
            // 
            // everythingEvents
            // 
            this.everythingEvents.Location = new System.Drawing.Point(15, 61);
            this.everythingEvents.Multiline = true;
            this.everythingEvents.Name = "everythingEvents";
            this.everythingEvents.Size = new System.Drawing.Size(459, 276);
            this.everythingEvents.TabIndex = 5;
            // 
            // repartitionByUser
            // 
            this.repartitionByUser.Location = new System.Drawing.Point(15, 32);
            this.repartitionByUser.Name = "repartitionByUser";
            this.repartitionByUser.Size = new System.Drawing.Size(221, 23);
            this.repartitionByUser.TabIndex = 7;
            this.repartitionByUser.Text = "Repartition by user";
            this.repartitionByUser.UseVisualStyleBackColor = true;
            this.repartitionByUser.Click += new System.EventHandler(this.repartitionByUser_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.heavyPosters);
            this.groupBox4.Controls.Add(this.emitWhenConditionDetected);
            this.groupBox4.Location = new System.Drawing.Point(525, 411);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(569, 352);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Emit";
            // 
            // emitWhenConditionDetected
            // 
            this.emitWhenConditionDetected.Location = new System.Drawing.Point(25, 19);
            this.emitWhenConditionDetected.Name = "emitWhenConditionDetected";
            this.emitWhenConditionDetected.Size = new System.Drawing.Size(526, 23);
            this.emitWhenConditionDetected.TabIndex = 8;
            this.emitWhenConditionDetected.Text = "Emit event when condition detected";
            this.emitWhenConditionDetected.UseVisualStyleBackColor = true;
            this.emitWhenConditionDetected.Click += new System.EventHandler(this.emitWhenConditionDetected_Click);
            // 
            // heavyPosters
            // 
            this.heavyPosters.Location = new System.Drawing.Point(25, 61);
            this.heavyPosters.Multiline = true;
            this.heavyPosters.Name = "heavyPosters";
            this.heavyPosters.Size = new System.Drawing.Size(526, 276);
            this.heavyPosters.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 775);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button postActivitiesButton;
        private System.Windows.Forms.Button stopPostingActivitiesButton;
        private System.Windows.Forms.TextBox postAs;
        private System.Windows.Forms.Label postAsLabel;
        private System.Windows.Forms.TextBox activities;
        private System.Windows.Forms.Label activitiesLable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button stopreading;
        private System.Windows.Forms.Button startReading;
        private System.Windows.Forms.TextBox events;
        private System.Windows.Forms.TextBox readAs;
        private System.Windows.Forms.Label readAsLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button repartitionByUser;
        private System.Windows.Forms.TextBox everythingEvents;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button emitWhenConditionDetected;
        private System.Windows.Forms.TextBox heavyPosters;
    }
}

